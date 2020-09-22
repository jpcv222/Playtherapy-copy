using MovementDetectionLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JointDataViewer : MonoBehaviour
{
    public Text text;

    public RUISSkeletonController skeleton;
    public FullBody mdl;

    public Transform leftHip;
    public Transform rightHip;
    public Transform leftFoot;
    public Transform rightFoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            //text.text = "" + Quaternion.Angle(Quaternion.Euler(0, -45, 0), Quaternion.identity);
            //text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftAnkle.rotation.eulerAngles.ToString();
            //text.text = leftFoot.position.ToString("0.####");            

            text.text = Vector2.Angle(new Vector2(leftHip.position.x, Mathf.Abs(leftHip.position.z)), new Vector2(leftFoot.position.x, Mathf.Abs(leftFoot.position.z))).ToString() + "\n" 
                + (new Vector2(leftHip.position.x, Mathf.Abs(leftHip.position.z))).ToString("0.####") + "\n" + (new Vector2(leftFoot.position.x, Mathf.Abs(leftFoot.position.z))).ToString("0.####");
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            //text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightAnkle.rotation.eulerAngles.ToString();
            //text.text = rightFoot.position.ToString("0.####");

            text.text = Vector2.Angle(new Vector2(rightHip.position.x, 1), new Vector2(rightFoot.position.x, Mathf.Abs(rightFoot.position.z))).ToString() + "\n"
                + rightFoot.position.ToString("0.####") + "\n" + rightHip.position.ToString("0.####");
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            //text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftFoot.rotation.eulerAngles.ToString();
             text.text = mdl.bodyMovements.hipLeftExtMovement().ToString("0.####");
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightFoot.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].leftKnee.rotation.eulerAngles.ToString();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            text.text = skeleton.skeletonManager.skeletons[skeleton.bodyTrackingDeviceID, skeleton.playerId].rightKnee.rotation.eulerAngles.ToString();
        }
    }
}
