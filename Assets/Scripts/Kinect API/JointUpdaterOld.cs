using UnityEngine;
using System.Collections;
using MovementDetectionLibrary;

public class JointUpdaterOld : MonoBehaviour
{
    public FullBody FullBodyObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (FullBodyObject != null && GameObject.Find("KinectTAdapter").GetComponent<KinectTwoAdapter>().currentBody != null)
        {
            BodyPointPosition SpineBase = FullBodyObject.ReturnPointPosition(BodyParts.SpineBase);
            Vector3 sb = new Vector3(SpineBase.x, SpineBase.y, SpineBase.z);
            GameObject.Find("SpineBase").transform.position = sb;

            BodyPointPosition SpineMid = FullBodyObject.ReturnPointPosition(BodyParts.SpineMid);
            Vector3 sm = new Vector3(SpineMid.x, SpineMid.y, SpineMid.z);
            GameObject.Find("SpineMid").transform.position = sm;

            BodyPointPosition SpineShoulder = FullBodyObject.ReturnPointPosition(BodyParts.SpineShoulder);
            Vector3 ss = new Vector3(SpineShoulder.x, SpineShoulder.y, SpineShoulder.z);
            GameObject.Find("SpineShoulder").transform.position = ss;

            //---------------------

            BodyPointPosition Neck = FullBodyObject.ReturnPointPosition(BodyParts.Neck);
            Vector3 n = new Vector3(Neck.x, Neck.y, Neck.z);
            GameObject.Find("Neck").transform.position = n;

            BodyPointPosition Head = FullBodyObject.ReturnPointPosition(BodyParts.Head);
            Vector3 h = new Vector3(Head.x, Head.y, Head.z);
            GameObject.Find("Head").transform.position = h;

            //-----------------

            BodyPointPosition ShoulderLeft = FullBodyObject.ReturnPointPosition(BodyParts.ShoulderLeft);
            Vector3 sl = new Vector3(ShoulderLeft.x, ShoulderLeft.y, ShoulderLeft.z);
            GameObject.Find("ShoulderLeft").transform.position = sl;

            BodyPointPosition ElbowLeft = FullBodyObject.ReturnPointPosition(BodyParts.ElbowLeft);
            Vector3 el = new Vector3(ElbowLeft.x, ElbowLeft.y, ElbowLeft.z);
            GameObject.Find("ElbowLeft").transform.position = el;

            BodyPointPosition WristLeft = FullBodyObject.ReturnPointPosition(BodyParts.WristLeft);
            Vector3 wl = new Vector3(WristLeft.x, WristLeft.y, WristLeft.z);
            GameObject.Find("WristLeft").transform.position = wl;

            //---------------------

            BodyPointPosition ShoulderRight = FullBodyObject.ReturnPointPosition(BodyParts.ShoulderRight);
            Vector3 sr = new Vector3(ShoulderRight.x, ShoulderRight.y, ShoulderRight.z);
            GameObject.Find("ShoulderRight").transform.position = sr;

            BodyPointPosition ElbowRight = FullBodyObject.ReturnPointPosition(BodyParts.ElbowRight);
            Vector3 er = new Vector3(ElbowRight.x, ElbowRight.y, ElbowRight.z);
            GameObject.Find("ElbowRight").transform.position = er;

            BodyPointPosition WristRight = FullBodyObject.ReturnPointPosition(BodyParts.WristRight);
            Vector3 wr = new Vector3(WristRight.x, WristRight.y, WristRight.z);
            GameObject.Find("WristRight").transform.position = wr;

            //-----------------------

            BodyPointPosition HipLeft = FullBodyObject.ReturnPointPosition(BodyParts.HipLeft);
            Vector3 hl = new Vector3(HipLeft.x, HipLeft.y, HipLeft.z);
            GameObject.Find("HipLeft").transform.position = hl;

            BodyPointPosition KneeLeft = FullBodyObject.ReturnPointPosition(BodyParts.KneeLeft);
            Vector3 kl = new Vector3(KneeLeft.x, KneeLeft.y, KneeLeft.z);
            GameObject.Find("KneeLeft").transform.position = kl;

            BodyPointPosition AnkleLeft = FullBodyObject.ReturnPointPosition(BodyParts.AnkleLeft);
            Vector3 al = new Vector3(AnkleLeft.x, AnkleLeft.y, AnkleLeft.z);
            GameObject.Find("AnkleLeft").transform.position = al;

            //----------------------

            BodyPointPosition HipRight = FullBodyObject.ReturnPointPosition(BodyParts.HipRight);
            Vector3 hr = new Vector3(HipRight.x, HipRight.y, HipRight.z);
            GameObject.Find("HipRight").transform.position = hr;

            BodyPointPosition KneeRight = FullBodyObject.ReturnPointPosition(BodyParts.KneeRight);
            Vector3 kr = new Vector3(KneeRight.x, KneeRight.y, KneeRight.z);
            GameObject.Find("KneeRight").transform.position = kr;

            BodyPointPosition AnkreRight = FullBodyObject.ReturnPointPosition(BodyParts.AnkleRight);
            Vector3 ar = new Vector3(AnkreRight.x, AnkreRight.y, AnkreRight.z);
            GameObject.Find("AnkleRight").transform.position = ar;
        }
    }
}
