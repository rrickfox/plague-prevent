using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition)).ToList().OrderBy(h => h.distance);
            foreach (var hit in hits)
            {
                Texture2D tex = hit.transform.GetComponent<SpriteRenderer>().material.mainTexture as Texture2D;
                var x = (int)(hit.textureCoord.x * tex.width);
                var y = (int)(hit.textureCoord.y * tex.height);
                var pix = tex.GetPixel(x, y);
                if (pix.a > 0)
                {
                    Debug.Log(hit.transform.gameObject.name);
                }
            }
        }
    }
}
