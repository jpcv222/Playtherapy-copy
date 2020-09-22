using UnityEngine;
namespace GuerraMedieval
{
    public class BulletBehavior : MonoBehaviour
    {
        public static BulletBehavior bbh;

        public float fireRate = 1f;

        private int currentBullet;
        private GameObject[] bullets;
        private GameObject player;

        private float nextFire;

        private bool destroyed;

        // Use this for initialization
        void Start()
        {
            if (bbh == null)
            {
                bbh = this.gameObject.GetComponent<BulletBehavior>();
            }

            // initialice the current bullet with 0
            currentBullet = 0;

            // Find all Bullets in the scene
            bullets = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject obj in bullets)
            {
                obj.SetActive(false); //Inactive all bullets
            }

            player = GameObject.FindGameObjectWithTag("Player");

            nextFire = Time.time + fireRate;
            destroyed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerMedieval.gmm.IsPlaying() && destroyed)
            {
                destroyed = false;
            }
            if (GameManagerMedieval.gmm.IsGameOver() && !destroyed)
            {
                DestroyAll();
                destroyed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Fire(Vector3 canonBallVelocity, Vector3 bulletPosition)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (!bullets[currentBullet].activeSelf)
                {
                    bullets[currentBullet].transform.position = bulletPosition;
                    bullets[currentBullet].SetActive(true);
                    bullets[currentBullet].GetComponent<Rigidbody>().velocity = canonBallVelocity;
                    currentBullet++;

                    if (currentBullet >= bullets.Length)
                    {
                        currentBullet = 0;
                    }
                }
            }
        }

        public void DestroyAll()
        {
            foreach (GameObject obj in bullets)
            {
                obj.GetComponent<BulletDestroy>().ResetObject();
            }
        }
    }
}

