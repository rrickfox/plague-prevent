using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public GameObject hexagon;                                                          //Standard plain blue hexagon
    private Vector2 spriteDim;                                                          //Sprite Dimensions
    private float pixelsPerUnit;                                                        //Scaling

    public Dictionary<Law, GameObject> laws = new Dictionary<Law, GameObject>();        //

    public Country country;

    private Manager manager;

    public string currentString;                                                        //Current selected Law branch


    public void Start()
    {
        //Fetch Sprite data
        spriteDim = hexagon.GetComponent<SpriteRenderer>().size;//*5.5f;
        pixelsPerUnit = hexagon.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //Debug.Log(string.Format("Sprite Dimensions X:{0} , Y:{1}    PPU:{2}", spriteDim.x, spriteDim.y, pixelsPerUnit));

        //Needs to be changed later when adding more countries
        country = GameObject.FindObjectOfType<Country>();


        manager = GetComponent<Manager>();

        //Automatically load hygiene
        LoadSkillTree("Hygiene");
    }

    public void LoadSkillTree(string name)
    {
        //If it finds a loaded skill tree it destroys it
        if (GameObject.Find(currentString) && name != currentString)
        {
            Destroy(GameObject.Find(currentString));
        }
        //If it can't already find the object it creates a new one
        if (!GameObject.Find(name))
        {
            GameObject g = new GameObject(name);
            g.transform.SetParent(GameObject.Find("ActionsMenu").transform);
            g.transform.SetAsFirstSibling();
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one;

            Debug.Log(Country.laws[name].subNode.Count - 1);

            Vector2 origin = Vector2.up * Screen.height *3f/ (2f* Country.laws[name].subNode.Count * 4f);   //Top of the screen
            Vector2 offset = Vector2.up * (Screen.height /((Country.laws[name].subNode.Count-1)*4f));   //Increments in symetrical steps so that each tree is spaced evenly
            int count = 0;
            //Ignore root node and only create nodes starting from the second layer
            foreach (LawNode node in Country.laws[name].subNode)
            {
                CreateNode(node, origin- offset* count, g.transform);
                count++;
            }
            currentString = name;
        }


    }

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
        nodeG.transform.SetParent(parent, false);
        foreach(LawNode subNode in node.subNode)                                        //Call function for each subnode
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
            GameObject.Find(currentString).transform.position += new Vector3(xMov, yMove);
        }
    }



}
