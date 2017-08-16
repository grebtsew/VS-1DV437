using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_sword_script : MonoBehaviour {

    public float damage = 100;
    public float rotate_speed = 10;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotate_speed);
       
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy temp = other.GetComponent<Enemy>();
            temp.TakeDamage(damage);
            
        }
    }
}
