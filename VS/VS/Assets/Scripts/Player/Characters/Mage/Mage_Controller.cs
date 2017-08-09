using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Controller : Player_Controller {

	// Use this for initialization
	public override void Start () {
        base.Start();
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


    public override void FirstAbility()
    {
        base.FirstAbility();

        if (player.a1_level > 0)
        {
            if (player.ability_mode == false)
            {
                GameObject go = Instantiate(Resources.Load("Abilities/Mage/MageFirstAbilityAim", typeof(GameObject))) as GameObject;
                go.transform.SetParent(player.parent);
                go.GetComponent<Spawn_GameObject_At>().initiate(player);
                player.ability_mode = true;
            }
        }
    }

    public override void SecondAbility()
    {
        base.SecondAbility();
        if (player.a2_level > 0)
        {
            if (player.ability_mode == false)
            {
                GameObject go = Instantiate(Resources.Load("Abilities/Mage/MageSecondAbilityAim", typeof(GameObject))) as GameObject;
                go.transform.SetParent(player.parent);
                go.GetComponent<Spawn_GameObject_At>().initiate(player);
                go.GetComponent<Rotate_Follow_Mouse>().setPlayer(player);
                player.ability_mode = true;
            }
        }
    }

    public override void ThirdAbility()
    {
        base.ThirdAbility();
        if (player.a3_level > 0 && !player.playerhud.cooldownslider_3.OnCooldown() && player.gotEnoughtEnergy(Mage_statics.ability_3_energycost))
        {
            for (int i = 0; i < player.a3_level; i++)
            {
                Ally_Controller go = Instantiate(Mage_statics.thirdability) as Ally_Controller;
                go.transform.position = transform.position + Vector3.forward * 4;
                go.setPlayer(player);
                go.transform.SetParent(player.parent);
            }
            player.useEnergy(Mage_statics.ability_3_energycost);
            player.playerhud.cooldownslider_3.StartCooldown();

        }
    }

    public override void FourthAbility()
    {
        base.FourthAbility();
        if (player.a4_level > 0 && !player.playerhud.cooldownslider_4.OnCooldown() && player.gotEnoughtEnergy(Mage_statics.ability_4_energycost))
        {
            deathball go = Instantiate(Mage_statics.fourthability) as deathball;
            go.transform.position = transform.position + Vector3.forward * 4;
            go.transform.SetParent(player.parent);
            go.setPlayer(player);
            player.useEnergy(Mage_statics.ability_4_energycost);
            player.playerhud.cooldownslider_4.StartCooldown();
        }
    }

}
