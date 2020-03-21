using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log(hit.transform.name);

            //var hits = Physics2D.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition),Vector2.up).ToList().OrderBy(h => h.distance);
            /*foreach (var hit in hits)
            {
                Debug.Log(hit.transform.name);
                Texture2D tex = hit.transform.GetComponent<SpriteRenderer>().material.mainTexture as Texture2D;
                var x = (int)(hit.textureCoord.x * tex.width);
                var y = (int)(hit.textureCoord.y * tex.height);
                var pix = tex.GetPixel(x, y);
                if (pix.a > 0)
                {
                    Debug.Log(hit.transform.gameObject.name);
                }
            }*/
        }
    }
}
