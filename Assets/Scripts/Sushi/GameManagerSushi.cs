using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManagerSushi : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManagerSushi gms;

	// public variables
	public int score=0;
    public int level = 1;

	public float startTime=5.0f;

    public int repetitions = 0;
    public int currentReps = 0;
    public int remainingReps = 0;
	
	public Text mainScoreDisplay;
	public GameObject mainScoreDisplayObj;
	public Text mainTimerDisplay;
	public GameObject mainTimerDisplayObj;
	public GameObject countdownDisplayObject;
	private Text countdownDisplay;

    public GameObject canvasScoreText;
    public GameObject canvasBestScoreText;
    public GameObject bronzeTrophy;
    public GameObject silverTrophy;
    public GameObject goldTrophy;
    public GameObject canvasResults;

    public GameObject resultMessage;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool countdownStarted = false;
    public bool gameIsStarted = false;
	public bool gameIsOver = false;
	private bool lastSeconds = false;

	public AudioSource countdownSound;
    public AudioSource beepSound;
    public AudioSource finalSound;
    public AudioSource resultsSoundtrack;

    public float currentTime;
	public float totalTime = 0.0f;
	private float countdownTime = 0.0f;

	private MovementDetectionLibrary.SpawnGameObjects spawner;

    public bool withTime = false;

    float floatTime = 0.0f;
    float upwardsTime = 0.0f;


    //Necessary elements for capturing the best angle in a section
    public GameObject FullBodyObject;
    private MovementDetectionLibrary.FullBody fBodyObject;
    private float bestPartialLeftShoulderAngle;
    private float bestTotalLeftShoulderAngle;
    private float bestPartialRightShoulderAngle;
    private float bestTotalRightShoulderAngle;

    //Necessary elements in order to display the final sushi table animation
    public float translateTime;
    public float timePerFloor;
    public int floorsNumber;
    public float animTimer;
    bool animEnded;

    public void StartGame(int levelToLoad, bool time, int value, float flTime, float uTime)
    {
        withTime = time; 

        if (time)
        {
            startTime = (float)value;
            currentTime = (float)value;
            mainTimerDisplay.text = "Tiempo: " + (((int)startTime)/60).ToString() + ":" + (((int)startTime) % 60).ToString("00");
        } else
        {
            repetitions = value;
            remainingReps = repetitions - 1;
            mainTimerDisplay.text = "Repeticiones: " + repetitions.ToString();
        }
        
        level = levelToLoad;

		countdownStarted = true;
		if (countdownSound) {
			countdownSound.Play ();
		}
        //gameIsStarted = true;
        mainScoreDisplay.text = "0 Aciertos";

		countdownDisplayObject.SetActive(true);
		lastSeconds = false;

        floatTime = flTime;
        upwardsTime = uTime;

    }

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gms == null) 
			gms = this.gameObject.GetComponent<GameManagerSushi>();

		// init scoreboard to 0
		mainScoreDisplay.text = "";

		if (countdownDisplayObject)
			countdownDisplay = countdownDisplayObject.GetComponent<Text>();


		spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();

        if (FullBodyObject)
            fBodyObject = FullBodyObject.GetComponent<MovementDetectionLibrary.FullBody>();

        //Initialize angle values
        bestPartialLeftShoulderAngle = 0.0f;
        bestTotalLeftShoulderAngle = 0.0f;
        bestPartialRightShoulderAngle = 0.0f;
        bestTotalRightShoulderAngle = 0.0f;

        //Initialize time values for final animation
        animEnded = false;
    }

	// this is the main game event loop
	void Update () {
		if (gameIsStarted) {
			if (!gameIsOver) {
				if (withTime) {
					if (currentTime < 0) { // check to see if timer has run out
                        gameIsOver = true;
                        floorsNumber = ((score - (score % 6)) / 6) + 1;
                        GameObject.Find("CanvasInfoManos").SetActive(false);
                        mainTimerDisplay.text = "TERMINADO";
                        gameObject.GetComponent<FinalAnimation>().startAnimation(translateTime, timePerFloor, floorsNumber);
					} else { // game playing state, so update the timer
						currentTime -= Time.deltaTime;
						totalTime += Time.deltaTime;
                        //mainTimerDisplay.text = "Tiempo: " + (((int)currentTime) / 60).ToString () + ":" + (((int)currentTime) % 60).ToString ("00");
                        mainTimerDisplay.text = "" + (((int)currentTime) / 60).ToString() + ":" + (((int)currentTime) % 60).ToString("00");
                    }
					if (!lastSeconds && currentTime <= 3.0f) {
						lastSeconds = true;
						if (countdownSound) {
                            musicAudioSource.Stop();
							countdownSound.Play ();
						}
                        gameObject.GetComponent<FinalCoutdownManager>().startCoutdown();
                        mainTimerDisplay.fontStyle = FontStyle.Bold;
						mainTimerDisplay.color = Color.red;
					}
				} else {
					if (remainingReps < 0) { // check to see if timer has run out
                        gameIsOver = true;
                        floorsNumber = ((score - (score % 6)) / 6) + 1;
                        GameObject.Find("HandInfo Holder").SetActive(false);
                        mainTimerDisplay.text = "TERMINADO";
                        gameObject.GetComponent<FinalAnimation>().startAnimation(translateTime, timePerFloor, floorsNumber);
                    } else { // game playing state, so update the timer
						currentTime -= Time.deltaTime;
						totalTime += Time.deltaTime;
						mainTimerDisplay.text = "Restantes: " + remainingReps.ToString ();
					}
				}

                float currentLeftShoulderAngle = 0.0f;
                float currentRightShoulderAngle = 0.0f;

                if (fBodyObject)
                {
                    currentLeftShoulderAngle = fBodyObject.getAngle("shoulderAbdLeft");
                    if (currentLeftShoulderAngle > bestPartialLeftShoulderAngle)
                        bestPartialLeftShoulderAngle = currentLeftShoulderAngle;

                    currentRightShoulderAngle = fBodyObject.getAngle("shoulderAbdRight");
                    if (currentRightShoulderAngle > bestPartialRightShoulderAngle)
                        bestPartialRightShoulderAngle = currentRightShoulderAngle;
                }
			}
            else
            {
                if (!animEnded)
                {
                    if (animTimer > (translateTime + (timePerFloor * floorsNumber)))
                    {
                        animEnded = true;
                        EndGame();
                    }
                }
                animTimer += Time.deltaTime;
            }
		} else if (countdownStarted) {
			if (3.0f - countdownTime > 2.0f) {
				countdownDisplay.text = "3";
				countdownDisplay.fontSize = 10 + (int)(90.0f * countdownTime); 
			} else if (3.0f - countdownTime > 1.0f) {
				countdownDisplay.text = "2";
				countdownDisplay.fontSize = 10 + (int)(90.0f * (countdownTime - 1.0f)); 
			} else if (3.0f - countdownTime > 0.0f) {
				countdownDisplay.text = "1";
				countdownDisplay.fontSize = 10 + (int)(90.0f * (countdownTime - 2.0f)); 
			} else if (3.0f - countdownTime > -1.0f) {
				countdownDisplay.text = "¡ADELANTE!";
				countdownDisplay.fontSize = 30 + (int)(70.0f * (countdownTime - 3.0f)); 
			} else {
				mainScoreDisplayObj.SetActive(true);
				mainTimerDisplayObj.SetActive(true);
				countdownDisplay.text = "";
				gameIsStarted = true;
				countdownDisplayObject.SetActive(false);


                spawner.SetTimes(floatTime, upwardsTime);
				spawner.MakeThingToSpawn ();


			}
			countdownTime += Time.deltaTime;

		}
		
	}

    public void NewRepetition()
    {
        currentReps++;
        remainingReps--;

        if (!withTime)
        {
            if (remainingReps <=2 && remainingReps >= 0)
            {
                gameObject.GetComponent<RepCountdownManager>().startCountdown(false);
                beepSound.Play();
            }
            else if (remainingReps < 0)
            {
                gameObject.GetComponent<RepCountdownManager>().startCountdown(true);
                musicAudioSource.Stop();
                finalSound.Play();
                resultsSoundtrack.PlayDelayed(1.0f);
            }
        }

        if (bestPartialLeftShoulderAngle > bestTotalLeftShoulderAngle)
        {
            bestTotalLeftShoulderAngle = bestPartialLeftShoulderAngle;
        }
        bestPartialLeftShoulderAngle = 0.0f;

        if (bestPartialRightShoulderAngle > bestTotalRightShoulderAngle)
        {
            bestTotalRightShoulderAngle = bestPartialRightShoulderAngle;
        }
        bestPartialRightShoulderAngle = 0.0f;
    }

    public int GetRepetitions()
    {
        return remainingReps;
    }

	void EndGame() {
		// game is over
		

		// repurpose the timer to display a message to the player
		


        // reduce the pitch of the background music, if it is set 
        //if (musicAudioSource)
        //	musicAudioSource.pitch = 0.5f; // slow down the music

        
        GameObject.Find("Canvas").SetActive(false);

		TherapySessionObject objTherapy = GameObject.Find ("TherapySession").GetComponent<TherapySessionObject> ();
		objTherapy.fillLastSession(score, currentReps, (int)totalTime, level.ToString());
		objTherapy.saveLastGameSession ();

        objTherapy.savePerformance((int)bestTotalLeftShoulderAngle, "4");
        objTherapy.savePerformance((int)bestTotalRightShoulderAngle, "5");

        int finalScore = (int)(((double)score / currentReps) * 100.0f);

        canvasScoreText.GetComponentInChildren<TextMesh>().text = finalScore + "%";
		canvasBestScoreText.GetComponentInChildren<TextMesh> ().text = objTherapy.getGameRecord() + "%";


        if (finalScore <= 60)
        {
            resultMessage.GetComponent<TextMesh>().text = "¡Muy bien!";
            bronzeTrophy.SetActive(true);
        }
        else if (finalScore <= 90)
        {
            resultMessage.GetComponent<TextMesh>().text = "¡Grandioso!";
            bronzeTrophy.SetActive(false);
            silverTrophy.SetActive(true);
        }
        else if (finalScore <= 100)
        {
            resultMessage.GetComponent<TextMesh>().text = "¡Increíble!";
            bronzeTrophy.SetActive(false);
            goldTrophy.SetActive(true);
        }        

        canvasResults.SetActive(true);



    }
	

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString () + " Acierto";
		if (score != 1) {
			mainScoreDisplay.text += "s";
		}

		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		//Application.LoadLevel (playAgainLevelToLoad);
	}
	

}
