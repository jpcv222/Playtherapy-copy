using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFence : MonoBehaviour {




	public List<GameObject> array_fences_types;
	int numberOfObjects = 20;
	float radius = 10;


	// Use this for initialization
	void Start () {


		GameObject fence;


		for (int i = 0; i < numberOfObjects; i++) {
			float  angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new  Vector3 (Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius+ transform.position;

			int random = Random.Range (0, 10);

			if (random > 8) {
				//breaked fence
				random = 1;
			} else {
				random = 0;
			}

			fence =Instantiate(array_fences_types[random], pos, Quaternion.identity);
			fence.transform.LookAt (Vector3.zero - fence.transform.position);
			fence.transform.rotation = Quaternion.Euler(fence.transform.eulerAngles.x+0.0f,fence.transform.eulerAngles.y+90.0f,fence.transform.eulerAngles.z+0.0f);
			fence.transform.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
