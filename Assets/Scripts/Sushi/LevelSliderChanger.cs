using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelSliderChanger : MonoBehaviour {

    public Text levText;
    public Slider levSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
        levText.text = "Nivel: " + ((int)levSlider.value ).ToString();
        levText.text += " (" + ((((int)levSlider.value) - 1) * 30).ToString() + "º - " + (((int)levSlider.value) * 30).ToString() + "º)";
    }
}
