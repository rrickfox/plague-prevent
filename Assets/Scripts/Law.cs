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
    public Law law;                                             //The Law head node of this tree
    public int childIndex;                                      //Index in parents subNode list
    public List<LawNode> subNode = new List<LawNode>();         //List of sub trees
    public LawNode prev = null;
    
    public LawNode(Law head, bool empty = false)
    {
        if (!empty)
        {
            law = head;
        }
        else
        {
            law = new Law("none", 0, 0f);
        }
        
    }

    public void SetChildIndex(int index)
    {
        childIndex = index;
    }
    public void SetPrev(LawNode previous)
    {
        prev = previous;
    }
    public LawNode AddTree(LawNode law)
    {
        law.SetPrev(this);
        law.SetChildIndex(subNode.Count);
        subNode.Add(law);

        return this;
    }

    public void UpdatePrev()
    {
        foreach(LawNode node in subNode)
        {
            node.SetPrev(this);
            node.UpdatePrev();
        }
    }


    //Must run UpdatePrev before this function
    public void UpdateChildIndex()
    {
        foreach (LawNode node in subNode)
        {
            int index;
            for(index =0; index < node.prev.subNode.Count; index++)
            {
                if(node.prev.subNode[index] == node)
                {
                    break;
                }
            }
            node.SetChildIndex(index);
            node.UpdateChildIndex();
        }
    }
}
