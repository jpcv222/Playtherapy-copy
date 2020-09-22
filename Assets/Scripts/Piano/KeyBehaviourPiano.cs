using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviourPiano : MonoBehaviour
{
    [SerializeField] AudioSource goodSound;
    [SerializeField] AudioSource badSound;

    public void PlayGoodSound()
    {
        goodSound.Play();
    }

    public void PlayBadSound()
    {
        badSound.Play();
    }
}
