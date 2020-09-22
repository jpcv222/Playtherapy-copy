using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerFromDistance : MonoBehaviour {



	GameObject player;
	[Range(-1000,1000)]
	public float distanceFromPlayer=25;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		PutInRightPosition ();
	}
	public void PutInRightPosition()
	{
			player = GameObject.FindGameObjectWithTag ("Player");
			Vector3 oldPosition = this.transform.position;


			float posZ = player.transform.position.z+distanceFromPlayer;



			Vector3 nextPosition = new Vector3 (oldPosition.x,oldPosition.y,posZ);
			this.transform.position = nextPosition;



	}
}
