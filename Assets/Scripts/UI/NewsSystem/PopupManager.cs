using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public GameObject gameUI;
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text messageText;

    public AnimationCurve animCurve;

    private Queue<NewsClass> queue = new Queue<NewsClass>();
    private bool activePopup;

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

    private void Update()
    {
        if(gameUI.activeSelf && queue.Count > 0)
        {
            if (!activePopup)
            {
                ShowPopup(queue.Dequeue());
            }
        }
    }

    public void Popup(string title, string message)
    {
        queue.Enqueue(new NewsClass(title, message));
    }

    public void Popup(NewsClass news)
    {
        queue.Enqueue(news);
    }

    public void ShowPopup(NewsClass news)
    {
        titleText.text = news.title;
        messageText.text = news.message;
        StartCoroutine(AnimatePopupCustom(1f, 2f));
    }

    IEnumerator AnimatePopupLinear(float animTime, float seconds)
    {
        activePopup = true;
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);
        for (float i = 0; i <= 1; i += 1 / (animTime * 50))
        {
            panel.transform.localScale = new Vector3(i, i, i);
            yield return null;
        }

        yield return new WaitForSeconds(seconds);

        for (float i = 1; i >= 0; i -= 1 / (animTime * 50))
        {
            panel.transform.localScale = new Vector3(i, i, i);
            yield return null;
        }

        panel.SetActive(false);
        activePopup = false;
    }

    IEnumerator AnimatePopupCustom(float animTime, float seconds)
    {
        activePopup = true;
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);
        for (float i = 0; i <= 1; i += 1 / (animTime * 50))
        {
            var value = animCurve.Evaluate(i);
            panel.transform.localScale = new Vector3(value , value, value);
            yield return null;
        }

        yield return new WaitForSeconds(seconds);

        for (float i = 1; i >= 0; i -= 1 / (animTime * 50))
        {
            var value = animCurve.Evaluate(i);
            panel.transform.localScale = new Vector3(value, value, value);
            yield return null;
        }

        panel.SetActive(false);
        activePopup = false;
    }
}
