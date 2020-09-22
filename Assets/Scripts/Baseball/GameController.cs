﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //public GameObject Camera_inv;
    private Vector3 vector;
    private Vector3 vectortest;
    public bool indicatorplayer;
    public Text HelpText;
    public GameObject positionIndicator;
    public GameObject ParametersBaseball;

    public bool HandInPosition;

    public GameObject ExtensionShoulderLeft;
    public GameObject ExtensionShoulderRight;
    public InputField inputObservation;

    public GameObject PausePanel;
    public GameObject TutorialMenu;
    public GameObject Eraser;
    public GameObject ConfirmationModal;
    public Text ConfirmationTextModal;
    GameObject particulas;
    public static GameController gc;
    Vector3 initialposition;

    GameObject target;

    //public GameObject indicator;

    //
    float numberRepetitions;
    float game_mode;
    float ArmSelection;
    public GameObject LeftShoulder;
    public GameObject RightShoulder;

    public GameObject danceUnityChan;
    public GameObject danceTaichi;
    public GameObject Camera;

    public Text textCurrentTime;
    public Slider sliderCurrentTime;
    public Text sliderText;


    public GameObject ParametersPanel;
    public GameObject TutorialPanel;
    public GameObject MainPanel;
    public GameObject ResultPanel;
    public GameObject pausa;
    public GameObject ObservationPanel;
    public GameObject ConfirmationModalMenu;
    public GameObject EndModal;
    public Button boton;
    public GameObject btObservation;

    public bool InGame;
    public bool GameOver;
    public bool progress;

    public GameObject Cannon;

    public GameObject Ball;

    public Animator pitcher;

    public GameObject test;

    public GameObject catcher;

    public GameObject PlayerCenter;
    public GameObject RealPlayerCenter;
    public GameObject RealPlayerLeft;
    public GameObject RealPlayerRight;

    public GameObject PhantomRight;
    public GameObject PhantomLeft;
    public GameObject catcherLefthand;
    public GameObject catcherRighthand;

    private RUISSkeletonManager skeletonManager;

    public float rate = 0f;

    float force = 50;

    float shootTime = 5.2f;

    float _time_game;

    float _repetitions;

    public float currentRepetitions;
    public GameObject array_balls;









    float _velocity_game;




    public GameObject rightHandPraticles;
    public GameObject positionParticles;
    public int selectArm;

    float _range_game;

    float _angleMinRight;
    float _angleMinLeft;
    float radius;






    float _angleRight;
    float _angleLeft;



    float finalTotalTime;
    float finalTotalRepetition;






    public bool movimientoLateral;

    private float time;
    public float maxAngle;

    private float currentTime;
    private float timeMillis;
    public float totalTime;

    PutDataResults results;
    public Text finalResult;
    public float lanzamiento;
    public Text total;

    public AudioClip catcher_sound;


    float maxTime = 2;

    public Text scoretext;

    private int score;

    public bool pivote;
    public bool send;





    public GameObject kinectPlayer;
    public bool left = false;
    public bool right = false;
    public string movement;


    void Reset()
    {

        target = catcher;
    }

    void realRetry()
    {
        Reset();
        Start();
    }


    // Use this for initialization
    void Start()
    {


        //Camera_inv = GameObject.Find("Camera Panel");
        //Camera_inv.SetActive(false);
        HandInPosition = false;
        Eraser.SetActive(false);
        RUISSkeletonController[] kinectPlayer1 = kinectPlayer.GetComponentsInChildren<RUISSkeletonController>();
        if (PlaylistManager.pm == null || (PlaylistManager.pm != null && !PlaylistManager.pm.active))
        {
            ParametersPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
        kinectPlayer1[0].updateRootPosition = true;
        indicatorplayer = false;
        initialposition = kinectPlayer.transform.position;

        gc = gameObject.GetComponent<GameController>();


        if (skeletonManager == null)
        {
            skeletonManager = FindObjectOfType(typeof(RUISSkeletonManager)) as RUISSkeletonManager;
            if (!skeletonManager)
                Debug.Log("The scene is missing " + typeof(RUISSkeletonManager) + " script!");


        }

        danceTaichi.SetActive(false);
        danceUnityChan.SetActive(false);
        Camera.transform.position = new Vector3(35f, 31.83f, 450.62f);
        positionIndicator.SetActive(true);
        //indicator.SetActive(true);

        //Camera.transform.position = new Vector3(0f, 31.83f,134.62f);

        vector = new Vector3(0, 0, 0);
        vectortest = new Vector3(0, 0, 0);

        score = 0;
        UpdateScore();
        InGame = false;
        MainPanel.SetActive(false);
        pausa.SetActive(false);
        ResultPanel.SetActive(false);
        movimientoLateral = false;
        force = 20;
        /*if(_angleMinLeft == 0){

			_angleMinLeft = 25;
		}
		if(_angleLeft == 0){

			_angleLeft = 25;
		}*/
        if (_range_game == 0)
        {

            _range_game = 25;
        }
        movimientoLateral = true;

        results = ResultPanel.GetComponent<PutDataResults>();

        //results = FindObjectOfType<PutDataResults> ();
        //Button btn = boton.GetComponent<Button> ();
        //btn.onClick.AddListener(StartGame);

        //sliderCurrentTime.onValueChanged.AddListener(delegate {SlideTime(); });
    }

    public void retry()
    {
        //Camera_inv.SetActive(false);
        ConfirmationModal.SetActive(false);
        HandInPosition = false;
        Eraser.SetActive(true);
        pitcher.Play("New State");
        gc = gameObject.GetComponent<GameController>();
        RUISSkeletonController[] kinectPlayer1 = kinectPlayer.GetComponentsInChildren<RUISSkeletonController>();
        kinectPlayer1[0].updateRootPosition = true;

        if (skeletonManager == null)
        {
            skeletonManager = FindObjectOfType(typeof(RUISSkeletonManager)) as RUISSkeletonManager;
            if (!skeletonManager)
                Debug.Log("The scene is missing " + typeof(RUISSkeletonManager) + " script!");
        }
        danceTaichi.SetActive(false);
        danceUnityChan.SetActive(false);
        Camera.transform.position = new Vector3(35f, 31.83f, 450.62f);
        //indicator.SetActive(true);
        //Camera.transform.Translate(0f, 0.83f,0 - 10.62f
        vector = new Vector3(0, 0, 0);
        vectortest = new Vector3(0, 0, 0);
        positionIndicator.SetActive(true);

        score = 0;
        UpdateScore();
        InGame = false;
        MainPanel.SetActive(false);
        pausa.SetActive(false);
        ResultPanel.SetActive(false);
        ParametersPanel.SetActive(true);
        movimientoLateral = false;
        force = 20;
        /*if(_angleMinLeft == 0){

			_angleMinLeft = 25;
		}
		if(_angleLeft == 0){

			_angleLeft = 25;
		}*/
        if (_range_game == 0)
        {

            _range_game = 25;
        }
        results = ResultPanel.GetComponent<PutDataResults>();
        lanzamiento = 0;
        movimientoLateral = true;
        //results = FindObjectOfType<PutDataResults> ();
        //Button btn = boton.GetComponent<Button> ();
        //btn.onClick.AddListener(StartGame);
    }








    // Update is called once per frame
    void Update()
    {

        //print(HandInPosition);
        //print(indicatorplayer);

        if (indicatorplayer)
        {

            HelpText.text = "Posición Correcta";

        }
        else
        {


            HelpText.text = "Posición Incorrecta";
        }


        if (InGame)
        {

            if (numberRepetitions == 0)
            {

                currentTime -= Time.deltaTime;
                if (currentTime > 0 && numberRepetitions == 0)
                {
                    timeMillis -= Time.deltaTime * 1000;
                    if (timeMillis < 0)
                        timeMillis = 1000f;
                    textCurrentTime.text = (((int)currentTime) / 60).ToString("00") + ":"
                        + (((int)currentTime) % 60).ToString("00") + ":"
                        + ((int)(timeMillis * 60 / 1000)).ToString("00");
                    sliderCurrentTime.value = currentTime * 100 / totalTime;


                }
                else
                {


                    textCurrentTime.text = "00:00:00";

                }
            }

            if (numberRepetitions == 1)
            {

                textCurrentTime.text = currentRepetitions + " Restante";


            }




            Time.timeScale = 1;
            if (numberRepetitions == 1)
            {
                if (currentRepetitions <= 0 && array_balls.transform.childCount == 0 && !progress)
                {

                    danceTaichi.SetActive(true);
                    danceUnityChan.SetActive(true);
                    GameOver = true;

                    faseFinal();


                }
            }
            else
            {

                if (currentTime <= 0 && array_balls.transform.childCount == 0 && !progress)
                {
                    danceTaichi.SetActive(true);
                    danceUnityChan.SetActive(true);
                    GameOver = true;
                    faseFinal();


                }
            }
            if (Time.time > shootTime && !GameOver && !progress)
            {

                if (array_balls.transform.childCount == 0)
                {
                    InvokeRepeating("Lanzar", 0f, 0f);
                }

                shootTime = shootTime + rate;

            }






        }
        else
        {

            //Time.timeScale = 0;
        }



    }
    public void EndGame()
    {
        /*_angleMinLeft = 0;
        sliderMinLeft.value = 0;
        sliderLeft.value = 0;
        _angleLeft = 0;*/

        MainPanel.SetActive(false);
        pausa.SetActive(false);
        ResultPanel.SetActive(true);
        danceTaichi.SetActive(false);
        danceUnityChan.SetActive(false);
        StopAllCoroutines();
        InGame = false;
        int result = Mathf.RoundToInt((score / lanzamiento) * 100);
        string idMinigame = "1";
        //print(lanzamiento);
        movimientoLateral = false;
        int angle = (int)_angleLeft;
        GameSessionController gameCtrl = new GameSessionController();
        gameCtrl.addGameSession(score, this.FinalTotalRepetition, this.FinalTotalTime, result, idMinigame);
        PerformanceController performanceCtrl = new PerformanceController();
        performanceCtrl.addPerformance(angle, this.GetMovement());
        results = ResultPanel.GetComponent<PutDataResults>();
        results.Minigame = idMinigame;
        results.updateData(result, 0);


        if (PlaylistManager.pm != null && PlaylistManager.pm.active)

        {
            PlaylistManager.pm.NextGame();
        }

    }


    void faseFinal()
    {
        Camera.transform.position = new Vector3(53f, 70f, 30.62f);
        movimientoLateral = false;
        RUISSkeletonController[] kinectPlayer1 = kinectPlayer.GetComponentsInChildren<RUISSkeletonController>();
        kinectPlayer1[0].updateRootPosition = movimientoLateral;

        StartCoroutine(animacionFinal());
    }

    IEnumerator animacionFinal()
    {


        yield return new WaitForSeconds(5f);


        EndGame();

    }




    public void UpdateSlide()
    {

        //Debug.Log(sliderCurrentTime.value);
    }


    public void TutorialPhase()
    {

        ParametersPanel.SetActive(false);
        TutorialPanel.SetActive(true);

    }

    public void TutorialPhaseFromMenu()
    {

        PausePanel.SetActive(false);
        TutorialMenu.SetActive(true);

    }

    public void EndTutorialFromMenu()
    {
        PausePanel.SetActive(true);
        TutorialMenu.SetActive(false);
    }

    public void PauseOn()
    {
        InGame = false;
        PausePanel = GameObject.Find("pause_data");



    }

    public void StartAgain()
    {

        pausa.SetActive(false);



        retry();
    }
    public void PauseOff()
    {

        InGame = true;

    }


    public void EndTutorial()
    {
        ParametersPanel.SetActive(true);
        TutorialPanel.SetActive(false);
    }

    public void StartGame(float Velocity, float rangeParam, bool lateralmovement, float numberrepetitions, float timegame, float repetitions, float forces, float anglemin, float anglemax, float gamemode, float armselection)
    {

        TutorialMenu.SetActive(false);
        Eraser.SetActive(false);

        InGame = true;
        GameOver = false;
        force = Velocity;
        _range_game = rangeParam;
        game_mode = gamemode;
        ArmSelection = armselection;
        positionIndicator.SetActive(false);
        //indicator.SetActive(false);

        movimientoLateral = lateralmovement;
        GameOver = false;
        MainPanel.SetActive(true);
        pausa.SetActive(true);
        ParametersPanel.SetActive(false);
        numberRepetitions = numberrepetitions;
        FinalTotalTime = timegame;
        FinalTotalRepetition = repetitions;
        SetMovement(gamemode);

        if (numberrepetitions == 0)
        {

            currentTime = timegame * 60;

        }
        if (numberrepetitions == 1)
        {

            currentRepetitions = repetitions;

        }

        if (forces == 0)
        {

            force = 20;
        }
        //if (numberRepetitions.value == 0 
        if (numberrepetitions == 0 && currentTime == 0)
        {

            currentTime = 60;

            //currentRepetitions = 1;
        }
        if (numberrepetitions == 1 && currentRepetitions == 0)
        {

            currentRepetitions = 1;
            //currentTime = 90000000000;
        }

        _angleMinLeft = anglemin;

        _angleLeft = anglemax;




        if (_angleMinLeft > _angleLeft)
        {

            _angleLeft = _angleMinLeft + 1;
        }


        RUISSkeletonController[] kinectPlayer1 = kinectPlayer.GetComponentsInChildren<RUISSkeletonController>();
        kinectPlayer1[0].updateRootPosition = movimientoLateral;


    }

    void Lanzar()
    {


        if (InGame)
        {

        }
        if (numberRepetitions == 0)
        {

            if (currentTime > 0)
            {
                StartCoroutine(Disparo());
            }
        }
        else
        {

            if (currentRepetitions > 0)
            {

                StartCoroutine(Disparo());

            }
        }




    }

    IEnumerator Disparo()
    {
        progress = true;
        pitcher.Play("Throw");
        lanzamiento = lanzamiento + 1;






        if (game_mode == 1)
        {



            double posX = 0;
            double posY = 0;
            double posZ = 0;
            double posXpart = 0;
            double posYpart = 0;
            double posZpart = 0;
            double angleRandom = 0;
            System.Random ranz = new System.Random();
            System.Random ranxy = new System.Random();
            System.Random ranxx = new System.Random(DateTime.Now.Millisecond);
            angleRandom = (_angleMinLeft + ranxx.NextDouble() * (_angleLeft - _angleMinLeft));

            selectArm = ranz.Next(1, 100);

            if (ArmSelection == 1)
            {

                //Derecho
                selectArm = 30;
            }
            if (ArmSelection == 2)
            {
                //Izquierdo
                selectArm = 80;

            }

            if (selectArm <= 50)
            {

                //angleRandom = (115+ _angleMinRight) + ranyy.NextDouble ()*((_angleRight+115) - (115+_angleMinRight));






                //angleRandom = -(_angleMinLeft + ranxy.NextDouble() * (_angleLeft - _angleMinLeft));


                Destroy(Instantiate(rightHandPraticles, catcherRighthand.transform.position, Quaternion.identity), 2.0f);
                posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * 35;
                posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * 35;
                posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * ((radius / 10) + 7);
                posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * ((radius / 10) + 7);

                vectortest = new Vector3((float)ExtensionShoulderRight.transform.position.x, (float)(ExtensionShoulderRight.transform.position.y - posYpart), (float)(ExtensionShoulderRight.transform.position.z - posX));
                vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(vectortest.z - Cannon.transform.position.z)).normalized;//force

                particulas = Instantiate(positionParticles, new Vector3((float)ExtensionShoulderRight.transform.position.x, (float)(ExtensionShoulderRight.transform.position.y - posYpart), (float)(ExtensionShoulderRight.transform.position.z - posXpart)), Quaternion.identity) as GameObject;
                //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);


                Destroy(particulas, 10);






            }
            else
            {






                Destroy(Instantiate(rightHandPraticles, catcherLefthand.transform.position, Quaternion.identity), 2.0f);
                posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * 35;
                posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * 35;
                posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * ((radius / 10) + 7);
                posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * ((radius / 10) + 7);

                vectortest = new Vector3((float)ExtensionShoulderLeft.transform.position.x, (float)(ExtensionShoulderLeft.transform.position.y - posYpart), (float)(ExtensionShoulderLeft.transform.position.z - posX));
                vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(vectortest.z - Cannon.transform.position.z)).normalized;//force

                particulas = Instantiate(positionParticles, new Vector3((float)ExtensionShoulderLeft.transform.position.x, (float)(ExtensionShoulderLeft.transform.position.y - posYpart), (float)(ExtensionShoulderLeft.transform.position.z - posXpart)), Quaternion.identity) as GameObject;
                //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);


                Destroy(particulas, 10);

            }










            //lanzamiento = lanzamiento + 1;
            DecrementRepetitions();
        }





        if (game_mode == 0)
        {
            radius = _range_game;

            double posX = 0;
            double posY = 0;
            double posXpart = 0;
            double posYpart = 0;
            float posZ = 0;

            System.Random sel = new System.Random();

            int pos = 0;

            if (movimientoLateral)
            {
                System.Random rand = new System.Random();

                pos = rand.Next(0, 2);

                if (ArmSelection == 2)
                {

                    //Derecho
                    pos = 0;
                    selectArm = 30;

                }
                if (ArmSelection == 1)
                {
                    //Izquierdo
                    pos = 1;
                    selectArm = 80;

                }



            }
            else
            {
                pos = 2;
            }




            double angleRandom = 0;

            if (pos == 0)
            {
                selectArm = 30;

                System.Random ranyy = new System.Random(DateTime.Now.Millisecond);

                angleRandom = _angleMinLeft + ranyy.NextDouble() * (_angleLeft - _angleMinLeft);
                Destroy(Instantiate(rightHandPraticles, catcherLefthand.transform.position, Quaternion.identity), 2.0f);

                posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * radius;
                posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * radius;
                posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                particulas = Instantiate(positionParticles, new Vector3((float)(RealPlayerLeft.transform.position.x - posXpart), (float)(RealPlayerLeft.transform.position.y - posYpart), (float)RealPlayerLeft.transform.position.z), Quaternion.identity) as GameObject;
                selectArm = 70;

                //virtual rehab revisar 
                vectortest = new Vector3((float)(RealPlayerLeft.transform.position.x - posXpart), (float)(RealPlayerLeft.transform.position.y - posYpart), (float)RealPlayerLeft.transform.position.z);
                vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(RealPlayerRight.transform.position.z - Cannon.transform.position.z)).normalized;//force


                //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);
                Destroy(particulas, 10.0f);

                //lanzamiento = lanzamiento + 1;

            }
            if (pos == 1)
            {//desplaz
                selectArm = 80;
                System.Random ranxx = new System.Random(DateTime.Now.Millisecond);
                angleRandom = -(_angleMinLeft + ranxx.NextDouble() * (_angleLeft - _angleMinLeft));

                selectArm = 25;

                Destroy(Instantiate(rightHandPraticles, catcherRighthand.transform.position, Quaternion.identity), 2.0f);

                posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * radius;
                posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * radius;
                posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                particulas = Instantiate(positionParticles, new Vector3((float)(RealPlayerRight.transform.position.x - posXpart), (float)(RealPlayerRight.transform.position.y - posYpart), (float)RealPlayerRight.transform.position.z), Quaternion.identity) as GameObject;

                //var vectortest = new Vector3((float)(RightShoulder.transform.position.x ), (float)(RightShoulder.transform.position.y ), (float)RightShoulder.transform.position.z);
                // var vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)vectortest.z).normalized * force;//force

                vectortest = new Vector3((float)(RealPlayerRight.transform.position.x - posXpart), (float)(RealPlayerRight.transform.position.y - posYpart), (float)RealPlayerRight.transform.position.z);
                vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(RealPlayerRight.transform.position.z - Cannon.transform.position.z)).normalized;//force

                //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);
                Destroy(particulas, 10.0f);

                //lanzamiento = lanzamiento + 1;
            }
            if (pos == 2)
            {//tirar al centro
                System.Random selection = new System.Random();
                System.Random ranyy = new System.Random();
                System.Random ranxy = new System.Random();
                System.Random ranz = new System.Random();

                int select = ranz.Next(1, 100);


                if (ArmSelection == 1)
                {
                    //Derecho
                    select = 30;
                    selectArm = 30;

                }
                if (ArmSelection == 2)
                {
                    //Izquierdo
                    select = 80;
                    selectArm = 80;

                }


                if (select <= 50)
                {

                    //angleRandom = (115+ _angleMinRight) + ranyy.NextDouble ()*((_angleRight+115) - (115+_angleMinRight));
                    //https://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values

                    selectArm = select;

                    System.Random ranxxy = new System.Random(DateTime.Now.Millisecond);

                    angleRandom = -(_angleMinLeft + ranxxy.NextDouble() * (_angleLeft - _angleMinLeft));
                    //print(angleRandom);


                    Destroy(Instantiate(rightHandPraticles, catcherRighthand.transform.position, Quaternion.identity), 2.0f);
                    posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * radius;
                    posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * radius;
                    posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                    posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                    vectortest = new Vector3((float)(RightShoulder.transform.position.x - posXpart), (float)(RightShoulder.transform.position.y - posYpart), (float)RightShoulder.transform.position.z);
                    vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(RightShoulder.transform.position.z - Cannon.transform.position.z)).normalized;//force

                    particulas = Instantiate(positionParticles, new Vector3((float)(RightShoulder.transform.position.x - posXpart), (float)(RightShoulder.transform.position.y - posYpart), (float)RightShoulder.transform.position.z), Quaternion.identity) as GameObject;
                    //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);


                    Destroy(particulas, 10);


                }
                else
                {

                    selectArm = select;
                    System.Random ranxxy = new System.Random(DateTime.Now.Millisecond);

                    angleRandom = (_angleMinLeft + ranxxy.NextDouble() * (_angleLeft - _angleMinLeft));
                    //print(angleRandom);



                    Destroy(Instantiate(rightHandPraticles, catcherLefthand.transform.position, Quaternion.identity), 2.0f);


                    posX = Math.Cos((angleRandom + 90) * Math.PI / 180) * radius;
                    posY = Math.Sin((angleRandom + 90) * Math.PI / 180) * radius;
                    posXpart = Math.Cos((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                    posYpart = Math.Sin((angleRandom + 90) * Math.PI / 180) * (radius / 4);
                    //particulas = Instantiate(positionParticles, new Vector3((float)(LeftShoulder.transform.position.x - posXpart), (float)(LeftShoulder.transform.position.y - posYpart), (float)LeftShoulder.transform.position.z), Quaternion.identity) as gameObject;
                    vectortest = new Vector3((float)(LeftShoulder.transform.position.x - posXpart), (float)(LeftShoulder.transform.position.y - posYpart), (float)LeftShoulder.transform.position.z);
                    vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(LeftShoulder.transform.position.z - Cannon.transform.position.z)).normalized;//force

                    particulas = Instantiate(positionParticles, new Vector3((float)(LeftShoulder.transform.position.x - posXpart), (float)(LeftShoulder.transform.position.y - posYpart), (float)LeftShoulder.transform.position.z), Quaternion.identity) as GameObject;
                    //Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);

                    Destroy(particulas, 10);

                }
                //angleRandom = 0;


                //Instantiate (rightHandPraticles, new Vector3 ((float) (RealPlayerCenter.transform.position.x-posX), (float) (RealPlayerCenter.transform.position.y-posY),(float) RealPlayerCenter.transform.position.z), Quaternion.identity), 2.0f);
                //Instantiate (rightHandPraticles, new Vector3 ((float) (RealPlayerCenter.transform.position.x-posX), (float) (RealPlayerCenter.transform.position.y-posY),(float) RealPlayerCenter.transform.position.z), Quaternion.identity);

                /*var vector = new Vector3 ((float)(PlayerCenter.transform.position.x - posX), (float)(PlayerCenter.transform.position.y - posY), (float)PlayerCenter.transform.position.z).normalized * force;//force

				particulas = Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity) as GameObject;
				//Destroy (Instantiate (positionParticles,new Vector3 ((float)(RealPlayerCenter.transform.position.x - posXpart), (float)(RealPlayerCenter.transform.position.y-posYpart), (float)RealPlayerCenter.transform.position.z), Quaternion.identity),4.0f);
				Destroy(particulas,4.0f);
				Temporary_Bullet_Handler.GetComponent<Rigidbody> ().velocity = vector;*/



            }

            //lanzamiento = lanzamiento + 1;
            DecrementRepetitions();

        }


        yield return new WaitForSeconds(2.7f);

        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Ball, Cannon.transform.position, Cannon.transform.rotation) as GameObject;
        Temporary_Bullet_Handler.transform.parent = array_balls.transform;
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        vector = new Vector3((float)(vectortest.x - Cannon.transform.position.x), (float)(vectortest.y - Cannon.transform.position.y), (float)(LeftShoulder.transform.position.z - Cannon.transform.position.z)).normalized;
        Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = vector * force;


        //int pivy = rany.Next(80.0, 80.23);



        progress = false;

    }

    public void AddScore(int newscore)
    {
        score += newscore;
        UpdateScore();
    }
    void UpdateScore()
    {

        scoretext.text = "" + score;
    }

    public void DecrementRepetitions()
    {


        currentRepetitions = currentRepetitions - 1;

    }
    public void ActivateObservation()
    {
        ObservationPanel.SetActive(true);
        ResultPanel.SetActive(false);
    }


    public float FinalTotalTime
    {
        get
        {
            return finalTotalTime;
        }

        set
        {
            finalTotalTime = value;
        }
    }

    public float FinalTotalRepetition
    {
        get
        {
            return finalTotalRepetition;
        }

        set
        {
            finalTotalRepetition = value;
        }
    }

    public string GetMovement()
    {
        return movement;
    }
    public void SetMovement(float movementX)
    {

        if (movementX == 0)
        {

            movement = "3";
        }
        if (movementX == 1)
        {
            movement = "2";
        }
    }
    public void GetObservation()
    {
        // inputObservation.text 
    }

    public void ConfirmationModalOptions(int option)
    {
        if(option == 1)
        {
            ConfirmationModal.SetActive(true);
        }
        if (option == 2)
        {
            ConfirmationModalMenu.SetActive(true);
        }
        if (option == 3)
        {
            ConfirmationModal.SetActive(false);
        }
        if (option == 4)
        {
            ConfirmationModalMenu.SetActive(false);
        }
        if (option == 0)
        {
            EndModal.SetActive(true);
        }
        if (option == 6)
        {
            EndModal.SetActive(false);
        }
    }
}
