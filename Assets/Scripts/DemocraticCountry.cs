using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DemocraticCountry : Country
{

    private IEnumerator StartLawProcess(@Law law)
    {
        enforcingLaw = true;
        //Pick random satisfaction between two values
        float satisfaction = Random.Range(law.satisfaction / 2f, law.satisfaction);
        float count = 0;

        law.active = true;
        enforcedLaws.Add(law);
       
        while (count < satisfaction*100f)
        {
            count += 1;
            lawEnforcementProgress = count / (satisfaction*100f);
            //Debug.Log(string.Format("Enforcing Law: {0}, Progress: {1}%", law.name, lawEnforcementProgress * 100f));
            yield return new WaitForSeconds(0.1f);
        }

        disease.r0 -= law.r0Dampener;
        disease.isolation -= law.isolationDampener;
        disease.mt -= law.mtDampener;

        enforcingLaw = false;
    }


    public override void EnforceLaw(Law law)
    {
        Debug.Log(string.Format("Enforcing Law: {0}",law.name));
        if (!enforcingLaw)
        {

            currentBudget -= law.cost;
            StartCoroutine(StartLawProcess(law));
        }
        
    }

    private void Start()
    {
        
        //Debug.Log("Population: " + population);
        var randomStateIndex = random.Next(states.Count);
        Debug.Log("Bundesland mit erstem Infiziertem: " + states[randomStateIndex].stateName);
        states[randomStateIndex].Infect();

        disease = new Corona();
    }

    private void FixedUpdate()
    {
        UpdateTick(disease);
    }

}
