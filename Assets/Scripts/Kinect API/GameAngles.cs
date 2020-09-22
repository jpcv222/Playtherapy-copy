using UnityEngine;
using System.Collections;

namespace MovementDetectionLibrary
{
    public class GameAngles
    {
        private float angleDegree;
        private ArrayList arrayAngles;
        private string side;
        public GameAngles(float angle, bool front, bool lat)
        {

            this.angleDegree = angle;
			Debug.Log ("angleDegree " + angleDegree);
            arrayAngles = new ArrayList();

        }

        // Use this for initialization
        public Vector3 getPosition(Vector3 pointOne, Vector3 pointTwo, float angle, float correction, string plane)
        {

            Vector3 pointFin = pointTwo - pointOne;
            Vector3 pointOneD = pointTwo - pointOne;

            //Debug.Log ("magnitude" + (pointTwo - pointOne).magnitude);


            if (plane == "z")
            {

                pointFin.z = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1f;

            }
            else
            {
                pointFin.x = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1f;
            }


            //pointFin.z = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1.2f;
            pointFin.y = Mathf.Sin(angle) * (pointTwo - pointOne).magnitude * 1.2f;

            pointFin = correctionPosition(pointFin, plane, correction);


            pointFin = pointFin + pointOne;

            return pointFin;

        }

		// Use this for initialization
		public Vector3 getPositionWithCross(Vector3 pointOne, Vector3 pointTwo, float angle)
		{

			Vector3 pointFin = pointTwo - pointOne;
			Vector3 pointOneD = pointTwo - pointOne;

			//Debug.Log ("magnitude" + (pointTwo - pointOne).magnitude);


			pointFin.x = Mathf.Cos(angle) * (pointTwo - pointOne).magnitude * 1.0f;
			pointFin.y = Mathf.Sin(angle) * (pointTwo - pointOne).magnitude * 1.0f;

			System.Random rnd = new System.Random();
			int crossNumber = rnd.Next(5);

			if (side == "left")
			{
				if (crossNumber <= 1) {
					pointFin.x -= 0.45f;
				}
				//pointFin.x += 0.3f;
			}
			else
			{
				if (crossNumber <= 1) {
					pointFin.x += 0.45f;
				}
				//pointFin.x -= 0.3f;
			}




			pointFin = pointFin + pointOne;

			return pointFin;

		}

        // Return the second vector for the angle
        public Vector3 createPointTwoShoulderAF(Vector3 pointOne, Vector3 pointTwo)
        {

            pointTwo.y = pointOne.y - (pointTwo - pointOne).magnitude;
            pointTwo.x = pointOne.x;
            pointTwo.z = pointOne.z;

            return pointTwo;
        }

        //Return an random angle between 0 and the angle in randians, angle is a degree
        public float setRamdomAngle(string side, string plane)
        {
            this.side = side;
            if (arrayAngles.Count == 0)
            {
                setArrayAngles(this.angleDegree);
            }

            if (plane == "z")
            {
                if (side == "left")
                {
                    int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                    float angle = (float)(arrayAngles[position]);
                    arrayAngles.RemoveAt(position);

                    float angleRad = Mathf.Deg2Rad * angle;

                    Debug.Log("disp izq");
                    //Debug.Log ("angulo" + (angleRad - Mathf.PI / 2));
                    //return -Mathf.PI / 4;
                    return angleRad- Mathf.PI / 2;

                }
                else
                {

                    int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                    float angle = (float)(arrayAngles[position]);

                    arrayAngles.RemoveAt(position);

                    float angleRad = Mathf.Deg2Rad * angle;

                    Debug.Log("disp der ");
                    //return -Mathf.PI / 4;
                    //Debug.Log("angulo" + (3 * Mathf.PI / 2 - angleRad));
                    return 3 * Mathf.PI / 2 - angleRad;

                }

            }else
            {
                int position = Mathf.RoundToInt(Random.Range(0, arrayAngles.Count));
                float angle = (float)(arrayAngles[position]);
                arrayAngles.RemoveAt(position);

                float angleRad = Mathf.Deg2Rad * angle;

                Debug.Log("disp izq");
                //Debug.Log ("angulo" + (angleRad - Mathf.PI / 2));
                return angleRad - Mathf.PI/2;
            }


        }

        //Function to return a array with angles 
        private void setArrayAngles(float angle)
        {

            for (int i = 0; i < 6; i++)
            {
                this.arrayAngles.Add(angle - 5 * i);
            }
        }

        private Vector3 correctionPosition(Vector3 point, string plane, float correction)
        {
            if(plane == "z")
            {
                if (side == "left")
                {
                    point.z +=  correction;

                }else
                {
                    point.z -= correction;
                }
            }
            else
            {
                Debug.Log("Correccion x");
                point.x += correction;
            }

            return point;

        }


    }
}