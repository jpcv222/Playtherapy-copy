using UnityEngine;

namespace GameSpace
{
    public class ImpactEffect : MonoBehaviour
    {

        public GameObject shieldParticle;
        public GameObject forceField;


        // Use this for initialization
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Shield")
            {
                forceField.SetActive(true);
                //Instantiate(shieldParticle, transform.position, Quaternion.identity);
            }
        }
    }
}