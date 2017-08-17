using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman_Controller : Player_Controller {

   
    private float lifesteal_amount = 2;
    private float lifesteal_delta = 2;
    private float lifesteal_time = 20;

    private float rotate_damage_time = 0.5f;
    private float rotate_delta = 1;
    private float rotate_speed = 20;
    private GameObject first_effect;
    private float throw_speed = 1000;

    // Use this for initialization
    public override void Start () {
        base.Start();

        first_effect = Instantiate(Swordsman_statics.firstability, transform) as GameObject;
        first_effect.SetActive(false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    // Update is called once per frame
    public override void Update () {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override void deal_damage(Enemy target)
    {
        base.deal_damage(target);

        if (lifesteal_delta > Time.time)
        {

            FloatingTextController.CreateFloatingText("+ " + lifesteal_amount + " health", transform, Color.green);
            player.PowerUpTaken(PowerUp.Health, lifesteal_amount);
        }

    }

    public override void HandleKeyAbilities()
    {
        if (Input.GetKey("1"))
        {
            FirstAbility();
        }
        if (Input.GetKeyUp("1"))
        {
            base.usingabilty = false;
            first_effect.SetActive(false);
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

    private void follow_mouse()
    {
        mousePos = getMousePos();

            if (Vector3.Distance(mousePos, transform.position) > 1)
            {

                transform.position = Vector3.MoveTowards(transform.position, mousePos, player.speed * Time.deltaTime);
            }

        }

    private Vector3 getMousePos()
    {
    playerPlane = new Plane(Vector3.up, transform.position);
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);


    hitdist = 0.0f;

    if (playerPlane.Raycast(ray, out hitdist))
    {

        return  ray.GetPoint(hitdist);
    }
        return Vector3.zero;
    }

    public override void FirstAbility()
    {
        base.FirstAbility();

        if (player.a1_level > 0 && player.gotEnoughtEnergy(Swordsman_statics.ability_1_energycost))
        {
            first_effect.SetActive(true);
            base.autoattacking = false;
            base.usingabilty = true;
            target = null;
            player.attackRange = player.attackRange + (player.a1_level / 4);


            // deal damage for all in range
            if(Time.time > rotate_delta)
            {
             
                player.useEnergy(Swordsman_statics.ability_1_energycost);
                rotate_delta = Time.time + rotate_damage_time ;
                foreach(Enemy e in InRangeEnemyList)
                {
                    deal_damage(e);
                }
            }

            follow_mouse();
            transform.Rotate(transform.up, rotate_speed + player.a1_level);
            
        } 
    }

    public override void SecondAbility()
    {
        base.SecondAbility();
        if (player.a2_level > 0 && !player.playerhud.cooldownslider_2.OnCooldown() && player.gotEnoughtEnergy(Swordsman_statics.ability_2_energycost))
        {
            player.playerhud.cooldownslider_2.StartCooldown();
            player.useEnergy(Swordsman_statics.ability_2_energycost);
            lifesteal_delta = player.a2_level* lifesteal_time + Time.time;
            lifesteal_amount += player.a2_level;
        }
    }

    public override void ThirdAbility()
    {
        base.ThirdAbility();
        if (player.a3_level > 0 && !player.playerhud.cooldownslider_3.OnCooldown() && player.gotEnoughtEnergy(Swordsman_statics.ability_3_energycost - player.a3_level))
        {
         
            GameObject go = Instantiate(Swordsman_statics.thirdability, player.parent) as GameObject;
            mousePos = getMousePos();
            go.transform.position = transform.position; // mousePos;
            go.GetComponent<Rigidbody>().AddForce((mousePos - go.transform.position).normalized * throw_speed);
           

            player.useEnergy(Swordsman_statics.ability_3_energycost - player.a3_level);
            player.playerhud.cooldownslider_3.StartCooldown();
        }
    }

    public override void FourthAbility()
    {
        base.FourthAbility();
        if (player.a4_level > 0 && !player.playerhud.cooldownslider_4.OnCooldown() && player.gotEnoughtEnergy(Swordsman_statics.ability_4_energycost))
        {
            for (int i = 0; i < player.a4_level; i++)
            {
                // spawn mini me, fix ai first!
                Ally_Controller go = Instantiate(Swordsman_statics.fourthability) as Ally_Controller;
                go.transform.position = transform.position + Vector3.forward * 4; // fix better spawn
                go.setPlayer(player);
                go.transform.SetParent(player.parent);

            }
            player.useEnergy(Swordsman_statics.ability_4_energycost);
            player.playerhud.cooldownslider_4.StartCooldown();
        }
    }

}
