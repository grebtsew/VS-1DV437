using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    private Vector3 offset;
    private float seePlayerOffset = -6;

    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;

    public Player target;

    public void setTarget(Player p)
    {
        target = p;
        Camera.main.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y  + seePlayerOffset, target.transform.position.z));
       
    }

    // Use this for initialization
    void Start()
    {

        offset = transform.position;

    }

    public void updateRotation()
    {
        Camera.main.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + seePlayerOffset, target.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target!= null) {
          
        transform.position = target.transform.position + offset;
        }
    }

    void Update()
    {
        float fov  = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
        
     }

    }
