using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DemocraticCountry : Country
{
    public float ticks = 0f;
    public float seconds = 0f;
    public System.Random random = new System.Random();
    public float chanceOfInfectedToTravel = 0.001f;
    public float chanceOfTravellerToInfect = 0.5f;

    public override void EnforceLaw(Law law)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        ReadLaws();

        Debug.Log("Population: " + population);
        var randomStateIndex = random.Next(states.Count);
        Debug.Log("Bundesland mit erstem Infiziertem: " + states[randomStateIndex].stateName);
        states[randomStateIndex].Infect();
    }

    private void FixedUpdate()
    {
        ticks++;
        if(ticks == 50)
        {
            ticks = 0;
            seconds++;
            foreach(var state in states)
                state.CalculateInfectionRates();
            foreach(var state in states)
            {
                if(random.NextDouble() <= state.infected * chanceOfInfectedToTravel * chanceOfTravellerToInfect)
                {
                    state.neighbouring[random.Next(state.neighbouring.Count)].Infect();
                }
            }
            Debug.Log("days: " + seconds / Constants.timeScale);
            Debug.Log("susceptible: " + susceptible);
            Debug.Log("exposed: " + exposed);
            Debug.Log("infected: " + infected);
            Debug.Log("hospitalized: " + hospitalized);
            Debug.Log("critical: " + critical);
            Debug.Log("recovered: " + recovered);
            Debug.Log("dead: " + dead);
        }
    }

}
