using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject actionsMenu;
    public GameObject statisticsMenu;

    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
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
        statisticsMenu.SetActive(false);
        manager.menu = false;
    }

    public void ToActionsMenu()
    {
        gameUI.SetActive(false);
        actionsMenu.SetActive(true);
        statisticsMenu.SetActive(false);
        manager.menu = true;
    }

    public void ToStatisticsMenu()
    {
        gameUI.SetActive(false);
        actionsMenu.SetActive(false);
        statisticsMenu.SetActive(true);
        manager.menu = true;
    }
}
