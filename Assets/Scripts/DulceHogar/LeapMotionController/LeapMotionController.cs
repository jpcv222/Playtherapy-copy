using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMotionController : MonoBehaviour
{
	/*Scripts para detectar la orientacion de la mano y convertirla en un vector3 de movimientos*/
	public GameObject middle_L;
//Dedo Medio Izquierda
	public GameObject middle_R;
//Dedo Medio Deracha
	private Transform transformMiddle_L;
	private Transform transformMiddle_R;
	// Use this for initialization
	void Start ()
	{
		transformMiddle_L = middle_L.GetComponent<Transform> ();
		transformMiddle_R = middle_R.GetComponent<Transform> ();

	}
	/**************************************************************Controlador de Leapmotion ***********************************************************************/
	float InputLeapMotionLeftHorizontal ()
	{

		/*Esta funcion detecta la orientacion del dedo
		medio y calcula asia que lado se testa desviando para Movimiento Horizontal*/
		float movimiento = 0.0f;
		Vector3 dedoMedioPosition = transformMiddle_L.transform.localPosition;
		Debug.Log ("Dedo Posicion z " + dedoMedioPosition);
		float desplasamientoDedo = dedoMedioPosition.x;
		if (desplasamientoDedo < 0.0f) {
			Debug.Log ("Izquierda");
			movimiento = -1.0f;
		}

		if (desplasamientoDedo > 0.0f) {
			Debug.Log ("Derecha ");
			movimiento = 1.0f;
		}
		return movimiento;
	}

	float InputLeapMotionRightVertical ()
	{
		/*Esta funcion detecta la orientacion del dedo
		medio y calcula asia que lado se esta desviando Para Movimiento Vertical*/
		float movimiento = 0.0f;
		Vector3 dedoMedioPosition = transformMiddle_R.transform.localPosition;
		Debug.Log ("Dedo Posicion z " + dedoMedioPosition);
		float desplasamientoDedo = dedoMedioPosition.y;
		if (desplasamientoDedo < 0.2f) {
			Debug.Log ("Izquierda");
			movimiento = -1.0f;
		}

		if (desplasamientoDedo > 0.2f) {
			Debug.Log ("Derecha");
			movimiento = 1.0f;
		}
		return movimiento;
	}
	/*Controlador de Leapmotion */
	float InputLeapMotionLeftVertical ()
	{

		/*Esta funcion detecta la orientacion del dedo
		medio y calcula asia que lado se testa desviando para Movimiento Veritical*/
		float movimiento = 0.0f;
		Vector3 dedoMedioPosition = transformMiddle_L.transform.localPosition;
		Debug.Log ("Dedo Posicion z " + dedoMedioPosition);
		float desplasamientoDedo = dedoMedioPosition.y;
		if (desplasamientoDedo < 0.2f) {
			Debug.Log ("Down");
			movimiento = -1.0f;
		}

		if (desplasamientoDedo > 0.2f) {
			Debug.Log ("Up");
			movimiento = 1.0f;
		}
		return movimiento;
	}
}
