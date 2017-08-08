using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class settings_script : MonoBehaviour
{
    private Canvas canvas;
    public Text enemies;
    public Text ai_char;
    public Text ai_amount;
    public Text game_mode;
    public Text diff;

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        int i = 50;
        string s = "Easy";
        enemies.text = PlayerPrefsHandler.GetPersistentVar<int>(Statics.enemy_amount, i).ToString();
        diff.text = PlayerPrefsHandler.GetPersistentVar<string>(Statics.ai_difficulty(1), s);
    }

    public void show()
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;

        }
        else
        {
            canvas.enabled = true;
        }

    }

    public void done()
    {
        int r = 0;
        string s = "";
        // save 
        PlayerPrefsHandler.SetPersistentVar<int>(Statics.ai_amount, ref r, Int32.Parse(ai_amount.text), false);
        PlayerPrefsHandler.SetPersistentVar<int>(Statics.enemy_amount, ref r, Int32.Parse(enemies.text), false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_character(1), ref s, ai_char.text, false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.game_mode, ref s, game_mode.text, false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.ai_difficulty(1), ref s, diff.text, false);

        //disable
        canvas.enabled = false;
    }


}
