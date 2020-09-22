using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
public class AnimalData : MonoBehaviour {


	IEnumerator JumpTimeFactor()
	{
		if (gameObject.activeSelf==true && isGrounded && isCaught==false) {
			isGrounded = false;
			Rigidbody rig = GetComponent<Rigidbody> ();

			yield return new WaitForSeconds(Random.Range(0f,2f));
			rig.AddForce (Vector3.up*200);
			yield break;
		}
	}

	public bool isCaught = false;
	public bool isChasen = false;


	public bool isGrounded=true;

	public void jumpAction()
	{
		StartCoroutine(JumpTimeFactor());
	}
	void OnTriggerEnter(Collider other)
	{


		if (other.name == "GrassCollider") {

			isGrounded = true;
		}

	}
}
