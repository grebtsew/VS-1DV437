using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class global_game_controller : MonoBehaviour {

    private bool gameOver = false;
    private bool ready = false;
    
    private bool restart = false;
    private bool doonce = true;

    public Canvas inGameMenu;
    public Canvas extraQuestion;
    public Canvas startQuestion;

    public Text scoreboard;

    private bool matchover = false;

    public Text gameOver_text;

    public Camera_Controller cc;

    float timeLeft = 5.0f;
    public Text text;

    public Game_Controller game;

    Game_Controller[] gamelist;

    public Map_script[] map_reference;
    public Map_script player_map;

    public Game_Controller player_game;

    private GameObject player;
    private global_camera_controller global_camera;

    // Use this for initialization
    void Start () {

        Time.timeScale = 0;

        // create world

        map_reference = FindObjectsOfType<Map_script>();
        foreach (Map_script map in map_reference)
        {
            if(map.id == 0)
            {
                player_map = map;
                break;
            }    
        }

        int p1 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(1), 0);
        int p2 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(0), 0);
        scoreboard.text = p1 + " : " + p2;

        // spawnPlayer
        string player_prefab_path = "Players(ingame)/" + PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_character(0), "Mage");

        player = Resources.Load<GameObject>(player_prefab_path);
        
        Player p = Instantiate(player, transform.position + transform.up * 2, transform.rotation).GetComponent<Player>();
        p.transform.SetParent(player_map.transform);
        p.InitiatePlayer(this, transform);

        cc.setTarget(p);

        global_camera = FindObjectOfType<global_camera_controller>();
        global_camera.initiate();

        player_game.initiate(p);

        // init menu
        inGameMenu.enabled = false;
        extraQuestion.enabled = false;
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
        } else
        {

           
            if (matchover)
            {
                Time.timeScale = 1;
                Application.LoadLevel(0);
            } else
            {
                Time.timeScale = 1;
                Application.LoadLevel(2);
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
            Application.LoadLevel(2);
        } else
        {
            Time.timeScale = 1;
            Application.LoadLevel(0);
        }
    }

    public void exit_pressed()
    {
        extraQuestion.enabled = true;
        restart = false;

    }

    public void GameOver(Player player)
    {
        Time.timeScale = 0;
        gameOver = true;
        Text text = inGameMenu.GetComponentInChildren<Text>();
        text.text = "Game Over!";
        inGameMenu.enabled = true;
        game.gameOver = true;

        // check who lost and set text
        string s = "";
        if (player.health <= 0)
        {
            s = player.Name + " died!";
        } else
        {
            s = player.Name + " didn't kill fast enought!";
        }


        // add score
        int i = 0;
        int p1 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(1), 0);
        int p2 = PlayerPrefsHandler.GetPersistentVar<int>(Statics.player_score(0), 0);
      
        if(player.map_reference.id == 0)
        {
            p1++;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, p1 , true);
        } else
        {
            p2++;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, p2 , true);
        }

        if(p1 >= 5  )
        {
            gameOver_text.text = "Winner is " + player.name + "\n" + " Score: " + p1 + " : " + p2;
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref i, 0, true);
            PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref i, 0, true);
            matchover = true;
        } else if(p2 >= 5)
        {
            gameOver_text.text = "Winner is "+player.name + "\n" + " Score: " + p1 + " : " + p2;
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
    void Update () {

        if (ready) { 
        if (!gameOver) { 
            if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                inGameMenu.enabled = false;
                extraQuestion.enabled = false;
                Time.timeScale = 1;
            } else
            {
                inGameMenu.enabled = true;

                Time.timeScale = 0;
            }
            }
            }
        } else
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
                game.Ready();
                doonce = false;        
            }
        }

    }
}
