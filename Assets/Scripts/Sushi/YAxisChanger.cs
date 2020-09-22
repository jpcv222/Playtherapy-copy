using UnityEngine;
using System.Collections;

public class YAxisChanger : MonoBehaviour {

	public float metersPerSecond = 0.34f;

	// Update is called once per frame
	void Update () {
		//Applies the metersPerSecond to the y component of the position
		gameObject.transform.Translate(Vector3.up * metersPerSecond * Time.deltaTime);
	}
}
