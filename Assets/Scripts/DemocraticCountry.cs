using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DemocraticCountry : Country
{

    public override void EnforceLaw(Law law)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        ReadLaws();

        Debug.Log("Population: " + states.Sum(s => s.population));
    }

    private void FixedUpdate()
    {
        // secpertick * 50 * 60 * 2 = 60 * 60 * 24
        // 14.4 seconds per tick
        foreach(var state in states)
            state.CalculateInfectionRates();
    }

}
