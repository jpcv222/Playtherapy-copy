using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsInVecinosInvasores : MonoBehaviour {




	public AudioSource sound_pig;
	public AudioSource sound_hen;
	public AudioSource sound_sheep;
	public AudioSource sound_teleport;
	public AudioSource sound_escape;
	public AudioSource sound_explotion;
	public AudioSource sound_abdution;
	// Use this for initialization
	void Start () {
		sound_pig = GameObject.Find ("sound_pig").GetComponent<AudioSource> ();
		sound_hen = GameObject.Find ("sound_hen").GetComponent<AudioSource> ();
		sound_sheep = GameObject.Find ("sound_sheep").GetComponent<AudioSource> ();
		sound_teleport = GameObject.Find ("sound_teleport").GetComponent<AudioSource> ();
		sound_escape = GameObject.Find ("sound_escape").GetComponent<AudioSource> ();
		sound_explotion = GameObject.Find ("sound_explotion").GetComponent<AudioSource> ();
		sound_abdution = GameObject.Find ("sound_abdution").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
