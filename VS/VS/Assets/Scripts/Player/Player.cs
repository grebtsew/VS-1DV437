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

    public float base_damage = 10;

    public float speed = 1;
    private Rigidbody rb;
    private Animator animator;
    private List<Enemy> InRangeEnemyList = new List<Enemy>();
    private bool autoattacking = false;
    private float attackspeed = 1;
    private float attackdelay = 1;
    private Enemy target;
    private RaycastHit hit;

    private bool abilityaim = false;
    private bool isdead = false;
    private float lastFireTime = 0;

    public Text available_levelup_label;

    public int level_ability_points = 0;

    private bool mouseMovement = false;
    private Vector3 mousePos;
    public Slider healthslider;
    public Slider experienceslider;
    public Slider energyslider;
    public Text level_label;

    private int fontsize = 10;
    

    private Target_Follow_Enemy target_follower;

    public void OnEnable()
    {

    }

    delegate void TakeDamage_delegate(float damage);
    public void TakeDamage(float damage)
    {
     
        health -= damage;
        healthslider.value = health;
        if (!isdead)
        {
           FloatingTextController.CreateFloatingText(damage.ToString(),  transform);
        }
    }

    public void use_ability_point()
    {
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

            level++;
            level_ability_points++;
            available_levelup_label.text = level_ability_points.ToString();
            level_experience = level * 20f;
            experience = 0;
            experienceslider.value = 0;
            level_label.text = level.ToString();
        }
    }

    public void disableTargetMarker()
    {
        target_follower.resetTarget();
    }

    // Use this for initialization
    public virtual void Start()
    {

       
        target_follower = Resources.Load("Prefabs/TargetPicker", typeof(Target_Follow_Enemy)) as Target_Follow_Enemy;
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

    private void updateTarget( Enemy target)
    {
        autoattacking = true;
       
        target_follower.setCurrentTarget(target);
      
        transform.LookAt(target.transform);
        //transform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);
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


            animator.SetTrigger("Attack");

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


    // Update is called once per frame
    public virtual void Update()
    {
        
        HandleKeyMovement();
       
        if(health <= 0)
        {
            isdead = true;
            // game over
            Debug.Break();
        }

        
        if(autoattacking && Time.time >= attackdelay)
        {
            if (target.isdead)
            {
                

                InRangeEnemyList.Remove(target);
                if(InRangeEnemyList.Count > 0)
                {
                    target = InRangeEnemyList[0];

                    updateTarget(target);

                   // transform.LookAt(target.transform.position);
                }
                else
                {
                    autoattacking = false;
                }

            }

            attackdelay += attackspeed;
             
            target.TakeDamage(base_damage);
            
        }
    }

    private void HandleKeyMovement()
    {

        if (Input.GetKey("w"))
        {
            mouseMovement = false;
            animator.SetBool("Run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey("s"))
        {
            mouseMovement = false;
            animator.SetBool("Run", true);
            // rb.AddForce(Vector3.back * speed * Time.deltaTime);
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            mouseMovement = false;
            animator.SetBool("Run", true);
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            mouseMovement = false;
            animator.SetBool("Run", true);
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
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


        if (Input.GetMouseButton(1))
        {
            mouseMovement = true;

            Plane playerPlane2 = new Plane(Vector3.up, transform.position);
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            float hitdist2 = 0.0f;

            

                if (Physics.Raycast(ray2, out hit))
            {

                //mousePos = hit.point;
                //target_follower.setPosition(hit.point);

                if (hit.transform.tag == "Enemy")
                {
                    mouseMovement = false;

                    updateTarget(hit.collider.GetComponent<Enemy>());
                    
                 
                } else
                {
                    if (playerPlane2.Raycast(ray2, out hitdist2))
                    {
                        mousePos = ray2.GetPoint(hitdist2);
                        target_follower.setPosition(mousePos);
                    }
                }

            } 


        }

        if (mouseMovement)
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

        }
    }

}
