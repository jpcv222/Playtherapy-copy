using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FollowPlayerFromDistance))]
public class FollowPlayerBehaviourScript : Editor 
{

	public override void OnInspectorGUI ()
	{
		FollowPlayerFromDistance mapGen = (FollowPlayerFromDistance)target;
		if (DrawDefaultInspector())
		{
			mapGen.PutInRightPosition ();
		}


	}

}

