using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject skillTreeUI;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log(hit.transform.name);
        }
    }


    public void ShowSkillTree()
    {
        if (skillTreeUI.active)
        {
            skillTreeUI.SetActive(false);
        }
        else
        {
            skillTreeUI.SetActive(true);
        }
    }


}
