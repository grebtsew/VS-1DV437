using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics {

    public static string test = "";

    public static string sound = "sound";

    // Camera Statics
    public static Vector3 camera_offset = new Vector3(9,8,-13);
    public static int CameraRotationOffset = 1;
    public static float MIN_SCROLL = 15f;
    public static float MAX_SCROLL = 90f;
    public static float SCROLL_SENSITIVITY = 10f;

    public static int floating_text_size = 5;

    // Map Statics
    public static int map_x = 60;
    public static int map_z = 60;
    public static int map_space = 10;

    // Number Statics
    public static int zero = 0;

    // Player
    public static float health = 100;

    // Save & Load Statics
    public static string ai_difficulty(int i)
    {
        return "ai_" + i + "_difficulty";
    }
    public static string player_controller(int i)
    {
        return "player_" + i + "_controller";
    }
    public static string player_name(int i)
    {
        return "player_" + i + "_name";
    }
    public static string player_team(int i)
    {
        return "player_" + i + "_team";
    }
    public static string player_amount = "player_amount";
    public static string player_score(int i)
    {
        return "player_" + i + "_score";
    }
    public static string game_mode = "game_mode";
    public static string ai_amount = "ai_amount";
    public static string enemy_amount = "enemy_amount";
    public static string player_character(int i)
    {
        return "player_" + i + "_character";
    }
    public static string total_wins = "total_wins";
    public static string total_matches = "total_matches";
    public static string character_wins(Player_Character character)
    {
        return character.ToString() + "_wins";
    }
    public static string team_amounts = "team_amount";

}
