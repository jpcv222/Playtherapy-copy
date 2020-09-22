using UnityEngine;

namespace GameSpace
{
    public class StarSpawner : MonoBehaviour
    {

        public float starsVelocity = 10.0f;

        private Vector3 planeSize;
        private Vector3 initSpawn;
        private Vector3 finalSpawn;

        private float savedTime;
        public float secondsBetweenSpawning = 0.5f;

        private int currentStar;
        private float currentStarSpawn;
        private GameObject[] stars;

        private bool invested;

        private bool destroyed;

        // Use this for initialization
        void Start()
        {
            stars = GameObject.FindGameObjectsWithTag("Coins");
            foreach (GameObject obj in stars)
            {
                obj.SetActive(false); // Inactive all stars
            }

            savedTime = Time.time;

            planeSize = GetComponent<MeshCollider>().bounds.size;
            initSpawn = new Vector3(-planeSize.x / 2, 0f, transform.position.z);
            finalSpawn = new Vector3(planeSize.x / 2, 0f, transform.position.z);

            currentStar = 0;
            currentStarSpawn = Random.Range(0, 10) / 10;

            invested = false;

            invested = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerSpace.gms.IsPlaying() && GameManagerSpace.gms.GetState() == GameManagerSpace.PlayState.STARS)
            {
                SendStars();
            }

            if (GameManagerSpace.gms.IsPlaying() && destroyed)
            {
                destroyed = false;
            }

            if (GameManagerSpace.gms.IsGameOver() && !destroyed)
            {
                DestroyAll();
            }
        }

        public void SendStars()
        {
            if (currentStar < stars.Length)
            {
                if (Time.time - savedTime >= secondsBetweenSpawning && !stars[currentStar].activeSelf)
                {
                    stars[currentStar].transform.position = Vector3.Lerp(initSpawn, finalSpawn, currentStarSpawn);
                    stars[currentStar].SetActive(true);
                    stars[currentStar].GetComponent<Rigidbody>().angularVelocity = Vector3.up * 7f;
                    stars[currentStar].GetComponent<Rigidbody>().velocity = Vector3.back * starsVelocity;

                    savedTime = Time.time;
                    currentStar++;

                    if (!invested)
                    {
                        currentStarSpawn += 0.2f;
                        if (currentStarSpawn > 1)
                        {
                            invested = true;
                        }
                    }
                    else
                    {
                        currentStarSpawn -= 0.2f;
                        if (currentStarSpawn < 0)
                        {
                            invested = false;
                        }
                    }
                }
            }
            else
            {
                currentStar = 0;
            }
        }

        public void DestroyAll()
        {
            foreach (GameObject obj in stars)
            {
                obj.GetComponent<StarDestroy>().ResetObject();
            }
        }
    }
}