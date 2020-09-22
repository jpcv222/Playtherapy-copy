using System.Collections;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (MapGeneratorFM))]
public class MapEditorFM : Editor {

	public override void OnInspectorGUI ()
	{
		MapGeneratorFM map = target as MapGeneratorFM;
		if (DrawDefaultInspector ()) {
			map.GenerateMap ();
		}

		if (GUILayout.Button("Generate")) 
		{
			map.GenerateMap ();	
		}

	}

}
