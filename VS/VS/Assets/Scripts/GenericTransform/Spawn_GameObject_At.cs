using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_GameObject_At : MonoBehaviour {

    public string prefab = "Abilities/Mage/MageFirstAbility";
    public Player player;
    public float energycost = 30;
    public string keypress = "1";
    private cooldown_slider cs;

    public void Spawn()
    {

            GameObject temp = Resources.Load(prefab, typeof(GameObject)) as GameObject;
            Instantiate(temp, transform.position, transform.rotation);
        player.ability_mode = false;
        Destroy(gameObject);

    }

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();

        switch (keypress)
        {
            case "1":
                cs = FindObjectsOfType<cooldown_slider>()[3];
                break;

            case "2":
                cs = FindObjectsOfType<cooldown_slider>()[2];
                break;

            case "3":
                cs = FindObjectsOfType<cooldown_slider>()[1];
                break;

            case "4":
                cs = FindObjectsOfType<cooldown_slider>()[0];
                break;

            
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keypress) || Input.GetMouseButtonDown(0))
        {

            Debug.Log(cs.OnCooldown());

            if (player.gotEnoughtEnergy(energycost) && !cs.OnCooldown())
            {


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
