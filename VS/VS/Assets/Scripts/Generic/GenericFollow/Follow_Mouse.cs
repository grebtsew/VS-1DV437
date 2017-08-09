using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Mouse : MonoBehaviour {

	void FixedUpdate () {  
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            transform.position = targetPoint;
        }

    }
}
