using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DemocraticCountry : Country
{

    bool enforcingLaw = false;

    private IEnumerator StartLawProcess(@Law law)
    {
        enforcingLaw = true;
        //Pick random satisfaction between two values
        float satisfaction = Random.Range(law.satisfaction / 2f, law.satisfaction);
        float count = 0;
        float progress = 0f;

        law.active = true;
        enforcedLaws.Add(law);
        enforcingLaw = false;

        while (count < satisfaction*100f)
        {
            count += 1;
            progress = count / (satisfaction*100f);
            Debug.Log(string.Format("Enforcing Law: {0}, Progress: {1}%", law.name, progress * 100f));
            yield return new WaitForSeconds(0.1f);
        }

        
        foreach (State state in states)
        {
            state.r0 -= law.r0Dampener;
            state.isolation -= law.isolationDampener;
            state.mt -= law.mtDampener;
        }


    }


    public override void EnforceLaw(Law law)
    {
        Debug.Log(string.Format("Enforcing Law: {0}",law.name));
        if (!enforcingLaw)
        {
            StartCoroutine(StartLawProcess(law));
        }
        
    }

    private void Start()
    {
        
        //Debug.Log("Population: " + population);
        var randomStateIndex = random.Next(states.Count);
        Debug.Log("Bundesland mit erstem Infiziertem: " + states[randomStateIndex].stateName);
        states[randomStateIndex].Infect();
    }

    private void FixedUpdate()
    {
        UpdateTick();
    }

}
