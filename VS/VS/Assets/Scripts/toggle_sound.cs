using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_sound : MonoBehaviour {



    public void toggle()
    {
       
        if(AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        } else
        {
            AudioListener.volume = 0;
        }

    }

}
