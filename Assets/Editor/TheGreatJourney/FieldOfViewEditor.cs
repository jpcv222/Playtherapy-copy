using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof(ShowAngle))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI()
	{

		ShowAngle sa = (ShowAngle)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (sa.transform.position, Vector3.forward, Vector3.up, 360,sa.viewRadius);
		Vector3 viewAngleA = sa.DirFromAngle (0, false);
		Vector3 viewAngleB = sa.DirFromAngle (-sa.viewAngle, false);

		Handles.DrawLine (sa.transform.position, sa.transform.position + viewAngleA * sa.viewRadius);
		Handles.DrawLine (sa.transform.position, sa.transform.position +viewAngleB * sa.viewRadius);
	}
}
