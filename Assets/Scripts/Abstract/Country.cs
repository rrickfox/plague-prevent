using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public abstract class Country : MonoBehaviour
{

    
    public string countryName;                                      //Name of the country

    public bool democratic;                                         //Whether the country is democratic or not
    public static List<LawNode> laws;                               //List of law trees that can be enforced
    

    public int icuBeds => states.Sum(s => s.icuBeds);               //How many total ICU Beds there are
    public int startingBudget;                                      //The starting budget for a country
    public int currentBudget;                                       //The current budget for a country

    public int infected => states.Sum(s => s.infected);             //How many total people are currently infected
    public int cured => states.Sum(s => s.cured);                   //How many total people have been cured
    public int dead => states.Sum(s => s.dead);                     //How many total people have died
    public int population => states.Sum(s => s.population);         //Total population of the country

    public List<State> states;                                      //List of states in the country

    public List<Country> neighbours;                                //List of neighbouring countries


    //Enforce a specific law (Depending on implementation democratic or totalitarian)
    public abstract void EnforceLaw(Law law);

    public virtual void ReadLaws()
    {
        //LawNode test = new LawNode(new Law("Wash    ing Hands", 10, 0.1f)).AddTree(new LawNode(new Law("Verteilen von Taschentüchern",1,0.01f))).AddTree(new LawNode(new Law("Verteilen von Disinfektionmittel", 1, 0.01f)));
        //Debug.Log(JsonUtility.ToJson(test));

        //LawNode temp = JsonUtility.FromJson<LawNode>();
    }

}
