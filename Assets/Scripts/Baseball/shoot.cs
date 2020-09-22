using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
public class shoot : MonoBehaviour {


	public GameObject Cannon;

	public GameObject Ball;

	public float force;

	float currenTime=0;
	float maxTime = 2;



	// Use this for initialization
	void Start () {

		InvokeRepeating ("Disparo", 4f, 4f);
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}


	void Disparo(){

		GameObject Temporary_Bullet_Handler;
		Temporary_Bullet_Handler = Instantiate (Ball, Cannon.transform.position, Cannon.transform.rotation) as GameObject;

		Temporary_Bullet_Handler.transform.Rotate (Vector3.left * 30);
		System.Random rany = new System.Random();
		System.Random ranx = new System.Random();

		int pivy = rany.Next(75, 90);
		int pivx = 0;

		double anglex = 0;
		double angley = 0;

		if (pivy < 80) {
			pivx = ranx.Next (80, 100);

			anglex = pivx * Math.PI / 180;
			angley = pivy * Math.PI / 180;

		} else {

			pivx = ranx.Next (0, 2);

			if (pivx == 1) {

				anglex = 100 * Math.PI / 180;
				angley = pivy * Math.PI / 180;
			}
			if (pivx == 0) {

				anglex = 80 * Math.PI / 180;
				angley = pivy * Math.PI / 180;
			}
		}






		Temporary_Bullet_Handler.GetComponent<Rigidbody> ().velocity = new Vector3(System.Convert.ToSingle(Math.Cos(anglex)),System.Convert.ToSingle(Math.Cos(angley)),1) * force;



		Destroy (Temporary_Bullet_Handler, 10.0f);


	}





}

