using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour {

    public Text timer_label;
    public Text score_label;
    public Text enemies_label;
    public Text opponents_label;
    private bool gameOver = false;
    private bool ready = false;

    public Player player;
    public int enemies;

    public GameObject enemyParent;

    private float time;
    public int score = 0;

    private Vector3 center;
    private Vector3 size = new Vector3(28,10,28);
    public float spawnTime;
    private float spawnDelay = 3;

    private string opponent_text;

    public Game_Controller[] gamelist;

    public void Ready()
    {
        spawnTime = Time.time;
        time = Time.fixedTime;
        ready = true;
    }

    // Use this for initialization
    void Start () {

        enemyParent = GameObject.FindGameObjectWithTag("EnemyParent");
        Instantiate(Resources.Load("Followers/MouseFollower"));

        
        gamelist = Game_Controller.FindObjectsOfType<Game_Controller>();

        spawnTime = Time.time;

        

    }

    private void spawnEnemy()
    {
        Vector3 randomPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(1, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Enemy instance = Instantiate(Resources.Load("Enemies/Bat_Green", typeof(Enemy))) as Enemy;
        instance.transform.SetParent(enemyParent.transform);
        instance.transform.position = randomPos;



    }

	
	// Update is called once per frame
	void Update () {

        if (!gameOver && ready) {
        
        updateGameHUD();

        if (spawnTime <= Time.time)
        {
            spawnTime += spawnDelay;
            spawnEnemy();
        }
        }
    }

    public void addScore()
    {
        score++;
    }

    private void updateGameHUD()
    {
        timer_label.text = Mathf.Round(Time.fixedTime - time).ToString();
        score_label.text = score.ToString();

        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemies_label.text = enemies.ToString();
      

        opponent_text = "";
        foreach (Game_Controller game in gamelist)
        {
            opponent_text += game.player.name + " "+ game.enemies.ToString() + " " +game.score.ToString() + "\n";
            opponent_text += game.player.name + " " + game.enemies.ToString() + " " + game.score.ToString() + "\n";
        }
        opponents_label.text = opponent_text;
    }


}
