using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman_Ally_Controller : Ally_Controller
{

   

    public override void Start()
    {
        base.Start();
    }

    public override void setLevel()
    {
        // speed up ally
        damage += player.a4_level * 10;
        animator.speed = (float) 1.5 * player.a4_level;
        speed += player.a4_level*2;
        attackspeed -= 0.04f * player.a4_level;
    }

    public override void deadAnimation()
    {
        base.deadAnimation();
       
    }

    public override void walkStartAnimation()
    {
        base.walkStartAnimation();
        animator.SetBool("Moving", true);
        animator.SetBool("Running", true);

    }

    public override void walkStopAnimation()
    {
        base.walkStopAnimation();
        animator.SetBool("Moving", false);
        animator.SetBool("Running", false);


    }

    public override void attackAnimation()
    {
        base.attackAnimation();
        animator.SetTrigger("Attack1Trigger");
    }
}
