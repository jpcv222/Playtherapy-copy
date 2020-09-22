using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FloatSliderChanger : MonoBehaviour {

    public Text flText;
    public Slider flSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
        flText.text = "Tiempo de flote: " + ((int)flSlider.value ).ToString() + " segs";
    }
}
