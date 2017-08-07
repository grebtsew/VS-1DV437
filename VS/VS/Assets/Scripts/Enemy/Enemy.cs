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

    public float powerupspawnchance = 10;

    public float experience = 5;

    public float attackspeed = 2;
    public float attackdelay;

    public Slider health_slider;

    public float attackRange = 3;

    private Color startcolor;
    private SkinnedMeshRenderer renderer;

    private GameObject[] powerups;
    private GameObject blooddamage;
    private GameObject blooddead;

    private AudioClip damage_sound;
    private AudioClip laugh_sound;
    private AudioClip take_damage_sound;
    private AudioClip dead_sound;

    private AudioSource audio;

    private Transform extraparent;

    public virtual void Start()
    {
        audio = GetComponent<AudioSource>();
        damage_sound = Resources.Load("Audio/enemy_damage", typeof(AudioClip)) as AudioClip;
        laugh_sound = Resources.Load("Audio/enemy_laugh", typeof(AudioClip)) as AudioClip;
        take_damage_sound = Resources.Load("Audio/enemy_take_damage", typeof(AudioClip)) as AudioClip;
        dead_sound = Resources.Load("Audio/enemydead", typeof(AudioClip)) as AudioClip;

        if (Random.Range(0, 100) < 50)
        {
            if (Random.Range(0, 100) < 50)
            {
                audio.clip = damage_sound;
                audio.Play();
            }
            else
            {
                audio.clip = laugh_sound;
                audio.Play();
            }
        }
          

        // l

        blooddamage = Resources.Load<GameObject>("Enemies/DamageEffects/BloodSprayEffect");
        blooddead = Resources.Load<GameObject>("Enemies/DamageEffects/BloodStreamEffect");
        powerups = Resources.LoadAll<GameObject>("PowerUps");
        attackdelay = Time.time;
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        mainTexture = renderer.material.mainTexture;

        animator = GetComponent<Animator>();

        animator.SetBool("Withinrange", false);

        
        rb = GetComponent<Rigidbody>();

    }

    public void SetMap(Game_Controller g)
    {
        gc = g;
    }

    public void SetTargetPlayer(Player p)
    {
        player = p;
        playerpos = player.transform.position;
        extraparent = player.parent;
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


        GameObject go = Instantiate(blooddamage, transform.position + transform.up * 0.8f, Quaternion.Euler(transform.rotation.x, Random.Range(0.0f, 360.0f), transform.rotation.z));
        go.transform.SetParent(extraparent);

        if (!isdead) {
        FloatingTextController.CreateFloatingText(damage.ToString(),  transform, Color.cyan, 10);
        }

        
            audio.clip = take_damage_sound;
            audio.Play();
        

    }

    public void Dead()
    {
        
        player.player_controller.disableTargetMarker();
        
        isdead = true;
        player.addXP(experience);
        FloatingTextController.CreateFloatingText("+ " + experience.ToString() + " xp", transform, Color.yellow, 10);
       
        if (Random.Range(0, 100) < powerupspawnchance)
        {
           GameObject gg = Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position + transform.up * 2, transform.rotation);
            PowerUp_Controller pu = gg.GetComponent<PowerUp_Controller>();
            pu.setPlayer(player);
            gg.transform.SetParent(extraparent);
           
        }

        GameObject go = Instantiate(blooddead, transform.position+ transform.up*0.8f, Quaternion.Euler(transform.rotation.x, Random.Range(0.0f, 360.0f), transform.rotation.z));
        go.transform.SetParent(extraparent);

        // make sound
        audio.clip = dead_sound;
        audio.Play();

        StartCoroutine(deathwait(1));

       
    }

    IEnumerator deathwait(int time)
    {
       
        
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        if(gc != null)
        {
            gc.addScore();
        }
       
    }

    // Each frame, rotate and move towards player, attack
    public virtual void Update()
    {
        if(player != null) { 
        if (!isdead) { 

        if (transform.position.y <= -10)
        {
            TakeDamage(100);
        }

        if (health <= 0)
        {
                health_slider.transform.position = new Vector3(0,-2,0);
                animator.SetBool("isdead", true);
                animator.SetTrigger("Dead");
                player.player_controller.Target_isDead(this);
                Dead();
                return;
        }
 

        transform.LookAt(player.transform);

        if (attack)
        {
            if(Time.time >= attackdelay && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {   
                animator.SetTrigger("Attack");
                attackdelay = Time.time;
                attackdelay += attackspeed;
                player.TakeDamage(damage);
            }
           

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
            transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);
       
        }
    }
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {
            animator.SetBool("Withinrange", true);
            inPlayerRange = true;
            attack = true;
        }

    }

    public virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            inPlayerRange = false;
            animator.SetBool("Withinrange", false);
            attack = false;
        }

    }

}
