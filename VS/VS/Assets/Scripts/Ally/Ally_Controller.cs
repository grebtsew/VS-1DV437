using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_Controller : MonoBehaviour {

    public Rigidbody rb;
    public Player player;
    public Vector3 playerpos;

    public Animator animator;
    public float damage = 1;

    public float energy_cost = 30f;

    public bool attack = false;
    public float speed = 1f;
    public bool inPlayerRange = false;
    public float health = 100;
    public bool isdead = false;

    public float attackspeed = 0.5f;
    public float attackdelay;
    
    public float lifetime = 20;

    private Enemy target;
   
    private List<Enemy> InRangeEnemyList = new List<Enemy>();


    private float time;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            InRangeEnemyList.Add(other.GetComponent<Enemy>());
                target = other.GetComponent<Enemy>();
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

    // Use this for initialization
    void Start () {
       

       

        time = Time.time + lifetime;
	}

    private void setLevel()
    {
        damage += player.a3_level*20;
        lifetime += player.a3_level * 20;
        if (player.a3_level > 2 && player.a3_level < 4)
        {
            transform.localScale += new Vector3(50, 50, 50);
        } else
        if(player.a3_level >= 4)
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

    // Update is called once per frame
    void Update () {
        if(player != null) {
        if(Time.time >= time)
        {
            animator.SetBool("isdead", true);
            animator.SetTrigger("Dead");
            Destroy(gameObject);
          
        } else { 
        
        if (InRangeEnemyList.Count <= 0)
        {
            // Move to player
            if (Vector3.Distance(player.transform.position, transform.position) > 8) {
                animator.SetBool("Withinrange", true);
            rotate_towards_transform(player.transform);
            playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
            } else
            {
                    // transform.RotateAround(player.transform.position, Vector3.up, speed * 6 * Time.deltaTime);
                    
                    foreach(Enemy e in FindObjectsOfType<Enemy>())
                    {
                            if(e.player == player)
                            {
                                InRangeEnemyList.Add(e);
                            }
                    }
                }

        } else
        {
            animator.SetBool("Withinrange", false);
            //attack target

            if (target != null) {
                    if(Vector3.Distance(target.transform.position, transform.position) > 4)
                    {
                        playerpos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
                    } else
                    {
                    if(Time.time > attackdelay) { 
                    attackdelay = Time.time + 0.4f;
                    animator.SetTrigger("Attack");
                    target.TakeDamage(damage);
                    }
                    }
                    rotate_towards_transform(target.transform);
                    
                } else
            {
                InRangeEnemyList.Remove(target);
                if(InRangeEnemyList.Count > 0)
                {
                    target = InRangeEnemyList[0];
                }
            }
        }
        }
    }
}
}
