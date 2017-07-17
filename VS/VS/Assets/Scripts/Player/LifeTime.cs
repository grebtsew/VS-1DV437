using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public float lifetime = 10;

    private float time;

	// Use this for initialization
	void Start () {
        time = Time.time+lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if(time <= Time.time)
        {
            Destroy(gameObject);
        }
	}
}
