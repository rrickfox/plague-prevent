using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (selectedCountry == hit.transform.gameObject)
                    SetFocus(selectedCountry);
                else
                {
                    selectedCountry = hit.transform.gameObject;
                    state = selectedCountry.GetComponent<State>();
                }
            }
            else {
                selectedCountry = null;
                state = null;
            }
            countryStats.SelectState(state);
            sideStats.SelectState(state);
        }
    }

    public void SetFocus(GameObject state)
    {
        var collider = state.GetComponent<PolygonCollider2D>();
        var center = new Vector2();
        foreach (var p in collider.points)
        {
            center += p;
        }
        center /= collider.points.Length;
        Camera.main.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z);
    }
}
