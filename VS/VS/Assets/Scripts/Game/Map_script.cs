using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_script : MonoBehaviour
{

    public Game_Controller game_controller;
    public Canvas gamecanvas;
    public Camera_Controller camera_controller;
    public Player player;
    public Camera map_camera;

    public Regeneration_Controller healthregzone;
    public Regeneration_Controller energyregzone;

    public Transform player_start;
    public Transform enemy_parent;
    public Transform extra_parent;

  

    public int id;

    public void initiate(Player p)
    {
        player = p;
        
        player.controller.initiate(p);

        
 
    }

    void Start()
    {
        game_controller = GetComponentInChildren<Game_Controller>();
        gamecanvas = GetComponentInChildren<Canvas>();
        camera_controller = GetComponentInChildren<Camera_Controller>();
        map_camera = camera_controller.GetComponentInChildren<Camera>();

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.tag == "EnemyParent")
            {
                enemy_parent = t;
            }
            else if (t.tag == "ExtraParent")
            {
                extra_parent = t;
            }
            else if (t.tag == "StartPos")
            {
                player_start = t;
            }
            else if (t.tag == "EnergyReg")
            {
                energyregzone = t.GetComponent<Regeneration_Controller>();
                energyregzone.initiate(player);
            }
            else if (t.tag == "HealthReg")
            {
                healthregzone = t.GetComponent<Regeneration_Controller>();
                healthregzone.initiate(player);

            }
        }

        if (player != null)
        {
            player.controller.initiate(player);
        }

    }

    public void setId(int i)
    {
        id = i;
    }


}
