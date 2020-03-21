using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Law
{
    public string name;             //Name of the law
    public int satisfaction;        //How happy the civilians are with this law
    public float dampener;          //Dampening effect of this law on the infection rate of the disease
    public bool active;             //Whether the law is active

    public Law(string name, int satisfaction, float dampener, bool active=false)
    {
        this.name = name;
        this.satisfaction = satisfaction;
        this.dampener = dampener;
        this.active = active;
    }
}

//A tree (more like a row) of laws which one can progress through
[System.Serializable]
public class LawNode
{
    public Law law;
    public List<LawNode> subNode = new List<LawNode>();
    public LawNode prev = null;
    
    public LawNode(Law head)
    {
        law = head;
    }
    
    public LawNode AddTree(LawNode law)
    {
        law.prev = this;
        subNode.Add(law);

        return this;
    }

}
