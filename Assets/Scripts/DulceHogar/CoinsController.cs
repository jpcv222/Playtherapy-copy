using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeapAPI;

namespace CompleteProject
{
	public class CoinsController : MonoBehaviour
	{

		// Use this for initialization
		public GameObject start;
	
		public int scoreValue = 10;
		private PlayerController playerController;

		void Start ()
		{
			playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			
		}

		void OnTriggerEnter (Collider other)
		{
			
			if (other.gameObject.CompareTag ("Player")) {
				playerController.SetStop (true);
			//	if (new Movements ().Grab () > 0.5f) {
					ScoreManager.score += scoreValue;
					start.SetActive (false);
					playerController.SetStop (false);
			//	}
			}
		}
	}
}