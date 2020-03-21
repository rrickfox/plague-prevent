using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountryStats : MonoBehaviour
{
    [Header("Debug")]
    [Range(0,1)]
    public float hea;
    [Range(0,1)]
    public float inf;
    [Range(0,1)]
    public float dea;

    public TMP_Text countryName;
    public Country world;
    public Bar bar;

    private State state;
    

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }

    public void SelectCountry(State state)
    {
        this.state = state;
        if (state == null)
            countryName.SetText(world.countryName);
        else
            countryName.SetText(state.stateName);
        
    }

    private void UpdateStats()
    {
        //if (state == null)
        //    bar.SetValues(world.healthy / world.population, world.infected / world.population, world.dead / world.population);
        //else
        //    bar.SetValues(state.healthy / state.population, state.infected / state.population, state.dead / state.population);
        bar.SetValues(hea, inf, dea);
    }
}
