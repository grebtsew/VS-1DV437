using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public int nextScene;

	// Use this for initialization
	void Start () {
        // Grab instance of component
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        // Invisible quit menu
        quitMenu.enabled = false;
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

    public void StartLevel()
    {
        Application.LoadLevel(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
