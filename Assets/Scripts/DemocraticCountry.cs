using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemocraticCountry : Country
{

    public override void EnforceLaw(Law law)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        ReadLaws();
    }
}
