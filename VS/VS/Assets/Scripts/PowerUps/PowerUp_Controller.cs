using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Controller : MonoBehaviour {

    public Player player;
    public PowerUp powerup;
    public float value = 10;
    public float life_time = 5;

    private Rigidbody rb;
    private float time;
    private float selfmovespeed = 100;

    private int textSize = 8;

    public void setPlayer(Player p)
    {
        player = p;
    }

	// Use this for initialization
	void Start () {
        time = Time.time;
      
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(0,100), 500, Random.Range(0, 100)));
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null) { 
		if(Time.time >= time + life_time)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 10)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * selfmovespeed * Time.smoothDeltaTime);
        }

        if(transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if(other == player.GetComponent<BoxCollider>()) { 
            player.PowerUpTaken(powerup, value);
            FloatingTextController.CreateFloatingText("+"+value + " " + powerup.ToString(), transform, Color.white);
            Destroy(gameObject);
            }
        }

    }

}
