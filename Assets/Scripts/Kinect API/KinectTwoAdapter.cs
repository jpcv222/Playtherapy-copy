using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

namespace MovementDetectionLibrary
{
    public class KinectTwoAdapter : MonoBehaviour {

		public KinectSensor sensor;
		public BodyFrameReader reader;
		public Body[] data = null;
        public Body currentBody;
        
        UnityEngine.Vector4 floor;
        Vector3 xNew;
        Vector3 yNew;
        Vector3 zNew;
        Matrix4x4 rotMatrix;

        public Text floorText;
        public Text oldText;

        Dictionary<BodyParts, JointType> bodyPartsTranslation;
        public void FillDictionary()
        {
            bodyPartsTranslation = new Dictionary<BodyParts, JointType>();
            bodyPartsTranslation.Add(BodyParts.SpineBase, JointType.SpineBase);
            bodyPartsTranslation.Add(BodyParts.SpineMid, JointType.SpineMid);
            bodyPartsTranslation.Add(BodyParts.Neck, JointType.Neck);
            bodyPartsTranslation.Add(BodyParts.Head, JointType.Head);
            bodyPartsTranslation.Add(BodyParts.ShoulderLeft, JointType.ShoulderLeft);
            bodyPartsTranslation.Add(BodyParts.ElbowLeft, JointType.ElbowLeft);
            bodyPartsTranslation.Add(BodyParts.WristLeft, JointType.WristLeft);
            bodyPartsTranslation.Add(BodyParts.HandLeft, JointType.HandLeft);
            bodyPartsTranslation.Add(BodyParts.ShoulderRight, JointType.ShoulderRight);
            bodyPartsTranslation.Add(BodyParts.ElbowRight, JointType.ElbowRight);
            bodyPartsTranslation.Add(BodyParts.WristRight, JointType.WristRight);
            bodyPartsTranslation.Add(BodyParts.HandRight, JointType.HandRight);
            bodyPartsTranslation.Add(BodyParts.HipLeft, JointType.HipLeft);
            bodyPartsTranslation.Add(BodyParts.KneeLeft, JointType.KneeLeft);
            bodyPartsTranslation.Add(BodyParts.AnkleLeft, JointType.AnkleLeft);
            bodyPartsTranslation.Add(BodyParts.FootLeft, JointType.FootLeft);
            bodyPartsTranslation.Add(BodyParts.HipRight, JointType.HipRight);
            bodyPartsTranslation.Add(BodyParts.KneeRight, JointType.KneeRight);
            bodyPartsTranslation.Add(BodyParts.AnkleRight, JointType.AnkleRight);
            bodyPartsTranslation.Add(BodyParts.FootRight, JointType.FootRight);
            bodyPartsTranslation.Add(BodyParts.SpineShoulder, JointType.SpineShoulder);
            bodyPartsTranslation.Add(BodyParts.HandTipLeft, JointType.HandTipLeft);
            bodyPartsTranslation.Add(BodyParts.ThumbLeft, JointType.ThumbLeft);
            bodyPartsTranslation.Add(BodyParts.HandTipRight, JointType.HandTipRight);
            bodyPartsTranslation.Add(BodyParts.ThumbRight, JointType.ThumbRight);
        }

        // Use this for initialization
        void Start () {
            //Debug.Log("Entra start kinectadapter");
            currentBody = null;
            FillDictionary();

			sensor = KinectSensor.GetDefault ();


			if (sensor != null)
			{
				reader = sensor.BodyFrameSource.OpenReader();

				if (!sensor.IsOpen)
				{
                    //Debug.Log("prendio");
					sensor.Open();
				}
			}
            //Initializes the floor kinect vector
            floor = new UnityEngine.Vector4();
        }
		
