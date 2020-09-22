using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwGems : MonoBehaviour {

	GameObject GemsParentArray;
	[Range (0,40)]
	public int maxGemsInScreen;
	[Range (1,60)]
	public float SecondsPerPlane;
	public GameObject[] gems_types;
	// Use this for initialization

	float timer =0;
	public float distanceFromCenter =15;
	void Start () {
		GemsParentArray = GameObject.Find ("GemsArray");
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		int actual_numbers_gems= GemsParentArray.transform.childCount;

		if (timer<=0 && actual_numbers_gems<maxGemsInScreen) {

			timer = SecondsPerPlane;
			releaseObject ();

		}


	}
	public void releaseObject()
	{
		int random = (int)Mathf.Floor(Random.Range (0, gems_types.Length));

		GameObject cloudType = gems_types [random];

		GameObject cloud = (GameObject)Instantiate(
			cloudType, 
			transform.position + new Vector3(0,1,0), Quaternion.identity);

		cloud.transform.parent = GemsParentArray.transform;
		cloud.transform.tag = "Coins";
		Vector3 randomPosition = new Vector3 (Random.Range (-distanceFromCenter, distanceFromCenter), 0, 0);

		//transform.rotation = Quaternion.AngleAxis(Random.Range (0, 360), Vector3.up);

		cloud.transform.position = this.gameObject.transform.position+randomPosition;

	}
}
