using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State: MonoBehaviour
{
    public string stateName;                //Name of the state

    public int icuBeds;                     //How many total ICU Beds there are

    public int infected;                    //How many people are currently infected
    public int cured;                       //How many people have been cured
    public int dead;                        //How many people have died
    public int population;                  //Total population of the state

    public List<State> neighbouring;        //Neighbouring states

}
