using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourRieles : MonoBehaviour
{
    public enum Controllers { Kinect, Keyboard };
    public Controllers controller;

    [SerializeField]
    float shake = 0.5f;
    [SerializeField]
    float blinkTime = 1.5f;

    private ShakeEffect mainCameraBehaviour;
    public Animator animator;
    public AudioSource obstacleHitSound;


    private void Start()
    {
        mainCameraBehaviour = Camera.main.GetComponent<ShakeEffect>();

        switch (controller)
        {
            case Controllers.Kinect:
                {
                    GetComponent<KinectPlayerController>().enabled = true;
                    GetComponent<PlayerControllerRieles>().enabled = false;

                    break;
                }
            case Controllers.Keyboard:
                {
                    GetComponent<KinectPlayerController>().enabled = false;
                    GetComponent<PlayerControllerRieles>().enabled = true;

                    break;
                }
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            TrackSpawner.ts.SpawnNextTrack();
        }
        else if (other.gameObject.CompareTag("Obstacle Rieles") && !animator.GetBool("Blinking"))
        {
            //GameManagerRieles.gm.isPlaying = false;
            //GameManagerRieles.gm.isGameOver = true;
            GameManagerRieles.gm.UpdateScore(other.gameObject.GetComponent<ObstacleBehaviourRieles>().value);
            StartCoroutine(Blink());
            //Debug.Log("obstacle hit");
        }
        else if (other.gameObject.CompareTag("Repetition"))
        {
            //GameManagerRieles.gm.isPlaying = false;
            //GameManagerRieles.gm.isGameOver = true;
            GameManagerRieles.gm.UpdateRepetitions();
            //Debug.Log("obstacle hit");
        }
    }

    private IEnumerator Blink()
    {
        obstacleHitSound.Play();
        animator.SetBool("Blinking", true);
        mainCameraBehaviour.Shake(shake);
        yield return new WaitForSeconds(blinkTime);
        animator.SetBool("Blinking", false);
    }
}
