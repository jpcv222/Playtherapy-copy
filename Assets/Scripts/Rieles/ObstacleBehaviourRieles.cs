using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviourRieles : MonoBehaviour
{
    public enum ObstacleType { Train, Barrel, Block };

    public ObstacleType type;

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float rotationSpeed = 300f;

    public int value = -1;

    private Vector3 newPosition;

    private void Start()
    {
        if (GameManagerRieles.gm != null)
        {
            speed = GameManagerRieles.gm.obstalcleSpeed;
            value = GameManagerRieles.gm.obstacleCollisionValue;

            if (type == ObstacleType.Train && (GameManagerRieles.gm.difficulty == 1 || GameManagerRieles.gm.difficulty == 2))
            {
                transform.localScale = new Vector3(1, 1, 0.5f);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
                Debug.Log(transform.localPosition);
            }

            if (GameManagerRieles.gm.difficulty == 1)
                speed = 3f;
        }

        newPosition = transform.position;

        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (GameManagerRieles.gm.isPlaying)
        {
            switch (type)
            {
                case ObstacleType.Train:
                    {
                        newPosition.z -= speed * Time.deltaTime;
                        transform.position = newPosition;

                        break;
                    }
                case ObstacleType.Barrel:
                    {
                        newPosition.z -= speed * Time.deltaTime;
                        transform.position = newPosition;
                        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

                        break;
                    }
                case ObstacleType.Block:
                    {
                        newPosition.z -= speed * Time.deltaTime;
                        transform.position = newPosition;

                        break;
                    }
                default:
                    break;
            }
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
            Debug.Log("obstacle hit");
        }
    }
}
