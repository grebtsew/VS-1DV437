using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Enemy : MonoBehaviour {

    public Canvas ui;
    public Enemy target;


	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        // ui.transform.position = Camera.main.WorldToViewportPoint(target.transform.position);
        transform.LookAt(Camera.main.transform.position);
    }
}
