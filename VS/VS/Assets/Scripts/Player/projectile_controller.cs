using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_controller : MonoBehaviour
{

    private Enemy target;
    private bool targetIsSet = false;
    public float speed = 100;
    public float damage = 10f;


    public void applyTarget(Enemy enemy)
    {
        target = enemy;
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
            target.TakeDamage(damage);

            Destroy(this);
        }

    }

}
