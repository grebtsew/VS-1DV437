using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public float lifetime = 10;
    public bool Notify_player_on_Destroy = false;
    public Buttons ability;
    private float time;

	// Use this for initialization
	void Start () {
        time = Time.time+lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if(time <= Time.time)
        {
            if (Notify_player_on_Destroy)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                Player player = go.GetComponent<Player>();
                player.ability_animation(ability);
            }
            Destroy(gameObject);
        }
	}
}
