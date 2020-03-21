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
        var xpos = 0f;
        if (newValues.Length < parts.Count)
            return;

        for(int i = 0; i < parts.Count; i++)
        {
            xpos += newValues[i] * bar.sizeDelta.x;
            parts[i].localPosition = new Vector3(xpos, 0f, 0f);
            parts[i].sizeDelta = new Vector2(newValues[i] * bar.sizeDelta.x, bar.sizeDelta.y);
        }


    }
}
