using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_sound : MonoBehaviour {


    private bool soundOn = true;

    void Start()
    {
        soundOn = PlayerPrefsHandler.GetPersistentVar<bool>(Statics.sound, true);

        if (soundOn == false)
        {
            AudioListener.volume = 0;
        }
    }

    public void toggle()
    {
        if(AudioListener.volume == 0)
        {
            soundOn = true;
           PlayerPrefsHandler.SetPersistentVar<bool>(Statics.sound, ref soundOn, soundOn, true);

            AudioListener.volume = 1;
        } else
        {
            soundOn = false;
            PlayerPrefsHandler.SetPersistentVar<bool>(Statics.sound, ref soundOn, soundOn, true);
            AudioListener.volume = 0;
        }

    }

}
