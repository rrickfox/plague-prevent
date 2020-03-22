using UnityEngine;
using TMPro;
using System.Globalization;

public class SideStats : MonoBehaviour
{
    public Country world;
    public State state;

    public TMP_Text population;
    public TMP_Text susceptible;
    public TMP_Text infected;
    public TMP_Text recovered;
    public TMP_Text dead;

    private CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");

    private void Start()
    {
        SelectState(null);
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }

    public void SelectState(State state) => this.state = state;

    public void UpdateStats()
    {
        if(state == null)
        {
            population.text = world.population.ToString("N0", culture);
            susceptible.text = world.susceptible.ToString("N0", culture);
            infected.text = (world.infected + world.hospitalized + world.critical).ToString("N0", culture);
            recovered.text = world.recovered.ToString("N0", culture);
            dead.text = world.dead.ToString("N0", culture);
        } else
        {
            population.text = state.population.ToString("N0", culture);
            susceptible.text = state.susceptible.ToString("N0", culture);
            infected.text = (state.infected + state.hospitalized + state.critical).ToString("N0", culture);
            recovered.text = state.recovered.ToString("N0", culture);
            dead.text = state.dead.ToString("N0", culture);
        }
    }
}