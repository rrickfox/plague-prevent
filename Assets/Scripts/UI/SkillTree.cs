using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour
{
    public GameObject hexagon;                                                          //Standard plain blue hexagon
    private Vector2 spriteDim;                                                          //Sprite Dimensions
    private float pixelsPerUnit;                                                        //Scaling

    public Dictionary<Law, GameObject> laws = new Dictionary<Law, GameObject>();        //

    public Country country;                                                             //Your country

    private Manager manager;                                                            //To know when the game is paused etc.

    public string currentBranch;                                                        //Current selected Law branch
    public string currentNode;                                                          //Current selected Law node

    //UI Params
    public TextMeshProUGUI massnahmeName;
    public TextMeshProUGUI massnhameBesch;
    public Button einfuehrenButton;                                            


    public Transform actionMenu;                                                        //To set parent to panel

    public void Start()
    {

        //Fetch Sprite data
        spriteDim = hexagon.GetComponent<SpriteRenderer>().size;//*5.5f;
        pixelsPerUnit = hexagon.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //Debug.Log(string.Format("Sprite Dimensions X:{0} , Y:{1}    PPU:{2}", spriteDim.x, spriteDim.y, pixelsPerUnit));

        //Needs to be changed later when adding more countries
        country = FindObjectOfType<Country>();

        country.ReadLaws();

        manager = FindObjectOfType<Manager>();

        //Automatically load hygiene
        currentNode = "Hände waschen";
        LoadSkillTree("Hygiene");
        UpdateSelected(currentNode);
    }

    public void LoadSkillTree(string name)
    {
        //If it finds a loaded skill tree it destroys it
        if (GameObject.Find(currentBranch) && name != currentBranch)
        {
            Destroy(GameObject.Find(currentBranch));
        }
        //If it can't already find the object it creates a new one
        if (!GameObject.Find(name))
        {
            GameObject g = new GameObject(name);
            g.transform.SetParent(actionMenu);
            g.transform.SetAsFirstSibling();
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one;

            //Debug.Log(Country.laws[name].subNode.Count - 1);

            Vector2 origin = Vector2.up * Screen.height *3f/ (2f* Country.laws[name].subNode.Count * 4f);   //Top of the screen
            Vector2 offset = Vector2.up * (Screen.height /((Country.laws[name].subNode.Count-1)*4f));   //Increments in symetrical steps so that each tree is spaced evenly
            int count = 0;
            //Ignore root node and only create nodes starting from the second layer
            foreach (LawNode node in Country.laws[name].subNode)
            {
                CreateNode(node, origin- offset* count, g.transform);
                count++;
            }
            currentBranch = name;
        }


    }

    //Recursive function that locates a child LawNode from a parent LawNode
    private LawNode GetNode(LawNode parent, string name)
    {
        //Break out of recursion
        if (parent.law.name == name)
        {
            return parent;
        }
        LawNode toReturn = null;
        foreach(LawNode node in parent.subNode)
        {
            LawNode temp = GetNode(node, name);
            if(temp != null && temp.law.name == name)
            {
                toReturn = temp;
            }
        }
        return toReturn;
    }



    //Recursive function that creates the node gameobject from a LawNode
    private void CreateNode(LawNode node, Vector2 position, Transform parent)
    {
        //Debug.Log(string.Format("Name: {0}, ChildIndex: {1}, Previous: {2}", node.law.name, node.childIndex, node.prev.law.name));

        //Offset to left, right or top
        Vector2[] offsets = new Vector2[3] { (Mathf.Cos(150f * Mathf.Deg2Rad) * Vector2.left + Mathf.Sin(150f * Mathf.Deg2Rad) * Vector2.up)* spriteDim.y,
        (Mathf.Cos(150f * Mathf.Deg2Rad) * Vector2.left - Mathf.Sin(150f * Mathf.Deg2Rad) * Vector2.up) * spriteDim.y,
        -Vector2.left * spriteDim.x };

        GameObject nodeG = Instantiate(hexagon, position, Quaternion.identity);         //Create Hexagon object
        nodeG.name = node.law.name;
        nodeG.SetActive(true);
        nodeG.layer = 8;
        nodeG.transform.SetParent(parent, false);

        //Check if previous Node is unlocked, otherwise lock
        if (!country.enforcedLaws.Contains(node.law) && node.prev.law.name != "")
        {
            nodeG.GetComponent<Image>().color = Color.gray;
        }

        if(node.prev.law.active && country.enforcedLaws.Contains(node.prev.law))
        {
            nodeG.GetComponent<Image>().color = Color.white;
        }


        //If active law
        if (node.law.active || country.enforcedLaws.Contains(node.law))
        {
            nodeG.GetComponent<Image>().color = Color.cyan;
        }


        foreach (LawNode subNode in node.subNode)                                        //Call function for each subnode
        {
            //Different cases, check notebook (Progyo)
            
            Vector2 newPos = position + offsets[subNode.childIndex];    

            CreateNode(subNode, newPos, parent);
        }
    }


    private float sensitivity = 10f;

    //Move Hexagons
    private void Update()
    {
        bool panPressed = Input.GetMouseButton(2);

        if (panPressed && manager.menu)
        {
            float xMov = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity * 100f;
            float yMove = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity * 100f;
            GameObject.Find(currentBranch).transform.position += new Vector3(xMov, yMove);
        }


        if (Input.GetMouseButtonDown(0) && manager.menu)
        {

            //Code from https://gamedev.stackexchange.com/questions/93592/graphics-raycaster-of-unity-how-does-it-work

            //Code to be place in a MonoBehaviour with a GraphicRaycaster component
            GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
            //Create the PointerEventData with null for the EventSystem
            PointerEventData ped = new PointerEventData(null);
            //Set required parameters, in this case, mouse position
            ped.position = Input.mousePosition;
            //Create list to receive all results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast it
            gr.Raycast(ped, results);

            if (results[0].gameObject.GetComponent<Button>())
            {
                //Code from https://stackoverflow.com/questions/49321922/unity-raycast-ui-button-and-call-its-on-click-event
                IPointerClickHandler clickHandler = results[0].gameObject.GetComponent<IPointerClickHandler>();
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                clickHandler.OnPointerClick(pointerEventData);
            }
            else if(results[0].gameObject.layer == 8)
            {
               
                currentNode = results[0].gameObject.name;
                UpdateSelected(currentNode);
            }

        }


    }


    public void UpdateSelected(string name)
    {
        //Searches for node with selected name
        LawNode selectedNode = GetNode(Country.laws[currentBranch], currentNode);

        //Updates the TMPro text elements
        massnahmeName.text = selectedNode.law.name;
        massnhameBesch.text = selectedNode.law.description;

        //If parent is unlocked and not active
        if(country.enforcedLaws.Contains(selectedNode.prev.law) && !selectedNode.law.active && !country.enforcedLaws.Contains(selectedNode.law))
        {
            einfuehrenButton.interactable = true;
        }
        else if (!country.enforcedLaws.Contains(selectedNode.prev.law) && selectedNode.prev.law.name != "")
        {
            einfuehrenButton.interactable = false;
        }
        else
        {
            einfuehrenButton.interactable = true;
        }

        
    }


    public void Enforce()
    {
        country.EnforceLaw(GetNode(Country.laws[currentBranch], currentNode).law);
    }

}
