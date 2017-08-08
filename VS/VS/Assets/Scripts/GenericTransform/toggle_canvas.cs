using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_canvas : MonoBehaviour
{

    private Player player;
    public float delay = 0.5f;
    private float time;
    private CanvasGroup cg;
    public Text text;
    public bool active = false;
    public bool countdown;
    private bool dead = false;
    private int timer = 10;
    private global_game_controller global_game_controller;
    private int low_health = 20;
    private int countdown_time = 10;

    public void initiate(Player p)
    {
        player = p;

    }


    void Start()
    {

        text.text = "";
        global_game_controller = FindObjectOfType<global_game_controller>();

        cg = this.GetComponent<CanvasGroup>();
        cg.alpha = 0;

        time = Time.time + delay;
    }

    public void stopCountdown()
    {
        active = false;
        countdown = false;
        timer = countdown_time;
        cg.alpha = 0;
        text.text = "";
    }

    public void startCountdown()
    {
        if (!active)
        {
            active = true;
            countdown = true;
            timer = countdown_time;
            text.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !dead)
        {
            if (active || player.health <= low_health)
            {
                if (Time.time > time)
                {

                    time = Time.time + delay;

                    if (countdown)
                    {
                        timer--;
                        text.text = timer.ToString();
                    }

                    if (cg.alpha == 0)
                    {
                        cg.alpha = 1;
                    }
                    else
                    {
                        cg.alpha = 0;
                    }
                }
            }

            if (timer == 0)
            {
                global_game_controller.GameOver(player);
                dead = true;
            }

            if (player.health > low_health && !active)
            {
                cg.alpha = 0;
            }

        }
    }
}
