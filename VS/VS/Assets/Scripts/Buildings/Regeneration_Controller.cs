using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration_Controller : MonoBehaviour
{

    public bool health = true;
    private bool inrange = false;

    public float regspeed = 0.001f;
    public float regvalue = 1;
    private float time;

    private Player player;
    private int regeneration_range = 5;

    public void initiate(Player p)
    {
        player = p;
    }

    void Start()
    {

        time = Time.time;
    }

    private void ShowRegenerationText()
    {
        if (!inrange)
        {
            inrange = true;
            if (health)
            {
                FloatingTextController.CreateFloatingText("+ Health Regeneration", transform, Color.grey);
            }
            else
            {
                FloatingTextController.CreateFloatingText("+ Energy Regeneration", transform, Color.grey);
            }

        }
    }

    private void regenerate()
    {
        if (Time.time >= time + regspeed)
        {
            time = Time.time;
            if (health)
            {
                player.PowerUpTaken(PowerUp.Health, regvalue);
            }
            else
            {
                player.PowerUpTaken(PowerUp.Energy, regvalue);
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= regeneration_range)
            {
                ShowRegenerationText();
                regenerate();
            }
            else
            {
                inrange = false;
            }
        }
    }
}
