using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Controller : Controller
{
    public cooldown ai_cooldown_ability_1;
    public cooldown ai_cooldown_ability_2;
    public cooldown ai_cooldown_ability_3;
    public cooldown ai_cooldown_ability_4;
    public cooldown ai_cooldown_potion;

    public Regeneration_Controller energyzone;
    public Regeneration_Controller healthzone;

    private Transform ai_move_target;

    private bool ai_move = false;

    public override void initiate(Player p)
    {
        base.initiate(p);

        player = p;
   
        energyzone = player.map_reference.energyregzone;
        healthzone = player.map_reference.healthregzone;

       

    }
    public override void Start()
    {
        base.Start();
        smallcanvas.enabled = true;
    }

    public void ai_unlock_ability()
    {
        // unsmart
        int upgrade_skill;
        if (player.level < 6)
        {
            upgrade_skill = UnityEngine.Random.Range(1, 5);
        }
        else
        {
            upgrade_skill = UnityEngine.Random.Range(1, 6);
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
                    FirstAbility();
                }
                else
                {
                    ai_ability(2);
                }
                break;
            case 2:
                if (player.a2_level > 0)
                {
                    SecondAbility();
                }
                else
                {
                    ai_ability(3);
                }

                break;
            case 3:
                if (player.a3_level > 0)
                {
                    ThirdAbility();
                }
                else
                {
                    ai_ability(4);
                }

                break;

            case 4:
                if (player.a4_level > 0)
                {
                    FourthAbility();
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
        if (player.level > 0)
        {
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
            if (ai_move_target != null)
            {

                rotate_y_towards_transform(ai_move_target);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(ai_move_target.position.x, 0, ai_move_target.position.z), player.speed * Time.deltaTime);

                target = null;

                animator.SetBool("Moving", true);
                animator.SetBool("Running", true);
            }
            else
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
            potionClicked();
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
        if (FindObjectsOfType<PowerUp_Controller>().Length > 0)
        {
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

    public override void Update()
    {
        ai_use_ability();
        ai_move_to();


        if (player.got_ability_point())
        {
            ai_unlock_ability();
        }

        base.Update();
    }
    public override void potionClicked()
    {
        base.potionClicked();
        if (player.potion_level > 0 && !ai_cooldown_potion.OnCooldown())
        {
            ai_cooldown_potion.StartCooldown();
            FloatingTextController.CreateFloatingText(("+ " + (potion_base * player.potion_level).ToString() + " health"), transform, Color.green);
            player.health += potion_base * player.potion_level;
            player.small_healthslider.value = player.health;
        }
    }
}
