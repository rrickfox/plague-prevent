using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 1.5f;            //Movement sensitivity

    public bool panPressed = false;             //Is pan button pressed

    public float maxZoom = 2.5f;                //Maximum zoom level
    public float minZoom = 7f;                  //Minimum zoom level

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
            Camera.main.transform.position -= new Vector3(xMov, yMove);
        }
        //Debug.Log(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")));
        if(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f && !manager.paused && !manager.menu)
        {
            //Clamps zoom level
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), maxZoom, minZoom);
        }
    }
}
