using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    private float seePlayerOffset = 1;

    float minFov = 15f;
    float maxFov = 90f;
 float sensitivity = 10f;


    // Use this for initialization
    void Start()
    {
        Camera.main.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y+ seePlayerOffset, player.transform.position.z));
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    void Update()
    {
        float fov  = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    }
