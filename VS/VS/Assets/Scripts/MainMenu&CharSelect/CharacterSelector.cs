using System.Collections;
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
    private string character_name;
    private Player_Character current_character;
    public Text warning;
    public Text wins;
    public Text matches;
    public Text charwins;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;

        initiateSelector();

        initiateLabels();

    }

    private void initiateLabels()
    {
        // total wins
        PlayerPrefsHandler.GetPersistentVar<int>(Statics.total_wins, Statics.zero);
        // total played
        PlayerPrefsHandler.GetPersistentVar<int>(Statics.total_matches, Statics.zero);
    }

    private void initiateSelector()
    {
        GameObject[] characters = Resources.LoadAll<GameObject>("Players");
        foreach (GameObject c in characters)
        {
            player = c.GetComponent(typeof(Player)) as Player;

            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("Character_controller").transform);

            characterList.Add(_char);
            _char.SetActive(false);

            name.text = characterList[index].name.Replace("(Clone)", "");
            characterList[index].SetActive(true);
            current_character = characterList[index].GetComponent<player_selection_script>().character;
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
        current_character = characterList[index].GetComponent<player_selection_script>().character;
        updatelabels();
        warning.text = "";
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
        current_character = characterList[index].GetComponent<player_selection_script>().character;
        updatelabels();
        warning.text = "";
    }

    private void updatelabels()
    {
        switch (current_character)
        {
            case Player_Character.Mage:
                background.text = Mage_statics.Background;
                break;
            case Player_Character.HandTheTank:
                background.text = "";
                break;
            case Player_Character.Archer:
                background.text = "";
                break;
            case Player_Character.BigSword:
                background.text = "";
                break;
            case Player_Character.Bow:
                background.text = "";
                break;
            case Player_Character.Hammer:
                background.text = "";
                break;
            case Player_Character.Spearman:
                background.text = "";
                break;
            case Player_Character.Swordsman:
                background.text = "";
                break;
             
        }

        // character wins label
        PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(current_character), Statics.zero);
    }

    public void BackPressed()
    {
        Application.LoadLevel(previousScene);
    }

    public void StartPressed()
    {

        // if character unlocked!
        if(current_character == Player_Character.Mage) { 


        SaveCharacterSelection();
        Application.LoadLevel(nextScene);

        } else
        {
            warning.text = "Can't choose this player!";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            Previous();
        }
        if (Input.GetKeyDown("right"))
        {
            Next();
        }
    }

    private void SaveCharacterSelection()
    {
        PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_character(0), ref character_name, current_character.ToString(), true);
    }
    
}
