using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controller : Controller
{
    private Vector3 previousLocation;
    private Vector3 currentLocation;
    public Vector3 mousePos;

    private bool mouseMovement = false;
    private bool keymovement = false;

    public float hitdist;
    public Plane playerPlane;
    public Ray ray;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        smallcanvas.enabled = false;
    }

    public virtual void FixedUpdate()
    {
        HandleMouseMovement();
    }

    public override void Update()
    {
        HandleKeyMovement();
        updateCanvas();
        HandleKeyAbilities();
        base.Update();
    }
 

    private void updateCanvas()
    {
        if (Input.GetKeyDown("v"))
        {
            if (smallcanvas.enabled)
            {
                smallcanvas.enabled = false;
            }
            else
            {
                smallcanvas.enabled = true;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // potion
            potionClicked();
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
    public virtual void HandleKeyAbilities()
    {
        if (Input.GetKeyDown("1"))
        {
            FirstAbility();
        }
        if (Input.GetKeyDown("2"))
        {
            SecondAbility();
        }
        if (Input.GetKeyDown("3"))
        {
            ThirdAbility();
        }
        if (Input.GetKeyDown("4"))
        {
            FourthAbility();
        }
    }
    private void HandleMouseMovement()
    {
        if (!usingabilty)
        {

            if (Input.GetMouseButton(1))
            {
                mouseMovement = true;

                 playerPlane = new Plane(Vector3.up, transform.position);
                ray  = Camera.main.ScreenPointToRay(Input.mousePosition);



                hitdist = 0.0f;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.tag == "Enemy")
                    {
                        mouseMovement = false;

                        updateTarget(hit.transform.GetComponent<Enemy>());


                    }
                    else
                    {
                        if (playerPlane.Raycast(ray, out hitdist))
                        {

                            mousePos = ray.GetPoint(hitdist);
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

    public override void potionClicked()
    {
        base.potionClicked();
        if (player.potion_level > 0 && !player.playerhud.potioncooldown.OnCooldown())
        {
            player.playerhud.potioncooldown.StartCooldown();
            FloatingTextController.CreateFloatingText(("+ " + (potion_base * player.potion_level).ToString() + " health"), transform, Color.green);
            player.health += potion_base * player.potion_level;
            player.playerhud.healthslider.value = player.health;
            player.small_healthslider.value = player.health;
        }
    }

    public override void use_ability(Buttons b)
    {
        base.use_ability(b);

        switch (b)
        {
            case Buttons.ability1:
                FirstAbility();
                break;
            case Buttons.ability2:
                SecondAbility();
                break;
            case Buttons.ability3:
                ThirdAbility();
                break;
            case Buttons.ability4:
                FourthAbility();
                break;
        }
    }

    public override void FirstAbility()
    {
        base.FirstAbility();
    }
    public override void SecondAbility()
    {
        base.SecondAbility();
    }
    public override void ThirdAbility()
    {
        base.ThirdAbility();
    }
    public override void FourthAbility()
    {
        base.FourthAbility();
    }

}
