using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public Rigidbody rb;
    public Player player;
    public Vector3 playerpos;
    public Animator animator;

    public Slider health_slider;

    public float damage = 1;
    public float health = 100;
    public float speed = 10;
    public float powerupspawnchance = 10;
    public float experience = 5;
    public float attackspeed = 2;
    public float attackdelay;
    public float attackRange = 3;

    public bool attack = false;
    public bool inPlayerRange = false;
    public bool isdead = false;

    public Game_Controller game_controller;

    // for hover
    private Texture normTexture;
    private Texture mainTexture;
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
        rb = GetComponent<Rigidbody>();

        initiateSound();
        initiateEffects();

        // attackspeed
        attackdelay = Time.time;

        // on hover init
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        mainTexture = renderer.material.mainTexture;

        animator = GetComponent<Animator>();
        animator.SetBool("Withinrange", false);
    }

    private void initiateEffects()
    {
        powerups = Resources.LoadAll<GameObject>("PowerUps");
        blooddamage = Resources.Load("Enemies/DamageEffects/BloodSprayEffect", typeof(GameObject)) as GameObject;
        blooddead = Resources.Load("Enemies/DamageEffects/BloodStreamEffect", typeof(GameObject)) as GameObject;

    }

    private void initiateSound()
    {
        // load 
        audio = GetComponent<AudioSource>();
        damage_sound = Resources.Load("Audio/enemy_damage", typeof(AudioClip)) as AudioClip;
        laugh_sound = Resources.Load("Audio/enemy_laugh", typeof(AudioClip)) as AudioClip;
        take_damage_sound = Resources.Load("Audio/enemy_take_damage", typeof(AudioClip)) as AudioClip;
        dead_sound = Resources.Load("Audio/enemydead", typeof(AudioClip)) as AudioClip;

        // 50% play sound on creation
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
    }

    public void SetMap(Game_Controller game)
    {
        game_controller = game;
    }

    public void SetTargetPlayer(Player p)
    {
        player = p;
        playerpos = player.transform.position;
        extraparent = player.parent;
        setDifficulty();
    }

    private void setDifficulty()
    {
        switch (player.player_controller.difficulty)
        {
            case Difficulty.Easy:

                break;
            case Difficulty.Normal:
                damage = 2;
                break;
            case Difficulty.Hard:
                damage = 5;
                break;
        }
    }

    void OnMouseEnter()
    {
        renderer.material.mainTexture = normTexture;
    }

    void OnMouseExit()
    {
        renderer.material.mainTexture = mainTexture;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health_slider.value = health;

        //Create small blood animation
        GameObject go = Instantiate(blooddamage, transform.position + transform.up * 0.8f, Quaternion.Euler(transform.rotation.x, Random.Range(0.0f, 360.0f), transform.rotation.z));
        go.transform.SetParent(extraparent);

        //Show damage text
        if (!isdead)
        {
            FloatingTextController.CreateFloatingText(damage.ToString(), transform, Color.cyan);
        }

        // start sound
        audio.clip = take_damage_sound;
        audio.Play();
    }

    public void Dead()
    {
        isdead = true;

        // remove marker
        player.player_controller.disableTargetMarker();

        // add xp
        player.addXP(experience);

        // show xp text
        FloatingTextController.CreateFloatingText("+ " + experience.ToString() + " xp", transform, Color.yellow);

        // spawn powerup
        if (Random.Range(0, 100) < powerupspawnchance)
        {
            GameObject gg = Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position + transform.up * 2, transform.rotation);
            PowerUp_Controller pu = gg.GetComponent<PowerUp_Controller>();
            pu.setPlayer(player);
            gg.transform.SetParent(extraparent);

        }

        // spawn larger blood animation
        GameObject go = Instantiate(blooddead, transform.position + transform.up * 0.8f, Quaternion.Euler(transform.rotation.x, Random.Range(0.0f, 360.0f), transform.rotation.z));
        go.transform.SetParent(extraparent);

        // start sound
        audio.clip = dead_sound;
        audio.Play();

        // death animation wait
        StartCoroutine(deathwait(1));


    }

    IEnumerator deathwait(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        if (game_controller != null)
        {
            game_controller.addScore();
        }

    }

    private void fallenOutOfBounds()
    {
        if (transform.position.y <= -10)
        {
            TakeDamage(100);
        }
    }

    public virtual void Update()
    {
        if (player != null)
        {

            if (!isdead)
            {

                fallenOutOfBounds();

                if (health <= 0)
                {
                    // remove healthslider
                    health_slider.transform.position = new Vector3(0, -2, 0);


                    // start animation
                    animator.SetBool("isdead", true);
                    animator.SetTrigger("Dead");

                    player.player_controller.Target_isDead(this);
                    Dead();
                    return;
                }

                transform.LookAt(player.transform);

                if (attack)
                {
                    if (Time.time >= attackdelay && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        animator.SetTrigger("Attack");
                        attackdelay = Time.time;
                        attackdelay += attackspeed;
                        player.TakeDamage(damage);
                    }
                }

                // Move to player
                if (!isPlayerInRange())
                {
                    playerpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, playerpos, speed * Time.deltaTime);

                }
            }
        }
    }

    private bool isPlayerInRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            attack = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {
            animator.SetBool("Withinrange", true);

            attack = true;
        }

    }

    public virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {

            animator.SetBool("Withinrange", false);
            attack = false;
        }

    }

}
