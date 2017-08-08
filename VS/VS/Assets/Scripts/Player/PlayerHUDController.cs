using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public Player player;

    public Text level_label;
    public Text damage_label;
    public Text resist_label;
    public Text attackspeed_label;
    public Text EnergyReg_label;
    public Text HealthReg_label;

    public Text available_levelup_label;

    public Slider healthslider;
    public Slider experienceslider;
    public Slider energyslider;

    public cooldown_slider cooldownslider_1;
    public cooldown_slider cooldownslider_2;
    public cooldown_slider cooldownslider_3;
    public cooldown_slider cooldownslider_4;
    public cooldown_slider potioncooldown;

    public void initiate(Player p)
    {
        player = p;
    }


    public void updateAllLabels()
    {
        if (player != null)
        {
            updateLevel();
            updateResist();
            updateDamage();
            updateAttackdamage();
            updateHealthReg();
            updateEnergyReg();
            updateLevelUpPoints();
        }
    }

    public void updateLevelUpPoints()
    {
        available_levelup_label.text = player.level_ability_points.ToString();
    }

    public void updateLevel()
    {
        if (level_label != null && player != null)
        {
            level_label.text = player.level.ToString();
        }
    }
    public void updateResist()
    {
        if (resist_label != null && player != null)
        {
            resist_label.text = player.resist.ToString();
        }
    }
    public void updateDamage()
    {
        if (damage_label != null && player != null)
        {
            damage_label.text = player.base_damage.ToString();
        }
    }
    public void updateAttackdamage()
    {
        if (damage_label != null && player != null)
        {
            attackspeed_label.text = player.attackspeed.ToString();
        }
    }
    public void updateHealthReg()
    {
        if (damage_label != null && player != null)
        {
            HealthReg_label.text = player.healthreg_speed.ToString();
        }
    }
    public void updateEnergyReg()
    {
        if (damage_label != null && player != null)
        {
            EnergyReg_label.text = player.energyreg_speed.ToString();
        }
    }


}
