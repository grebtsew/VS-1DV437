using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_camera_controller : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;

    void Start()
    {

        Camera1.GetComponent<Camera>().enabled = true;
        Camera2.GetComponent<Camera>().enabled = false;
    }


    public void ChangeCamera()
    {

        if (Camera1.GetComponent<Camera>().enabled)
        {
            Camera2.GetComponent<Camera>().enabled = true;
            Camera1.GetComponent<Camera>().enabled = false;
        }
        else if (Camera2.GetComponent<Camera>().enabled)
        {
            Camera1.GetComponent<Camera>().enabled = true;
            Camera2.GetComponent<Camera>().enabled = false;
        }

       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera();
        }

    }
}
