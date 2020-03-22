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

        Debug.Log("Population: " + population);
        var randomStateIndex = random.Next(states.Count);
        Debug.Log("Bundesland mit erstem Infiziertem: " + states[randomStateIndex].stateName);
        states[randomStateIndex].Infect();
    }

    private void FixedUpdate()
    {
        UpdateTick();
    }

}
