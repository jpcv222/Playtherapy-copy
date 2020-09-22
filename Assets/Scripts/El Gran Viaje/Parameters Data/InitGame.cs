using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InitGame : MonoBehaviour {

	List<MonoBehaviour> array_scrips_disabled;
	// Use this for initialization
	void Start () {
		array_scrips_disabled = new List<MonoBehaviour> ();
		array_scrips_disabled.Add (FindObjectOfType<SpannerOfMovements>());
		array_scrips_disabled.Add (FindObjectOfType<SpanwClouds>());
		array_scrips_disabled.Add (FindObjectOfType<PlaneController>());

	}
	public void StartGame()
	{

		foreach (MonoBehaviour behaviour in array_scrips_disabled)
		{
			behaviour.enabled = true;   
		}

	}
}
