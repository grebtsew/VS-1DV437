using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_Controller : MonoBehaviour
{

    public Rigidbody rb;
    public Player player;
    public Vector3 playerpos;
    public Animator animator;

    public bool attack = false;
    public bool inPlayerRange = false;
    public bool isdead = false;

    private int min_player_distance = 8;
    private int attack_range = 4;

    public float attackspeed = 0.4f;

    public float damage = 0;
    public float energy_cost = 30f;
    public float speed = 1f;
    private float attackdelay;
    private float health;
    public float lifetime = 20;
    private float time;

    private Enemy target;
    private List<Enemy> InRangeEnemyList = new List<Enemy>();

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        health = Statics.health;
        time = Time.time + lifetime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy temp = other.GetComponent<Enemy>();
            InRangeEnemyList.Add(temp);
            target = temp;

        }
    }

    public virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            InRangeEnemyList.Remove(other.GetComponent<Enemy>());
        }
    }

    public void setPlayer(Player p)
    {
        player = p;
        setLevel();
    }

    public virtual void setLevel()
    {
        

    }

    public void rotate_y_towards_transform(Transform t)
    {
        if (t != null)
        {
            var lookPos = t.position - transform.position;
            lookPos.y = 0;
            if (lookPos != new Vector3(0, 0, 0))
            {
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * player.speed);
            }
        }
    }

    public virtual void deadAnimation()
    {
       
    }
    public virtual void walkStartAnimation()
    {

    }
    public virtual void walkStopAnimation()
    {

    }
    public virtual void attackAnimation()
    {

    }
  
    void Update()
    {
        if (player != null)
        {
            if (Time.time >= time || isdead)
            {
                // Destroy Ally (just once)
                if (!isdead)
                {
                    isdead = true;
                    deadAnimation();
                    Destroy(gameObject);
                }

            }
            else
            {

                // got no enemies
                if (InRangeEnemyList.Count <= 0)
                {
                    //if not within range
                    if (Vector3.Distance(player.transform.position, transform.position) > min_player_distance)
                    {
                        // move to player
                        walkStartAnimation();

                        rotate_y_towards_transform(player.transform);
                        playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
                    }
                    else
                    {
                        walkStopAnimation();
                        // else find new enemies
                        foreach (Enemy e in FindObjectsOfType<Enemy>())
                        {
                            if (e.player == player)
                            {
                                InRangeEnemyList.Add(e);
                            }
                        }
                    }

                }
                else
                {
                   
                 
                    if (target != null)
                    {
                        // if not within attackrange
                        if (Vector3.Distance(target.transform.position, transform.position) > attack_range)
                        {
                            // move to target
                            walkStartAnimation();
                            playerpos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                            transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
                        }
                        else
                        {
                            walkStopAnimation();
                            // Attack target
                            if (Time.time > attackdelay)
                            {
                                attackdelay = Time.time + attackspeed;
                                attackAnimation();
                               
                                target.TakeDamage(damage);
                            }
                        }
                        rotate_y_towards_transform(target.transform);

                    }
                    else
                    {
                        //set next target
                        InRangeEnemyList.Remove(target);
                        if (InRangeEnemyList.Count > 0)
                        {
                            target = InRangeEnemyList[0];
                        }
                    }
                }
            }
        }
    }
}
