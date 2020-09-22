using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MovementDetectionLibrary
{

	public class MovementsCollection {


		public Dictionary<BodyParts, BodyPoint> bodyPointsCollection;

		public void setBodyPointsCollection(Dictionary<BodyParts, BodyPoint> bodyPoints){

			bodyPointsCollection = bodyPoints;

		}

        public double headLateralAngle()
        {

            // lateral flexion
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));
        }

        public double headFrontalExtAngle()
        {

            //  frontal extension 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition(); 
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));
        }


        public double headFrontalFleAngle()
        {

            //  frontal flexion 

            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.Neck].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.Head].getCurrentPosition();
            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));
        }

    
        public double shoulderAbdRigthMovements()
        {                      
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderFlexRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }



        public double shoulderRotIntRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double shoulderRotExtRigthMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double elbowFleExtRigthMovement()
        {

            // Flexion

            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristRight].getCurrentPosition();
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.ShoulderRight].getCurrentPosition();

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipRigthAbMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();

            Movement objMove = new Movement();

            return Math.Abs(90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipRigthAdMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();

            Movement objMove = new Movement();

            return Math.Abs(90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }

        public double hipRigthFlexMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }

        public double hipRigthExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipRigthRotIntMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipRigthRotExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }







        public double kneeRigthMovement()
        {
            //Flexion
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeRight].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleRight].getCurrentPosition() ;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));
        }


        public double shoulderAbdLeftMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderFlexLeftMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double shoulderExtLeftMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ShoulderLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }



        public double shoulderRotIntLeftMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double shoulderRotExtLeftMovements()
        {


            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.z = pointOne.z - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double elbowFleExtLeftMovement()
        {

            // Flexion

            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.ElbowLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.WristLeft].getCurrentPosition();
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.ShoulderLeft].getCurrentPosition();

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipLeftAbMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();

            Movement objMove = new Movement();

            return Math.Abs(90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipLeftAdMovement()
        {
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipRight].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();

            Movement objMove = new Movement();

            return Math.Abs(90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }

        public double hipLeftFlexMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }

        public double hipLeftExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y + 0.2f;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }


        public double hipLeftRotIntMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }


        public double hipLeftRotExtMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleLeft].getCurrentPosition();
            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

        }

        public double kneeLeftMovement()
        {
            //Flexion
            BodyPointPosition pointOne = bodyPointsCollection[BodyParts.HipLeft].getCurrentPosition();
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.KneeLeft].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.AnkleLeft].getCurrentPosition();

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));
        }

        public double spineLatMovement_Abs()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.SpineBase].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();

            BodyPointPosition pointOne = pointCenter;
            pointOne.x = pointOne.x + 0.2f;

            Movement objMove = new Movement();

            return Math.Abs(90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }
		public double spineLatMovement()
		{
			BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.SpineBase].getCurrentPosition();
			BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();

			BodyPointPosition pointOne = pointCenter;
			pointOne.x = pointOne.x + 0.2f;

			Movement objMove = new Movement();

			return 90-objMove.getAngleJoints(pointOne, pointCenter, pointTwo);

		}
        public double spineIncMovement()
        {
            BodyPointPosition pointCenter = bodyPointsCollection[BodyParts.SpineBase].getCurrentPosition();
            BodyPointPosition pointTwo = bodyPointsCollection[BodyParts.SpineShoulder].getCurrentPosition();

            BodyPointPosition pointOne = pointCenter;
            pointOne.y = pointOne.y - 0.2f;

            Movement objMove = new Movement();

            return (180-objMove.getAngleJoints(pointOne, pointCenter, pointTwo));

        }

    }
}
