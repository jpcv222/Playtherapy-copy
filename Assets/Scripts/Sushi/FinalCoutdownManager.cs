using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalCoutdownManager : MonoBehaviour {

    public Text mainTimerDisplay;
    public AudioSource resultsSoundtrack;
    float countdownTime;
    bool countdownStarted;
    bool isPlaying;


	// Use this for initialization
	void Start () {
        countdownTime = 0.0f;
        countdownStarted = false;
        isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
        {
            if (countdownStarted)
            {
                if (3.0f - countdownTime > 2.0f)
                {
                    mainTimerDisplay.fontSize = 30 + (int)(20.0f * countdownTime);
                }
                else if (3.0f - countdownTime > 1.0f)
                {
                    mainTimerDisplay.fontSize = 30 + (int)(20.0f * (countdownTime - 1.0f));
                }
                else if (3.0f - countdownTime > 0.0f)
                {
                    mainTimerDisplay.fontSize = 30 + (int)(30.0f * (countdownTime - 2.0f));
                }
                else if (3.0f - countdownTime > -1.0f)
                {
                    mainTimerDisplay.fontSize = 30 + (int)(15.0f * (countdownTime - 3.0f));
                }
                else if (3.0f - countdownTime > -1.5f)
                {
                    if (!isPlaying)
                    {
                        resultsSoundtrack.Play();
                        isPlaying = true;
                    }
                    mainTimerDisplay.fontSize = 30;
                }
                countdownTime += Time.deltaTime;
            }
        }

    }

    public void startCoutdown ()
    {
        countdownStarted = true;
    }
}
