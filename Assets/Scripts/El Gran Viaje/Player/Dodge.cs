using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge : MonoBehaviour {


	AudioSource coinSound;
    ScoreHandler handler;
    void Start()
    {
        handler = FindObjectOfType<ScoreHandler>();
		coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        HasCollide collideWithOther;
       
        switch (other.tag)
        {
		case "Airballoon":
			collideWithOther = other.GetComponent<HasCollide> ();
			if (collideWithOther.hasCollide == false) {
				collideWithOther.hasCollide = true;
				handler.sum_score (1);
				handler.airplanes_dodge++;
			}
			coinSound.Play ();
                
                break;
		case "Planes":
			collideWithOther = other.GetComponent<HasCollide> ();
			if (collideWithOther.hasCollide == false) {
				collideWithOther.hasCollide = true;
				handler.sum_score (2);
				handler.airplanes_dodge++;
			}
			coinSound.Play ();
                break;

            default:
                break;
        }

    }
}
