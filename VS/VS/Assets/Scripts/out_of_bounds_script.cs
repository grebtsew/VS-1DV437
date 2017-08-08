using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class out_of_bounds_script : MonoBehaviour {

    private BoxCollider bounds;

	// Use this for initialization
	void Start () {
        bounds = GetComponent<BoxCollider>();
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            Player_Controller p = other.GetComponent<Player_Controller>();
            
        }

    }
}
