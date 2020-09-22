using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeSliderChanger : MonoBehaviour {

    public Text timeText;
    public Slider timeSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
		timeText.text = "Tiempo (mins): " + ((int) Math.Floor( (double)timeSlider.value/2 )).ToString() + ":";
        if (timeSlider.value % 2 == 0)
        {
            timeText.text += "00";
        } else
        {
            timeText.text += "30";
        }
    }
}
