﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LeapAPI;

namespace GuerraMedieval
{
    public class GameManagerMedieval : MonoBehaviour
    {
        public static GameManagerMedieval gmm;

        // Panels used in the scene
        public GameObject mainPanel;
        public GameObject parametersPanel;
        public GameObject resultsPanel;
        public GameObject leapPanel;
        public GameObject pausePanel;

        // Used for states of the game
        public enum GameState
        {
            PLAYING,
            GAMEOVER,
            PAUSE,
            STARTING,
            END
        }
        private GameState gameState;
        private bool withTime;                              // If the game is with time or repetitions
        public bool withKeyboard = false;

        // Timers
        private float totalTime;                            //
        private float timeMillis;
        private float currentTime;
        public Slider sliderCurrentTime;
        public Text currentTimeText;
        public GameObject timerPanel;

        // Repetitions
        private int totalRepetitions;
        private int remainingRepetitions;
        public Text repetitionsText;
        public GameObject repetitionsPanel;

        private int score;                                  // Current score in the game
        public Text scoreText;                              // Current score on the screen

        public Text resultsScoreText;
        public Text resultsBestScoreText;
        public Sprite starOn;
        public Sprite starOff;
        public Image star1;
        public Image star2;
        public Image star3;

        // Parameters of Parameters panel
        private float spawnTime;

        private bool withGrab;
        private bool withFlexionExtension;
        private bool withPronation;
        private bool withBothHands;
        private bool isRightHand;
        private float flexion;
        private float extension;

