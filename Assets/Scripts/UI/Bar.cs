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
        var xpos = -bar.rect.width / 2f;
        if (newValues.Length < parts.Count)
            return;

        for(int i = 0; i < parts.Count; i++)
        {
            parts[i].localPosition = new Vector3(xpos, 0f, 0f);
            parts[i].sizeDelta = new Vector2(newValues[i] * bar.rect.width, bar.rect.height);
            xpos += newValues[i] * bar.rect.width;
        }


    }
}
