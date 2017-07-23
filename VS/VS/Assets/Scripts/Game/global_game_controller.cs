using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class global_game_controller : MonoBehaviour {

    private bool gameOver = false;
    private bool ready = false;
    public Canvas inGameMenu;
    public Canvas extraQuestion;
    public Canvas startQuestion;
    private bool restart = false;
    private bool doonce = true;


    float timeLeft = 5.0f;
    public Text text;

    public Game_Controller game;

    Game_Controller[] gamelist;

    // Use this for initialization
    void Start () {
        // init menu
        inGameMenu.enabled = false;
        extraQuestion.enabled = false;

        

            Time.timeScale = 0;

        // create world

        // create start question

        // start countdown
       
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

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver = true;
        Text text = inGameMenu.GetComponentInChildren<Text>();
        text.text = "Game Over!";
        inGameMenu.enabled = true;
        game.gameOver = true;
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
