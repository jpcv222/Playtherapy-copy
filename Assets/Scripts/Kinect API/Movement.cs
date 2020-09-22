using UnityEngine;
using System;
using System.Collections;

namespace MovementDetectionLibrary
{

	public class Movement {


		// name of the movement
		private string name;
		// angle initial of the movement
		private double initialAngle;
		// angle final of the movement
		private double finalAngle;
		//Body to get the position of the joints 
		private FullBody myBody;

		// joint to set the point one of the vector one 
		private BodyPointPosition pointOne;

		// joint vertex of the angle, point of intersection
		private BodyPointPosition pointCenter;

		//joint to set the point two of the vector two
		private BodyPointPosition pointTwo;

		// angle of movement 
		private double deltaAngle;

		// percent of the max movement
		private double percentaje;

		// bool to indicate if the movement start
		private bool initial;

        //Get angle of two vectors
		public double getAngleJoints(BodyPointPosition pointOne, BodyPointPosition pointCenter, BodyPointPosition pointTwo)
		{


			BodyPointPosition positionOne = pointOne;
			//cout << positionOne.x << ", " << positionOne.y << ", " << positionOne.z << endl;
			BodyPointPosition positionCenter = pointCenter;
			//cout << positionCenter.x << ", " << positionCenter.y << ", " << positionCenter.z << endl;
			BodyPointPosition positionTwo = pointTwo;
			//cout << positionTwo.x << ", " << positionTwo.y << ", " << positionTwo.z << endl;

			double[] vecAB = { positionOne.x - positionCenter.x, positionOne.y - positionCenter.y, positionOne.z - positionCenter.z };
			double[] vecBC = { positionTwo.x - positionCenter.x, positionTwo.y - positionCenter.y, positionTwo.z - positionCenter.z };

			double magAB = Math.Sqrt(vecAB[0] * vecAB[0] + vecAB[1] * vecAB[1] + vecAB[2] * vecAB[2]);
			double magBC = Math.Sqrt(vecBC[0] * vecBC[0] + vecBC[1] * vecBC[1] + vecBC[2] * vecBC[2]);

			double[] vecNormAB = { vecAB[0] / magAB, vecAB[1] / magAB, vecAB[2] / magAB };
			double[] vecNormBC = { vecBC[0] / magBC, vecBC[1] / magBC, vecBC[2] / magBC };

			double producto = vecNormAB[0] * vecNormBC[0] + vecNormAB[1] * vecNormBC[1] + vecNormAB[2] * vecNormBC[2];
			double angulo = Math.Acos(producto) * 180.0f / (Math.PI);

			return angulo;
		}



        // Get the poit of max vector 
        public Vector3 getVector(Vector3 pointOne, Vector3 pointTwo, double angle)
        {
            Vector3 maxPosition = new Vector3();

            Vector3 positionIni = pointTwo - pointOne;

            double[] arrayOne = new double[] { Math.Cos(angle), 0, Math.Sin(angle)};
            double[] arrayTwo = new double[] { 0, 1, 0 };
            double[] arrayThree = new double[] { -1 * Math.Sin(angle), 0, Math.Cos(angle) };

            maxPosition.x = (float)(positionIni.x * arrayOne[0] + positionIni.y * arrayOne[1] + positionIni.z * arrayOne[2]);
            maxPosition.y = (float)(positionIni.x * arrayTwo[0] + positionIni.y * arrayTwo[1] + positionIni.z * arrayTwo[2]);
            maxPosition.z = (float)(positionIni.x * arrayThree[0] + positionIni.y * arrayThree[1] + positionIni.z * arrayThree[2]);

            Debug.Log("position trans " + maxPosition.x + " " + maxPosition.y + " " + maxPosition.z);


            return maxPosition +pointOne;

        }



        //Calculate the point two
        public Vector3 calculateVectorPosition(Vector3 pointOne, Vector3 pointTwo)
        {

            Vector3 pointResult = pointTwo - pointOne;

            pointResult.y = pointOne.y - pointResult.magnitude;

            return pointResult;

        }

    }
}
