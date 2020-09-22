using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAngle : MonoBehaviour {

	public float viewRadius;
	[Range (-360,360)]
	public float viewAngle;
	[Range (0,360)]
	public float startAnglePos;
	public float meshResolution;
	public MeshFilter viewMeshFilter;
	Mesh viewMesh; 
	public CanvasRenderer canvas;
	public Material material;


	void Start()

	{
		
		viewMesh = new Mesh ();
		viewMesh.name="view Mesh";
		//viewMeshFilter.mesh = viewMesh;
		canvas.Clear ();
		canvas.SetMesh (viewMesh);
		canvas.SetMaterial (material,0);
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}

		return new Vector3(Mathf.Sin((startAnglePos+angleInDegrees)*Mathf.Deg2Rad),Mathf.Cos((startAnglePos+angleInDegrees)*Mathf.Deg2Rad),0);
	}

	void Update ()
	{
		DrawFieldView ();
	}
		

	void DrawFieldView()
	{
		List<Vector3> posVertex= new List<Vector3>();
		Vector3 pos;
		int stepCount = Mathf.RoundToInt (viewAngle* meshResolution);
		float StepAnlgeSize = viewAngle / stepCount;
		for (int i = 0; i < stepCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle + StepAnlgeSize * i;
			pos = transform.position + DirFromAngle (angle, true) * viewRadius;
			//Debug.DrawLine (transform.position, pos, Color.red);
			posVertex.Add (pos);
		}


		int vertexCount = posVertex.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices [0] = Vector3.zero;

		for (int i = 0; i < vertexCount-1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(posVertex [i]);

			if (i< vertexCount-2) {
				triangles [i * 3] = 0;
				triangles [i * 3+1] = i+1;
				triangles [i * 3+2] = i+2;
			}

		}
		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();

		canvas.Clear ();
		canvas.SetMesh (viewMesh);
		canvas.SetMaterial (material,0);
	}

}
