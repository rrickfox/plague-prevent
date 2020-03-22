using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountryStats : MonoBehaviour
{
    public TMP_Text countryName;
    public Country world;
    public Bar bar;

    private State state;

    private void Start()
    {
        SelectCountry(null);
        UpdateStats();
    }

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
        if (state == null)
            bar.SetValues(world.susceptible / world.population, world.recovered / world.population, (world.infected + world.exposed + world.hospitalized + world.critical) / world.population, world.dead / world.population);
        else
            bar.SetValues(state.susceptible / state.population, state.recovered / state.population, (state.infected + state.exposed + state.hospitalized + state.critical) / state.population, state.dead / state.population);
    }
}
