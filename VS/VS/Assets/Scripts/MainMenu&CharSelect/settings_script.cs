using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class settings_script : MonoBehaviour
{
    private Canvas canvas;

    public Dropdown diff_dropdown;
    public Dropdown game_mode_dropdown;
    public Dropdown ai_amount_dropdown;
    public Dropdown ai_char_dropdown;
    public Dropdown enemies_dropdown;
    

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        loadValues();

    }

    private void loadValues()
    {
        switch (PlayerPrefsHandler.GetPersistentVar<int>(Statics.enemy_amount, 50))
        {
            case 50:
                enemies_dropdown.value = 2;
                break;
            case 100:
                enemies_dropdown.value = 1;
                break;
            case 150:
                enemies_dropdown.value = 0;
                break;
        }

        switch (PlayerPrefsHandler.GetPersistentVar<string>(Statics.ai_difficulty(1), "Easy"))
        {
            case "Easy":
                diff_dropdown.value = 0;
                break;
            case "Normal":
                diff_dropdown.value = 1;
                break;
            case "Hard":
                diff_dropdown.value = 2;
                break;
        }

        switch (PlayerPrefsHandler.GetPersistentVar<int>(Statics.ai_amount, 1))
        {
            case 1:
                ai_amount_dropdown.value = 1;
                break;
        }

        switch (PlayerPrefsHandler.GetPersistentVar<string>(Statics.game_mode, "1vs1"))
        {
            case "1vs1":
                game_mode_dropdown.value = 1;
                break;
        }

        switch (PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_character(1), Player_Character.Mage.ToString()))
        {
            case "Mage" :
                game_mode_dropdown.value =0;
                break;
        }
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
        PlayerPrefsHandler.SetPersistentVar<int>(Statics.ai_amount, ref r, Int32.Parse(ai_amount_dropdown.options[ai_amount_dropdown.value].text), false);
        PlayerPrefsHandler.SetPersistentVar<int>(Statics.enemy_amount, ref r, Int32.Parse(enemies_dropdown.options[enemies_dropdown.value].text), false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_character(1), ref s,ai_char_dropdown.options[ai_char_dropdown.value].text, false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.game_mode, ref s, game_mode_dropdown.options[game_mode_dropdown.value].text, false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.ai_difficulty(1), ref s, diff_dropdown.options[diff_dropdown.value].text, false);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.ai_difficulty(0), ref s, diff_dropdown.options[diff_dropdown.value].text, true);

        //disable
        canvas.enabled = false;
    }


}
