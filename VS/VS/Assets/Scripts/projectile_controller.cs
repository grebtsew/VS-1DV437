using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_controller : MonoBehaviour
{

    private Enemy target;
    private bool targetIsSet = false;
    public float speed = 100;

    // Use this for initialization
    void Start()
    {

    }

    public void applyTarget(Enemy go)
    {
        target = go;
        targetIsSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetIsSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Enemy")
        {
            target.TakeDamage(10f);
            
         //   Destroy(this.GameObject);
        }

    }

}
