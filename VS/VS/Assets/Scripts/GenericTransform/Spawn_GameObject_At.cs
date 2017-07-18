using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_GameObject_At : MonoBehaviour {

    public string prefab = "Abilities/Mage/MageFirstAbility";
    public Player player;
    public float energycost = 30;
    public string keypress = "1";

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
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keypress) || Input.GetMouseButtonDown(0))
        {
            if (player.gotEnoughtEnergy(energycost))
            {
                player.useEnergy(energycost);
                Spawn();
               
            }
           
        } else if (Input.GetMouseButtonDown(1) || Input.anyKeyDown)
        {
            player.ability_mode = false;
            Destroy(gameObject);
        }
    }
}
