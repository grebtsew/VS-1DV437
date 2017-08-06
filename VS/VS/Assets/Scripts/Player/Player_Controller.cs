using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour{

    public Player_Controll controll_mode;

    public Player player;

    private float lastFireTime = 0;
    private bool keymovement = false;
    private Vector3 previousLocation;
    private Vector3 currentLocation;

    private bool isdead = false;
    private Target_Follow_Enemy target_follower;
    private Buttons abilityinusage;
    private bool mouseMovement = false;

    public Regeneration_Controller energyzone;
    public Regeneration_Controller healthzone;

    public Canvas smallcanvas;

    //Attack
    private bool abilityaim;
    public bool usingabilty = false;
    private Rigidbody rb;
    private Animator animator;
    private List<Enemy> InRangeEnemyList = new List<Enemy>();
    private bool autoattacking = false;
    
    private float attackdelay = 1;
    private Enemy target;
    private RaycastHit hit;

    private bool hold_animation = false;

    private bool ai_move = false;
    private Transform ai_move_target;

    private Vector3 mousePos;

    public void initiate()
    {
        energyzone = player.map_reference.energyregzone;
        healthzone = player.map_reference.healthregzone;
    }

    // Use this for initialization
    public virtual void Start()
    {
        
        attackdelay = Time.time;
        
        target_follower = Resources.Load("Followers/TargetPicker", typeof(Target_Follow_Enemy)) as Target_Follow_Enemy;
        target_follower = Instantiate(target_follower);
        target_follower.transform.SetParent(player.parent);

        if(controll_mode == Player_Controll.Player)
        {
            smallcanvas.enabled = false;
        } 

        if(controll_mode == Player_Controll.Ai)
        {
            smallcanvas.enabled = true;
            
        }

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

       
    }

    public void disableTargetMarker()
    {
        target_follower.resetTarget();
    }

    public void ability_animation(Buttons ability, bool animation_start)
    {
        abilityinusage = ability;
        // remove target
        target = null;

        if (!animation_start)
        {
            hold_animation = true;
        }

        switch (ability)
        {
            case Buttons.ability1:
                ability_animation_1();
                break;
            case Buttons.ability2:
                ability_animation_2();
                break;
            case Buttons.ability3:
                ability_animation_3();
                break;
            case Buttons.ability4:
                ability_animation_4();
                break;
        }
    }

    public void ability_animation_1()
    {
        if (usingabilty)
        {
            usingabilty = false;
        }
        else
        {

        }
    }
    public void ability_animation_2()
    {
        /*
        if (usingabilty && !hold_animation)
        {
            usingabilty = false;
              abilityaim = false;
        }
        else
        {
            abilityaim = true;
            usingabilty = true;
        }
        */
    }
    public void ability_animation_3()
    {
        if (usingabilty)
        {
            usingabilty = false;
        }
        else
        {

        }
    }
    public void ability_animation_4()
    {
        if (usingabilty)
        {
            usingabilty = false;
        }
        else
        {

        }
    }

    public void ai_unlock_ability()
    {
        // unsmart
        int upgrade_skill;
        if(player.level < 6)
        {
            upgrade_skill = UnityEngine.Random.Range(1,5);
        } else
        {
            upgrade_skill = UnityEngine.Random.Range(1,6);
        }

        ai_unability(upgrade_skill);
    }
    private void ai_unability(int upgrade_skill)
    {
        switch (upgrade_skill)
        {
            case 1:
                if (player.a1_level < 6)
                {
                    player.a1_level++;
                }
                else
                {
                    ai_unability(2);
                }
                break;
            case 2:
                if (player.a2_level < 6)
                {
                    player.a2_level++;
                }
                else
                {
                    ai_unability(3);
                }

                break;
            case 3:
                if (player.a3_level < 6)
                {
                    player.a3_level++;
                }
                else
                {
                    ai_unability(4);
                }

                break;
            case 4:
                player.potion_level++;
                break;
            case 5:
                player.passive++;
                break;
            case 7:
                if (player.a4_level < 6)
                {
                    player.a4_level++;
                }
                else
                {
                    ai_unability(5);
                }
                break;
        }
    }
    private void ai_ability(int skill)
    {
        switch (skill)
        {
            case 1:
                if (player.a1_level > 0)
                {
                    player.FirstAbility();
                }
                else
                {
                    ai_ability(2);
                }
                break;
            case 2:
                if (player.a2_level > 0)
                {
                    player.SecondAbility();
                }
                else
                {
                    ai_ability(3);
                }

                break;
            case 3:
                if (player.a3_level > 0)
                {
                    player.ThirdAbility();
                }
                else
                {
                    ai_ability(4);
                }

                break;

            case 4:
                if (player.a4_level > 0)
                {
                    player.FourthAbility();
                }
                else
                {
                    ai_ability(5);
                }
                break;
           
        }
    }
    private void ai_use_ability()
    {
        if(player.level > 0) {
            int skill = 0;
            if (player.energy > 50)
        {
                if (player.level < 6)
            {
                skill = UnityEngine.Random.Range(1, 3);
            }
            else
            {
                skill = UnityEngine.Random.Range(1, 4);
            }
        }

            ai_ability(skill);
        }
    }
    private void ai_set_move_target(Transform t)
    {
        ai_move_target = t;
        ai_move = true;
    }
    private void ai_move_to()
    {
       
            if (ai_move)
        {
            if(ai_move_target != null) {
          
            rotate_y_towards_transform(ai_move_target);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ai_move_target.position.x, 0, ai_move_target.position.z), player.speed * Time.deltaTime);

            target = null;

            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
            } else
            {
                ai_move = false;
            }
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }



        // at target
        if (ai_move_target != null)
        {
            if (Vector3.Distance(ai_move_target.position, transform.position) < 2)
            {
                ai_move = false;
            }
        }
        else
        {
            if (player.health < 50)
            {
                ai_set_move_target(healthzone.transform);
            }
            else
            {
                if (player.energy < 40)
                {
                    ai_set_move_target(energyzone.transform);
                }
            }
        }

        // use potion
        if (player.health < 20)
        {
            player.potionClicked();
        }

            // pick up powerup
            if (getPowerUpsOnMap().Count > 0)
            {

                ai_set_move_target(getPowerUpsOnMap()[0].transform);
            }
    }

    public List<PowerUp_Controller> getPowerUpsOnMap()
    {
        List<PowerUp_Controller> list = new List<PowerUp_Controller>();
        if (FindObjectsOfType<PowerUp_Controller>().Length > 0) { 
        foreach (PowerUp_Controller pu in FindObjectsOfType<PowerUp_Controller>())
        {
               

                if (pu.player == player)
            {
                   
                    list.Add(pu);
            }
        }
        }
        return list;
    }

    public void Target_isDead(Enemy t)
    {
        if (t == null)
        {

            InRangeEnemyList.Remove(t);

            if (InRangeEnemyList.Count > 0)
            {
                target = InRangeEnemyList[0];

                updateTarget(target);

            }
            else
            {
                autoattacking = false;
            }

        }
    }

    public virtual void FixedUpdate()
    {
        if (controll_mode == Player_Controll.Player)
        {
            HandleMouseMovement();
        }
    }

    public virtual void Update()
    {
       if (controll_mode == Player_Controll.Player){
            
            HandleKeyMovement();
            updateCanvas();
        }

        if (controll_mode == Player_Controll.Ai)
        {
            ai_use_ability();
            ai_move_to();
        }

            if (target != null)
        {
            rotate_y_towards_transform(target.transform);
        }

        autoattack();

        
    }

    private void updateCanvas()
    {
        if (Input.GetKeyDown("v"))
        {
          if(smallcanvas.enabled)
            {
                smallcanvas.enabled = false;
            } else
            {
                smallcanvas.enabled = true;
            }
        }
    }

    public virtual void LateUpdate()
    {
        if (autoattacking)
        {

            if (Time.deltaTime - 1f > lastFireTime)
            {
                lastFireTime = Time.deltaTime;

                //make a projectile fly towards him
            }
        }
    }

    public void rotate_towards_transform(Transform t)
    {
        if (t != null)
        {
            if (t.position - transform.position != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t.position - transform.position), Time.fixedDeltaTime * player.speed);

            }
        }
    }

    private void autoattack()
    {
        if (autoattacking && Time.time >= attackdelay && !usingabilty)
        {
            if (target == null)
            {

                InRangeEnemyList.Remove(target);

                if (InRangeEnemyList.Count > 0)
                {
                    target = InRangeEnemyList[0];

                    updateTarget(target);

                }
                else
                {
                    autoattacking = false;
                }
            }


            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= player.attackRange)
                {

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (!target.isdead)
                        {
                            StartCoroutine(attackwait(0.5f));
                        }
                        else
                        {
                            target = null;
                        }

                    }

                }
                else
                {
                    autoattacking = false;
                }
            }
        }
    }

    private void rotate_y_towards_transform(Transform t)
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

    private void movementkeypressed()
    {
        if (target_follower != null)
        {
            target_follower.resetTarget();
        }
        keymovement = true;
        target = null;
        mouseMovement = false;
        animator.SetBool("Moving", true);
        animator.SetBool("Running", true);
        transform.position = currentLocation;
        if (transform.position - previousLocation != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - previousLocation), Time.fixedDeltaTime * player.speed);
        }
    }

    private void HandleKeyMovement()
    {
        previousLocation = currentLocation;
        currentLocation = transform.position;


        if (Input.GetKeyDown("1"))
        {
            player.FirstAbility();
        }
        if (Input.GetKeyDown("2"))
        {
            player.SecondAbility();
        }
        if (Input.GetKeyDown("3"))
        {

        }
        if (Input.GetKeyDown("4"))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // potion
            player.potionClicked();
        }

        if (!usingabilty)
        {
            if (Input.GetKey("w"))
            {
                movementkeypressed();
                transform.Translate(Vector3.forward * player.speed * Time.deltaTime, Space.World);

            }
            else
        if (Input.GetKey("s"))
            {
                movementkeypressed();
                transform.Translate(Vector3.back * player.speed * Time.deltaTime, Space.World);
            }
            else
        if (Input.GetKey("d"))
            {
                movementkeypressed();
                transform.Translate(Vector3.right * player.speed * Time.deltaTime, Space.World);
            }
            else
        if (Input.GetKey("a"))
            {
                movementkeypressed();
                transform.Translate(Vector3.left * player.speed * Time.deltaTime, Space.World);
            }
            else
        if (!mouseMovement)
            {
                keymovement = false;
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }

        }

    }

    private void HandleMouseMovement()
    {
        if (abilityaim)
        {
            
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;
         
            if (playerPlane.Raycast(ray, out hitdist))
            {
               
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, player.speed * Time.deltaTime);
            }
        } else { 

        if (!usingabilty)
        {

            if (Input.GetMouseButton(1))
            {
                mouseMovement = true;

                Plane playerPlane2 = new Plane(Vector3.up, transform.position);
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);



                float hitdist2 = 0.0f;

                if (Physics.Raycast(ray2, out hit))
                {

                    if (hit.transform.tag == "Enemy")
                    {
                        mouseMovement = false;

                        updateTarget(hit.collider.GetComponent<Enemy>());


                    }
                    else
                    {
                        if (playerPlane2.Raycast(ray2, out hitdist2))
                        {

                            mousePos = ray2.GetPoint(hitdist2);
                            if (Vector3.Distance(mousePos, transform.position) > 1)
                            {

                                target_follower.setPosition(mousePos);
                            }
                            else
                            {
                                mouseMovement = false;
                            }

                        }
                    }

                }
            }

        }

        if (mouseMovement && !keymovement)
        {

            if (Vector3.Distance(transform.position, mousePos) < 1)
            {
                mouseMovement = false;
                if (!target_follower.hasTarget())
                {
                    target_follower.resetTarget();
                }
            }

            Vector3 targetPoint = mousePos;

            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, player.speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, player.speed * Time.deltaTime);

            target = null;

            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);

        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }
        }
    }

    IEnumerator attackwait(float time)
    {
        animator.SetTrigger("Attack1Trigger");
        attackdelay = Time.time;
        attackdelay += player.attackspeed;
        yield return new WaitForSeconds(time);
        if (target != null)
        {
            target.TakeDamage(player.base_damage);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (target == other.GetComponent<GameObject>())
            {
                autoattacking = false;
            }
            InRangeEnemyList.Remove(other.GetComponent<Enemy>());
        }

    }

    private void updateTarget(Enemy target)
    {
        autoattacking = true;

        target_follower.setCurrentTarget(target);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            InRangeEnemyList.Add(other.GetComponent<Enemy>());


            if (!autoattacking)
            {
                target = other.GetComponent<Enemy>();

                updateTarget(target);

            }
        }
    }

}
