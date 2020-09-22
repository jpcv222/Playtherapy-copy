using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerControllerFM : MonoBehaviour {

	public Vector3 velocity;
	Rigidbody myRigidbody;
	public Animator controlAnim;
	void Start () {
		controlAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
	}


	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
		//print ("speed: "+_velocity.magnitude);
		controlAnim.SetFloat ("Speed", _velocity.magnitude );
	}
	public void LookAt(Vector3 point)
	{
		Vector3 heighCorrected = new Vector3 (point.x,transform.position.y, point.z);
		transform.LookAt (heighCorrected);
	}
	public void FixedUpdate()
	{
		//myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
		myRigidbody.MovePosition (myRigidbody.position + velocity*Time.fixedDeltaTime);
	}

}
