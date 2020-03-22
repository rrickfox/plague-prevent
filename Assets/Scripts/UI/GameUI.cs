using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject actionsMenu;
    public GameObject statisticsMenu;

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
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        actionsMenu.SetActive(false);
        statisticsMenu.SetActive(false);
        Manager.Instance.menu = false;
        Manager.Instance.action = false;
    }

    public void ToPauseMenu()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        actionsMenu.SetActive(false);
        statisticsMenu.SetActive(false);
        Manager.Instance.menu = true;
        Manager.Instance.action = false;
    }

    public void ToSettingsMenu()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        actionsMenu.SetActive(false);
        statisticsMenu.SetActive(false);
        Manager.Instance.menu = true;
        Manager.Instance.action = false;
    }

    public void ToActionsMenu()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        actionsMenu.SetActive(true);
        statisticsMenu.SetActive(false);
        Manager.Instance.menu = false;
        Manager.Instance.action = true;
    }

    public void ToStatisticsMenu()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        actionsMenu.SetActive(false);
        statisticsMenu.SetActive(true);
        Manager.Instance.menu = false;
        Manager.Instance.action = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
