using UnityEngine;
using System.Linq;

public class Graph : MonoBehaviour
{
    public Country world;
    public State state;
    public LineRenderer susceptible;
    public LineRenderer infected;
    public LineRenderer recovered;
    public LineRenderer dead;

    private RectTransform rect;

    private void Start()
    {
        rect = transform.GetComponent<RectTransform>();
        susceptible.sortingOrder = 4;
        susceptible.sortingLayerName = "UI"; 
    }

    public void SelectState(State state)
    {
        this.state = state;
        UpdateLines();
    }

    public void UpdateLines()
    {
        if(state == null)
        {
            susceptible.positionCount = world.susceptibleHistory.Count;
            var suspos = world.susceptibleHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(susceptible.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / world.population) * rect.rect.height,
                    -1)
            ).ToArray();
            susceptible.SetPositions(suspos);

            infected.positionCount = world.infectedHistory.Count;
            var infpos = world.infectedHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(infected.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / world.population) * rect.rect.height,
                    -1)
            ).ToArray();
            infected.SetPositions(infpos);

            recovered.positionCount = world.recoveredHistory.Count;
            var recpos = world.recoveredHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(recovered.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / world.population) * rect.rect.height,
                    -1)
            ).ToArray();
            recovered.SetPositions(recpos);

            dead.positionCount = world.deadHistory.Count;
            var deadpos = world.deadHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(dead.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / world.population) * rect.rect.height,
                    -1)
            ).ToArray();
            susceptible.SetPositions(deadpos);
        } else
        {
            susceptible.positionCount = state.susceptibleHistory.Count;
            var suspos = state.susceptibleHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(susceptible.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / state.population) * rect.rect.height,
                    -1)
            ).ToArray();
            susceptible.SetPositions(suspos);

            infected.positionCount = state.infectedHistory.Count;
            var infpos = state.infectedHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(infected.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / state.population) * rect.rect.height,
                    -1)
            ).ToArray();
            infected.SetPositions(infpos);

            recovered.positionCount = state.recoveredHistory.Count;
            var recpos = state.recoveredHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(recovered.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / state.population) * rect.rect.height,
                    -1)
            ).ToArray();
            recovered.SetPositions(recpos);

            dead.positionCount = state.deadHistory.Count;
            var deadpos = state.deadHistory.Select((val, index) => 
                new Vector3(
                    ((float) index / Mathf.Clamp(dead.positionCount - 1, 1, Mathf.Infinity)) * rect.rect.width,
                    (val / state.population) * rect.rect.height,
                    -1)
            ).ToArray();
            susceptible.SetPositions(deadpos);
        }
    }
}