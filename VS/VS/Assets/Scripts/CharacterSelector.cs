using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelector : MonoBehaviour {

    public List<GameObject> characterList;
    public int index = 0;
    private static int previousScene = 0;
    private static int nextScene = 2;

    // Use this for initialization
    void Start () {
        GameObject[] characters = Resources.LoadAll<GameObject>("Prefabs/Players");
        foreach(GameObject c in characters)
        {
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("Character_controller").transform);

            characterList.Add(_char);
            _char.SetActive(false);
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

    // Update is called once per frame
    void Update () {
		
	}
}