		// Update is called once per frame
		void Update () {
			if (reader != null) {
				var frame = reader.AcquireLatestFrame ();
				if (frame != null) {
					if (data == null) {
                        //Debug.Log("Instancia");
						data = new Body[sensor.BodyFrameSource.BodyCount];
                        //Debug.Log("count: " + sensor.BodyFrameSource.BodyCount);
					}

					frame.GetAndRefreshBodyData (data);

                    //Gets the floor clip plane
                    floor = ConvertVector (frame.FloorClipPlane);
                    

                    yNew = new Vector3(floor.x, floor.y, floor.z);
                    zNew = new Vector3(0.0f, 1.0f, (float) (-floor.y / floor.z));
                    zNew.Normalize();
                    xNew = Vector3.Cross(yNew, zNew);

                    if (floorText)
                    floorText.text = ("Floor new: " + floor.x + ", " + floor.y + ", " + floor.z + ", " + floor.w + "\n" + xNew + "\n" + yNew + "\n" + zNew);

                    rotMatrix = new Matrix4x4();
                    rotMatrix.SetColumn(0, new UnityEngine.Vector4(xNew.x, xNew.y, xNew.z, 0.0f));
                    rotMatrix.SetColumn(1, new UnityEngine.Vector4(yNew.x, yNew.y, yNew.z, 0.0f));
                    rotMatrix.SetColumn(2, new UnityEngine.Vector4(zNew.x, zNew.y, zNew.z, 0.0f));
                    rotMatrix.SetColumn(3, new UnityEngine.Vector4(0.0f, 0.0f, 0.0f, 1.0f));

                    frame.Dispose ();
					frame = null;

				}
			}

            //Debug.Log("data: " + data.Length);
            int currentTrack = -1;

            if (data != null)
            {
                for (int j = 0; j < sensor.BodyFrameSource.BodyCount; j++)
                {
                    if (data[j].IsTracked)
                    {
                        currentTrack = j;
                    }
                    //Debug.Log("Data[" + j + "]: " + data[j].IsTracked);
                }

                if (currentTrack != -1)
                {
                    if (data[currentTrack] != null)
                    {
                        if (data[currentTrack].IsTracked)
                        {
                            //trackedIds.Add(data[0].TrackingId);
                            UpdateBody(data[currentTrack]);
                            //Debug.Log("Nuevo cuerpo");
                        }
                    }
                }
            }
		}

        private UnityEngine.Vector4 ConvertVector (Windows.Kinect.Vector4 toConvert)
        {
            UnityEngine.Vector4 converted = new UnityEngine.Vector4();
            converted.x = (float) toConvert.X;
            converted.y = (float) toConvert.Y;
            converted.z = (float) toConvert.Z;
            converted.w = (float) toConvert.W;
            return converted;
        }

        public BodyPointPosition ReturnPosition(BodyParts joint)
        {
            BodyPointPosition pos = new BodyPointPosition();
            pos.name = joint;
            if (currentBody == null)
            {
                pos.x = 0;
                pos.y = 0;
                pos.z = 0;
            } else
            {
                Windows.Kinect.Joint sourceJoint = currentBody.Joints[bodyPartsTranslation[joint]];
                UnityEngine.Vector4 coord = new UnityEngine.Vector4(sourceJoint.Position.X, sourceJoint.Position.Y, sourceJoint.Position.Z, 1.0f);
                UnityEngine.Vector4 newCoord = rotMatrix.inverse * coord;

                if (joint == BodyParts.ElbowLeft)
                {
                    if(oldText)
                    oldText.text = ("Posicion Old " + joint + ": " + sourceJoint.Position.X + " " + sourceJoint.Position.Y + " " + sourceJoint.Position.Z + " ");

                }

                pos.x = newCoord.x;
                pos.y = newCoord.y + floor.w;
                pos.z = newCoord.z;
            }
            return pos;
        }


        void UpdateBody (Body body){
            currentBody = body;
		}

        void OnApplicationQuit()
        {

            if (sensor != null)
            {

                if (sensor.IsOpen)
                {
                    sensor.Close();
                }
            }
        }
    }
}