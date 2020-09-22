//using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MovementDetectionLibrary{

	public enum BodyParts
	{
        SpineBase,
        SpineMid,
        Neck,
        Head,
        ShoulderLeft,
        ElbowLeft,
        WristLeft,
        HandLeft,
        ShoulderRight,
        ElbowRight,
        WristRight,
        HandRight,
        HipLeft,
        KneeLeft,
        AnkleLeft,
        FootLeft,
        HipRight,
        KneeRight,
        AnkleRight,
        FootRight,
        SpineShoulder,
        HandTipLeft,
        ThumbLeft,
        HandTipRight,
        ThumbRight,
    };


	public struct BodyPointPosition
	{
		public BodyParts name;
		public float x;
		public float y;
		public float z;
	}

	public class BodyPoint {

		private BodyPointPosition currentPosition;
		public BodyParts pointRepresented;


		public BodyPoint(BodyParts point)
		{
			pointRepresented = point;
			Console.WriteLine("Inicia " + point.ToString());
			currentPosition.name = point;
			currentPosition.x = 0.0f;
			currentPosition.y = 0.0f;
			currentPosition.z = 0.0f;
		}

		public BodyPointPosition getCurrentPosition()
		{
			return currentPosition;
		}

		public void setPosition(BodyPointPosition position)
		{
			currentPosition = position;
		}




	}

}