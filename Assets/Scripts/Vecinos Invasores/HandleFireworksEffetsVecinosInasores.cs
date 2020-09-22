using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFireworksEffetsVecinosInasores : MonoBehaviour {


	List<ParticleSystem> fireworks;
	int cont_fireworks;
	// Use this for initialization
	void Start () {
		cont_fireworks = 0;
		fireworks = new List<ParticleSystem> ();
		ParticleSystem firework;
		for (int i = 1; i < 4; i++) {
			firework = GameObject.Find ("fireworks_" + i).GetComponent<ParticleSystem>();
			fireworks.Add (firework);
		}

	}
	public void simulateFireworkIn(Vector3 pos)
	{
		if (cont_fireworks>=fireworks.Count) {
			cont_fireworks = 0;
		}
		fireworks [cont_fireworks].transform.position = pos;
		fireworks [cont_fireworks].Play ();
		cont_fireworks++;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
