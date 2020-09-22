using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsFM : MonoBehaviour {

	public List<AudioSource> effects_magic;
	public AudioSource block;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void playBlock()
	{
		block.Play ();
	}
	public void playRandomSound()
	{
		int random = Random.Range (0, effects_magic.Count);
		playIndex (random);

	}
	public void playIndex(int index=0)
	{
		if (effects_magic!=null && effects_magic.Count>0) {
			effects_magic [index].Play ();
		}


	}
}
