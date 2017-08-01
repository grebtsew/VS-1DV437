using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100;
    public float energy = 100;
    public float experience = 0;
    public float level = 1;
    private float level_experience = 20;
    public float resist = 0;

    private bool keymovement = false;
    private Vector3 previousLocation;
    private Vector3 currentLocation;

    public float energyreg_speed = 0.5f;
    public float healthreg_speed = 0.5f;
    private float energytime;
    private float healthtime;

    private Buttons abilityinusage;

    public bool usingabilty = false;

    private bool abilityaim;

    private float currentability = 0; //0 == null 1== 1 ....

    public global_game_controller global_game_controller;

    public Text damage_label;
    public Text resist_label;
    public Text attackspeed_label;
    public Text EnergyReg_label;
    public Text HealthReg_label;

    public cooldown_slider cooldownslider_1;
    public cooldown_slider cooldownslider_2;
    public cooldown_slider cooldownslider_3;
    public cooldown_slider cooldownslider_4;
    public cooldown_slider potioncooldown;

    private bool gameOver = false;

    public float base_damage = 10;

    private bool doonce = false;

    public bool ability_mode = false;

    public float speed = 1;
    private Rigidbody rb;
    private Animator animator;
    private List<Enemy> InRangeEnemyList = new List<Enemy>();
    private bool autoattacking = false;
    private float attackspeed = 1;
    private float attackdelay = 1;
    private Enemy target;
    private RaycastHit hit;

   // private bool abilityaim = false;
    private bool isdead = false;
    private float lastFireTime = 0;

    public float attackRange = 5;

    public Text available_levelup_label;

    public int level_ability_points = 0;

    private bool mouseMovement = false;
    private Vector3 mousePos;
    public Slider healthslider;
    public Slider experienceslider;
    public Slider energyslider;
    public Text level_label;

    public int a1_level = 0;
    public int a2_level = 0;
    public int a3_level = 0;
    public int a4_level = 0;
    public int passive = 0;
    public int potion_level = 0;

    private int fontsize = 10;

    public string Name = "The Mage";
    public string Background = "A combat mage with alot of damage, might be a little squishy tought!";


    private Target_Follow_Enemy target_follower;

    public void OnEnable()
    {

    }

    public void PowerUpTaken(PowerUp bonus, float value)
    {
      
        switch (bonus)
        {
            case PowerUp.Energy:
                energy += value;
                energyslider.value = energy;
                break;
            case PowerUp.Health:
                health += value;
                healthslider.value = health;
                break;
            case PowerUp.Damage:
                updateDamage();
                base_damage += value;
                break;
        }
    }

    public void updateResist()
    {
        if(resist_label != null) { 
       resist_label.text = resist.ToString();
        }
    }

    public void updateDamage()
    {
        if(damage_label != null) { 
        damage_label.text = base_damage.ToString();
        }
    }
    public void updateAttackdamage()
    {
        if (damage_label != null)
        {
            attackspeed_label.text = attackspeed.ToString();
        }
    }
    public void updateHealthReg()
    {
        if (damage_label != null)
        {
            HealthReg_label.text = healthreg_speed.ToString();
        }
    }
    public void updateEnergyReg()
    {
        if (damage_label != null)
        {
            EnergyReg_label.text = energyreg_speed.ToString();
        }
    }

    public void TakeDamage(float damage)
    {
     
        health -= damage - (damage/100)*resist*4;
        healthslider.value = health;
        if (!isdead)
        {
           FloatingTextController.CreateFloatingText(damage.ToString(),  transform);
        }
    }

    public virtual void passiveStatic()
    {

    }

    public virtual void passiveUpdate()
    {
        
    }

    public void ability_animation(Buttons ability)
    {
        abilityinusage = ability;
        // remove target
        target = null;

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
        } else
        {
           
        }
    }
    public void ability_animation_2()
    {
        if (usingabilty)
        {
            usingabilty = false;
            abilityaim = false;
        }
        else
        {
            abilityaim = true;
            usingabilty = true;
        }
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

    public void use_ability_point(Buttons b)
    {
        switch (b)
        {
            case Buttons.ability1:
                a1_level++;
                break;
            case Buttons.ability2:
                a2_level++;
                break;
            case Buttons.ability3:
                a3_level++;
                break;
            case Buttons.ability4:
                a4_level++;
                break;
            case Buttons.potion:
                potion_level++;
                break;
            case Buttons.passive:
                passive++;
                passiveStatic();
                break;
        }

        level_ability_points--;
        available_levelup_label.text = level_ability_points.ToString();
    }

    public bool got_ability_point()
    {
        return level_ability_points > 0;
    }

    public void addXP(float xp)
    {
        experience += xp;
        experienceslider.value = (experience/level_experience)*100;

        if (experienceslider.value >= 100)
        {
            //level up
            levelUp();
        }
    }

    private void levelUp()
    {

        level++;
        level_ability_points++;
        available_levelup_label.text = level_ability_points.ToString();
        level_experience = level * 20f;
        experience = 0;
        experienceslider.value = 0;
        level_label.text = level.ToString();

        resist++;
        base_damage++;
        updateResist();
        updateDamage();

    }

    public void disableTargetMarker()
    {
        target_follower.resetTarget();
    }

    // Use this for initialization
    public virtual void Start()
    {

        updateDamage();
        updateResist();
        updateAttackdamage();
        updateEnergyReg();
        updateHealthReg();

        attackdelay = Time.time;
        target_follower = Resources.Load("Followers/TargetPicker", typeof(Target_Follow_Enemy)) as Target_Follow_Enemy;
        target_follower = Instantiate(target_follower);
        

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
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

    public void potionClicked()
    {
        if (potion_level > 0 && !potioncooldown.OnCooldown())
        {
            potioncooldown.StartCooldown();
            FloatingTextController.CreateFloatingText(("+ " + (20 * potion_level).ToString() + " health"), transform);
            health += 20 * potion_level;
            healthslider.value = health;
        }
    }

    private void updateTarget( Enemy target)
    {
        autoattacking = true;
       
        target_follower.setCurrentTarget(target);
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
  
    public virtual void FixedUpdate()
    {
        HandleMouseMovement();

    }

    private void checkOutOfBounds()
    {
        if (transform.position.y <= -10)
        {
            TakeDamage(100);
        }
    }

    private void checkIsDead()
    {
        if (health <= 0)
        {
            isdead = true;
            // game over
            gameOver = true;

            global_game_controller.GameOver();
            //Debug.Break();
        }
    }

    private void checkToMuchResources()
    {
        if (health > 100)
        {
            health = 100;
        }

        if (energy > 100)
        {
            energy = 100;
        }
    }

    private void energyRegeneration()
    {
        if (energy < 100)
        {
            if (Time.time >= energytime)
        {
            energytime = Time.time + energyreg_speed;
       

            energy++;
                energyslider.value = energy;
        }
        }
    }

    private void healthRegeneration()
    {
        if (health < 100)
        {
            if (Time.time >= healthtime)
            {
                healthtime = Time.time + healthreg_speed;

                health++;
                healthslider.value = health;
            }
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
                if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                {

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (!target.isdead) { 
                        StartCoroutine(attackwait(0.5f));
                        }else
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

    IEnumerator attackwait(float time)
    {   
        animator.SetTrigger("Attack1Trigger");
        attackdelay = Time.time;
        attackdelay += attackspeed;
        yield return new WaitForSeconds(time);
        if(target != null) { 
        target.TakeDamage(base_damage);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {

        passiveUpdate();

        HandleKeyMovement();

        checkOutOfBounds();

        checkIsDead();

        checkToMuchResources();

        healthRegeneration();
        energyRegeneration();

        if(target != null) { 
        rotate_towards_transform(target.transform);
        }

        autoattack();

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
        if(transform.position - previousLocation != new Vector3(0,0,0)) { 
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - previousLocation), Time.fixedDeltaTime * speed);
        }
    }

    private void HandleKeyMovement()
    {
        previousLocation = currentLocation;
        currentLocation = transform.position;


        if (Input.GetKeyDown("1")){
            FirstAbility();
        }
        if (Input.GetKeyDown("2"))
        {
            SecondAbility();
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
            potionClicked();
        }

        if (!usingabilty) { 
            if (Input.GetKey("w"))
        {
            movementkeypressed();
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        }
        else
        if (Input.GetKey("s"))
        {
            movementkeypressed();
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        else
        if (Input.GetKey("d"))
        {
            movementkeypressed();
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else
        if (Input.GetKey("a"))
        {
            movementkeypressed();
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
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
            // Generate a plane that intersects the transform's position with an upwards normal.
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            // Generate a ray from the cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Determine the point where the cursor ray intersects the plane.
            // This will be the point that the object must look towards to be looking at the mouse.
            // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
            //   then find the point along that ray that meets that distance.  This will be the point
            //   to look at.
            float hitdist = 0.0f;
            // If the ray is parallel to the plane, Raycast will return false.
            if (playerPlane.Raycast(ray, out hitdist))
            {
                // Get the point along the ray that hits the calculated distance.
                Vector3 targetPoint = ray.GetPoint(hitdist);

                // Determine the target rotation.  This is the rotation if the transform looks at the target point.
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }
        }

        if (!usingabilty) { 

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
                    
                 
                } else
                {
                    if (playerPlane2.Raycast(ray2, out hitdist2))
                    {

                        mousePos = ray2.GetPoint(hitdist2);
                        if (Vector3.Distance(mousePos, transform.position) > 1)
                        {
                            
                            target_follower.setPosition(mousePos);
                        } else
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

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);

        } else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }
    }

    public bool gotEnoughtEnergy(float cost)
    {
        return energy >= cost; 
    }

    public void useEnergy(float useamount)
    {
        if(energy >= useamount)
        {
            energy -= useamount;
            energyslider.value = energy;
        }
    }

    public virtual void FirstAbility()
    {
     

    }

    public virtual void SecondAbility()
    {

       

    }

    public virtual void ThirdAbility()
    {

        

    }

    public virtual void FourthAbility()
    {

       

    }

}
