using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{

    private float energy_per_level = 0.01f;
    private float damage_per_level = 1;

    public override void Start()
    {
        base.Start();
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
        // Mage Passive ability is extra damage and mana reg

        base.energyreg_speed -= energy_per_level;
        base.base_damage += damage_per_level;

        playerhud.updateDamage();
        playerhud.updateResist();
        playerhud.updateEnergyReg();
    }
}
