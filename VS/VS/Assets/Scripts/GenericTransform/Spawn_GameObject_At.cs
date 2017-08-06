using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_GameObject_At : MonoBehaviour {

    public string prefab = "Abilities/Mage/MageFirstAbility";
    public Player player;
    public float energycost = 30;
    public Buttons ability = Buttons.ability1;
    private cooldown_slider cs;
    private string keypress;
   

    public void Spawn()
    {
        GameObject temp = Instantiate(Resources.Load(prefab, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        temp.transform.SetParent(player.parent);
        player.ability_mode = false;
        Destroy(gameObject);

    }

    public void initiate(Player p)
    {
        player = p;
    }

    // Use this for initialization
    void Start () {
        

        // get correct slider
        foreach(cooldown_slider c in FindObjectsOfType<cooldown_slider>())
        {
           if(c.ability == ability)
            {
                cs = c;
               
            }
        }

        // set keypress
        switch (ability)
        {
            case Buttons.ability1:
                keypress = "1";
                break;
            case Buttons.ability2:
                keypress = "2";
                break;
            case Buttons.ability3:
                keypress = "3";
                break;
            case Buttons.ability4:
                keypress = "4";
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(player != null) { 
        if (Input.GetKeyDown(keypress) || Input.GetMouseButtonDown(0))
        {

            if (player.gotEnoughtEnergy(energycost) && !cs.OnCooldown())
            {
               
                player.player_controller.ability_animation(ability, true);

                cs.StartCooldown();
                player.useEnergy(energycost);
                Spawn();
            } // maybe add a mana warning!
           
        } else if (Input.GetMouseButtonDown(1) || Input.anyKeyDown)
        {
            player.ability_mode = false;
            Destroy(gameObject);
        }
        }
    }
}
