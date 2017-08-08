using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class global_camera_controller : MonoBehaviour
{
    public List<Camera_Controller> cameraList;

    public Text following_label;
    public Text following_name_label;
    public Text following_level_label;

    private Camera_Controller Active_Camera_Controller;

    private bool initiated = false;

    private void ChangeToPlayerCamera()
    {
        foreach(Camera_Controller c in cameraList)
        {

            if(c.target.player_controller.controll_mode == Player_Controll.Player)
            {
                c.GetComponentInChildren<Camera>().enabled = true;
                c.updateRotation();
                Active_Camera_Controller = c;
            } else
            {
                c.GetComponentInChildren<Camera>().enabled = false;
            }
        }
    }

    public void initiate()
    {
        foreach(Camera_Controller c in FindObjectsOfType<Camera_Controller>())
        {
            cameraList.Add(c);
        }
        ChangeToPlayerCamera();
        initiated = true;
    }

    public void ChangeCamera()
    {
       int index = cameraList.IndexOf(Active_Camera_Controller);

        cameraList[index].GetComponentInChildren<Camera>().enabled = false;

        if(cameraList.Count > index + 1) {
        cameraList[index+1].GetComponentInChildren<Camera>().enabled = true;
            Active_Camera_Controller = cameraList[index + 1];
            cameraList[index + 1].updateRotation();
        } else {
        cameraList[Statics.zero].GetComponentInChildren<Camera>().enabled = true;
            Active_Camera_Controller = cameraList[Statics.zero];
            cameraList[Statics.zero].updateRotation();
        }

        updateLabels();
    }

    public Player_Controll following_controller()
    {
        return Active_Camera_Controller.target.player_controller.controll_mode;
    }
    public string following_name()
    {
        return Active_Camera_Controller.target.Name;
    }
    public float following_level()
    {
        return Active_Camera_Controller.target.level;
    }

    void Update()
    {
        if (initiated) { 
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeToPlayerCamera();
        }

            updateLabels();
        }
    }

    private void updateLabels()
    {
        if (initiated) {
            following_level_label.text = following_level().ToString();
            following_name_label.text = following_name();
            following_label.text = following_controller().ToString();
        }
    }
}
