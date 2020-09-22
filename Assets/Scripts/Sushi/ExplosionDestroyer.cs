using UnityEngine;
using System.Collections;

public class ExplosionDestroyer : MonoBehaviour {

	private ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		explosion = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (explosion) {
			if (!explosion.IsAlive ()) {
				Destroy (gameObject);
			}
		}
	}
}
