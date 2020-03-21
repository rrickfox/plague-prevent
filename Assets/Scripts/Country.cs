using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Country : MonoBehaviour
{
    public bool democratic;     //Whether the country is democratic or not
    public int icuBeds;         //How many total ICU Beds there are
    public int startingBudget;  //The starting budget for a country
    public int currentBudget;   //The current budget for a country

    public int infected;        //How many total people are currently infected
    public int cured;           //How many total people have been cured
    public int dead;            //How many total people have died
    public int population;      //Total population of the country

    public List<State> states;  //List of states in the country



    //Enforce a specific law (Depending on implementation democratic or totalitarian)
    public abstract void EnforceLaw(Law law);
}
