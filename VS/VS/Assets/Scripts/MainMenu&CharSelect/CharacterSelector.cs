﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelector : MonoBehaviour {

    public List<GameObject> characterList;
    public int index = 0;
    private static int previousScene = 0;
    private static int nextScene = 2;
    public Text name;
    public Text background;
    private Player player;


    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        GameObject[] characters = Resources.LoadAll<GameObject>("Players");
        foreach(GameObject c in characters)
        {
            
            player = c.GetComponent(typeof(Player)) as Player;
           
           


            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("Character_controller").transform);

            characterList.Add(_char);
            _char.SetActive(false);

            name.text = characterList[index].name.Replace("(Clone)", "");
            characterList[index].SetActive(true);
        }

	}

    public void Next()
    {
        characterList[index].SetActive(false);
        if(index == characterList.Count - 1)
        {
            index = 0;
        } else
        {
            index++;
        }
        name.text = characterList[index].name.Replace("(Clone)", "");
        characterList[index].SetActive(true);
    }

    public void Previous()
    {
        characterList[index].SetActive(false);
        if (index == 0)
        {
            index = characterList.Count-1;
        }
        else
        {
            index--;
        }
        name.text = characterList[index].name.Replace("(Clone)", "");
        characterList[index].SetActive(true);
    }

    public void BackPressed()
    {
        Application.LoadLevel(previousScene);
    }

    public void StartPressed()
    {
        Application.LoadLevel(nextScene);
    }

    
}