using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum Player_Character
{

   None, Mage, Adventurer, HandTheTank, Archer, BigSword, Bow, Hammer, Spearman, Swordsman

 }

static class Player_Character_Functions
{

    public static Player_Character GetCharacterByString(string name)
    {
        switch (name)
        {
            case "Mage":
                return Player_Character.Mage;
            case "Adventurer":
                return Player_Character.Adventurer;
            case "HandTheTank":
                return Player_Character.HandTheTank;
            case "Archer":
                return Player_Character.Archer;
            case "BigSword":
                return Player_Character.BigSword;
            case "Bow":
                return Player_Character.Bow;
            case "Hammer":
                return Player_Character.Hammer;
            case "Spearman":
                return Player_Character.Spearman;
            case "Swordsman":
                return Player_Character.Swordsman;
        }
        return Player_Character.None;
    }
}