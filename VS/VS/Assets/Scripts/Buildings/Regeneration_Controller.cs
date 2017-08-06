using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration_Controller : MonoBehaviour {

    public bool health = true;
    public float regspeed = 0.001f;
    public float regvalue = 1;

    private Player player;
    private float time;
    private bool doonce = false;
    private int textSize = 8;

    public void initiate(Player p)
    {
        player = p;
    }

    // Use this for initialization
    void Start () {
        
        time = Time.time;
	}

  

        // Update is called once per frame
        void Update () {
        if(player != null) { 
        if (Vector3.Distance(player.transform.position, transform.position) <= 5)
        {
            if (!doonce)
            {
                doonce = true;
                if (health)
                {
                    FloatingTextController.CreateFloatingText("+ Health Regeneration", transform, Color.grey, textSize);
                } else
                {
                    FloatingTextController.CreateFloatingText("+ Energy Regeneration", transform, Color.grey, textSize);
                }
               
            }

            if(Time.time >= time + regspeed)
            {
                time = Time.time;
                if (health)
                {
                    player.PowerUpTaken(PowerUp.Health, regvalue);
                } else
                {
                    player.PowerUpTaken(PowerUp.Energy, regvalue);
                }
            }
        } else
        {
            doonce = false;
        }
        }
    }
}
