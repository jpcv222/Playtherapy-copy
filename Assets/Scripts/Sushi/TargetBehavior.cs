using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;

	// explosion when hit?
	public GameObject explosionPrefab;

    private AudioSource explosionSound;

	// information when hit?
	public GameObject informationPrefab;

	private MovementDetectionLibrary.SpawnGameObjects spawner;
	private GameManagerSushi gameM;

	private SushiSpawner sSpawner;

	private string fishingHand;

    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();
        sSpawner = GameObject.Find("Spawner").GetComponent<SushiSpawner>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
        explosionSound = GameObject.Find("cutFish").GetComponent<AudioSource>();
	}

	public void SetHand(string side){
		fishingHand = side;
	}

	// when collided with another gameObject
	//void OnCollisionEnter (Collision newCollision)
	void OnTriggerEnter (Collider newCollision)
	{
		Debug.Log ("Colisiona");
		// exit if there is a game manager and the game is over
		if (GameManagerSushi.gms) {
			if (GameManagerSushi.gms.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if ((newCollision.gameObject.tag == "KatanaLeft" && fishingHand == "left")|| (newCollision.gameObject.tag == "KatanaRight" && fishingHand == "right")) {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			if (informationPrefab) {
				//Intantiate an information dialog at the gameObjects position and rotation
				Instantiate (informationPrefab, transform.position, GameObject.FindWithTag("MainCamera").transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManagerSushi.gms) {
				GameManagerSushi.gms.targetHit (scoreAmount);
			}
            
            // if explosion sound exists, make adjustments based on target properties
            if (explosionSound)
                explosionSound.Play();

            // destroy the projectile
            //Destroy (newCollision.gameObject);

            gameM.NewRepetition();

			if (gameM.withTime) {
				if (gameM.currentTime > 0.0f) {
					spawner.MakeThingToSpawn ();
				}
			} else {
				if (gameM.GetRepetitions () >= 0) {
					spawner.MakeThingToSpawn ();
				}
			}

			sSpawner.MakeSpawn();

            GameObject.Find("GameManager").GetComponent<PointFeedbackManager>().GreenPoint();
            // destroy self
            Destroy (gameObject);
		}
        else if ((newCollision.gameObject.tag == "KatanaLeft" && fishingHand == "right") || (newCollision.gameObject.tag == "KatanaRight" && fishingHand == "left"))
        {
            GameObject.Find("GameManager").GetComponent<PointFeedbackManager>().RedPoint();
        }

    }
}
