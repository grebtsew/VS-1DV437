using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
        {
            popupText = Resources.Load<FloatingText>("Prefabs/PopuptextParent");
        }
       
    }

    public static void CreateFloatingText(string text, Transform location)
    {

        FloatingText instance = Instantiate(popupText);
       // Vector3 screenPosition = Camera.main.WorldToScreenPoint(location.position);

        
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;
        instance.setText(text);

        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
