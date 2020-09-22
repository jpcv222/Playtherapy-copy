using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RepSliderChanger : MonoBehaviour {

    public Text repText;
    public Slider repSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
        repText.text = "Repeticiones: " + ((int)repSlider.value ).ToString();
    }
}
