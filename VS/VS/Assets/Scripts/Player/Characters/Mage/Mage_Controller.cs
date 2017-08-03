using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Controller : Player_Controller {

	// Use this for initialization
	void Start () {
        base.Start();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void LateUpdate()
    {
        base.LateUpdate();
    }

    // Update is called once per frame
    void Update () {
        base.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }


    
}
