using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour {


    public float speed = 30;
	// Update is called once per frame
	void Update () {
        transform.position += -transform.forward * Time.deltaTime * speed;
    }
}
