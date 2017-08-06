using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics {

    public static string test = "";

    public static int zero = 0;
    public static string ai_difficulty = "ai_difficulty";

    // strings for saving
    public static string player_name(int i)
    {
        return "player_" + i + "_name";
    }

    public static string player_amount = "player_amount";
    public static string player_score(int i)
    {
        return "player_" + i + "_score";
    }
   

    //gamesettings
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

    


}
