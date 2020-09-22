using UnityEngine;

namespace GuerraMedieval
{
    public class AutoDestroy : MonoBehaviour
    {
        public float time = 1f;
        private float startTime;

        void Update()
        {
            if(Time.time - startTime > time)
            {
                Destroy(gameObject);
            }
        }

        public void OnEnable()
        {
            startTime = Time.time;
        }
    }
}

