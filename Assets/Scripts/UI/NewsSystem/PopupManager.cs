﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text messageText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    public void Popup(string title, string message)
    {
        titleText.text = title;
        messageText.text = message;
        panel.SetActive(true);
    }

    public void Popup(NewsClass news)
    {
        titleText.text = news.title;
        messageText.text = news.message;
        StartCoroutine(AnimatePopup(1f, 2f));
    }

    IEnumerator AnimatePopup(float animTime, float seconds)
    {
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);
        for (float i = 0; i <= 1; i += 1 / (animTime * 50))
        {
            panel.transform.localScale = new Vector3(i, i, i);
            yield return null;
        }

        yield return new WaitForSeconds(seconds);

        for(float i = 1; i>=0;i-= 1 / (animTime * 50))
        {
            panel.transform.localScale = new Vector3(i, i, i);
            yield return null;
        }
    }
}
