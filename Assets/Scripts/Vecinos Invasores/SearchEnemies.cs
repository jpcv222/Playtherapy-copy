using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
public class SearchEnemies : MonoBehaviour {


	ProximityDetector detector;

	// Use this for initialization
	void Start () {
		detector = GetComponent<ProximityDetector> ();
		GameObject[] enemies;

		enemies = GameObject.FindGameObjectsWithTag ("Enemies");

		detector.TargetObjects = enemies;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
