using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Continous_Damage : MonoBehaviour {

    public float damage = 20;
    public float damagedelay = 1;
    public float range = 8;

    private List<Enemy> InRangeEnemyList = new List<Enemy>();

    private float time;

	// Use this for initialization
	void Start () {
        time = Time.time + damagedelay;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {

       
        InRangeEnemyList.Add(other.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            InRangeEnemyList.Remove(other.GetComponent<Enemy>());
        }
    }

    // Update is called once per frame
    void Update () {

        foreach(Enemy e in InRangeEnemyList)
        {
            if(e == null)
            {
                continue;
            }

       
        if (time <= Time.time && Vector3.Distance(transform.position, e.transform.position) <= range)
        {

                e.TakeDamage(damage);
            time = Time.time + damagedelay;
        }
        }
    }
}
