using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviourRieles : MonoBehaviour
{
    public PickupType type;
    public int value = 1;

    public float movementMagnitude = 0.5f;
    public float movementSpeed = 1f;
    public float rotatioSpeed = 300f;

    public enum PickupType { Coin, Star };

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private ParticleSystem particles;
    private AudioSource pickupSound;

	// Use this for initialization
	void Start ()
    {
        initialPosition = transform.position;
        particles = GetComponent<ParticleSystem>();
        pickupSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerRieles.gm.isPlaying)
        {
            if (transform.position.y <= initialPosition.y)
            {
                targetPosition = initialPosition;
                targetPosition.y += movementMagnitude;
            }
            else if (transform.position.y >= targetPosition.y)
            {
                targetPosition = initialPosition;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            transform.Rotate(new Vector3(0, rotatioSpeed * Time.deltaTime, 0));
        }
        else
        {
            gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPickup();
        }
    }

    public void PlayerPickup()
    {
        GameManagerRieles.gm.UpdateScore(value);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        particles.Play();
        pickupSound.Play();
        yield return new WaitUntil(IsDestroyReady);

        Destroy(gameObject);
    }

    public bool IsDestroyReady()
    {
        return particles.isStopped && !pickupSound.isPlaying;
    }
}
