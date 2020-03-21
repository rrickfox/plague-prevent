using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disease : MonoBehaviour
{
    public string name;                         //Name of the disease
    public float infectionRate;                 //How fast the disease spreads
    public enum types {Bacteria, Virus};        //Types of disease
    public types type;                          //Which type this disease is

    //Infect a country
    public virtual void Infect(Country country)
    {
        //Select random state in country and add one infected
        int index = Mathf.RoundToInt(Random.Range(0f, country.states.Count - 1));
        country.states[index].infected += 1;
    }

    //Infect a state
    public abstract void Infect(State state);
}
