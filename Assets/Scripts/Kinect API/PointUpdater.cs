using UnityEngine;
using System.Collections;
using System;

namespace MovementDetectionLibrary
{
    public class PointUpdater : MonoBehaviour
    {
        public GameObject FullBodyObject;
        private FullBody _FullBodyObject;
        private Vector3 StartPosition;
        private bool flag;

        // Use this for initialization
        void Start()
        {
            StartPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameObject.Find("KinectTAdapter").GetComponent<KinectTwoAdapter>().currentBody != null)
            {
                if (FullBodyObject == null)
                {
                    return;
                }

                _FullBodyObject = FullBodyObject.GetComponent<FullBody>();
                if (_FullBodyObject == null)
                {
                    return;
                }

                        
                BodyPointPosition pos = _FullBodyObject.ReturnPointPosition((BodyParts)Enum.Parse(typeof(BodyParts), gameObject.transform.name.ToString()));
                Vector3 B = new Vector3(pos.x, pos.y, pos.z);
                Vector3 C = B.normalized * StartPosition.magnitude;
                Debug.Log("magnitud b "+StartPosition.magnitude+" magnitude c "+C.magnitude);

                //float factor = B.magnitude / StartPosition.magnitude;
                //B.Scale(new Vector3(factor, factor, factor));
                transform.position = C;
                //StartPosition = v;

                //Debug.Log("Elbow: x:"+pos.x + ", " + pos.y + "" + pos.z);
            }
        }
    }
}

