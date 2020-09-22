using UnityEngine;

namespace GameSpace
{
    public class StarDestroy : MonoBehaviour
    {

        private Rigidbody m_rigidbody;

        // Use this for initialization
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ResetObject();
                GameManagerSpace.gms.UpdateScore(1);
            }
            if (other.gameObject.tag == "Wall")
            {
                ResetObject();
            }
        }

        public void ResetObject()
        {
            m_rigidbody.velocity = new Vector3(0f, 0f, 0f);
            m_rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            gameObject.SetActive(false);
        }
    }
}