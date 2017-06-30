using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDestroy : MonoBehaviour {

   
    public GameObject explosion;
    public GameObject playerexplosion;
    private Game_Controller controller;

    // Use this for initialization
    void Start()
    {
        GameObject tmp = GameObject.FindGameObjectWithTag("GameController");
        controller = tmp.GetComponent<Game_Controller>();
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameObject temp = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(temp, 1);

            if (other.tag == "Player")
            {
                temp = Instantiate(playerexplosion, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(temp, 1);
                controller.EndGame();
            }

            if (other.tag != "Player")
            {
                controller.AddScore(10);

            }
        }
        
    }


	
	
	// Update is called once per frame
	void Update () {
		
	}
}
