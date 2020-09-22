using UnityEngine;
using System.Collections;

public class TextMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.y += Time.deltaTime;
        this.transform.position = pos;

	}
}
