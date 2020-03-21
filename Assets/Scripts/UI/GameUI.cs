using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject actionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        ToGameUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameUI()
    {
        gameUI.SetActive(true);
        actionsMenu.SetActive(false);
    }

    public void ToActionsMenu()
    {
        gameUI.SetActive(false);
        actionsMenu.SetActive(true);
    }
}
