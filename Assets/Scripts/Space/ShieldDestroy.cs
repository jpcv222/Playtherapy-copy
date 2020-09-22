using UnityEngine;

namespace GameSpace
{
    public class ShieldDestroy : MonoBehaviour
    {

        //public GameObject bonusParticle;
        private Rigidbody m_rigidbody;

        // Use this for initialization
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
            {
                ResetObject();
            }
        }

        public void ResetObject()
        {
            m_rigidbody.velocity = new Vector3(0f, 0f, 0f);
            m_rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            gameObject.SetActive(false);
        }

    }
}