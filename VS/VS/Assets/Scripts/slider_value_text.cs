﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_value_text : MonoBehaviour {

    public Text text;
    public Slider slider;

    public void textChanged()
    {
        text.text = slider.value.ToString();
    }



}