        // Use this for initialization
        void Start()
        {
            if (PlaylistManager.pm == null || (PlaylistManager.pm != null && !PlaylistManager.pm.active))
            {
                parametersPanel.SetActive(true);
                mainPanel.SetActive(false);
            }
            if (gmm == null)
            {
                gmm = this.gameObject.GetComponent<GameManagerMedieval>();
            }
            currentTime = totalTime;
            timeMillis = 1000f;

            gameState = GameState.STARTING;
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (LeapService.IsConected() | withKeyboard)
            {
                if (leapPanel.activeSelf)
                {
                    leapPanel.SetActive(false);
                    Time.timeScale = 1;
                }

                switch (gameState)
                {
                    case GameState.PLAYING:
                        {
                            if (withTime)
                            {
                                currentTime -= Time.deltaTime;

                                if (currentTime >= 0)
                                {
                                    timeMillis -= Time.deltaTime * 1000f;
                                    if (timeMillis < 0)
                                    {
                                        timeMillis = 1000f;
                                    }
                                    currentTimeText.text = (((int)currentTime) / 60).ToString("00") + ":"
                                        + (((int)currentTime) % 60).ToString("00") + ":"
                                        + ((int)(timeMillis * 60 / 1000)).ToString("00");
                                    sliderCurrentTime.value = currentTime * 100 / totalTime;
                                }
                                else
                                {
                                    gameState = GameState.GAMEOVER;
                                    currentTimeText.text = "00:00:00";
                                    //state = PlayState.NONE;
                                }
                            }
                            else
                            {
                                totalTime += Time.deltaTime;
                            }
                        }
                        break;
                    case GameState.GAMEOVER:
                        {
                            gameState = GameState.END;
                            EndGame();
                        }
                        break;
                    case GameState.STARTING:
                        {
                            if (!parametersPanel.activeSelf)
                            {
                                parametersPanel.SetActive(true);
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            else if (!leapPanel.activeSelf)
            {
                leapPanel.SetActive(true);
                Time.timeScale = 0;
            }

        }



        public void StartGame(bool withTime, float time, int repetitions, float spawnTime, bool withGrab,
            bool withFlexionExtension, bool withPronation, bool withBothHands, float flexion, float extension,
            bool rightHand)
        {
            this.withTime = withTime;

            totalTime = time;
            currentTime = totalTime;
            remainingRepetitions = totalRepetitions;
            repetitionsText.text = remainingRepetitions.ToString();
            this.spawnTime = spawnTime;
            this.WithGrab = withGrab;
            this.WithFlexionExtension = withFlexionExtension;
            this.WithPronation = withPronation;
            this.WithBothHands = withBothHands;
            IsRightHand = rightHand;
            this.Flexion = flexion;
            this.Extension = extension;


            if (withTime)
            {
                repetitionsPanel.SetActive(false);
                timerPanel.SetActive(true);
            }
            else
            {
                timerPanel.SetActive(false);
                repetitionsPanel.SetActive(true);
            }

            mainPanel.SetActive(true);
            parametersPanel.SetActive(false);
            pausePanel.SetActive(true);
            gameState = GameState.PLAYING;
        }

        public void UpdateScore(int points)
        {
            score += points;
            scoreText.text = score.ToString();

            if (withTime)
            {
                totalRepetitions++;
            }
            else
            {
                UpdateRepetition(1);
            }
        }

        public void UpdateRepetition(int repetitionsDone)
        {
            remainingRepetitions -= repetitionsDone;
            repetitionsText.text = remainingRepetitions.ToString();

            if (remainingRepetitions <= 0)
            {
                gameState = GameState.GAMEOVER;

            }
        }

        public void EndGame()
        {
            mainPanel.SetActive(false);
            pausePanel.SetActive(false);
            StartCoroutine(EndGameAnimator());
        }

        private IEnumerator EndGameAnimator()
        {
            //shipAnimator.Play("ShipFinal");
            yield return new WaitForSeconds(0f);
            SaveAndShowResults();
        }

        public void SaveAndShowResults()
        {
            TherapySessionObject objTherapy = TherapySessionObject.tso;

            //if (objTherapy != null)
            //{
            //    objTherapy.fillLastSession(score, totalRepetitions, (int)totalTime, level.ToString());
            //    objTherapy.saveLastGameSession();
            //}
            string idMinigame = "10";
            GameSessionDAO gameDao = new GameSessionDAO();

            int finalScore;
            if (totalRepetitions == 0)
            {
                finalScore = 0;
            }
            else
            {
                finalScore = (int)(((float)score / totalRepetitions) * 100.0f);
            }
            resultsScoreText.text = "Desempeño: " + finalScore + "%";
            if (this.withTime == true)
            {
                GameSessionController gameCtrl = new GameSessionController();
                gameCtrl.addGameSession(finalScore, 0, this.totalTime, score, idMinigame);
            }
            else
            {
                GameSessionController gameCtrl = new GameSessionController();
                gameCtrl.addGameSession(finalScore, this.totalRepetitions, 0, score, idMinigame);
            }
            if (objTherapy != null)
                resultsBestScoreText.text = "Mejor: " + objTherapy.getGameRecord() + "%";
            else
                resultsBestScoreText.text = "Mejor: " + gameDao.GetScore(idMinigame) + "%";

            if (finalScore <= 60)
            {
                //resultMessage.GetComponent<TextMesh>().text = "¡Muy bien!";
                star1.sprite = starOn;
                star2.sprite = starOff;
                star3.sprite = starOff;
            }
            else if (finalScore <= 90)
            {
                //resultMessage.GetComponent<TextMesh>().text = "¡Grandioso!";
                star1.sprite = starOn;
                star2.sprite = starOn;
                star3.sprite = starOff;
            }
            else if (finalScore <= 100)
            {
                //resultMessage.GetComponent<TextMesh>().text = "¡Increíble!";
                star1.sprite = starOn;
                star2.sprite = starOn;
                star3.sprite = starOn;
            }

            resultsPanel.SetActive(true);

    
            SendPerformance();

            if (PlaylistManager.pm != null && PlaylistManager.pm.active)
            {
                PlaylistManager.pm.NextGame();
            }
        }

        public void SendPerformance()
        {
            if (withFlexionExtension == true)
            {
                PerformanceController performanceCtrl = new PerformanceController();
                performanceCtrl.addPerformance((int)this.Flexion, "31");
                performanceCtrl.addPerformance((int)this.Extension, "32");

            }      

        }

        public GameState GetGameState()
        {
            return gameState;
        }

        public bool IsPlaying()
        {
            if (gameState == GameState.PLAYING)

                return true;
            else
                return false;
        }

        public bool IsGameOver()
        {
            if (gameState == GameState.GAMEOVER)
                return true;
            else
                return false;
        }

        // Encapsulation
        public bool WithGrab
        {
            get
            {
                return withGrab;
            }

            set
            {
                withGrab = value;
            }
        }

        public bool WithFlexionExtension
        {
            get
            {
                return withFlexionExtension;
            }

            set
            {
                withFlexionExtension = value;
            }
        }

        public bool WithPronation
        {
            get
            {
                return withPronation;
            }

            set
            {
                withPronation = value;
            }
        }

        public bool WithBothHands
        {
            get
            {
                return withBothHands;
            }

            set
            {
                withBothHands = value;
            }
        }

        public bool IsRightHand
        {
            get
            {
                return isRightHand;
            }

            set
            {
                isRightHand = value;
            }
        }

        public float Flexion
        {
            get
            {
                return flexion;
            }

            set
            {
                flexion = value;
            }
        }

        public float Extension
        {
            get
            {
                return extension;
            }

            set
            {
                extension = value;
            }
        }


    }
}