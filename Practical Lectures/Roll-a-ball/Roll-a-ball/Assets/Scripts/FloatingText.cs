using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private Text dmgText;


	// Use this for initialization
	void OnEnable () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);


        Destroy(gameObject, 1);//clipInfo[0].clip.length);
        
        dmgText = animator.GetComponent<Text>();
	}
	
    public void setText(string text) {
         //animator.GetComponent<Text>().text = text;
       // dmgText.text = text;
       
    }

	// Update is called once per frame
	void Update () {
       
	}
}
