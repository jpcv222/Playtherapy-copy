using UnityEngine;

namespace GuerraMedieval
{
    public class PropellerShaft : MonoBehaviour
    {

        public float velocity = 10f;

        //Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * velocity, Space.Self);
        }
    }
}