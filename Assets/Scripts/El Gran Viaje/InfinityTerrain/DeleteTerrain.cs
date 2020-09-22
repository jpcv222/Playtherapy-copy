using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTerrain : MonoBehaviour {

	void OnTriggerEnter(Collider other) {


		switch (other.gameObject.name) {
		case "Terrain Chunk":
			Destroy (other.gameObject);
			break;
		}



	}
}
