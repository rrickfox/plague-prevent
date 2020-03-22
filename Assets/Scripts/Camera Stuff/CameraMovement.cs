using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 1.5f;            //Movement sensitivity

    public bool panPressed = false;             //Is pan button pressed

    public float maxZoom = 2.5f;                //Maximum zoom level
    public float minZoom = 7f;                  //Minimum zoom level

    public Vector2 border1;                  //Border
    public Vector2 border2;                    //Border

    private Manager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
    }

    private void Update()
    {
        panPressed = Input.GetMouseButton(2);

        if (panPressed && !manager.paused && !manager.menu)
        {
            float xMov = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
            float yMove = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
            float xPos = Mathf.Clamp(Camera.main.transform.position.x - xMov, Mathf.Min(border1.x, border2.x), Mathf.Max(border1.x, border2.x));
            float yPos = Mathf.Clamp(Camera.main.transform.position.y - yMove, Mathf.Min(border1.y, border2.y), Mathf.Max(border1.y, border2.y));
            Camera.main.transform.position = new Vector3(xPos, yPos, Camera.main.transform.position.z);
        }
        //Debug.Log(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")));
        if(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f && !manager.paused && !manager.menu)
        {
            //Clamps zoom level
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), maxZoom, minZoom);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(border1, 1f);
        Gizmos.DrawWireSphere(border2, 1f);
        Gizmos.DrawLine(border1, new Vector2(border1.x, border2.y));
        Gizmos.DrawLine(border1, new Vector2(border2.x, border1.y));
        Gizmos.DrawLine(border2, new Vector2(border1.x, border2.y));
        Gizmos.DrawLine(border2, new Vector2(border2.x, border1.y));
    }
}
