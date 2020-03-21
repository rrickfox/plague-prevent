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
        //ReadLaws();

        Debug.Log("Population: " + population);
    }

    private void FixedUpdate()
    {
        foreach(var state in states)
            state.CalculateInfectionRates();
    }

}
