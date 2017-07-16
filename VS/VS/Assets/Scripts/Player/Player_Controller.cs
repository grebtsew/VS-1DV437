using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour {


    public float speed = 1;
    private Rigidbody rb;
    private Animator animator;
    private List<GameObject> InRangeEnemyList = new List<GameObject>();
    private bool autoattacking = false;
    private GameObject target;
    private bool abilityaim = false;

    private float lastFireTime = 0;

    private bool mouseMovement = false;
    private Vector3 mousePos;

    public void OnEnable()
    {
        
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    
    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Enemy")
        {
            
            InRangeEnemyList.Add(other.GetComponent<GameObject>());

            if (!autoattacking)
            {
                target = other.GetComponent<GameObject>();
                autoattacking = true;
                transform.LookAt(other.transform);
            }
          
           
        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if(target == other.GetComponent<GameObject>())
            {
                autoattacking = false;
            }
            InRangeEnemyList.Remove(other.GetComponent<GameObject>());
        }

    }

    void LateUpdate()
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

    void FixedUpdate()
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


        if (Input.GetMouseButton(0))
        {
            mouseMovement = true;

            Plane playerPlane2 = new Plane(Vector3.up, transform.position);
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist2 = 0.0f;

            if (playerPlane2.Raycast(ray2, out hitdist2))
            {
                mousePos = ray2.GetPoint(hitdist2);
            }
            }

        if (mouseMovement) {

           if (Vector3.Distance(transform.position, mousePos) < 1)
            {
                mouseMovement = false;
            }

            Vector3 targetPoint = mousePos;

            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleKeyMovement();
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
}
