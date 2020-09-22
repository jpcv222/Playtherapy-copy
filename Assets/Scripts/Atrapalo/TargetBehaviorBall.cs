using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MovementDetectionLibrary;
public class TargetBehaviorBall : MonoBehaviour
{

	// target impact on game
	public int scoreAmount;
	public float timeAmount = 0.0f;

	// explosion when hit?
	public GameObject explosionPrefab;
    public TextMesh textPoint;
    public Material mat;
    public Font font;

    // information when hit?
    public GameObject informationPrefab;


	private MovementDetectionLibrary.SpawnGameObjects spawner;
	private GameManagerAtrapalo gameM;

	private SpawnGameObjectsBall sSpawner;


	void Start()
	{
		spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();
		sSpawner = GameObject.Find("Spawner").GetComponent<SpawnGameObjectsBall>();
		gameM = GameObject.Find("GameManager").GetComponent<GameManagerAtrapalo>();
        if (scoreAmount > 0)
        {
            textPoint.text = "+"+scoreAmount.ToString();
        }
        else
        {
            textPoint.text =  scoreAmount.ToString();

        }

        textPoint.color = getColorBall(scoreAmount);
	}

	// when collided with another gameObject
	void OnTriggerEnter  (Collider newCollision)
	{

		// exit if there is a game manager and the game is over
		if (GameManagerAtrapalo.gms) {
			if (GameManagerAtrapalo.gms.gameIsOver)
				return;
		}
		if (newCollision.gameObject.tag == "Wall") {
			// destroy the projectile
			Debug.Log ("destruir pelota");
			Destroy(this.gameObject);//comentado para prueba de domenico
			sSpawner.can_trow = true;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "HandRight"||newCollision.gameObject.tag == "HandLeft")
		{
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
                Instantiate(textPoint, transform.position, textPoint.transform.rotation);
                // create 3d text mesh


            }

            /*if (informationPrefab) {
				//Intantiate an information dialog at the gameObjects position and rotation
				Instantiate (informationPrefab, transform.position, GameObject.FindWithTag("MainCamera").transform.rotation);
			}*/

            // if game manager exists, make adjustments based on target properties
            if (GameManagerAtrapalo.gms) {
				GameManagerAtrapalo.gms.targetHit (scoreAmount);
				putBallInBasket ();

			}
				

		}
	}

    private Color getColorBall(int score)
    {
        Color ballColor;

        switch (score)
        {
            case 1:
                ballColor = new Color(0, 95, 195, 1);
                break;
            case -2:
                ballColor = new Color(255, 0, 0, 1);
                break;
           case 3:
                ballColor = new Color(233, 255, 98, 1);
                break;
            default:
                ballColor = new Color(253, 0, 0, 1);
                break;
        }
        return ballColor;
    }

	void putBallInBasket()
	{		
		GameObject basket = GameObject.Find ("Basket");

		int ballsAlive = GameManagerAtrapalo.gms.ballsAlive - 1;

		GameManagerAtrapalo.gms.ballsAlive = ballsAlive;

		Debug.Log ("hola:"+basket.transform.localScale);
		float randomX = basket.transform.position.x+1*Random.Range (-1,1);
		float randomZ = basket.transform.position.z+1*Random.Range (-1,1);
		Vector3 position = new Vector3 (randomX, basket.transform.position.y +4, randomZ);

		this.transform.position = position;
		this.gameObject.GetComponent<Shoot>().enabled= false;
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.GetComponent<Rigidbody> ().useGravity = true;
		sSpawner.can_trow = true;
	}
}
