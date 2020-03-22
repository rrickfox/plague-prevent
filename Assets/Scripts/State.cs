﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

[System.Serializable]
public class State : MonoBehaviour
{
    public string stateName;                //Name of the state

    public float susceptible = 0;           //how many people are susceptible to the disease
    public float exposed = 0;               //How many people are infected but don't show symptoms
    public float infected = 0;              //How many people are currently infected
    public float hospitalized = 0;          //How many people have been hospitalized
    public float critical = 0;              //How many people need intensive care
    public float dead = 0;                  //How many people have died
    public float recovered = 0;             //How many people have recovered from the virus
    public float population;                //Total population of the state

    public float susceptibleChange;         //Change in number of not infected people
    public float exposedChange;             //Change in number of exposed people
    public float infectedChange;            //Change in number of infected
    public float hospitalizedChange;        //Change in number of hospitalized people
    public float criticalChange;            //Change in critical cases
    public float deadChange;                //Change in deaths
    public float recoveredChange;           //Change in number of recovered people

    public List<State> neighbouring;        //Neighbouring states

    public float infectedThreshold = 0.5f;
    public float deadThreshold = 0.1f;

    private SpriteRenderer renderer;

    

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        susceptible = population;
        Debug.Log("beta: " +  beta);
    }

    private void Update()
    {
        //Copyright JP
        Color inf = Color.Lerp(Color.white, Color.red, infected / (population * infectedThreshold));
        Color deadC = Color.Lerp(inf, Color.black, dead / (population * deadThreshold));
        renderer.color = deadC;
    }

    public float beta => (r0 * isolation * mt) / ti; //transmission rate
    public float r0 = 4.5f;                 //secondary infections, range 2-3
    public float isolation = 1f;            //degree to which people are isolated from the population
    public float mt = 1f;                   //time course of mitigation measures
    public float tl => 2f * timeScale;      //latency time from infection to infectiousness
    public float ti => 14f * timeScale;     //time an individual is infectious after which he/she recovers or falls severely ill
    public float th => 17f * timeScale;     //time a sick person recovers or deteriorates into a critical state
    public float tc => 21f * timeScale;     //time a person remains critical before dying or stabilizing
    public float m => 0.10f;                //fraction of infectious that are asymptomatic or mild
    public float c => 0.15f;                //fraction of severe cases that turn critical
    public float f => 0.3f;                 //fraction of critical cases that are fatal

    // https://neherlab.org/covid19/about
    public void CalculateInfectionRates()
    {
        susceptibleChange  = -beta * susceptible * infected / population;
        exposedChange      = beta * susceptible * infected / population - exposed / tl;
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

        susceptible  = Mathf.Clamp(susceptible, 0, population);
        exposed      = Mathf.Clamp(exposed, 0, population);
        infected     = Mathf.Clamp(infected, 0, population);
        hospitalized = Mathf.Clamp(hospitalized, 0, population);
        critical     = Mathf.Clamp(critical, 0, population);
        recovered    = Mathf.Clamp(recovered, 0, population);
        dead         = Mathf.Clamp(dead, 0, population);
    }

    public void Infect()
    {
        if(infected == 0)
            Debug.LogWarning(stateName + " was Infected!");
        susceptible -= 1;
        infected += 1;
    }
}
