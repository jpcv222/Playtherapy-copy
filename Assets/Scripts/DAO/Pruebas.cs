using UnityEngine;
using System.Collections;

public class Pruebas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var ts = new TherapySession("23123", "sdfsdsaddsad");
        Debug.Log(ts.Date);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
