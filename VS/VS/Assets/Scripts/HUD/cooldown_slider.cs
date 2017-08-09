using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_slider : MonoBehaviour {

    public Canvas shower;
    public Slider slider;
    public float cooldown;
    public Buttons ability = Buttons.ability1;

    private bool onCooldown = false;
    private float temp = 100;
    private float slider_max = 100;

    private float time_delay = 0.01f;

    void Start () {

        if (shower) {
        shower.enabled = false;
        slider.value = 0;
        }

    }
	
    public void StartCooldown()
    {
        if (slider) { 
        slider.value = slider_max;
        slider.enabled = true;
        shower.enabled = true;
        }

        StartCoroutine(AnimateSliderOverTime(cooldown));
    }

    public bool OnCooldown()
    {
        return onCooldown;
    }



    IEnumerator AnimateSliderOverTime(float time)
    {

        onCooldown = true;
        temp = 100;
        while (temp > 0)
        {
           

            temp -= 1/time;
        
            slider.value = temp;
            yield return new WaitForSeconds(time_delay);
        }
        onCooldown = false;
        if (shower) { 
        shower.enabled = false;
        }

    }


}
