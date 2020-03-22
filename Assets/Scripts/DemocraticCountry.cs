﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DemocraticCountry : Country
{

    public override void EnforceLaw(Law law)
    {
        foreach(State state in states)
        {
            state.r0 -= law.r0Dampener;
            state.isolation -= law.isolationDampener;
            state.mt -= law.mtDampener;

        }

        enforcedLaws.Add(law);
    }

    private void Start()
    {
        
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
