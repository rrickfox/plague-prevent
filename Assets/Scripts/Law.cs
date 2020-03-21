using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Law
{
    public string name;             //Name of the law
    public int satisfaction;        //How happy the civilians are with this law
    public float dampener;          //Dampening effect of this law on the infection rate of the disease

    public Law(string name, int satisfaction, float dampener)
    {
        this.name = name;
        this.satisfaction = satisfaction;
        this.dampener = dampener;
    }
}
