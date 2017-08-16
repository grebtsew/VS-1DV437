using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : Player
{
    private float attack_animation_speed_per_level = 1f;
    private float wait_attack_time_per_level = 0.2f;

    private float attackspeed_per_level = 0.2f;
    private float damage_per_level = 1;

    public override void Start()
    {
        base.Start();
        speed += 10;
        base_damage += 50;
        base.controller.wait_attack_time = 0.25f;

    }

   

    public override void Update()
    {
        base.Update();

    }

    public override void passiveUpdate()
    {
        base.passiveUpdate();


    }
    public override void passiveStatic()
    {
        base.passiveStatic();
        // Swordman Passive ability is extra damage and attackspeed


        base.base_damage += damage_per_level;

        base.controller.animator.speed += attack_animation_speed_per_level;
        base.controller.wait_attack_time -= wait_attack_time_per_level;
        base.attackspeed -= attackspeed_per_level;

        playerhud.updateAllLabels();

    }
}
