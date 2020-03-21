using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public RectTransform bar;
    public List<RectTransform> parts;

    public void SetValues(params float[] newValues)
    {
        if (newValues.Length < parts.Count)
            return;

        for(int i = 0; i < parts.Count; i++)
        {
            parts[i].sizeDelta = new Vector2(newValues[i] * bar.sizeDelta.x, bar.sizeDelta.y);
        }


    }
}
