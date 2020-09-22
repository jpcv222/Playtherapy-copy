using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwPlanes : MonoBehaviour {

	GameObject PlanesParentArray;
	[Range (0,40)]
	public int maxPlanesInScreen;
	[Range (1,60)]
	public float SecondsPerPlane;
	public GameObject[] planes_types;
	// Use this for initialization
	public float distanceFromCenter =15;
	float timer =0;
	void Start () {
		PlanesParentArray = GameObject.Find ("PlanesArray");
	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		int actual_numbers_planes= PlanesParentArray.transform.childCount;

		if (timer<=0 && actual_numbers_planes<maxPlanesInScreen) {

			timer = SecondsPerPlane;
			releaseObject ();

		}

        
    }
	public void releaseObject()
	{
		int random = (int)Mathf.Floor(Random.Range (0, planes_types.Length));

		GameObject planeType = planes_types [random];

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + new Vector3(0,1,0), Quaternion.identity);
        SetVelocity velocity = plane.AddComponent<SetVelocity>();

        switch (random)
        {
            case 0:
                velocity.speed = 30;
            break;
            case 1:
                velocity.speed = 20;
                break;
            case 2:
                velocity.speed = 5;
                break;
            default:
                break;
        }



        plane.transform.parent = PlanesParentArray.transform;
        if (random!=2)
        {
            plane.transform.GetChild(0).tag = "Planes";
        }
        else
        {
            plane.transform.GetChild(0).tag = "Airballoon";
        }
        
		Vector3 randomPosition = new Vector3 (Random.Range (-distanceFromCenter, distanceFromCenter), 0, 0);

        //transform.rotation = Quaternion.AngleAxis(Random.Range (0, 360), Vector3.up);

        plane.transform.position = this.gameObject.transform.position+randomPosition;
    }
}
