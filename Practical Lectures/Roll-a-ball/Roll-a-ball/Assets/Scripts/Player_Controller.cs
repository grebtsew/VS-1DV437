using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Controller : MonoBehaviour {

    public float speed = 2;
    private int score;
    public GUIText scoreText;
    public GameObject particle;
    public GameObject movetext;
    public float jumpspeed = 300;

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText.text = "Score: " + score;
        FloatingTextController.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpspeed, 0));
        }
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveH, 0.0f, moveV);

        Rigidbody body = GetComponent<Rigidbody>();

        body.AddForce(movement*speed*Time.fixedDeltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "pickup")
        {
            other.gameObject.SetActive(false);
            score++;
            scoreText.text = "Score: " + score;

            FloatingTextController.CreateFloatingText("1 point", transform);
            
            if(score == 8)
            {
                Instantiate(particle);
            }
        }
    }
}
