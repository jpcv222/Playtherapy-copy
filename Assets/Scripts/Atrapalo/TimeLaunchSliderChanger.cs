using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeLaunchSliderChanger : MonoBehaviour {

    public Text timeLaunchText;
    public Slider timeLaunchSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
		timeLaunchText.text = "Tiempo de lanzamiento: " + ((int) Math.Floor( (double)timeLaunchSlider.value/2 )).ToString() + ":";
		if (timeLaunchSlider.value % 2 == 0)
		{
			timeLaunchText.text += "00";
		} else
		{
			timeLaunchText.text += "30";
		}
    }
}
