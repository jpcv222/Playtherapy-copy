using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwClouds : MonoBehaviour {


	GameObject cloudsParentArray;
	[Range (0,40)]
	public int maxCloudsInScreen;
	[Range (1,60)]
	public float SecondsPerCloud;
	public GameObject[] clouds_types;
	// Use this for initialization

	float timer =0;
	void Start () {
		cloudsParentArray = GameObject.Find ("CloudsArray");
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		int actual_numbers_clouds= cloudsParentArray.transform.childCount;

		if (timer<=0 && actual_numbers_clouds<maxCloudsInScreen) {

			timer = SecondsPerCloud;
			releaseObject ();

		}


	}
	void makeTransparent(GameObject gameObject)
	{
		Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();

		foreach (Renderer render in renders)
		{
			render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.75f);
		}

	}
	public void releaseObject()
	{
		int random = (int)Mathf.Floor(Random.Range (0, clouds_types.Length));

		GameObject cloudType = clouds_types [random];

		GameObject cloud = (GameObject)Instantiate(
			cloudType, 
			transform.position + new Vector3(0,1,0), Quaternion.identity);

		cloud.transform.parent = cloudsParentArray.transform;
		cloud.transform.GetChild(0).tag = "Clouds";
		Vector3 randomPosition = new Vector3 (Random.Range (-80, 80), 0, 0);

		transform.rotation = Quaternion.AngleAxis(Random.Range (0, 360), Vector3.up);

		cloud.transform.position = this.gameObject.transform.position+randomPosition;
		makeTransparent (cloud);
	}
}
