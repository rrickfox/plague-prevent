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

    public void LoadSkillTree()
    {
        spriteDim = hexagon.GetComponent<SpriteRenderer>().size;//*5.5f;
        pixelsPerUnit = hexagon.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        Debug.Log(string.Format("Sprite Dimensions X:{0} , Y:{1}    PPU:{2}", spriteDim.x, spriteDim.y, pixelsPerUnit));

        //Needs to be changed later when adding more countries
        country = GameObject.FindObjectOfType<Country>();



        int j = 0;  //Count for outer foreach loop
        foreach(KeyValuePair<string, LawNode> val in Country.laws)
        {

            int i = 0;  //Count for inner foreach loop
            //Ignore root node and only create nodes starting from the second layer
            foreach (LawNode node in val.Value.subNode)
            {
                CreateNode(node, new Vector2(-Screen.width/3.5f,-Screen.height/3.5f) - Vector2.left * (j+i) * 300f);
                i++;
            }
            j++;

        }
    }

    private void CreateNode(LawNode node, Vector2 position)
    {
        //Debug.Log(string.Format("Name: {0}, ChildIndex: {1}, Previous: {2}", node.law.name, node.childIndex, node.prev.law.name));

        //Offset to left, right or top
        Vector2[] offsets = new Vector2[3] { (Mathf.Cos(150f * Mathf.Deg2Rad) * Vector2.left + Mathf.Sin(150f * Mathf.Deg2Rad) * Vector2.up)* spriteDim.y,
        (Mathf.Cos(150f * Mathf.Deg2Rad) * Vector2.left - Mathf.Sin(150f * Mathf.Deg2Rad) * Vector2.up) * spriteDim.y,
        -Vector2.left * spriteDim.x };

        GameObject nodeG = Instantiate(hexagon, position, Quaternion.identity);         //Create Hexagon object
        nodeG.name = node.law.name;
        nodeG.transform.SetParent(GameObject.Find("ActionsMenu").transform, false);
        foreach(LawNode subNode in node.subNode)                                        //Call function for each subnode
        {
            //Different cases, check notebook (Progyo)
            
            Vector2 newPos = position + offsets[subNode.childIndex];    

            CreateNode(subNode, newPos);
        }
    }

}
