using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RepCountdownManager : MonoBehaviour {

    public Text mainTimerDisplay;
    bool countdownStarted;
    float countdownTime;
    bool isFinal;

    // Use this for initialization
    void Start () {
        countdownStarted = false;
        isFinal = false;
        countdownTime = 0.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (countdownStarted)
        {
            if (1.0f - countdownTime > 0.0f)
            {
                if (!isFinal)
                {
                    mainTimerDisplay.fontSize = 30 + (int)(20.0f * countdownTime);
                }
                else
                {
                    mainTimerDisplay.fontSize = 30 + (int)(15.0f * countdownTime);
                }
            }
            else
            {
                mainTimerDisplay.fontSize = 30;
                countdownStarted = false;
            }
            countdownTime += Time.deltaTime;
        }
	}

    public void startCountdown (bool final)
    {
        if (final)
        {
            isFinal = true;
        } else
        {
            isFinal = false;
        }
        mainTimerDisplay.fontStyle = FontStyle.Bold;
        mainTimerDisplay.color = Color.red;
        countdownStarted = true;
        countdownTime = 0.0f;
    }
}
