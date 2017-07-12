using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilitybutton : MonoBehaviour {

    private Text level_label;
    private float level;
    public LevelUpPanel_Controller lup;
    public Player player;

	// Use this for initialization
	void Start () {
        level_label = GetComponentInChildren<Text>();
         
    }
	
    public void AddLevel()
    {
        if (lup.isEnabled() && player.got_ability_point()) { 
        level++;
        level_label.text = level.ToString();
        player.use_ability_point();
        lup.disable();
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
