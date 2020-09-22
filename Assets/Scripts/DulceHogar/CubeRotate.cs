using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour {

	// Use this for initialization
	public float speed = 5;
	public GameObject ObjetoGame;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ObjetoGame.transform.Rotate (new Vector3(0.0f,45.0f,0.0f)* Time.deltaTime * speed);
	}
}
