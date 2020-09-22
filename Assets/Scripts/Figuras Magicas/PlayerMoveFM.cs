using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerControllerFM))]
public class PlayerMoveFM : MonoBehaviour {

	Camera viewCamera;
	public float moveSpeed = 5;
	PlayerControllerFM controller;


	bool use_kinect;
	// Use this for initialization

	void Awake()
	{
		use_kinect = FindObjectOfType<GesturesShapeManager> ().use_kinect;
	}

	void Start () {
		
		controller = GetComponent<PlayerControllerFM> ();
		viewCamera = Camera.main;

	}

	// Update is called once per frame
	void Update () {

		if (use_kinect==false) {
			float moveX = Input.GetAxisRaw ("Horizontal");
			float moveZ = Input.GetAxisRaw ("Vertical");
			Vector3 moveInput = new Vector3 (moveX, 0, moveZ);
			Vector3 moveVelocity = moveInput.normalized * moveSpeed;



			controller.Move (moveVelocity);
		}



		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up,Vector3.zero);

		float rayDistance;

		if (groundPlane.Raycast(ray,out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			Debug.DrawLine (ray.origin, point, Color.red);
			controller.LookAt(point);
		}

	}

}
