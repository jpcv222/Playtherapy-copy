using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class SpawnGameObjects : MonoBehaviour
    {
        // public variables
        //public float secondsBetweenSpawning = 0.1f;
        //public float xMinRange = -25.0f;
        //public float xMaxRange = 25.0f;
        //public float yMinRange = 8.0f;
        //public float yMaxRange = 25.0f;
        //public float zMinRange = -25.0f;
        //public float zMaxRange = 25.0f;
        public GameObject[] spawnObjects; // what prefabs to spawn

        //private float nextSpawnTime;

        public GameObject panelIzq;
        public GameObject panelDer;

        public GameObject leftLight;
        public GameObject rightLight;

        float angle;
        public GameAngles calc;
        private Vector3 pointFin;
        private bool side;
        GameManagerSushi gameM;

        private float upTime = 0.0f;
        private float flTime = 0.0f;


        // Use this for initialization
        void Start()
        {
            // determine when to spawn the next object
            //nextSpawnTime = Time.time+secondsBetweenSpawning;
            gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
            if (gameM)
            {
                Debug.Log("Entra a mirar gms");
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            // exit if there is a game manager and the game is over
            if (gameM)
            {
                if (gameM.gameIsOver || !gameM.gameIsStarted)
                    return;
            }
            /*
            // if time to spawn a new game object
            if (Time.time  >= nextSpawnTime) {
                // Spawn the game object through function below
                MakeThingToSpawn ();

                // determine the next time to spawn the object
                nextSpawnTime = Time.time+secondsBetweenSpawning;
            }*/
        }

        public void SetTimes(float fTime, float uTime)
        {
            flTime = fTime;
            upTime = uTime;
        }

        public void MakeThingToSpawn()
        {
			angle = (180 * gameM.level) / 6;
			calc = new GameAngles(angle, true, true);
            //Vector3 spawnPosition;

			/*
            System.Random rnd = new System.Random();
            int sideNumber = rnd.Next(1);
            if (sideNumber == 0)
            {
                side = true;
            }
            else
            {
                side = false;
            }*/
			int objectToSpawn = 0;
			string hand = "";

            if (side)
            {
                Debug.Log("lado der");
                shootPosition("ShoulderRight", "HandRight", "left");
				side = false;
				objectToSpawn = 1;
				hand = "right";
            }
            else
            {
                Debug.Log("lado izq");
                shootPosition("ShoulderLeft", "HandLeft", "right");
				side = true;
				objectToSpawn = 0;
				hand = "left";
            }

            // get a random position between the specified ranges
            //      spawnPosition.x = Random.Range (xMinRange, xMaxRange);
            //spawnPosition.y = Random.Range (yMinRange, yMaxRange);
            //spawnPosition.z = Random.Range (zMinRange, zMaxRange);

            // determine which object to spawn
            //int objectToSpawn = Random.Range(0, spawnObjects.Length);

            if (objectToSpawn == 0)
            {
                panelIzq.SetActive(true);
                panelDer.SetActive(false);

                leftLight.SetActive(true);
                rightLight.SetActive(false);
            } else
            {
                panelIzq.SetActive(false);
                panelDer.SetActive(true);

                leftLight.SetActive(false);
                rightLight.SetActive(true);
            }

            // actually spawn the game object
            GameObject spawnedObject = Instantiate(spawnObjects[objectToSpawn], pointFin, spawnObjects[objectToSpawn].transform.rotation) as GameObject;

            // make the parent the spawner so hierarchy doesn't get super messy
            spawnedObject.transform.parent = gameObject.transform;

            Debug.Log("Spawner: " + pointFin.ToString());
            spawnedObject.GetComponent<TargetMover>().StartMoving(pointFin, flTime, upTime);
			spawnedObject.GetComponent<TargetBehavior>().SetHand(hand);

        }

        public void shootPosition(string jointOneName, string jointTwoName, string side)
        {
			
            float angleRad = 0.0f;
            if (calc != null)
            {
                angleRad = calc.setRamdomAngle(side, "z");
            }
            else
            {
                Debug.Log("no existe");
            }


            Vector3 pointOne = GameObject.FindGameObjectWithTag(jointOneName).transform.position;
            Debug.Log("PointOne: " + pointOne);
            Vector3 pointTwo = GameObject.FindGameObjectWithTag(jointTwoName).transform.position;
            Debug.Log("PointTwo: " + pointTwo);


            pointFin = calc.getPositionWithCross(pointOne, calc.createPointTwoShoulderAF(pointOne, pointTwo), angleRad);
            Debug.Log("PointFin: " + pointFin);


        }

    }
}


