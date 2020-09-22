using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;


public class ChangeLevelDescription : MonoBehaviour {


    public Text labelLevel;
    public Slider levelSlider;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeLabelDescriptionText()
    {
        int level = Int32.Parse(levelSlider.value.ToString());
        //Debug.Log(level);
        switch (level)
        {
            case 1:
                labelLevel.text = "Nivel (0° - 30°)";
                break;
            case 2:
                labelLevel.text = "Nivel (30° - 60°)";
                break;
            case 3:
                labelLevel.text = "Nivel (60° - 90°)";
                break;
            case 4:
                labelLevel.text = "Nivel (90° - 120°)";
                break;
            case 5:
                labelLevel.text = "Nivel (120° - 150°)";
                break;
            case 6:
                labelLevel.text = "Nivel (150° - 180°)";
                break;
        }
        //labelLevel.text = levelSlider.value.ToString() ;
    }
}
