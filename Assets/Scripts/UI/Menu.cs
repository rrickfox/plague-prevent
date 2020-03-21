using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject diseaseMenu;
    public GameObject diseaseInfo;
    public GameObject countryMenu;

    // Start is called before the first frame update
    void Start()
    {
        ToMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        mainMenu.SetActive(true);
        diseaseMenu.SetActive(false);
        diseaseInfo.SetActive(false);
        countryMenu.SetActive(false);
    }

    public void ToDiseaseMenu()
    {
        mainMenu.SetActive(false);
        diseaseMenu.SetActive(true);
        diseaseInfo.SetActive(false);
        countryMenu.SetActive(false);
    }

    public void ToDiseaseInfo()
    {
        mainMenu.SetActive(false);
        diseaseMenu.SetActive(false);
        diseaseInfo.SetActive(true);
        countryMenu.SetActive(false);
    }

    public void ToCountryMenu()
    {
        mainMenu.SetActive(false);
        diseaseMenu.SetActive(false);
        diseaseInfo.SetActive(false);
        countryMenu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
