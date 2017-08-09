using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cooldown
{


    public float Cooldown;

    private bool onCooldown = false;
    private float temp = 100;

    private float time_delay = 0.01f;

    public cooldown(float Cooldown)
    {
        this.Cooldown = Cooldown;
    }

    public void StartCooldown()
    {
        AnimateSliderOverTime(Cooldown);
    }

    public bool OnCooldown()
    {
        return onCooldown;
    }

    IEnumerator AnimateSliderOverTime(float time)
    {
        onCooldown = true;
        temp = 100;
        while (temp > 0)
        {
            temp -= 1 / time;
            yield return new WaitForSeconds(time_delay);
        }
        onCooldown = false;


    }


}
