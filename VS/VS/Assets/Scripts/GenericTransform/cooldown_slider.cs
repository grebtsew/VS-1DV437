using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown_slider : MonoBehaviour {

    public Canvas shower;
    public Slider slider;
    public float cooldown;
    public Buttons ability = Buttons.ability1;

    private float temp = 100;
    private float slider_max = 100;
    

    // Use this for initialization
    void Start () {
  
        shower.enabled = false;
        slider.value = 0;
        
	}
	
    public void StartCooldown()
    {
        slider.value = slider_max;
        slider.enabled = true;
        shower.enabled = true;
        
        StartCoroutine(AnimateSliderOverTime(cooldown));
    }

    public bool OnCooldown()
    {
        return shower.enabled;
    }



    IEnumerator AnimateSliderOverTime(float time)
    {


        temp = 100;
        while (temp > 0)
        {
            temp -= 0.4f;
      
            slider.value = temp;
            yield return new WaitForSeconds(time);
        }

        shower.enabled = false;
        
    
    }


}
