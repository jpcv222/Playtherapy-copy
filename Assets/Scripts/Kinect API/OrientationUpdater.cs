using UnityEngine;
using System.Collections;
using Windows.Kinect;
using MovementDetectionLibrary;

public class OrientationUpdater : MonoBehaviour
{
    public Transform floorTransform;
    private Body currentBody;
    
	// Use this for initialization
	void Start ()
    {
        currentBody = GameObject.Find("KinectTAdapter").GetComponent<KinectTwoAdapter>().currentBody;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentBody = GameObject.Find("KinectTAdapter").GetComponent<KinectTwoAdapter>().currentBody;

        if (currentBody != null)
        {
            Quaternion comp = Quaternion.FromToRotation(new Vector3(floorTransform.position.x, floorTransform.position.y, floorTransform.position.z), Vector3.up);
            Quaternion SpineBase = VToQ(currentBody.JointOrientations[JointType.SpineBase].Orientation, comp);
            Quaternion SpineMid = VToQ(currentBody.JointOrientations[JointType.SpineMid].Orientation, comp);
            Quaternion SpineShoulder = VToQ(currentBody.JointOrientations[JointType.SpineShoulder].Orientation, comp);
            Quaternion ShoulderLeft = VToQ(currentBody.JointOrientations[JointType.ShoulderLeft].Orientation, comp);
            Quaternion ShoulderRight = VToQ(currentBody.JointOrientations[JointType.ShoulderRight].Orientation, comp);
            Quaternion ElbowLeft = VToQ(currentBody.JointOrientations[JointType.ElbowLeft].Orientation, comp);
            Quaternion WristLeft = VToQ(currentBody.JointOrientations[JointType.WristLeft].Orientation, comp);
            Quaternion HandLeft = VToQ(currentBody.JointOrientations[JointType.HandLeft].Orientation, comp);
            Quaternion ElbowRight = VToQ(currentBody.JointOrientations[JointType.ElbowRight].Orientation, comp);
            Quaternion WristRight = VToQ(currentBody.JointOrientations[JointType.WristRight].Orientation, comp);
            Quaternion HandRight = VToQ(currentBody.JointOrientations[JointType.HandRight].Orientation, comp);
            Quaternion KneeLeft = VToQ(currentBody.JointOrientations[JointType.KneeLeft].Orientation, comp);
            Quaternion AnkleLeft = VToQ(currentBody.JointOrientations[JointType.AnkleLeft].Orientation, comp);
            Quaternion KneeRight = VToQ(currentBody.JointOrientations[JointType.KneeRight].Orientation, comp);
            Quaternion AnkleRight = VToQ(currentBody.JointOrientations[JointType.AnkleRight].Orientation, comp);

            Quaternion q = transform.rotation;
            transform.rotation = Quaternion.identity;

            GameObject.Find("SpineMid").transform.rotation = SpineMid * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("ElbowRight").transform.rotation = ElbowRight * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("WristRight").transform.rotation = WristRight * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            //GameObject.Find("HandRight").transform.rotation = HandRight * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("ElbowLeft").transform.rotation = ElbowLeft * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("WristLeft").transform.rotation = WristLeft * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            //GameObject.Find("LeftHand").transform.rotation = HandLeft * Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            GameObject.Find("KneeRight").transform.rotation = KneeRight * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("AnkleRight").transform.rotation = AnkleRight * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("KneeLeft").transform.rotation = KneeLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
            GameObject.Find("AnkleLeft").transform.rotation = AnkleLeft * Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            transform.rotation = q;
        }
    }

    private Quaternion VToQ(Windows.Kinect.Vector4 kinectQ, Quaternion comp)
    {
        return Quaternion.Inverse(comp) * (new Quaternion(-kinectQ.X, -kinectQ.Y, kinectQ.Z, kinectQ.W));
    }
}
