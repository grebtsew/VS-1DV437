using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player {

	// Use this for initialization
	void Start () {
        base.Start();
       
    }	

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }


    public override void FirstAbility()
    {
        if (player_controller.controll_mode == Player_Controll.Player)
        {
            if (a1_level > 0)
            {
                if (ability_mode == false)
                {
                    Instantiate(Resources.Load("Abilities/Mage/MageFirstAbilityAim"));
                    ability_mode = true;
                }
            }
        }

        if (player_controller.controll_mode == Player_Controll.Ai && gotEnoughtEnergy(40))
        {
            // fix cooldown
            player_controller.ability_animation(Buttons.ability1, true);
            useEnergy(40);
            GameObject temp = Resources.Load("Abilities/Mage/MageFirstAbility", typeof(GameObject)) as GameObject;
            Instantiate(temp, transform.position, transform.rotation);
           
        }
    }

    public override void SecondAbility()
    {
        if (player_controller.controll_mode == Player_Controll.Player)
        {
            if (a2_level > 0)
            {
                if (ability_mode == false)
                {
                    Instantiate(Resources.Load("Abilities/Mage/MageSecondAbilityAim"));
                    ability_mode = true;
                }
            }
        }

        if (player_controller.controll_mode == Player_Controll.Ai && gotEnoughtEnergy(40))
        {
            // fix cooldown
            player_controller.ability_animation(Buttons.ability2, true);
            useEnergy(40);
           follow_player_rotation temp = Instantiate(Resources.Load("Abilities/Mage/MageSecondAbility_ai", typeof(follow_player_rotation))) as follow_player_rotation;
            temp.setPlayer(this);
            //Instantiate(temp, transform.position, transform.rotation);
        }
    }

    public override void ThirdAbility()
    {
        if (player_controller.controll_mode == Player_Controll.Player)
        {

            if (a3_level > 0 && !playerhud.cooldownslider_3.OnCooldown() && gotEnoughtEnergy(30))
            {
                for (int i = 0; i < a3_level; i++)
                {
                    Ally_Controller go = Instantiate(Resources.Load("Allies/Mage/Slime_Blue", typeof(Ally_Controller))) as Ally_Controller;

                    go.transform.position = transform.position + Vector3.forward * 4;
                    go.setPlayer(this);
                    go.transform.SetParent(parent);
                }
                useEnergy(30);
                playerhud.cooldownslider_3.StartCooldown();

            }
        }

        if (player_controller.controll_mode == Player_Controll.Ai && gotEnoughtEnergy(30))
            {
                // fix cooldown
                player_controller.ability_animation(Buttons.ability3, true);
                useEnergy(30);
                

            Ally_Controller go = Instantiate(Resources.Load("Allies/Mage/Slime_Blue", typeof(Ally_Controller))) as Ally_Controller;
            go.transform.position = transform.position + Vector3.forward * 4;
            go.setPlayer(this);
            go.transform.SetParent(parent);
        }
        }

    public override void FourthAbility()
    {
        if (player_controller.controll_mode == Player_Controll.Player)
        {
            if (a4_level > 0 && !playerhud.cooldownslider_4.OnCooldown() && gotEnoughtEnergy(40))
            {
                deathball go = Instantiate(Resources.Load("Abilities/Mage/MageFourthAbility", typeof(deathball))) as deathball;
                go.transform.position = transform.position + Vector3.forward * 4;
                go.transform.SetParent(parent);
                go.setPlayer(this);
                useEnergy(40);
                playerhud.cooldownslider_4.StartCooldown();
            }
        }

        if (player_controller.controll_mode == Player_Controll.Ai && gotEnoughtEnergy(40))
        {
            // fix cooldown
            player_controller.ability_animation(Buttons.ability4, true);
            useEnergy(40);


            deathball go = Instantiate(Resources.Load("Abilities/Mage/MageFourthAbility", typeof(deathball))) as deathball;
            go.transform.position = transform.position + Vector3.forward * 4;
            go.transform.SetParent(parent);
            go.setPlayer(this);
        }
    }


    public override void passiveUpdate()
    {
        base.passiveUpdate();
        

    }
    public override void passiveStatic()
    {
        base.passiveStatic();
        // Mage Passive ability is extra damage and mana reg

        base.energyreg_speed -= 0.01f;
        base.base_damage += 5;

        playerhud.updateDamage();
        playerhud.updateResist();
        playerhud.updateEnergyReg();
    }
}
