﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100;
    public float energy = 100;
    public float experience = 0;
    public float level = 1;
    private float level_experience = 20;
    public float resist = 0;

    public float energyreg_speed = 0.5f;
    public float healthreg_speed = 0.5f;
    private float energytime;
    private float healthtime;

    public global_game_controller global_game_controller;

    private bool gameOver = false;
    private bool doonce = false;
    public bool ability_mode = false;

    private bool isdead = false;
  
    public int level_ability_points = 0;

    public float base_damage = 10;
    public float attackRange = 5;
    public float speed = 1;
    public float attackspeed = 1;

    public int a1_level = 0;
    public int a2_level = 0;
    public int a3_level = 0;
    public int a4_level = 0;
    public int passive = 0;
    public int potion_level = 0;

    private int fontsize = 10;
    public string Name = "The Mage";
    public string Background = "A combat mage with alot of damage, might be a little squishy tought!";

    public PlayerHUDController playerhud;
    public Player_Controller player_controller;

    // Use this for initialization
    public virtual void Start()
    {

        playerhud.updateAllLabels();
        
    }

    public void PowerUpTaken(PowerUp bonus, float value)
    {
      
        switch (bonus)
        {
            case PowerUp.Energy:
                energy += value;
                playerhud.energyslider.value = energy;
                break;
            case PowerUp.Health:
                health += value;
                playerhud.healthslider.value = health;
                break;
            case PowerUp.Damage:
               playerhud.updateDamage();
                base_damage += value;
                break;
        }
    }

    public void TakeDamage(float damage)
    {
     
        health -= damage - (damage/100)*resist*4;
        playerhud.healthslider.value = health;
        if (!isdead)
        {
           FloatingTextController.CreateFloatingText(damage.ToString(),  transform);
        }
    }

    public virtual void passiveStatic()
    {

    }

    public virtual void passiveUpdate()
    {
        
    }

    public void use_ability_point(Buttons b)
    {
        switch (b)
        {
            case Buttons.ability1:
                a1_level++;
                break;
            case Buttons.ability2:
                a2_level++;
                break;
            case Buttons.ability3:
                a3_level++;
                break;
            case Buttons.ability4:
                a4_level++;
                break;
            case Buttons.potion:
                potion_level++;
                break;
            case Buttons.passive:
                passive++;
                passiveStatic();
                break;
        }

        level_ability_points--;
        playerhud.updateLevelUpPoints();
    }

    public bool got_ability_point()
    {
        return level_ability_points > 0;
    }

    public void addXP(float xp)
    {
        experience += xp;
        playerhud.experienceslider.value = (experience/level_experience)*100;

        if (playerhud.experienceslider.value >= 100)
        {
            //level up
            levelUp();
        }
    }

    private void levelUp()
    {

        level++;
        level_ability_points++;
        level_experience = level * 20f;
        experience = 0;
        playerhud.experienceslider.value = 0;

        resist++;
        base_damage++;

        playerhud.updateAllLabels();

    }

    public void potionClicked()
    {
        if (potion_level > 0 && !playerhud.potioncooldown.OnCooldown())
        {
            playerhud.potioncooldown.StartCooldown();
            FloatingTextController.CreateFloatingText(("+ " + (20 * potion_level).ToString() + " health"), transform);
            health += 20 * potion_level;
            playerhud.healthslider.value = health;
        }
    }

    private void checkOutOfBounds()
    {
        if (transform.position.y <= -10)
        {
            TakeDamage(100);
        }
    }

    private void checkIsDead()
    {
        if (health <= 0)
        {
            isdead = true;
            // game over
            gameOver = true;

            global_game_controller.GameOver();
            //Debug.Break();
        }
    }

    private void checkToMuchResources()
    {
        if (health > 100)
        {
            health = 100;
        }

        if (energy > 100)
        {
            energy = 100;
        }
    }

    private void energyRegeneration()
    {
        if (energy < 100)
        {
            if (Time.time >= energytime)
        {
            energytime = Time.time + energyreg_speed;
       

            energy++;
                playerhud.energyslider.value = energy;
        }
        }
    }

    private void healthRegeneration()
    {
        if (health < 100)
        {
            if (Time.time >= healthtime)
            {
                healthtime = Time.time + healthreg_speed;

                health++;
                playerhud.healthslider.value = health;
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        passiveUpdate();
        checkOutOfBounds();
        checkIsDead();
        checkToMuchResources();
        healthRegeneration();
        energyRegeneration();
    }

    public bool gotEnoughtEnergy(float cost)
    {
        return energy >= cost; 
    }

    public void useEnergy(float useamount)
    {
        if(energy >= useamount)
        {
            energy -= useamount;
            playerhud.energyslider.value = energy;
        }
    }

    public virtual void FirstAbility()
    {
    }

    public virtual void SecondAbility()
    {
    }

    public virtual void ThirdAbility()
    {
    }

    public virtual void FourthAbility()
    {
    }

}
