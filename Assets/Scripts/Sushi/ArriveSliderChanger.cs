using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ArriveSliderChanger : MonoBehaviour {

    public Text arrText;
    public Slider arrSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onValueChanged()
    {
        arrText.text = "Tiempo de llegada: " + ((int)arrSlider.value ).ToString() + " segs";
    }
}
