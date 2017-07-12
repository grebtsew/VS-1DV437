using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public Rigidbody rb;
    public Player player;
    public Vector3 playerpos;
    public Animator animator;
    public float damage = 1;
    public bool attack = false;
    public float speed = 200;
    public bool inPlayerRange = false;
    public float health = 100;
    public bool isdead = false;
    public Game_Controller gc;
    public Texture normTexture;
    private Texture mainTexture;

    private int fontsize = 10;
   

    public float experience = 5;

    public float attackspeed = 4;
    public float attackdelay;

    public Slider health_slider;

    public float attackRange = 3;

    private Color startcolor;
    private SkinnedMeshRenderer renderer;

    public virtual void Start()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        mainTexture = renderer.material.mainTexture;

        gc = Game_Controller.FindObjectOfType<Game_Controller>();
        animator = GetComponent<Animator>();
        animator.SetBool("Withinrange", false);

        
        rb = GetComponent<Rigidbody>();

        
        player = Player.FindObjectOfType<Player>();
        playerpos = player.transform.position;
    }

    void OnMouseEnter()
    {
        renderer.material.mainTexture  = normTexture ;
    }
    void OnMouseExit()
    {
        renderer.material.mainTexture = mainTexture;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        health_slider.value = health;
        if (!isdead) {
           

        FloatingTextController.CreateFloatingText(damage.ToString(),  transform);
        }
    }

    // Each frame, rotate and move towards player, attack
    public virtual void Update()
    {

        if (health <= 0)
        {
            player.disableTargetMarker();
            animator.SetTrigger("Dead");
            isdead = true;
            player.addXP(experience);
            FloatingTextController.CreateFloatingText("+ "+ experience.ToString() + " xp",  transform);
            Destroy(this.gameObject);
            gc.addScore();
           
        }
        
        


        transform.LookAt(player.transform);

        if (attack)
        {
            if(Time.time >= attackdelay)
            {
                attackdelay += attackspeed;
                player.TakeDamage(damage);
            }

            animator.SetTrigger("Attack");
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            inPlayerRange = true;
             attack = true;
        } else
        {
            inPlayerRange = false;
        }

        // Move to player
        if (!inPlayerRange)
        {
            playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            //Here, the zombie's will follow the waypoint.

            transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
            // rb.AddForce((playerpos - transform.position) * speed * Time.deltaTime);  
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {
            

            inPlayerRange = true;
            // Attack Player here
            attack = true;

        }

    }

    public virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            inPlayerRange = false;

            attack = false;
        }

    }

}
