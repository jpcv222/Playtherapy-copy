using UnityEngine;

namespace GameSpace
{
    public class DroidSpawner : MonoBehaviour
    {

        public GameObject droid;
        public float velocity = 10f;
        public float distance = 20f;

        private Vector3 planeSize;

        //private float savedTime;

        private bool destroyed;

        // Use this for initialization
        void Start()
        {
            droid.SetActive(false);
            planeSize = gameObject.GetComponent<MeshCollider>().bounds.size;
            destroyed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerSpace.gms.IsPlaying() && GameManagerSpace.gms.GetState() == GameManagerSpace.PlayState.ENEMIES)
            {
                Spawn();
                CalculateLimit();
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
            if (!droid.activeSelf)
            {
                droid.transform.position = new Vector3(Random.Range(-planeSize.x / 2, planeSize.x / 2), 0f, gameObject.transform.position.z);
                droid.SetActive(true);
                droid.GetComponent<Rigidbody>().velocity = Vector3.back * velocity;
            }
        }

        public void CalculateLimit()
        {
            if (droid.transform.position.z <= distance)
            {
                droid.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public void DestroyAll()
        {
            droid.GetComponent<DroidDestroy>().ResetObject();
        }
    }
}