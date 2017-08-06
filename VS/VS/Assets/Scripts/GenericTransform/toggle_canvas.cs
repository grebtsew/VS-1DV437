using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_canvas : MonoBehaviour {

    private Player player;
    public bool active = false;
    public float delay = 0.5f;
    private float time;
    private CanvasGroup cg;
    public Text text;
    public bool countdown;
    private int timer = 10;
    private global_game_controller ggc;

        // Use this for initialization
        void Start () {
        // get player
        foreach (Player p in FindObjectsOfType<Player>())
        {
            if (p.player_controller.controll_mode == Player_Controll.Player)
            {
                player = p;
            }
        }

        text.text = "";
        ggc = FindObjectOfType<global_game_controller>();
        
       cg = this.GetComponent<CanvasGroup>();
       cg.alpha = 0;

        time = Time.time + delay;
    }
	
    public void stopCountdown()
    {
        active = false;
        countdown = false;
        timer = 10;
        cg.alpha = 0;
        text.text = "";
    }

    public void startCountdown()
    {
        if (!active) {
        active = true;
        countdown = true;
        timer = 10;
        text.text = "";
        }
    }

    // Update is called once per frame
    void Update () {
        if(player != null) { 
        if (active || player.health <= 20)
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
                } else
                {
                    cg.alpha = 0;
                }
        }
        }

        if(timer == 0)
        {
            ggc.GameOver();
        }

        if(player.health > 20)
        {
            cg.alpha = 0;
        }

        }
    }
}
