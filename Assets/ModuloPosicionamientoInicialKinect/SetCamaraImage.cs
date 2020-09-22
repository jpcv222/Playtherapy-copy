using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetCamaraImage : MonoBehaviour {

	RawImage image;
	GameObject manager;
	ColorSourceManager manager_color;
	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		manager = GameObject.Find ("ManagerInitialPosition");
		manager_color = manager.GetComponent<ColorSourceManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (image !=null) {
			if (manager_color!=null) {
				image.texture = manager_color.GetColorTexture ();
			}
		}

	}
}
