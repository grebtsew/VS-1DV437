using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{

    public List<GameObject> characterList;
    private int index = 0;
    private static int previousScene = 0;
    private static int nextScene = 2;
    public Text selected_name;
    public Text background;
    private Player player;
    private string character_name;
    private Player_Character current_character;
    public Text warning;
    public Text wins;
    public Text matches;
    public Text charwins;

    private int wins_i;
    private int matches_i;

    public Text unlock_text;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        initiateSelector();
        initiateLabels();
    }

    private void initiateLabels()
    {
        // total wins
        wins_i = PlayerPrefsHandler.GetPersistentVar<int>(Statics.total_wins, Statics.zero);
        wins.text = wins_i.ToString();

        // total played
        matches_i = PlayerPrefsHandler.GetPersistentVar<int>(Statics.total_matches, Statics.zero);
        matches.text = matches_i.ToString();
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

            selected_name.text = characterList[index].name.Replace("(Clone)", "");
            characterList[index].SetActive(true);
            current_character = characterList[index].GetComponent<player_selection_script>().character;
           
            charwins.text =  PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(current_character), 0).ToString();
            updatelabels();
        }
    }

    public void Next()
    {
        characterList[index].SetActive(false);
        if (index == characterList.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        selected_name.text = characterList[index].name.Replace("(Clone)", "");
        characterList[index].SetActive(true);
        current_character = characterList[index].GetComponent<player_selection_script>().character;
        charwins.text = PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(current_character), 0).ToString();
        updatelabels();
        warning.text = "";
    }

    public void Previous()
    {
        characterList[index].SetActive(false);
        if (index == 0)
        {
            index = characterList.Count - 1;
        }
        else
        {
            index--;
        }
        selected_name.text = characterList[index].name.Replace("(Clone)", "");
        characterList[index].SetActive(true);
        current_character = characterList[index].GetComponent<player_selection_script>().character;
        charwins.text = PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(current_character), 0).ToString();
        updatelabels();
        warning.text = "";
    }

    private void updatelabels()
    {
        switch (current_character)
        {
            case Player_Character.Mage:
                background.text = Mage_statics.Background;

                if (wins_i >= Mage_statics.unlock)
                {
                    if (Mage_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }

                }
                else
                {
                    unlock_text.text = "Most win " + Mage_statics.unlock + " matches to unlock this character!";
                }

                break;
            case Player_Character.HankTheTank:
                background.text = HankTheTank_statics.Background;

                if (wins_i >= HankTheTank_statics.unlock)
                {
                    if (HankTheTank_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + HankTheTank_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Archer:
                background.text = Archer_statics.Background;

                if (wins_i >= Archer_statics.unlock)
                {
                    if (Archer_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Archer_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.BigSword:
                background.text = BigSword_statics.Background;

                if (wins_i >= BigSword_statics.unlock)
                {
                    if (BigSword_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + BigSword_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Bow:
                background.text = Bow_statics.Background;

                if (wins_i >= Bow_statics.unlock)
                {
                    if (Bow_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Bow_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Hammer:
                background.text = Hammer_statics.Background;

                if (wins_i >= Hammer_statics.unlock)
                {
                    if (Hammer_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Hammer_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Spearman:
                background.text = Spearman_statics.Background;

                if (wins_i >= Spearman_statics.unlock)
                {
                    if (Spearman_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Spearman_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Swordsman:
                background.text = Swordsman_statics.Background;

                if (wins_i >= Swordsman_statics.unlock)
                {
                    if (Swordsman_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Swordsman_statics.unlock + " matches to unlock this character!";
                }
                break;
            case Player_Character.Adventurer:
                background.text = Adventurer_statics.Background;

                if (wins_i >= Adventurer_statics.unlock)
                {
                    if (Adventurer_statics.developed)
                    {
                        characterList[index].GetComponent<player_selection_script>().unlock_character();
                        unlock_text.text = "";
                    }
                    else
                    {
                        unlock_text.text = "It appears this character is not completely developed at this moment. Please select another one!";
                    }
                }
                else
                {
                    unlock_text.text = "Most win " + Adventurer_statics.unlock + " matches to unlock this character!";
                }
                break;
        }

        // character wins label
        PlayerPrefsHandler.GetPersistentVar<int>(Statics.character_wins(current_character), Statics.zero);
    }

    public void BackPressed()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void StartPressed()
    {

        // if character unlocked!
        if (characterList[index].GetComponent<player_selection_script>().unlocked)
        {

            int player_amount = PlayerPrefsHandler.GetPersistentVar<int>(Statics.ai_amount, 0);

            string s = "";

            // reset score for amount of players
            for (int i = 0; i < player_amount; i++)
            {
                // reset score
                PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(i), ref i, 0, true);
                

            }
            
            PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_name(1), ref s, "Ai_play", false);

            // reset score
            int j = 0;
             PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(1), ref j, 0, false);
             PlayerPrefsHandler.SetPersistentVar<int>(Statics.player_score(0), ref j, 0, true);


            SaveCharacterSelection();
            SceneManager.LoadScene(nextScene);

        }
        else
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
