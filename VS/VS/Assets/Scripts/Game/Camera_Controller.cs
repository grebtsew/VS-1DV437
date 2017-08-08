using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    private Vector3 offset;
    public Player target;
    private bool targetset = false;

    public void setTarget(Player p)
    {
        target = p;

        // disable extra audiolisteners
        if (p.player_controller.controll_mode == Player_Controll.Ai)
        {
            GetComponentInChildren<AudioListener>().enabled = false;
        }

        offset = Statics.camera_offset;
        transform.position = target.transform.position + offset;
        targetset = true;
    }

    public void updateRotation()
    {
        // make camera focus on correct player
        Camera.main.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + Statics.CameraRotationOffset, target.transform.position.z));
    }

    void LateUpdate()
    {
        // make camera follow player
        if (target != null)
        {

            transform.position = target.transform.position + offset;
        }
    }

    void Update()
    {
        // scroll functionality
        if (targetset)
        {
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * Statics.SCROLL_SENSITIVITY;
            fov = Mathf.Clamp(fov, Statics.MIN_SCROLL, Statics.MAX_SCROLL);
            Camera.main.fieldOfView = fov;
        }
    }

}
