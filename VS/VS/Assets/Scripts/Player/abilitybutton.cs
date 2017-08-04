﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilitybutton : MonoBehaviour {

    public Buttons b;
    private Text level_label;
    private float level;
    public LevelUpPanel_Controller lup;
    public Player player;
    public Image active_image;
    private bool active;


    // Use this for initialization
    void Start () {
        level_label = GetComponentInChildren<Text>();

        // get player
        foreach (Player p in FindObjectsOfType<Player>())
        {
            if (p.player_controller.controll_mode == Player_Controll.Player)
            {
                player = p;
            }
        }
    }
	
    public void MakeActive()
    {
        
            Color c = active_image.color;
            c.a = 0;
            active_image.color = c;
        active = true;
    }

    private bool IsActive()
    {
        return active;
    }

    public void MakeInActive()
    {
            Color c = active_image.color;
            c.a = 0.90f;
            active_image.color = c;
        active = false;
    }


    public void AddLevel()
    {

        if (lup.isEnabled() && player.got_ability_point() && IsActive()) {
            level++;
        if (level > 0)
        {
            MakeActive();
            }
            level_label.text = level.ToString();
        player.use_ability_point(b);
            lup.toggleLevelUp();
        }
    }

    public float getAbilityLevel()
    {
        return level;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
