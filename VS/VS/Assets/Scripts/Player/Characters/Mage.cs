using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player {

	// Use this for initialization
	void Start () {
        base.Start();
	}	

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
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
    void Update()
    {
        base.Update();
        HandleKeyAbilities();

    }

    private void HandleKeyAbilities()
    {
        if (Input.GetKey("1"))
        {
            Debug.Log("Ability 1");
        }
        if (Input.GetKey("2"))
        {
            Debug.Log("Ability 2");
        }
        if (Input.GetKey("3"))
        {
            Debug.Log("Ability 3");
        }
        if (Input.GetKey("4"))
        {
            Debug.Log("Ability 4");
        }
    }
}
