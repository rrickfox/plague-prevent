using System.Collections;
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

    public List<float> susceptibleHistory = new List<float>();
    public List<float> infectedHistory = new List<float>(){0};
    public List<float> recoveredHistory = new List<float>(){0};
    public List<float> deadHistory = new List<float>(){0};

    public List<State> neighbouring;        //Neighbouring states

    public float infectedThreshold = 0.5f;
    public float deadThreshold = 0.1f;

    private SpriteRenderer renderer;

    

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        susceptible = population;
        susceptibleHistory.Add(susceptible);
    }

    private void Update()
    {
        //Copyright JP
        Color inf = Color.Lerp(Color.white, Color.red, infected / (population * infectedThreshold));
        Color deadC = Color.Lerp(inf, Color.black, dead / (population * deadThreshold));
        renderer.color = deadC;
    }

    // https://neherlab.org/covid19/about
    public void CalculateInfectionRates(IDisease disease)
    {
        susceptibleChange  = -disease.beta * susceptible * infected / population;
        exposedChange      = disease.beta * susceptible * infected / population - exposed / disease.tl;
        infectedChange     = exposed / disease.tl - infected / disease.ti;
        hospitalizedChange = (1 - disease.m) * infected / disease.ti + (1 - disease.f) * critical / disease.tc - hospitalized / disease.th;
        criticalChange     = disease.c * hospitalized / disease.th - critical / disease.tc;
        recoveredChange    = disease.m * infected / disease.ti + (1 - disease.c) * hospitalized / disease.th;
        deadChange         = disease.f * critical / disease.tc;

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

        susceptibleHistory.Add(susceptible);
        infectedHistory.Add(exposed + infected + hospitalized + critical);
        recoveredHistory.Add(recovered);
        deadHistory.Add(dead);
    }

    public void Infect()
    {
        if(infected == 0)
            Debug.LogWarning(stateName + " was Infected!");
        susceptible -= 1;
        infected += 1;
    }
}
