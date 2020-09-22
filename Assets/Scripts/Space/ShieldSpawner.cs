using UnityEngine;

namespace GameSpace
{
    public class ShieldSpawner : MonoBehaviour
    {

        public GameObject shield;
        public float velocity = 10f;

        public float minSecondsBetweenSpawning = 3.0f;
        public float maxSecondsBetweenSpawning = 6.0f;

        private Vector3 planeSize;

        private float savedTime;
        private float secondsBetweenSpawning;

        private bool destroyed;

        // Use this for initialization
        void Start()
        {
            shield.SetActive(false);
            planeSize = gameObject.GetComponent<MeshCollider>().bounds.size;

            savedTime = Time.time;
            secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);

            destroyed = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManagerSpace.gms.IsPlaying() && GameManagerSpace.gms.GetState() == GameManagerSpace.PlayState.ASTEROIDS)
            {
                Spawn();
            }

            if (GameManagerSpace.gms.IsPlaying() && destroyed)
            {
                destroyed = false;
            }

            if (GameManagerSpace.gms.IsGameOver() && !destroyed)
            {
                DestroyAll();
                destroyed = true;
            }
        }

        public void Spawn()
        {
            if (Time.time - savedTime >= secondsBetweenSpawning)
            {
                if (!shield.activeSelf)
                {
                    shield.transform.position = new Vector3(Random.Range(-planeSize.x / 2, planeSize.x / 2), 0f, gameObject.transform.position.z);
                    shield.SetActive(true);
                    shield.GetComponent<Rigidbody>().angularVelocity = Vector3.up * 7f;
                    shield.GetComponent<Rigidbody>().velocity = Vector3.back * velocity;
                }

                savedTime = Time.time;
                secondsBetweenSpawning = Random.Range(minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
            }
        }

        public void DestroyAll()
        {
            shield.GetComponent<ShieldDestroy>().ResetObject();
        }
    }
}