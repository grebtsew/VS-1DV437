using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{

    public Text timer_label;
    public Text score_label;
    public Text enemies_label;
    public Text opponents_label;

    public bool gameOver = false;
    private bool ready = false;
    private bool timer_on = false;
    private bool isPlayer = false;

    private Enemy enemy;
    private toggle_canvas toggle_canvas;
    public Player player;
    public GameObject enemyParent;
    public Transform playerstart;
    private global_game_controller global_game_controller;
    public List<Game_Controller> gamelist;

    public int MAX_ENEMY = 150;
    public int score = 0;
    private int enemy_on_map = 0;

    private Vector3 center;
    private Vector3 size = new Vector3(28, 10, 28);

    public float spawnTime;
    public float spawnaccelaration = 0.02f;
    private float spawnDelay = 3;
    private float time;
    public static float COUNTDOWN = 10;
    public float timer = 10;
    private float timer_time;
    private float LOWEST_SPAWN_TIME = 0.3f;
  

    private string opponent_text;

    public void Ready()
    {
        spawnTime = Time.time;
        time = Time.fixedTime;
        ready = true;
    }

    public void initiate(Player p)
    {
        player = p;
        // Is player
        if (player.controll == Player_Controll.Player)
        {

            isPlayer = true;
            GameObject go = Instantiate(Resources.Load("Followers/MouseFollower", typeof(GameObject))) as GameObject;
            go.transform.SetParent(player.parent);

        }

    }

    void Start()
    {
      
        global_game_controller = FindObjectOfType<global_game_controller>();
        toggle_canvas = FindObjectOfType<toggle_canvas>();

       

        // get texts
        foreach (Text text in FindObjectOfType<game_panel>().GetComponentsInChildren<Text>())
        {
            switch (text.tag)
            {
                case "timer_l":
                    timer_label = text;
                    break;
                case "kill_l":
                    score_label = text;
                    break;
                case "enemies_l":
                    enemies_label = text;
                    break;
                case "opponent_l":
                    opponents_label = text;
                    break;
            }
        }

        if (player != null)
        {
            // ai is ready
            if (player.controll == Player_Controll.Ai)
            {
                Ready();
            }
        }

        // get map center
        center = playerstart.position;

        foreach (Game_Controller game in Game_Controller.FindObjectsOfType<Game_Controller>())
        {
            gamelist.Add(game);
        }
        updateGameHUD();
        spawnTime = Time.time;

        /* Get random enemy */
        switch ((Enemies)Random.Range(0, 3))
        {
            case Enemies.Bat:
                enemy = Statics.enemy_bat;
                break;
            case Enemies.Ghost:
                enemy = Statics.enemy_ghost;
                break;
            case Enemies.Rabbit:
                enemy = Statics.enemy_rabbit;
                break;
        }
       

    }

    public void spawnEnemy()
    {
        // Random Pos
        Vector3 randomPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(1, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

        //  instantiate enemy
        Enemy instance = Instantiate(enemy);
        instance.transform.SetParent(enemyParent.transform);
        instance.transform.position = randomPos;
        instance.SetMap(this);
        instance.SetTargetPlayer(player);
        enemy_on_map++;

        // Over MAX_ENEMY start countdown
        if (enemy_on_map >= MAX_ENEMY)
        {
            if (player.controll == Player_Controll.Player)
            {
                toggle_canvas.startCountdown();
            }
            else
            {

                StartTimer();
            }
        }
    }

    public bool allisReady()
    {
        foreach (Game_Controller game in gamelist)
        {
            if (game.ready == false)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {

        if (!gameOver && allisReady())
        {

            // update gameHUD
            if (isPlayer)
            {
                updateGameHUD();
            }

            // countdown
            if (timer_on)
            {
                if (timer_time < Time.time)
                {
                    gameOver = true;
                    global_game_controller.GameOver(player);
                }
            }

            // spawn and lower spawntime
            if (spawnTime <= Time.time)
            {
                spawnTime += spawnDelay;
                spawnEnemy();

                if (spawnDelay > LOWEST_SPAWN_TIME)
                {
                    spawnDelay -= spawnaccelaration;
                }
            }
        }
    }

    private void StartTimer()
    {
        if (!timer_on)
        {
            timer_time = Time.time + timer;
            timer_on = true;
            timer = COUNTDOWN;
        }
    }

    private void StopTimer()
    {
        timer_on = false;
        timer = COUNTDOWN;
    }

    public void addScore()
    {

        score++;
        enemy_on_map--;

        // stop countdowns
        if (enemy_on_map < MAX_ENEMY)
        {

            if (player.controll == Player_Controll.Player)
            {
                if (toggle_canvas.active)
                {
                    toggle_canvas.stopCountdown();
                }
            }
            else
            {
                if (timer_on)
                {
                    StopTimer();
                }
            }
        }

        // spawn enemy on all other maps
        foreach (Game_Controller game in gamelist)
        {
            if (game != this)
            {
                game.spawnEnemy();
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

        if (spawnTime <= Time.time)
        {
            // sort list
            gamelist.Sort(delegate (Game_Controller a, Game_Controller b)
        {
            return (b.score).CompareTo(a.score);
        });
        }

        //display
        opponent_text = "";
        foreach (Game_Controller game in gamelist)
        {
            opponent_text += string.Format("{0}\t {1,10:#####0} \t {2,10:#####0} \r\n", game.player.Name, game.enemy_on_map, game.score);

        }

        opponents_label.text = opponent_text;
    }


}

