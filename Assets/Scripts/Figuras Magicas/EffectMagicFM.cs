using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EffectMagicFM : MonoBehaviour {



	SpawnnerFM spawnner;

	public List<GameObject> circles_magic;
	public List<GameObject> hits_magic;
	public List<Gradient> color_magics;
	GameObject player;
	GameObject actual_circle;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		spawnner = FindObjectOfType<SpawnnerFM> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (actual_circle!=null) {
			actual_circle.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y+5, player.transform.position.z);
		}
	}

	public void startHitMagicIn(Vector3 pos, int index)
	{
		GameObject hit_magic;



		hit_magic = Instantiate (hits_magic [0], new Vector3(pos.x,pos.y+4,pos.z),Quaternion.identity);
	
		ParticleSystem ps = hit_magic.GetComponent<ParticleSystem>();
		var col = ps.colorOverLifetime;
		col.enabled = true;

		Gradient gradient = color_magics [index];
		col.color = gradient;
		hit_magic.transform.localScale = Vector3.one * 5;
		hit_magic.GetComponent<ParticleSystem> ().Play ();
		Destroy (hit_magic, 2);
	}

	public void startCircleMagic(int index)
	{
		actual_circle=Instantiate (circles_magic [0], player.transform.position,Quaternion.identity);
		ParticleSystem ps = actual_circle.GetComponent<ParticleSystem>();
		var col = ps.colorOverLifetime;
		col.enabled = true;

		Gradient gradient = color_magics [index];
		col.color = gradient;

		actual_circle.transform.localScale = Vector3.one * 10;
		actual_circle.GetComponent<ParticleSystem> ().Play ();
		Destroy (actual_circle.gameObject,2);

	}


}
