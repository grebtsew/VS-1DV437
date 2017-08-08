using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Invisible_script : MonoBehaviour
{


    void Start()
    {
        Image i = GetComponent<Image>();
        i.enabled = false;
    }


}
