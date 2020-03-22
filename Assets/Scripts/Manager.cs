using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool paused;
    public bool menu;

    public CountryStats countryStats;
    public SideStats sideStats;
    public GameObject selectedCountry;
    public State state;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                selectedCountry = hit.transform.gameObject;
                state = selectedCountry.GetComponent<State>();
            }
            else {
                selectedCountry = null;
                state = null;
            }
            countryStats.SelectState(state);
            sideStats.SelectState(state);
        }
    }
}
