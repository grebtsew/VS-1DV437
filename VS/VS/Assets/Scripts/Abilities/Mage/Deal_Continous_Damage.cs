using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deal_Continous_Damage : MonoBehaviour
{

    public float damage = 20;
    public float damagedelay = 1;
    public float range = 8;

    private List<Enemy> InRangeEnemyList = new List<Enemy>();

    private float time;
    private Player player;


    // Use this for initialization
    void Start()
    {
        time = Time.time + damagedelay;

    }

    public void setPlayer(Player p)
    {
        player = p;
        setlevel();
    }

    private void setlevel()
    {

        damage = player.base_damage + player.level;
        range = range + (player.level / 20);
        damagedelay *= player.attackspeed;
        transform.localScale += new Vector3(player.level * 0.01f, 0, player.level * 0.01f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
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
    void Update()
    {
        if (time <= Time.time)
        {
            time = Time.time + damagedelay;

            foreach (Enemy e in InRangeEnemyList)
            {
                if (e == null)
                {
                    continue;
                }
                else
                {
                    e.TakeDamage(damage);
                }
            }
        }
    }
}
