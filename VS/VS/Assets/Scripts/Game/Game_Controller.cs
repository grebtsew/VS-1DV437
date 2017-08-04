using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour {

    public Text timer_label;
    public Text score_label;
    public Text enemies_label;
    public Text opponents_label;
    public bool gameOver = false;
    private bool ready = false;

    public Player player;

    public GameObject enemyParent;

    private int MAX_ENEMY = 150;

    private float time;
    public int score = 0;

    public Transform playerstart;
    private Vector3 center;
    private Vector3 size = new Vector3(28,10,28);
    public float spawnTime;
    private float spawnDelay = 3;

    private string opponent_text;

    private int enemy_on_map = 0;
    public List<Game_Controller> gamelist;

    private bool isPlayer = false;

    public void Ready()
    {
        spawnTime = Time.time;
        time = Time.fixedTime;
        ready = true;
    }

    // Use this for initialization
    void Start () {

        // Is player
        if(player.player_controller.controll_mode == Player_Controll.Player)
        {
            isPlayer = true;
            Instantiate(Resources.Load("Followers/MouseFollower"));
        }

        if (player.player_controller.controll_mode == Player_Controll.Ai)
        {
            Ready();
        }

        // get map center
        center = playerstart.position;
        
        foreach(Game_Controller gc in Game_Controller.FindObjectsOfType<Game_Controller>())
        {
            gamelist.Add(gc);
        }

       

        spawnTime = Time.time;
    }

 

    public void spawnEnemy()
    {
        Vector3 randomPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(1, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Enemy instance = Instantiate(Resources.Load("Enemies/Bat_Green", typeof(Enemy))) as Enemy;
        instance.transform.SetParent(enemyParent.transform);
        instance.transform.position = randomPos;
        instance.SetMap(this);
        instance.SetTargetPlayer(player);
        enemy_on_map++;

        if (enemy_on_map >= MAX_ENEMY)
        {
           // warn and start 10 s timer!
        }
    }

	// Update is called once per frame
	void Update () {

        if (!gameOver && ready) {

            if (isPlayer) {
                updateGameHUD();
            }

            if (spawnTime <= Time.time)
        {
            spawnTime += spawnDelay;
            spawnEnemy();

            if(spawnDelay > 0.5f) { 
            spawnDelay -= 0.02f;
                }
            }
        }
    }

    public void addScore()
    {
        score++;
        enemy_on_map--;

        // spawn enemy on all other maps
        foreach(Game_Controller gc in gamelist)
        {
            if(gc != this)
            {
                gc.spawnEnemy();
            }
        }
    }

    private void updateGameHUD()
    {
        // timer and score
        timer_label.text = Mathf.Round(Time.fixedTime - time).ToString();
        score_label.text = score.ToString();

        // enemies
        enemies_label.text = enemy_on_map.ToString();

        if (spawnTime <= Time.time) { 
            // sort list
            gamelist.Sort(delegate (Game_Controller a, Game_Controller b)
        {
            return (a.score).CompareTo(b.score);
        });
        }

        //display
        opponent_text = "";
        foreach (Game_Controller game in gamelist)
        {
          
            opponent_text += game.player.name +"(" + game.player.player_controller.controll_mode.ToString() + ")"+  " "+ game.enemy_on_map.ToString() + " " +game.score.ToString() + "\n";
        }

        opponents_label.text = opponent_text;
    }


}

