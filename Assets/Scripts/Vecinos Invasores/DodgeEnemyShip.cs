using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemyShip : MonoBehaviour {

	float speed=250f;
	public float DodgeCoolDown = 0.5f;
	GameObject animal;
	Rigidbody rb_Animal;
	AnimalData data;
	float time_cool_down;
	// Use this for initialization
	void Start () 
	{
		time_cool_down = -1f;
		animal = transform.parent.gameObject;
		rb_Animal = animal.GetComponent<Rigidbody> ();
		data = animal.GetComponent<AnimalData> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		time_cool_down -= Time.deltaTime;

	}
	void OnTriggerEnter(Collider other)
	{
		
		if (data!=null) {
			if (time_cool_down<0) {

				if (other.tag=="Enemies") 
				{
					
					if (data.isCaught==false) 
					{

						rb_Animal.AddForce (rb_Animal.velocity.normalized * speed);
						time_cool_down = DodgeCoolDown;

					}
				}


			}

		}
	
	}
}
