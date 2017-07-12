using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel_Controller : MonoBehaviour {

    private Canvas canvas;

    // Use this for initialization
    void Start () {
        canvas = GetComponent<Canvas>();
        disable();


    }

    public void disable()
    {
        canvas.enabled = false;
    }

    public bool isEnabled()
    {
        return canvas.enabled;
    }

    public void toggleLevelUp()
    {
       
        if (canvas.enabled)
        {
            canvas.enabled = false;
        } else
        {
            canvas.enabled = true;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
