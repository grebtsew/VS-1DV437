using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_GameObject_At : MonoBehaviour
{

    public string prefab = "Abilities/Mage/MageFirstAbility";
    public Player player;
    public float energycost = 30;
    public Buttons ability;
    private cooldown_slider cooldown_slider;
    private string keypress;

    private GameObject temp;

    public void Spawn()
    {
        switch (ability)
        {
            case Buttons.ability1:
                temp = Instantiate(Mage_statics.firstability, transform.position, transform.rotation) as GameObject;
                break;
            case Buttons.ability2:
                temp = Instantiate(Mage_statics.secondability, transform.position, transform.rotation) as GameObject;
                break;
            case Buttons.ability3:
                break;
            case Buttons.ability4:
                break;
        }

        temp.transform.SetParent(player.parent);

        // initiate ability
        if (temp.GetComponent<Rotate_Follow_Mouse>())
        {
            temp.GetComponent<Rotate_Follow_Mouse>().setPlayer(player);
        }
        if (temp.GetComponent<Deal_Continous_Damage>())
        {
            temp.GetComponent<Deal_Continous_Damage>().setPlayer(player);
        }

        player.ability_mode = false;
        Destroy(gameObject);
    }

    public void initiate(Player p)
    {
        player = p;
    }

    // Use this for initialization
    void Start()
    {


        // get correct slider
        foreach (cooldown_slider c in FindObjectsOfType<cooldown_slider>())
        {
            if (c.ability == ability)
            {
                cooldown_slider = c;

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
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(keypress) || Input.GetMouseButtonDown(0))
            {

                if (player.gotEnoughtEnergy(energycost) && !cooldown_slider.OnCooldown())
                {

                    player.controller.ability_animation(ability, true);

                    cooldown_slider.StartCooldown();
                    player.useEnergy(energycost);
                    Spawn();
                } // maybe add a mana warning!

            }
            else if (Input.GetMouseButtonDown(1) || Input.anyKeyDown)
            {
                player.ability_mode = false;
                Destroy(gameObject);
            }
        }
    }
}
