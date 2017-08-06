using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public int nextScene;

    private string name;
    public Text name_text;
    public InputField name_inputfield;

	// Use this for initialization
	void Start () {
        // Grab instance of component
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        // Invisible quit menu
        quitMenu.enabled = false;

        // Set Name
        name_inputfield.text = PlayerPrefsHandler.GetPersistentVar<string>(Statics.player_name, "Player" + Random.Range(1, 100));
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void NameUpdated()
    {
        // save Name
        if (name_text.text == null || name_text.text == "")
        {
            PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_name, ref name, "Player" + Random.Range(1, 100), true);
        }
        else
        {
            PlayerPrefsHandler.SetPersistentVar<string>(Statics.player_name, ref name, name_text.text, true);
        }
    }

    public void StartLevel()
    {
        Application.LoadLevel(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    
}
