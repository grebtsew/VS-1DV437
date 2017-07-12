using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

   
    public static void Initialize()
    {
        
        canvas = GameObject.Find("GameCanvas");
        if (!popupText)
        {
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTexts/PopupTextParent");
        }
        
    }

    public static void CreateFloatingText(string text, Transform location)
    {

        Initialize();

        FloatingText instance = Instantiate(popupText);
        
        instance.setText(text);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;
  
    }

    

}
