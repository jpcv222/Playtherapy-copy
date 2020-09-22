using UnityEngine;
using System.Collections;

public class ScaleChanger : MonoBehaviour {

	public float scalePerSecond = 0.34f;

	// Update is called once per frame
	void Update () {
		//Applies the metersPerSecond to the y component of the position
		transform.localScale += new Vector3 (scalePerSecond, scalePerSecond, scalePerSecond);
		//gameObject.transform.Sca(Vector3.up * metersPerSecond * Time.deltaTime);

	}
}
