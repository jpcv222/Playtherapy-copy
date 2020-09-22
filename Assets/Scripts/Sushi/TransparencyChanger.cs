using UnityEngine;
using System.Collections;

public class TransparencyChanger : MonoBehaviour {

	public float alphaPerSecond = 0.2f;

	// Update is called once per frame
	void Update () {
		//Gets the current mesh renderer full color
		Color currentColor = GetComponent<MeshRenderer> ().material.color;
		if (currentColor.a <= 0.0f) {
			Destroy (gameObject);
		} else {
			//Calculates the new alpha of the Mesh renderer
			float alphaToApply = GetComponent<MeshRenderer> ().material.color.a - (alphaPerSecond * Time.deltaTime);
			if (alphaToApply < 0.0f) {
				alphaToApply = 0.0f;
			}
			currentColor.a = alphaToApply;
			//Applies the new alpha to the color
			GetComponent<MeshRenderer> ().material.color = currentColor;
		}
	}
}
