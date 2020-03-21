using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State : MonoBehaviour
{
    public string stateName;                //Name of the state

    public float susceptible = 0;
    public float exposed = 0;               //How many people are infected but don't show symptoms
    public float infected = 0;              //How many people are currently infected
    public float hospitalized = 0;          //How many people have been hospitalized
    public float critical = 0;              //How many people need intensive care
    public float dead = 0;                  //How many people have died
    public float recovered = 0;             //How many people have recovered from the virus
    public float population;                //Total population of the state
    public float healthy => (population - dead - infected );

    public float susceptibleChange;         //Change in number of not infected people
    public float exposedChange;             //Change in number of exposed people
    public float infectedChange;            //Change in number of infected
    public float hospitalizedChange;        //Change in number of hospitalized people
    public float criticalChange;            //Change in critical cases
    public float deadChange;                //Change in deaths
    public float recoveredChange;           //Change in number of recovered people

    public List<State> neighbouring;        //Neighbouring states

    private SpriteRenderer renderer;

    

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        susceptible = population - 1;
        exposed = 1;
    }

    private void Update()
    {
        //Copyright JP
        Color inf = Color.Lerp(Color.white, Color.red, (float)infected / (float)population);
        Color deadC = Color.Lerp(inf, Color.black, (float)dead / (float)population);
        renderer.color = deadC;
    }

    public float beta = 1;                  //transmission rate
    public float tl = 1;                    //latency time from infection to infectiousness
    public float ti = 1;                    //time an individual is infectious after which he/she recoveres of falls severely ill
    public float th = 1;                    //time a sick person recovers or deteriorates into a critical state
    public float tc = 1;                    //time a person remains critical before dying or stabilizing
    public float m = 0.1f;                  //fraction of infectious that are asymptomatic or mild
    public float c = 0.1f;                  //fraction of severe cases that turn critical
    public float f = 0.1f;                  //fraction of critical cases that are fatal

    // https://neherlab.org/covid19/about
    public void CalculateInfectionRates()
    {
        susceptibleChange  = -beta * susceptible * infected;
        exposedChange      = beta * susceptible * infected - exposed / tl;
        infectedChange     = exposed / tl - infected / ti;
        hospitalizedChange = (1 - m) * infected / ti + (1 - f) * critical / tc - hospitalized / th;
        criticalChange     = c * hospitalized / th - critical / tc;
        recoveredChange    = m * infected / ti + (1 - c) * hospitalized / th;
        deadChange         = f * critical / tc;

        susceptible  += susceptibleChange;
        exposed      += exposedChange;
        infected     += infectedChange;
        hospitalized += hospitalizedChange;
        critical     += criticalChange;
        recovered    += recoveredChange;
        dead         += deadChange;
    }
}
