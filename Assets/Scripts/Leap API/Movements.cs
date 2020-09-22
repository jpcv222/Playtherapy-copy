using Leap;
using System;
//using UnityEngine;

namespace LeapAPI
{
    public class Movements
    {
        //LeapService ls = new LeapService
        public double FlexoExtention()
        {
            Frame frame = LeapService.GetFrame();


            if (frame.Hands.Count != 0)
            {
                Vector elbow = frame.Hands[0].Arm.ElbowPosition;
                Vector wrist = frame.Hands[0].Arm.WristPosition;
                Vector middle = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_MIDDLE].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;

                float orientation = middle.y - wrist.y;

                if(Math.Abs(frame.Hands[0].PalmNormal.y) < 0.8)
                {
                    return (180 - LeapUtils.getAngleJoints(elbow, wrist, middle, y: false)) * Math.Sign(orientation);
                }
                else
                {
                    return 0;
                }

                
            }
            else
            {
                return 0;
            }
        }

        public double UlnarRadial()
        {
            if (LeapService.IsConected())
            {

            }
            Frame frame = LeapService.GetFrame();
            

            if (frame.Hands.Count != 0)
            {
                Vector elbow = frame.Hands[0].Arm.ElbowPosition;
                Vector wrist = frame.Hands[0].Arm.WristPosition;
                Vector middle = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_MIDDLE].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;

                float orientation = middle.x - wrist.x;


                if (Math.Abs(frame.Hands[0].PalmNormal.y) > 0.8)
                {
                    return (180 - LeapUtils.getAngleJoints(elbow, wrist, middle, y: false)) * Math.Sign(orientation);
                }
                else
                {
                    return 0;
                }
                //Vector middle = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_MIDDLE].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;
                //Vector wrist = frame.Hands[0].Arm.WristPosition;
                //Vector arm = frame.Hands[0].Arm.Direction;

                //Vector3 v1 = new Vector3();
                //Vector3 v2 = new Vector3();

                //v1.x = (middle.x - wrist.x) * Math.Abs(arm.x);
                //v1.y = 0;
                //v1.z = (middle.z - wrist.z) * Math.Abs(arm.z);

                //v2.x = middle.x - wrist.x;
                //v2.y = 0;
                //v2.z = middle.z - wrist.z;

                //Debug.Log(arm);

                //return Vector3.Angle(v1, v2);
            }
            else
            {
                return 0;
            }


        }

        public double TumbleExtensionIF()
        {
            Frame frame = LeapService.GetFrame();

            if (frame.Hands.Count != 0)
            {
                Vector thumbInit = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                Vector thumbEnd = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint;
                Vector indexEnd = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_INDEX].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint;

                return LeapUtils.getAngleJoints(thumbEnd, thumbInit, indexEnd);
            }
            else
            {
                return 0;
            }
        }

        public double AbductionAdductionTumble()
        {
            Frame frame = LeapService.GetFrame();

            if (frame.Hands.Count != 0)
            {
                Vector thumbInit = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                Vector thumbEnd = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint;
                Vector indexEnd = frame.Hands[0].Fingers[(int)Leap.Finger.FingerType.TYPE_INDEX].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint;

                return LeapUtils.getAngleJoints(thumbEnd, thumbInit, indexEnd, y:false);
            }
            else
            {
                return 0;
            }
        }

        public double PronoSupra()
        {
            Frame frame = LeapService.GetFrame();

            if (frame.Hands.Count != 0)
            {
                LeapQuaternion q = frame.Hands[0].Rotation;
                if (frame.Hands[0].IsRight)
                    q = new LeapQuaternion(q.x * 1, q.y * 1, q.z * -1, q.w * 1);
                Vector rotation = LeapUtils.ToEulerianAngle(q);
                //Debug.Log(rotation);

                if (rotation.z > 270)
                    rotation.z = 360 - rotation.z;

                if (frame.Hands[0].IsRight)
                    return Math.Abs(rotation.z) - 90;
                else
                    return 90 - Math.Abs(rotation.z);
                //return Math.Abs(Math.Abs(rotation.z));
            }
            else
            {
                return 0;
            }
        }

        public double FingerFlexoExtensionMF(Leap.Finger.FingerType finger)
        {
            Frame frame = LeapService.GetFrame();

            if(frame.Hands.Count != 0)
            {
                Vector start = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint;
                Vector middle = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;
                Vector end = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;

                return 180 - LeapUtils.getAngleJoints(start, middle, end);
            }
            else
            {
                return 0;
            }
        }

        public double FingerFlexoExtensionIF(Leap.Finger.FingerType finger)
        {
            Frame frame = LeapService.GetFrame();

            if (frame.Hands.Count != 0)
            {
                Vector start = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                Vector middle = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
                Vector end = frame.Hands[0].Fingers[(int)finger].Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint;

                return 180 - LeapUtils.getAngleJoints(start, middle, end);
            }
            else
            {
                return 0;
            }
        }

		public bool IsLeft(){
			bool flag = false;
			Frame frame = LeapService.GetFrame();
			if (frame.Hands.Count != 0) {
				if (!frame.Hands [0].IsRight) {
					flag = true;
				}
			}
			return flag;
		
		}

        public float Grab()
        {
            Frame frame = LeapService.GetFrame();
            if (frame.Hands.Count != 0)
            {
                return frame.Hands[0].GrabStrength;
            }

            return 0;
        }

    }
}
