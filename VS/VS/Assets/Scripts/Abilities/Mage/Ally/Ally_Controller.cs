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

    public float damage = 0;
    public float energy_cost = 30f;
    public float speed = 1f;
    private float attackdelay;
    private float health;
    public float lifetime = 20;
    private float time;

    private Enemy target;
    private List<Enemy> InRangeEnemyList = new List<Enemy>();

    void Start()
    {
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

    private void setLevel()
    {
        damage += player.a3_level * 20;
        lifetime += player.a3_level * 5;
        if (player.a3_level > 2 && player.a3_level < 4)
        {
            transform.localScale += new Vector3(50, 50, 50);
        }
        else
        if (player.a3_level >= 4)
        {
            transform.localScale += new Vector3(100, 100, 100);
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
                    animator.SetBool("isdead", true);
                    animator.SetTrigger("Dead");
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
                        animator.SetBool("Withinrange", true);
                        rotate_towards_transform(player.transform);
                        playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
                    }
                    else
                    {
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

                    animator.SetBool("Withinrange", false);

                    if (target != null)
                    {
                        // if not within attackrange
                        if (Vector3.Distance(target.transform.position, transform.position) > attack_range)
                        {
                            // move to target
                            playerpos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                            transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
                        }
                        else
                        {

                            // Attack target
                            if (Time.time > attackdelay)
                            {
                                attackdelay = Time.time + 0.4f;
                                animator.SetTrigger("Attack");
                                target.TakeDamage(damage);
                            }
                        }
                        rotate_towards_transform(target.transform);

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
