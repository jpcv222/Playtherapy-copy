using UnityEngine;
using System.Collections;

public class SpawnGameObjectsBall : MonoBehaviour
{
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xBallPos= -14.0f;
	public float yBallPos = 0.0f;
	public float zBallPos = 15.0f;
	int n = 0;
	public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;
	public bool can_trow = true;
	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManagerAtrapalo.gms) {
			if (GameManagerAtrapalo.gms.gameIsOver || !GameManagerAtrapalo.gms.gameIsStarted)
				return;
		}
		
		// if time to spawn a new game object
//		if (((Time.time  >= nextSpawnTime && GameManagerAtrapalo.gms.remainingReps>0&&!GameManagerAtrapalo.gms.withTime)||(Time.time  >= nextSpawnTime && GameManagerAtrapalo.gms.currentTime>0&&GameManagerAtrapalo.gms.withTime))&&(Time.time >= nextSpawnTime && GameManagerAtrapalo.gms.ballsAlive == 0)) {
		//		if (((Time.time  >= nextSpawnTime 
		//&& GameManagerAtrapalo.gms.remainingReps>0
		//&& !GameManagerAtrapalo.gms.withTime)
		//|| (Time.time  >= nextSpawnTime 
		//&& GameManagerAtrapalo.gms.currentTime>0
		//&&GameManagerAtrapalo.gms.withTime))&&(Time.time >= nextSpawnTime && GameManagerAtrapalo.gms.ballsAlive == 0)) {

		//
		if (GameManagerAtrapalo.gms.withTime) {

			if (GameManagerAtrapalo.gms.currentTime>0) 
			{
				launchBall ();
			}

		}
		else
		{
			launchBall ();
		}
			
	}
	public void launchBall()
	{
		if(can_trow && Time.time  >= nextSpawnTime && GameManagerAtrapalo.gms.repetitions >0)	//
		{
			// Spawn the game object through function below
			MakeThingToSpawn ();
			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
			can_trow = false;	
		}
	}
	//Function to return the number of the kind of the ball
	public int chooseBall(int ball){

		if (ball <= 60) {
			return 0;
		}
		if (ball > 60 && ball <= 80) {
			return 1;
		}
		if (ball > 80 && ball <= 100) {
			return 2;
		}
		return 0;

	}

	public void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		// get a random position between the specified ranges
		spawnPosition.x = xBallPos;
		spawnPosition.y = yBallPos;
        if (GameManagerAtrapalo.gms.side)
        {
            spawnPosition.z = zBallPos;
        }else
        {
            spawnPosition.z = -1*zBallPos;

        }


        // determine which object to spawn
        int objectToSpawn = this.chooseBall(Random.Range (0, 100));

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, spawnObjects[objectToSpawn].transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;

	}



}
