using UnityEngine;
using System.Collections;

public class RotateBasketAnimation : MonoBehaviour {

	GameObject cam;
	GameObject basket;

	Vector3 deltaRotation;
	Vector3 deltaPosition;

	public int status;
	public float timer;
	public float totalTime = 10;
    public float speed = 1;


	// Use this for initialization
	void Start () {
		status = 0;
		cam = GameObject.Find("Camera");
		basket = GameObject.Find("Basket");
		deltaPosition = basket.transform.position+ new Vector3(-2.0f,5.0f,0.0f);
		deltaRotation = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
	}



	public void orbitAround()
	{
//		cam.transform.RotateAround (basket.transform.position, Vector3.up, 50 * Time.deltaTime);
		cam.transform.RotateAround (basket.transform.position, Vector3.up, speed * timer/totalTime * Time.deltaTime);
	}
	public void startAnimation()
	{
		gameObject.GetComponent<GameManagerAtrapalo> ().putBallsCount ();
		status = 1;	
		timer = totalTime;
		cam.transform.position = Vector3.MoveTowards (cam.transform.position, deltaPosition, 10.0f*Time.deltaTime);
		cam.transform.LookAt (basket.transform.position);

	}

	// Update is called once per frame
	void Update () {
		if (status ==1) 
		{
			orbitAround ();

			timer -= Time.deltaTime;

			if (timer<=0) {
				status = 0;
				gameObject.GetComponent<GameManagerAtrapalo> ().EndGame ();

			}
		}

	}
}
