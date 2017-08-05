using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class global_camera_controller : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;

    public Text following_label;
    public Text following_name_label;
    public Text following_level_label;

    private Player player;
    private Player ai;

    void Start()
    {
        Camera1.GetComponent<Camera>().enabled = true;
        Camera2.GetComponent<Camera>().enabled = false;

        //getplayer for info
        foreach (Player p in FindObjectsOfType<Player>())
        {
            if (p.player_controller.controll_mode == Player_Controll.Player)
            {
                player = p;
            }
            else
            {
                ai = p;
            }
        }
    }


    public void ChangeCamera()
    {
        if (Camera1.GetComponent<Camera>().enabled)
        {
            Camera2.GetComponent<Camera>().enabled = true;
            Camera1.GetComponent<Camera>().enabled = false;
        }
        else if (Camera2.GetComponent<Camera>().enabled)
        {
            Camera1.GetComponent<Camera>().enabled = true;
            Camera2.GetComponent<Camera>().enabled = false;
        }


        updateLabels();

    }

    public Player_Controll currentlyFollowing()
    {
        if (Camera1.GetComponent<Camera>().enabled)
        {
            return Player_Controll.Player;
        } else
        {
            return Player_Controll.Ai;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera();
        }

        updateLabels();
    }

    private void updateLabels()
    {
        if (currentlyFollowing() == Player_Controll.Player)
        {
            following_level_label.text = player.level.ToString();
            following_name_label.text = player.Name;
        }
        else
        {
            following_level_label.text = ai.level.ToString();
            following_name_label.text = ai.Name;
        }

        following_label.text = currentlyFollowing().ToString();
    }
}
