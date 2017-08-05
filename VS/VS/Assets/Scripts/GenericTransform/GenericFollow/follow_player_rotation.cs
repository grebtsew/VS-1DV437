using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_player_rotation : MonoBehaviour {

    public Player player;

    public void setPlayer(Player p)
    {
        player = p;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null)
        {
            transform.rotation = player.transform.rotation;
            transform.position = player.transform.position;
        }
		
	}
}
