using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHelice : MonoBehaviour {

	public float  spinSpeed = 700f;
    public bool useX = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (useX==true)
        {
            transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        }
        
    }
}
