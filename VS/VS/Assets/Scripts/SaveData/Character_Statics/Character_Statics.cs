using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mage_statics
{

    public static string Background = "A combat mage with alot of damage, might be a little squishy tought!";
    public static int unlock = 0;
    public static bool developed = true;

    public static float ability_1_cooldown = 5;
    public static float ability_2_cooldown = 5;
    public static float ability_3_cooldown = 10;
    public static float ability_4_cooldown = 20;

    public static float ability_1_energycost = 40;
    public static float ability_2_energycost = 30;
    public static float ability_3_energycost = 30;
    public static float ability_4_energycost = 40;

    public static GameObject firstability = Resources.Load("Abilities/Mage/MageFirstAbility", typeof(GameObject)) as GameObject;
    public static GameObject secondability = Resources.Load("Abilities/Mage/MageSecondAbility", typeof(GameObject)) as GameObject;
    public static Ally_Controller thirdability = Resources.Load("Allies/Mage/Slime_Blue", typeof(Ally_Controller)) as Ally_Controller;
    public static deathball fourthability = Resources.Load("Abilities/Mage/MageFourthAbility", typeof(deathball)) as deathball;
}

public static class Adventurer_statics
{
    public static string Background = "The Adventurer knows his path on the map!";
    public static int unlock = 10;
    public static bool developed = false;
}
public static class Archer_statics
{

    public static string Background = "Crossbow, enought said!";
    public static int unlock = 15;
    public static bool developed = false;
}
public static class BigSword_statics
{

    public static string Background = "Big guy, big sword!";
    public static int unlock = 20;
    public static bool developed = false;
}
public static class Bow_statics
{

    public static string Background = "Bow and arrow!";
    public static int unlock = 25;
    public static bool developed = false;
}
public static class Hammer_statics
{

    public static string Background = "Hard hits and high health!";
    public static int unlock = 30;
    public static bool developed = false;
}
public static class HankTheTank_statics
{

    public static string Background = "The pure definition of Tank!";
    public static int unlock = 35;
    public static bool developed = false;
}
public static class Spearman_statics
{

    public static string Background = "Poker!";
    public static int unlock = 40;
    public static bool developed = false;
}
public static class Swordsman_statics
{

    public static string Background = "Fast sword magician!";
    public static int unlock = 0;
    public static bool developed = true;

    public static float ability_1_cooldown = 0;
    public static float ability_2_cooldown = 5;
    public static float ability_3_cooldown = 10;
    public static float ability_4_cooldown = 20;

    public static float ability_1_energycost = 2;
    public static float ability_2_energycost = 30;
    public static float ability_3_energycost = 30;
    public static float ability_4_energycost = 40;

    public static GameObject firstability = Resources.Load("Abilities/Swordman/spiral1", typeof(GameObject)) as GameObject;
    public static GameObject secondability = null;
    public static GameObject thirdability = Resources.Load("Abilities/Swordman/third_ability", typeof(GameObject)) as GameObject;
    public static Ally_Controller fourthability = Resources.Load("Allies/Swordman/Swordsman_ally", typeof(Ally_Controller)) as Ally_Controller;
}