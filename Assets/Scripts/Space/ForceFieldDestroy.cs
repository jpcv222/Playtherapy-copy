using System.Collections;
using UnityEngine;

namespace GameSpace
{
    public class ForceFieldDestroy : MonoBehaviour
    {

        public float lifeTime = 10f;                // The lifetime of the force field
        private float savedTime;                    // Temporal variable for save the init of the life
        private Animator animator;

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
            savedTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {

            if (Time.time - savedTime >= lifeTime)
            {
                //gameObject.SetActive(false);
                StartCoroutine(Deactivate());
            }
        }

        // Used for set the beginnig of life
        private void OnEnable()
        {
            savedTime = Time.time;
            animator.Play("ForceFieldEnable", 0);
        }

        private IEnumerator Deactivate()
        {
            animator.Play("ForceFieldDeactivate", 0);
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}