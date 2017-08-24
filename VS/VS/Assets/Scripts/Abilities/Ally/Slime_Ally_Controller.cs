using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Ally_Controller : Ally_Controller{



    public override void setLevel()
    {
        base.setLevel();

        damage += player.a3_level * 20;
        lifetime += player.a3_level * 5;
        if (player.a3_level > 2 && player.a3_level < 4)
        {
            transform.localScale += new Vector3(50, 50, 50);
        }
        else
        if (player.a3_level >= 4)
        {
            transform.localScale += new Vector3(100, 100, 100);
        }
    }

    public override void deadAnimation()
    {
        base.deadAnimation();
        animator.SetBool("isdead", true);
        animator.SetTrigger("Dead");
    }

    public override void walkStartAnimation()
    {
        base.walkStartAnimation();
        animator.SetBool("Withinrange", true);
    }

    public override void walkStopAnimation()
    {
        base.walkStopAnimation();
        animator.SetBool("Withinrange", false);
    }

    public override void attackAnimation()
    {
        base.attackAnimation();
        animator.SetTrigger("Attack");
    }
   
}
