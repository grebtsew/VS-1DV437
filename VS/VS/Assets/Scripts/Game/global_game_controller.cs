using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class global_game_controller : MonoBehaviour
{

    private bool gameOver = false;
    private bool ready = false;
    private bool restart = false;
    private bool doonce = true;
    private bool matchover = false;

    public Canvas inGameMenu;
    public Canvas extraQuestion;
    public Canvas startQuestion;

    public Text scoreboard;
    public Text gameOver_text;
    public Text text;
    public Text enemy_limit;
    public Text diff_value;

    private float timeLeft = 5.0f;

    public List<Game_Controller> Game_List;
    private GameObject player;

    public Map_script map_reference;
    public Map_script player_map;

    private int match_limit = 5;

    public Game_Controller game_controller;
    public Camera_Controller camera_controller;
    private global_camera_controller global_camera;

    private string map_prefab_path = "Maps/Map";

    void Start()
    {
       

        Time.timeScale = 0;
        string s = "";
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_controller(0), ref s, Player_Controll.Player.ToString(), true);
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_controller(1), ref s, Player_Controll.Ai.ToString(), true);

        int player_amount = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_amount, 2);

        // Game Mode (only have one)
      //  string game_mode = PlayerPrefsHandler.GetPersistentVar<string>(Statics.game_mode, "1vs1");

        // Create entire gameworld
        for (int i = 0; i < player_amount; i++)
        {
            // create map
            Transform map = Instantiate(Resources.Load<GameObject>(map_prefab_path), new Vector3((Statics.map_x + Statics.map_space) * i, 0, 0), transform.rotation).GetComponent<Transform>();
            map_reference = map.GetComponent<Map_script>();
            map_reference.id = i;


            // spawn and instantiate Player 
            string player_prefab_path = "Players(ingame)/" + PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_character(i), "Mage");
            player = Resources.Load<GameObject>(player_prefab_path);
            Player p = Instantiate(player, map.transform.position + transform.up * 2, transform.rotation).GetComponent<Player>();

            // set player parent
            p.transform.SetParent(map);

            // set player name
            p.Name = PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_name(i), "Player1");


            // set controller
            string controller = PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_controller(i), "Ai");
            if (controller == "Ai")
            {
                p.setController(Player_Controll.Ai);
                p.controller.setDifficulty(PlayerPrefsHandler.GetPersistentVar<string>(Statics.ai_difficulty(i), "Easy"));
            
            }
            else
            {
                p.setController(Player_Controll.Player);
                p.controller.setDifficulty(PlayerPrefsHandler.GetPersistentVar<string>(Statics.ai_difficulty(i), "Easy"));
               
            }
            diff_value.text = p.controller.difficulty.ToString();

            // get enemyparent and init player
            foreach (Transform t in map.GetComponentsInChildren<Transform>())
            {
                if (t.tag == "EnemyParent")
                {
                    p.InitiatePlayer(this, t);
                    break;
                }
            }

            // init camera
            camera_controller = map.GetComponentInChildren<Camera_Controller>();
            camera_controller.setTarget(p);

            // init game
            game_controller = map.GetComponentInChildren<Game_Controller>();
            game_controller.MAX_ENEMY = PlayerPrefsHandler.GetPersistentVar<int>(Statics.enemy_amount, 50);
            enemy_limit.text = game_controller.MAX_ENEMY.ToString();
            game_controller.initiate(p);

            // add to list
            Game_List.Add(game_controller);

        }

        global_camera = FindObjectOfType<global_camera_controller>();
        global_camera.initiate();

        // init menu
        inGameMenu.enabled = false;
        extraQuestion.enabled = false;

        // get score
        int p1 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(0), 0);
        int p2 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(1), 0);
        scoreboard.text = p1 + " : " + p2;
    }


    public void ready_pressed()
    {
        ready = true;
        startQuestion.enabled = false;
        Time.timeScale = 1;
    }

    public void continue_pressed()
    {
        if (!gameOver)
        {
            inGameMenu.enabled = false;
            Time.timeScale = 1;
        }
        else
        {
            if (matchover)
            {
                int i = 0;
                PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, 0, true);
                PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, 0, true);
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(2);
            }
        }
    }

    public void restart_pressed()
    {
        extraQuestion.enabled = true;
        restart = true;

    }

    public void no_pressed()
    {
        extraQuestion.enabled = false;
    }

    public void yes_pressed()
    {
        if (restart)
        {
            int i = 0;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, 0, true);
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, 0, true);
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void exit_pressed()
    {
        extraQuestion.enabled = true;
        restart = false;

    }

    public void GameOver(Player player)
    {
        /*
        Below is fast written code to have a game for show...
        */

        Time.timeScale = 0;
        gameOver = true;
        Text text = inGameMenu.GetComponentInChildren<Text>();
        text.text = "Game Over!";
        inGameMenu.enabled = true;

        foreach (Game_Controller game in Game_List)
        {
            game.gameOver = true;
        }

        // check who lost and set text
        string s = "";
        if (player.health <= 0)
        {
            s = player.Name + " died!";
        }
        else
        {
            s = player.Name + " didn't kill fast enough!";
        }

        // add score
        int i = 0;
        int p1 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(0), 0);
        int p2 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(1), 0);

        if (player.map_reference.id == 0)
        {
            p2++;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, p2, true);
        }
        else
        {
            p1++;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, p1, true);
        }

        if (p1 >= match_limit)
        {
            gameOver_text.text = "Winner is " + PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_name(0), s) + "\n" + " Score: " + p1 + " : " + p2;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, 0, true);
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, 0, true);
            matchover = true;

            // add player won game and total wins
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.character_wins(player.character), ref i, PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(player.character), 0) + 1, true);
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.total_wins, ref i, PlayerPrefsHandler.GetPersistentVar<int>(Statics.total_wins, 0) + 1, true);
        }
        else if (p2 >= match_limit)
        {
            gameOver_text.text = "Winner is " + PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_name(1), s) + "\n" + " Score: " + p1 + " : " + p2;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, 0, true);
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, 0, true);
            matchover = true;
        }
        else
        {

            scoreboard.text = p1 + " : " + p2;
            gameOver_text.text = s + "\n" + " Score: " + p1 + " : " + p2;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (ready)
        {

            if (!gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (Time.timeScale == 0)
                    {
                        inGameMenu.enabled = false;
                        extraQuestion.enabled = false;
                        Time.timeScale = 1;
                    }
                    else
                    {
                        inGameMenu.enabled = true;

                        Time.timeScale = 0;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return"))
            {
                ready_pressed();
            }
        }


        if (ready && doonce)
        {
            timeLeft -= Time.deltaTime;
            text.text = Mathf.Round(timeLeft).ToString();
            if (timeLeft < 0)
            {
                foreach (Game_Controller game in Game_List)
                {
                    game.Ready();
                }
                doonce = false;
            }
        }

    }
}
