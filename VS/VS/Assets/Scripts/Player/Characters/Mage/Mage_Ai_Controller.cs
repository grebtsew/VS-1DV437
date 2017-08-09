using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Ai_Controller : Ai_Controller {

    // Use this for initialization
   public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
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


    public override void FirstAbility()
    {
        base.FirstAbility();

        if (player.gotEnoughtEnergy(40) && !ai_cooldown_ability_1.OnCooldown())
        {
            if (player.a1_level > 0)
            {
                // fix cooldown
                ability_animation(Buttons.ability1, true);
                player.useEnergy(Mage_statics.ability_1_energycost);
                ai_cooldown_ability_1.StartCooldown();

                GameObject temp = Instantiate(Mage_statics.firstability, transform.position, transform.rotation) as GameObject;
                temp.transform.SetParent(player.parent);
            }
        }

    }

    public override void SecondAbility()
    {
        base.SecondAbility();
        if (player.gotEnoughtEnergy(Mage_statics.ability_2_energycost) && !ai_cooldown_ability_2.OnCooldown())
        {
            if (player.a2_level > 0)
            {
                // fix cooldown
                ability_animation(Buttons.ability2, true);
                player.useEnergy(Mage_statics.ability_2_energycost);
                follow_player_rotation temp = Instantiate(Resources.Load("Abilities/Mage/MageSecondAbility_ai", typeof(follow_player_rotation))) as follow_player_rotation;
                temp.setPlayer(player);
                temp.transform.SetParent(player.parent);
                ai_cooldown_ability_2.StartCooldown();
            }
        }
    }

    public override void ThirdAbility()
    {
        base.ThirdAbility();

        if (player.gotEnoughtEnergy(Mage_statics.ability_3_energycost) && !ai_cooldown_ability_3.OnCooldown())
        {
            if (player.a3_level > 0)
            {
                // fix cooldown
                ability_animation(Buttons.ability3, true);
                player.useEnergy(Mage_statics.ability_3_energycost);

                ai_cooldown_ability_3.StartCooldown();
                Ally_Controller go = Instantiate(Mage_statics.thirdability) as Ally_Controller;
                go.transform.position = transform.position + Vector3.forward * 4;
                go.setPlayer(player);
                go.transform.SetParent(player.parent);
            }
        }
    }

    public override void FourthAbility()
    {
        base.FourthAbility();

        if (player.gotEnoughtEnergy(Mage_statics.ability_4_energycost) && !ai_cooldown_ability_4.OnCooldown())
        {
            if (player.a4_level > 0)
            {
                // fix cooldown
                ability_animation(Buttons.ability4, true);
                player.useEnergy(Mage_statics.ability_4_energycost);

                ai_cooldown_ability_4.StartCooldown();
                deathball go = Instantiate(Mage_statics.fourthability) as deathball;
                go.transform.position = transform.position + Vector3.forward * 4;
                go.transform.SetParent(player.parent);
                go.setPlayer(player);
            }
        }
    }

}
