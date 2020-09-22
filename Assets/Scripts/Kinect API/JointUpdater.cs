using UnityEngine;
using System.Collections;
using Windows.Kinect;
using MovementDetectionLibrary;
using System; 

public class JointUpdater : MonoBehaviour
{
    public FullBody FullBodyObject;

    private Vector3 SpineBaseStart;
    private Vector3 SpineMidStart;
    private Vector3 SpineShoulderStart;
    private Vector3 NeckStart;
    private Vector3 HeadStart;
    private Vector3 WristLeftStart;
    private Vector3 ElbowLeftStart;
    private Vector3 ShoulderLeftStart;
    private Vector3 WristRightStart;
    private Vector3 ElbowRightStart;
    private Vector3 ShoulderRightStart;
    private Vector3 HipLeftStart;
    private Vector3 KneeLeftStart;
    private Vector3 AnkleLeftStart;
    private Vector3 HipRightStart;
    private Vector3 KneeRightStart;
    private Vector3 AnkleRightStart;

    // Use this for initialization
    void Start ()
    {
        SpineBaseStart = GameObject.Find("SpineBase").transform.position;
        SpineMidStart = GameObject.Find("SpineMid").transform.position;
        SpineShoulderStart = GameObject.Find("SpineShoulder").transform.position;
        NeckStart = GameObject.Find("Neck").transform.position;
        HeadStart = GameObject.Find("Head").transform.position;
        WristLeftStart = GameObject.Find("WristLeft").transform.position;
        ElbowLeftStart = GameObject.Find("ElbowLeft").transform.position;
        ShoulderLeftStart = GameObject.Find("ShoulderLeft").transform.position;
        WristRightStart = GameObject.Find("WristRight").transform.position;
        ElbowRightStart = GameObject.Find("ElbowRight").transform.position;
        ShoulderRightStart = GameObject.Find("ShoulderRight").transform.position;
        HipLeftStart = GameObject.Find("HipLeft").transform.position;
        KneeLeftStart = GameObject.Find("KneeLeft").transform.position;
        AnkleLeftStart = GameObject.Find("AnkleLeft").transform.position;
        HipRightStart = GameObject.Find("HipRight").transform.position;
        KneeRightStart = GameObject.Find("KneeRight").transform.position;
        AnkleRightStart = GameObject.Find("AnkleRight").transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (FullBodyObject != null && GameObject.Find("KinectTAdapter").GetComponent<KinectTwoAdapter>().currentBody != null)
        {
            BodyPointPosition SpineBase = FullBodyObject.ReturnPointPosition(BodyParts.SpineBase);
            Vector3 sb = new Vector3(SpineBase.x, SpineBase.y, SpineBase.z);
            float factor = SpineBaseStart.magnitude / sb.magnitude;
            sb.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("SpineBase").transform.position = sb;

            BodyPointPosition SpineMid = FullBodyObject.ReturnPointPosition(BodyParts.SpineMid);
            Vector3 sm = new Vector3(SpineMid.x, SpineMid.y, SpineMid.z);
            factor = SpineMidStart.magnitude / sm.magnitude;
            sm.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("SpineMid").transform.position = sm;

            BodyPointPosition SpineShoulder = FullBodyObject.ReturnPointPosition(BodyParts.SpineShoulder);
            Vector3 ss = new Vector3(SpineShoulder.x, SpineShoulder.y, SpineShoulder.z);
            factor = SpineShoulderStart.magnitude / ss.magnitude;
            ss.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("SpineShoulder").transform.position = ss;

            //---------------------

            BodyPointPosition Neck = FullBodyObject.ReturnPointPosition(BodyParts.Neck);
            Vector3 n = new Vector3(Neck.x, Neck.y, Neck.z);
            factor = NeckStart.magnitude / n.magnitude;
            n.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("Neck").transform.position = n;

            BodyPointPosition Head = FullBodyObject.ReturnPointPosition(BodyParts.Head);
            Vector3 h = new Vector3(Head.x, Head.y, Head.z);
            factor = HeadStart.magnitude / h.magnitude;
            h.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("Head").transform.position = h;

            //-----------------

            BodyPointPosition ShoulderLeft = FullBodyObject.ReturnPointPosition(BodyParts.ShoulderLeft);
            Vector3 sl = new Vector3(ShoulderLeft.x, ShoulderLeft.y, ShoulderLeft.z);
            factor = ShoulderLeftStart.magnitude / sl.magnitude;
            sl.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("ShoulderLeft").transform.position = sl;

            BodyPointPosition ElbowLeft = FullBodyObject.ReturnPointPosition(BodyParts.ElbowLeft);
            Vector3 el = new Vector3(ElbowLeft.x, ElbowLeft.y, ElbowLeft.z);
            factor = ElbowLeftStart.magnitude / el.magnitude;
            el.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("ElbowLeft").transform.position = el;

            BodyPointPosition WristLeft = FullBodyObject.ReturnPointPosition(BodyParts.WristLeft);
            Vector3 wl = new Vector3(WristLeft.x, WristLeft.y, WristLeft.z);
            factor = WristLeftStart.magnitude / wl.magnitude;
            wl.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("WristLeft").transform.position = wl;

            //---------------------

            BodyPointPosition ShoulderRight = FullBodyObject.ReturnPointPosition(BodyParts.ShoulderRight);
            Vector3 sr = new Vector3(ShoulderRight.x, ShoulderRight.y, ShoulderRight.z);
            factor = ShoulderRightStart.magnitude / sr.magnitude;
            sr.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("ShoulderRight").transform.position = sr;

            BodyPointPosition ElbowRight = FullBodyObject.ReturnPointPosition(BodyParts.ElbowRight);
            Vector3 er = new Vector3(ElbowRight.x, ElbowRight.y, ElbowRight.z);
            factor = ElbowRightStart.magnitude / er.magnitude;
            er.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("ElbowRight").transform.position = er;

            BodyPointPosition WristRight = FullBodyObject.ReturnPointPosition(BodyParts.WristRight);
            Vector3 wr = new Vector3(WristRight.x, WristRight.y, WristRight.z);
            factor = WristRightStart.magnitude / wr.magnitude;
            wr.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("WristRight").transform.position = wr;

            //-----------------------

            BodyPointPosition HipLeft = FullBodyObject.ReturnPointPosition(BodyParts.HipLeft);
            Vector3 hl = new Vector3(HipLeft.x, HipLeft.y, HipLeft.z);
            factor = HipLeftStart.magnitude / hl.magnitude;
            hl.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("HipLeft").transform.position = hl;

            BodyPointPosition KneeLeft = FullBodyObject.ReturnPointPosition(BodyParts.KneeLeft);
            Vector3 kl = new Vector3(KneeLeft.x, KneeLeft.y, KneeLeft.z);
            factor = KneeLeftStart.magnitude / kl.magnitude;
            kl.Scale(KneeLeftStart);
            GameObject.Find("KneeLeft").transform.position = kl;

            BodyPointPosition AnkleLeft = FullBodyObject.ReturnPointPosition(BodyParts.AnkleLeft);
            Vector3 al = new Vector3(AnkleLeft.x, AnkleLeft.y, AnkleLeft.z);
            factor = AnkleLeftStart.magnitude / al.magnitude;
            al.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("AnkleLeft").transform.position = al;

            //----------------------

            BodyPointPosition HipRight = FullBodyObject.ReturnPointPosition(BodyParts.HipRight);
            Vector3 hr = new Vector3(HipRight.x, HipRight.y, HipRight.z);
            factor = HipRightStart.magnitude / hr.magnitude;
            hr.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("HipRight").transform.position = hr;

            BodyPointPosition KneeRight = FullBodyObject.ReturnPointPosition(BodyParts.KneeRight);
            Vector3 kr = new Vector3(KneeRight.x, KneeRight.y, KneeRight.z);
            factor = KneeRightStart.magnitude / kr.magnitude;
            kr.Scale(KneeRightStart);
            GameObject.Find("KneeRight").transform.position = kr;

            BodyPointPosition AnkreRight = FullBodyObject.ReturnPointPosition(BodyParts.AnkleRight);
            Vector3 ar = new Vector3(AnkreRight.x, AnkreRight.y, AnkreRight.z);
            factor = AnkleRightStart.magnitude / ar.magnitude;
            ar.Scale(new Vector3(factor, factor, factor));
            GameObject.Find("AnkleRight").transform.position = ar;
        }
    }
}
