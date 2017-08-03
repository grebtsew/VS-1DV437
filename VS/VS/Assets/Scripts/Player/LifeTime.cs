using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTime : MonoBehaviour {

    public float lifetime = 10;
    public bool Notify_player_on_Destroy = false;
    public Buttons ability;
    private float time;
    public bool leveldependent = false;
    public bool labeltime = false;
    public Text label;
    

    // Use this for initialization
    void Start () {

        if (leveldependent)
        {
            Player player = FindObjectOfType<Player>();
            lifetime += player.level; 
        }

        time = Time.time + lifetime;
    }
	
	// Update is called once per frame
	void Update () {
		if(time <= Time.time)
        {
            if (Notify_player_on_Destroy)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                Player player = go.GetComponent<Player>();
                player.player_controller.ability_animation(ability, false);
            }
            Destroy(gameObject);
        }

        if (labeltime)
        {
            lifetime -= Time.deltaTime;
            updatelabel();
        }
	}

    private void updatelabel()
    {
        label.text = lifetime.ToString();
    }
}
