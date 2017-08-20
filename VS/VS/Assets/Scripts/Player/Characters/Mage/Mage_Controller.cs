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

    private Vector3 GetSpawnPositionWithinBounds(Transform t)
    {
        Vector3 map_center = player.map_reference.transform.position;
        Vector3 up_left = new Vector3(map_center.x - Statics.map_x / 2, map_center.y, map_center.z - Statics.map_z / 2);
        Vector3 down_right = new Vector3(map_center.x + Statics.map_x / 2, map_center.y, map_center.z + Statics.map_z / 2);

        float spawn_distance = 4;

        Vector3 result = t.position + Vector3.forward * spawn_distance;
        if (result.z < up_left.z)
        {

            return result;
        }
        result = transform.position + Vector3.back * spawn_distance;
        if (result.z > down_right.z)
        {

            return result;
        }

        result = transform.position + Vector3.left * spawn_distance;
        if (result.x > up_left.x)
        {
            return result;
        }

        result = transform.position + Vector3.right * spawn_distance;
        if (result.x < down_right.x)
        {
            return result;
        }

        return Vector3.zero;
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
                go.transform.position = GetSpawnPositionWithinBounds(transform);
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
