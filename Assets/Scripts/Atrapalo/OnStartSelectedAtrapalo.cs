using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class OnStartSelectedAtrapalo : MonoBehaviour {

    public Toggle toggleRep;
    public Toggle toggleTime;

    public InputField inputRep;
    public InputField inputTime;
    public Slider sliderLevel;
	public Slider sliderTimeLaunch;

	private GameObject menu;

    // Use this for initialization
    void Start () {
		menu = GameObject.Find ("SelectionMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RunGame()
    {

        int level = (int)sliderLevel.value;
        bool time = false;
        int timeRepValue = 0;
		int timeLaunch = setVelocBalls((int)sliderTimeLaunch.value);
        if (toggleTime.isOn)
        {
            time = true;
            timeRepValue = Int32.Parse(inputTime.text) * 60;
        }
        else
        {
            time = false;
            timeRepValue = Int32.Parse(inputRep.text);
        }
        if (GameManagerAtrapalo.gms)
        {
			GameManagerAtrapalo.gms.StartGame(level, time, timeRepValue, timeLaunch);
        }
		menu.SetActive (false);
        
    }

    /**
     * 
     * */
    private int setVelocBalls(int option)
    {
        int timeLaunch = 0;
        switch (option)
        {
            case 1:
                timeLaunch = 4;
                break;
            case 2:
                timeLaunch = 6;
                break;
            case 3:
                timeLaunch = 9;
                break;
        }

        return timeLaunch;
    }
}
