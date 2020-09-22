using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
public class SpannerOfMovements : MonoBehaviour {

	public GameObject PlanesParentArray;
	GameObject GemsParentArray;
	[Range (0,40)]
	public int maxPlanesInScreen;
	[Range (1,60)]
	public float SecondsPerPlane;
	public GameObject[] planes_types;
	public GameObject[] gems_types;
	// Use this for initialization
	public float distanceFromCenter =15;
	List<SideData> repeticiones_lados;
	float timer =0;
	void Start () {
		PlanesParentArray = GameObject.Find ("PlanesArray");
		GemsParentArray = GameObject.Find ("GemsArray");

	}
	public void setup()
	{
		timer = 0;
		repeticiones_lados = new List<SideData>();
		if (HoldParametersGreatJourney.use_time==false) {
			FillRepetitions ();
		}
		SecondsPerPlane = HoldParametersGreatJourney.select_descanso;


	}
	void FillRepetitions()
	{

		repeticiones_lados = new List<SideData> ();
		switch (HoldParametersGreatJourney.lados_involucrados) {
		case HoldParametersGreatJourney.LADO_TODOS:
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_DERECHO));
			}
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_IZQUIERDO));
			}
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_ABAJO));
			}
			break;
		case HoldParametersGreatJourney.LADO_IZQ_DER:
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_DERECHO));
			}
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_IZQUIERDO));
			}
			break;
		case HoldParametersGreatJourney.LADO_DERECHO:
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_DERECHO));
			}
			break;
		case HoldParametersGreatJourney.LADO_IZQUIERDO:
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_IZQUIERDO));
			}
			break;
		case HoldParametersGreatJourney.LADO_ABAJO:
			for (int i = 0; i < HoldParametersGreatJourney.select_jugabilidad; i++) {
				repeticiones_lados.Add (new SideData(HoldParametersGreatJourney.LADO_ABAJO));
			}
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		int actual_numbers_planes= PlanesParentArray.transform.childCount;

		if (timer<=0 && actual_numbers_planes<maxPlanesInScreen) {

			timer = SecondsPerPlane;
			releaseObject ();

		}
	}
	SideData pullRepetition()
	{
		SideData side;

		int randomIndex = (int)Random.Range (0, repeticiones_lados.Count);

		side = repeticiones_lados [randomIndex];
		repeticiones_lados.RemoveAt (randomIndex);



		return side;
	}
	public void releaseObject()
	{
		

		int time;

		if (HoldParametersGreatJourney.sostener_aleatorio == true) {
			time = (int)Mathf.Floor (Random.Range (0, HoldParametersGreatJourney.select_sostener));

		} else {
			time = Random.Range(0,(int)HoldParametersGreatJourney.select_sostener);
		}



		if (HoldParametersGreatJourney.use_time == true) {

			desideWhichSideSend (time);

		} else {
			

			if (HoldParametersGreatJourney.repeticiones_restantes>0) 
			{
				SideData movement = pullRepetition (); 
				HoldParametersGreatJourney.repeticiones_restantes--;
				switch (movement.side) {
				case  HoldParametersGreatJourney.LADO_DERECHO:
					sendMoveToRight (time);
					break;
				case  HoldParametersGreatJourney.LADO_IZQUIERDO:
					sendMoveToLeft(time);
					break;
				case  HoldParametersGreatJourney.LADO_ABAJO:
					sendMoveDown (time);
					break;

				default:
					break;
				}
			}

		}




	}
	public void desideWhichSideSend(int time)
	{
		switch (HoldParametersGreatJourney.lados_involucrados) {

		case HoldParametersGreatJourney.LADO_IZQUIERDO:
			sendMoveToLeft (time);
			break;
		case HoldParametersGreatJourney.LADO_DERECHO:
			sendMoveToRight (time);
			break;
		case HoldParametersGreatJourney.LADO_ABAJO:
			sendMoveDown (time);
			break;
		case HoldParametersGreatJourney.LADO_IZQ_DER:
			if (Mathf.Floor (Random.Range (0, 2)) > 1) 
			{
				sendMoveToLeft(time);
			} 
			else
			{
				sendMoveToRight(time);
			}	
			break;
		case HoldParametersGreatJourney.LADO_TODOS:
			switch (Mathf.FloorToInt (Random.Range (0, 3))) {
			case 0:
				sendMoveToLeft (time);
				break;
			case 1:
				sendMoveToRight (time);
				break;
			case 2:
				sendMoveDown (time);
				break;
			default:
				break;
			}
			break;

		}

	}
	/// <summary>
	/// Sends the move down.
	/// The planes gone to be send for upper the player to avoid
	/// </summary>
	/// <param name="time">Time.</param>
	/// <param name="speed">Speed.</param>
	public void sendMoveDown(int time= 1, float speed=5)
	{
		switch (time) {
		case 0:
			createSmallPlane (new Vector3 (-9, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (0, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 0), speed);
			createGem (new Vector3 (0, -5f, 15), speed);
			break;
		case 1:
			createWarPlane (new Vector3 (-6, -0.5f, 0), speed);
			createWarPlane (new Vector3 (6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (0, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 15), speed);

			createGem (new Vector3 (0, -5f, 15), speed);
			break;
		case 2:
			createSmallPlane (new Vector3 (-3, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 0), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-6, -0.5f, 30), speed);
			createWarPlane (new Vector3 (6, -0.5f, 30), speed);
			createGem (new Vector3 (0, -5f, 30), speed);
			break;
		case 3:
			createSmallPlane (new Vector3 (-3, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-6, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 0), speed);

			createSmallPlane (new Vector3 (3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (6, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-6, -0.5f, 45), speed);
			createWarPlane (new Vector3 (6, -0.5f, 45), speed);
			createGem (new Vector3 (0, -5f, 45), speed);
			break;
		case 4:
			createWarPlane (new Vector3 (-6, -0.5f, 0), speed);
			createWarPlane (new Vector3 (6, -0.5f, 0), speed);
			createWarPlane (new Vector3 (0, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-6, -0.5f, 30), speed);
			createWarPlane (new Vector3 (6, -0.5f, 30), speed);
			createWarPlane (new Vector3 (0, -0.5f, 45), speed);
			createWarPlane (new Vector3 (-6, -0.5f, 60), speed);
			createWarPlane (new Vector3 (6, -0.5f, 60), speed);
			createWarPlane (new Vector3 (6, -0.5f, 75), speed);
			createGem (new Vector3 (0, -5f, 45), speed);
			break;
		case 5:
			createWarPlane (new Vector3 (-9, -5f, 0), speed);
			createWarPlane (new Vector3 (9, -5f, 0), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (6, -5f, 30), speed);
			createSmallPlane (new Vector3 (-6, -5f, 30), speed);
			createWarPlane (new Vector3 (-9, -0.5f, 45), speed);
			createWarPlane (new Vector3 (9, -0.5f, 45), speed);
			createGem (new Vector3 (0, -5f, 45), speed);
			createSmallPlane (new Vector3 (6, -0.5f, 60), speed);
			createSmallPlane (new Vector3 (-6, -0.5f, 60), speed);
			createWarPlane (new Vector3 (0, -0.5f, 75), speed);
			createWarPlane (new Vector3 (0, -0.5f, 90), speed);
			createGem (new Vector3 (0, -5f, 60), speed);
			break;
		case 6:
			createWarPlane (new Vector3 (-9, -5f, 0), speed);
			createWarPlane (new Vector3 (9, -5f, 0), speed);
			createWarPlane (new Vector3 (-9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-9, -5f, 30), speed);
			createWarPlane (new Vector3 (9, -5f, 30), speed);
			createWarPlane (new Vector3 (0, -0.5f, 45), speed);
			createWarPlane (new Vector3 (-9, -5f, 60), speed);
			createWarPlane (new Vector3 (9, -5f, 60), speed);
			createWarPlane (new Vector3 (-9, -5f, 90), speed);
			createWarPlane (new Vector3 (9, -5f, 90), speed);
			createWarPlane (new Vector3 (0, -0.5f, 105), speed);
			createGem (new Vector3 (0, -5f, 60), speed);
			break;

		default:
			break;
		}




	}
	/// <summary>
	/// Sends the move to right.
	/// The planes gone to be send for the left line to avoid
	/// </summary>
	/// <param name="time">Time.</param> used to know how much have the patient in this potition
	/// <param name="speedPlane">Time.</param> used to put the speed of the planes
	public void sendMoveToRight(int time=1,float speed=5)
	{

		// we are goint to send the wave of planes or airballons
		switch (time) {
		case 0: 
			createSmallPlane (new Vector3 (-9, -0.5f, 0), speed);
			//createAirBalloon (new Vector3 (-6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (0, -0.5f, 0), speed);
			//createAirBalloon (new Vector3 (3, -0.5f, 0), speed);
			createGem (new Vector3 (9, -0.5f, 15), speed);
			break;
		case 1:
			createWarPlane (new Vector3 (-6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 15), speed);
			createGem (new Vector3 (9, -0.5f, 15), speed);
			break;
		case 2:
			createSmallPlane (new Vector3 (-3, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 0), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 30), speed);
			createGem (new Vector3 (9, -0.5f, 30), speed);
			break;
		case 3:
			createSmallPlane (new Vector3 (-6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 45), speed);
			createSmallPlane (new Vector3 (-9, -0.5f, 45), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 60), speed);
			createGem (new Vector3 (9, -0.5f, 60), speed);
			break;
		case 4:
			createWarPlane (new Vector3 (-6, -0.5f, 0), speed);
			createWarPlane (new Vector3 (-4, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-1, -0.5f, 30), speed);
			//createWarPlane (new Vector3 (-0, -0.5f, 45), speed);
			createWarPlane (new Vector3 (1, -0.5f, 60), speed);
			createWarPlane (new Vector3 (1, -0.5f, 75), speed);
			createGem (new Vector3 (9, -0.5f, 60), speed);
			break;
		case 5:
			createWarPlane (new Vector3 (-9, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-1, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (-1, -0.5f, 45), speed);
			createWarPlane (new Vector3 (0, -0.5f, 75), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 90), speed);
			createGem (new Vector3 (9, -0.5f, 90), speed);
			break;
		case 6:
			createSmallPlane (new Vector3 (-1, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 0), speed);

			createWarPlane (new Vector3 (0, -0.5f, 15), speed);
			createWarPlane (new Vector3 (-4, -4, 15), speed);
			createWarPlane (new Vector3 (-6, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 45), speed);
			createWarPlane (new Vector3 (0, -0.5f, 75), speed);
			createSmallPlane (new Vector3 (3, -2f, 90), speed);
			createSmallPlane(new Vector3 (5, -0.5F, 105), speed);
			createGem (new Vector3 (9, -0.5f, 90), speed);
			break;
		default:
			break;
		}
	}
	/// <summary>
	/// Sends the move to left.
	/// The planes gone to be send for the left line to avoid
	/// </summary>
	/// <param name="time">Time.</param> used to know how much have the patient in this potition
	/// <param name="speedPlane">Time.</param> used to put the speed of the planes
	public void sendMoveToLeft(int time=1,float speed=5)
	{

		// we are goint to send the wave of planes or airballons
		switch (time) {
		case 0: 
			createSmallPlane (new Vector3 (9, -0.5f, 0), speed);
			//createAirBalloon (new Vector3 (6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (0, -0.5f, 0), speed);
			//createAirBalloon (new Vector3 (-3, -0.5f, 0), speed);
			createGem (new Vector3 (-9, -0.5f, 15), speed);
			break;
		case 1:
			createWarPlane (new Vector3 (6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-1, -0.5f, 15), speed);
			createGem (new Vector3 (-9, -0.5f, 15), speed);
			break;
		case 2:
			createSmallPlane (new Vector3 (3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 45), speed);
			createGem (new Vector3 (-9, -0.5f, 45), speed);
			break;
		case 3:
			createSmallPlane (new Vector3 (6, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 15), speed);
			createWarPlane (new Vector3 (1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (3, -0.5f, 45), speed);
			createSmallPlane (new Vector3 (9, -0.5f, 45), speed);
			createWarPlane (new Vector3 (1, -0.5f, 60), speed);
			createGem (new Vector3 (-9, -0.5f, 60), speed);
			break;
		case 4:
			createWarPlane (new Vector3 (6, -0.5f, 0), speed);
			createWarPlane (new Vector3 (4, -0.5f, 15), speed);
			createWarPlane (new Vector3 (2, -0.5f, 30), speed);
			createWarPlane (new Vector3 (0, -0.5f, 45), speed);
			createWarPlane (new Vector3 (-2, -0.5f, 60), speed);
			createWarPlane (new Vector3 (-2, -0.5f, 75), speed);
			createGem (new Vector3 (-9, -0.5f, 60), speed);
			break;
		case 5:
			createWarPlane (new Vector3 (9, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 15), speed);
			createSmallPlane (new Vector3 (-1, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (1, -0.5f, 45), speed);
			createWarPlane (new Vector3 (0, -0.5f, 75), speed);
			createSmallPlane (new Vector3 (-1, -0.5f, 90), speed);
			createGem (new Vector3 (-9, -0.5f, 90), speed);
			break;
		case 6:
			createSmallPlane (new Vector3 (1, -0.5f, 0), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 0), speed);

			createWarPlane (new Vector3 (0, -0.5f, 15), speed);
			createWarPlane (new Vector3 (4, -4, 15), speed);
			createWarPlane (new Vector3 (6, -0.5f, 30), speed);
			createSmallPlane (new Vector3 (-3, -0.5f, 45), speed);
			createWarPlane (new Vector3 (0, -0.5f, 75), speed);
			createSmallPlane (new Vector3 (-3, -2f, 90), speed);
			createSmallPlane(new Vector3 (-5, -0.5F, 105), speed);
			createGem (new Vector3 (-9, -0.5f, 90), speed);
			break;
		default:
			break;
		}
	}
	public void createSmallPlane(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[0];// small plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.transform.GetChild(0).tag = "Planes";

	}
	public void createWarPlane(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[1];// war plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.transform.GetChild(0).tag = "Planes";

	}
	public void createAirBalloon(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject planeType = planes_types[2];// war plane

		GameObject plane = (GameObject)Instantiate(planeType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = plane.AddComponent<SetVelocity>();
		velocity.speed = speed;
		plane.transform.parent = PlanesParentArray.transform;
		plane.tag = "Airballoon";

	}
	public void createGem(Vector3 initialPos=default(Vector3),float speed=5)
	{
		GameObject gemType = gems_types[0];// gem

		GameObject gem = (GameObject)Instantiate(gemType, transform.position + initialPos, Quaternion.identity);
		SetVelocity velocity = gem.AddComponent<SetVelocity>();
		velocity.speed = speed;
		gem.transform.parent = GemsParentArray.transform;
		gem.tag = "Coins";

	}
	private void TweenRotate()
	{
		float startAngle = this.transform.rotation.eulerAngles.z;
		float endAngle = startAngle + 360.0f;
		this.gameObject.Tween("RotateCircle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t) =>
			{
				// progress
				this.transform.rotation = Quaternion.identity;
				this.transform.Rotate(-Camera.main.transform.right, t.CurrentValue);
			}, (t) =>
			{
				// completion
				this.gameObject.Tween("Rotate2Circle", startAngle, endAngle, 2.0f, TweenScaleFunctions.CubicEaseInOut, (t2) =>
					{
						// progress
						this.transform.rotation = Quaternion.identity;
						this.transform.Rotate(-Camera.main.transform.right, t2.CurrentValue);
					}, (t2) =>
					{
						// completion
					});
			});
	}


	class SideData
	{
		public int side;

		public SideData(int side_=HoldParametersGreatJourney.LADO_DERECHO)
		{
			side = side_;
		}


	}
}
