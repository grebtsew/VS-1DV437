using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player {

	// Use this for initialization
	void Start () {
        base.Start();
	}	

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }


    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void LateUpdate()
    {
        base.LateUpdate();
    }

   

    // Update is called once per frame
    void Update()
    {
        base.Update();
        HandleKeyAbilities();

    }

    private void HandleKeyAbilities()
    {
        if (Input.GetKeyDown("1"))
        {
            FirstAbility();
        }
        if (Input.GetKeyDown("2"))
        {
            SecondAbility();
        }
        if (Input.GetKeyDown("3"))
        {
         //   Debug.Log("Ability 3");
        }
        if (Input.GetKeyDown("4"))
        {
          //  Debug.Log("Ability 4");
        }
    }


    public override void  FirstAbility()
    {
        if(a1_level > 0) { 
        if (ability_mode == false )
        {
                
                Instantiate(Resources.Load("Abilities/Mage/MageFirstAbilityAim"));
            
            ability_mode = true;
        }
        }
    }

    public override void SecondAbility()
    {
        if (a2_level > 0)
        {
            if (ability_mode == false )
            {
               
                Instantiate(Resources.Load("Abilities/Mage/MageSecondAbilityAim"));
                ability_mode = true;
            }
        }

    }

    public override void ThirdAbility()
    {
        if (a3_level > 0)
        {
            cooldownslider_3.StartCooldown();

        }
    }

    public override void FourthAbility()
    {
        if (a4_level > 0)
        {
            cooldownslider_4.StartCooldown();

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

        updateDamage();
        updateResist();
        updateEnergyReg();
    }
}
