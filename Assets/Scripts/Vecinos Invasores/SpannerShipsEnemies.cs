using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
public class SpannerShipsEnemies : MonoBehaviour {


    int numberOfObjects = 100;
	int actual_ship_index;
	public int min_radius= 20;
	public int max_radius= 25;
	public GameObject[] array_types_ships;

	public List<GameObject> all__ships;

	public int LIMIT_TIME_TO_SPAWN;
	public float timer_spawn;
	// Use this for initialization

	public Dictionary<int ,Dictionary<Finger.FingerType,Material>> materials_by_ships;
	void Start () {


		materials_by_ships = new Dictionary<int, Dictionary<Finger.FingerType, Material>> ();

		Dictionary<Finger.FingerType,Material> temp;


		temp = new Dictionary<Finger.FingerType, Material> ();

		temp.Add(Finger.FingerType.TYPE_THUMB,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipSmall"));
		temp.Add(Finger.FingerType.TYPE_INDEX,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipSmall_red"));
		temp.Add(Finger.FingerType.TYPE_MIDDLE,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipSmall_blue"));
		temp.Add(Finger.FingerType.TYPE_RING,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipSmall_orange"));
		temp.Add(Finger.FingerType.TYPE_PINKY,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipSmall_pink"));


		materials_by_ships.Add (0, temp);

		temp = new Dictionary<Finger.FingerType, Material> ();

		temp.Add(Finger.FingerType.TYPE_THUMB,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipMedium"));
		temp.Add(Finger.FingerType.TYPE_INDEX,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipMedium_red"));
		temp.Add(Finger.FingerType.TYPE_MIDDLE,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipMedium_blue"));
		temp.Add(Finger.FingerType.TYPE_RING,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipMedium_orange"));
		temp.Add(Finger.FingerType.TYPE_PINKY,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipMedium_pink"));

		materials_by_ships.Add (1, temp);

		temp = new Dictionary<Finger.FingerType, Material> ();

		temp.Add(Finger.FingerType.TYPE_THUMB,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipBig"));
		temp.Add(Finger.FingerType.TYPE_INDEX,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipBig_red"));
		temp.Add(Finger.FingerType.TYPE_MIDDLE,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipBig_blue"));
		temp.Add(Finger.FingerType.TYPE_RING,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipBig_orange"));
		temp.Add(Finger.FingerType.TYPE_PINKY,(Material)Resources.Load("Materials/Vecinos Invasores/SpaceShipBig_pink"));

		materials_by_ships.Add (2, temp);


		all__ships = new List<GameObject> ();
		actual_ship_index = 0;
		for (int i = 0; i < numberOfObjects; i++) {
			createRandomShip ();
		}

	}
	void createRandomShip()
	{
		GameObject ship;
		EnemyData data;
		int randomShip = Random.Range (0, array_types_ships.Length);
		ship =Instantiate(array_types_ships[randomShip], transform.position, Quaternion.identity);
		ship.transform.position = Vector3.forward * 50;
		ship.transform.LookAt (transform.position);
		ship.transform.parent = this.transform;
		all__ships.Add (ship);
		data = ship.AddComponent<EnemyData> ();
		data.ship_random_index = randomShip;
		ship.SetActive (false);


	}

	void sendObject()
	{
		if (HoldParametersVecinosInvasores.use_time==true) {

			ManagerVecinosInvasores manager = GameObject.FindObjectOfType<ManagerVecinosInvasores> ();
			if (manager.timer_game>0) {
				sendShip ();
			}
		}
		else if (HoldParametersVecinosInvasores.repeticiones_restantes>0) {
			sendShip ();
			HoldParametersVecinosInvasores.repeticiones_restantes--;
		}

			

	}
	void sendShip()
	{

		if (actual_ship_index>=all__ships.Count) {
			actual_ship_index = 0;
		}

		GameObject ship= all__ships[actual_ship_index];

		ship.SetActive (true);



		float radius = Random.Range (min_radius, max_radius);

		float angle = Random.Range (0, 360) * Mathf.PI / 180;

//		float coordX = Random.Range (min_radius, max_radius);
//		float coordZ = Random.Range (min_radius, max_radius);
//		if (Random.Range(0,2) ==0) {
//			coordX = -coordX;
//		}
//
//		if (Random.Range(0,2) ==0) {
//			coordZ = -coordZ;
//		}
		Vector3 new_position = new Vector3 (radius*Mathf.Cos(angle), 0, radius*Mathf.Sin(angle));
		ship.transform.position = transform.position + new_position;

		CatchAnimalAndRun catchScript =  ship.AddComponent<CatchAnimalAndRun>();
		catchScript.index_ship = actual_ship_index;
		catchScript.light_abduction = ship.transform.Find ("LightShip").gameObject;
		catchScript.light_abduction.SetActive (false);

		actual_ship_index++;



			//sending a space ship random color 

			EnemyData data = ship.GetComponent<EnemyData> ();

			int index = data.ship_random_index;
		int randomFinger = 0;

		Finger.FingerType fingerUse=Finger.FingerType.TYPE_THUMB;

		if (HoldParametersVecinosInvasores.mode_game == HoldParametersVecinosInvasores.COLORS) 
		{
			randomFinger = Random.Range (0, HoldParametersVecinosInvasores.fingerTypes.Count);
			fingerUse = HoldParametersVecinosInvasores.fingerTypes [randomFinger];
		}
		data.setMaterialFinger(materials_by_ships[index][fingerUse],fingerUse);


	}
	public bool isActiveAnyShip()
	{
		bool isActive = false;

		foreach (var item in all__ships) {

			if (item.activeSelf==true) {
				isActive = true;
				break;
			}
		}

		return isActive;
	}
	// Update is called once per frame
	void Update () {



		if (timer_spawn > 0) {

			timer_spawn -= Time.deltaTime;

			if (timer_spawn <= 0) {
				
				timer_spawn = LIMIT_TIME_TO_SPAWN;
				sendObject ();
			}

		}
			

	}
}


