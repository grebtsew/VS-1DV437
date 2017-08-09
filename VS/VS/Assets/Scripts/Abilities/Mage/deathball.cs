using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathball : MonoBehaviour
{

    private float damage = 100;
    public float energy = 40;

    public Player player;
    public float speed = 4;

    private List<Enemy> TargetList = new List<Enemy>();
    private int index;

    public void setPlayer(Player p)
    {
        player = p;
        setLevel();
        getEnemies();
    }

    public void getEnemies()
    {
        if (TargetList != null)
        {
            TargetList.Clear();
        }

        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {


            if (e.player == player)
            {
                TargetList.Add(e);
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    private void setLevel()
    {
        speed += player.a4_level * 4;
        energy -= player.a4_level;

        if (player.a4_level > 3)
        {
            transform.localScale = transform.localScale += new Vector3(2, 2, 2);
        }

        if (player.a4_level == 6)
        {

        }
    }

    public void rotate_towards_transform(Transform t)
    {
        if (t != null)
        {
            if (t.position - transform.position != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t.position - transform.position), Time.fixedDeltaTime * speed);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {


            if (index < TargetList.Count)
            {
                if (TargetList[index] != null)
                {
                    Vector3 targetpos = new Vector3(TargetList[index].transform.position.x, transform.position.y, TargetList[index].transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetpos, speed * Time.deltaTime);
                    rotate_towards_transform(TargetList[index].transform);
                }
                else
                {
                    index++;
                }
            }
            else
            {
                bool alldead = true;

                getEnemies();

                for (int i = 0; i < TargetList.Count; i++)
                {
                    if (TargetList[i] != null)
                    {
                        index = i;
                        alldead = false;
                        break;
                    }

                }

                if (alldead)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy temp = other.GetComponent<Enemy>();
            temp.TakeDamage(damage);
            index++;
        }
    }
}
