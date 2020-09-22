using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for your own scripts make sure to add the following line:
using DigitalRuby.Tween;
public class CatchAnimalAndRun : MonoBehaviour {


	public GameObject animal_tochase;
	public GameObject light_abduction;
	public int index_ship;
	float min_radius=60;
	float max_radius=80;

	Vector3 position_to_run;
	public float speed =0.1f;
	public bool isAbduccing = false;
	SoundsInVecinosInvasores sound_handler;



	Vector3 last_pos;
	// Use this for initialization
	void Start () {
		//animal_tochase = GameObject.Find("animal");

		sound_handler = GameObject.FindObjectOfType<SoundsInVecinosInvasores> ();

		if (sound_handler.sound_teleport.isPlaying==true) {
			sound_handler.sound_teleport.Stop();
		}
		sound_handler.sound_teleport.Play();

		GameObject[] animals = GameObject.FindGameObjectsWithTag ("Animals");

		List<GameObject> temp_animals_free = new List<GameObject> ();

		for (int i = 0; i < animals.Length; i++) {
			if (animals[i].GetComponent<AnimalData>().isChasen==false && animals[i].GetComponent<AnimalData>().isCaught==false) {
				temp_animals_free.Add(animals[i]);
			}
		}



		if (temp_animals_free.Count > 0) {
			int random = Random.Range (0, temp_animals_free.Count);

			animal_tochase = temp_animals_free [random];
			animal_tochase.GetComponent<AnimalData> ().isChasen = true;


		} else {
			
			ManagerVecinosInvasores manager = GameObject.FindObjectOfType<ManagerVecinosInvasores> ();
			manager.timer_game = -1;
			HoldParametersVecinosInvasores.repeticiones_restantes = 0;
		}




	}
	private void TweenAbductAnimal()
	{
		if (animal_tochase!=null) 
		{
			switch (animal_tochase.name) {

			case "sheep":
				if (sound_handler.sound_sheep.isPlaying==true) {
					sound_handler.sound_sheep.Stop();
				}
				sound_handler.sound_sheep.Play();
				break;

			case "pig":
				if (sound_handler.sound_pig.isPlaying==true) {
					sound_handler.sound_pig.Stop();
				}
				sound_handler.sound_pig.Play();
				break;
			case "hen":
				if (sound_handler.sound_hen.isPlaying==true) {
					sound_handler.sound_hen.Stop();
				}
				sound_handler.sound_hen.Play();
				break;

			default:
				break;
			}
			isAbduccing = true;
			light_abduction.SetActive(true);
			this.gameObject.Tween("AbductAnimal"+index_ship, animal_tochase.transform.position, transform.position, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) =>
				{
					// progress
					animal_tochase.transform.position =t.CurrentValue;

				}, (t) =>
				{
					if (sound_handler.sound_abdution.isPlaying==true) {
						sound_handler.sound_abdution.Stop();
					}
					sound_handler.sound_abdution.Play();

					isAbduccing = false;
					light_abduction.SetActive(false);
					animal_tochase.SetActive(false);
					seeWhereToRunWithAnimal();


					if (this.gameObject.activeSelf==false) {
						releaseAnimal();
					}

					//complete
				});
			
		}




	}
	private void TweenResetAnimal()
	{
		if (animal_tochase != null) {





			animal_tochase.SetActive (true);
			animal_tochase.transform.parent = GameObject.Find("Animals").transform;
			this.gameObject.Tween ("ResetAnimal" + index_ship, animal_tochase.transform.position, Vector3.up * 4, 0.75f, TweenScaleFunctions.QuadraticEaseOut, (t) => {
				// progress
				animal_tochase.transform.position = t.CurrentValue;

			}, (t) => {
				animal_tochase.GetComponent<Rigidbody> ().isKinematic = false;
				animal_tochase.GetComponent<AnimalData> ().isCaught = false;
				animal_tochase.GetComponent<AnimalData> ().isChasen = false;
				animal_tochase.GetComponent<Rigidbody> ().useGravity = true;

				destroyScript();
				//complete
			});

		
		}
	}
	public void releaseAnimal()
	{
		if (sound_handler.sound_explotion.isPlaying==true) {
			sound_handler.sound_explotion.Stop();
		}
		sound_handler.sound_explotion.Play();
		//print ("release animal");
		if (animal_tochase!=null) {
			if (animal_tochase.GetComponent<AnimalData>().isCaught==true) 
			{
				//print ("reset animal");
				TweenResetAnimal ();
			}
		}

	}
	void seeWhereToRunWithAnimal()
	{
		
//		float coordX = Random.Range (min_radius, min_radius);
//		float coordZ = Random.Range (min_radius, max_radius);

		float coordX = 40;
		float coordZ = 40;
		if (Random.Range (0, 2) == 0) {
			coordX = -coordX;
		}

		if (Random.Range (0, 2) == 0) {
			coordZ = -coordZ;
		}
		position_to_run = new Vector3 (coordX * Mathf.Cos (Random.Range (0, 360)), transform.position.y, coordZ * Mathf.Sin (Random.Range (0, 360)));

		//make animation of catch animal

	}
	void followAnimal()
	{
		if (animal_tochase!=null && isAbduccing==false) 
		{
			Vector3 vect = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.PingPong (Time.time * -10, 10));
			transform.eulerAngles = vect;

			if (animal_tochase.GetComponent<AnimalData> ().isCaught == false) {


				Vector3 pos_animal = new Vector3 (animal_tochase.transform.position.x, transform.position.y, animal_tochase.transform.position.z);
				Vector3 dir = pos_animal - transform.position;
				dir = dir.normalized;
				transform.Translate (dir * speed, Space.World);
				transform.LookAt (pos_animal);

				if (Mathf.Abs(Vector3.Distance (transform.position, pos_animal)) < 0.05) {

					animal_tochase.transform.parent = this.transform;
					animal_tochase.GetComponent<AnimalData> ().isCaught = true;
					animal_tochase.GetComponent<Rigidbody> ().isKinematic = true;
					animal_tochase.GetComponent<Rigidbody> ().useGravity = false;

					TweenAbductAnimal ();
				}

			} 
			else
			{
				//run for your life
				Vector3 dir = position_to_run - transform.position;
				dir = dir.normalized;
				transform.Translate (dir * speed, Space.World);
				transform.LookAt (position_to_run);


				float distance = Mathf.Abs (Vector3.Distance (transform.position, position_to_run)); 

				if ( distance< 0.05) {

					escape ();
				} else {

					if (distance > 70) {
						escape ();
					}
				}


			}
		}


	}
	void escape(){

		if (sound_handler.sound_escape.isPlaying == true) {
			sound_handler.sound_escape.Stop ();
		}
		sound_handler.sound_escape.Play ();

		//Destroy (this.gameObject);
		this.gameObject.SetActive (false);
		destroyScript ();
		Destroy (animal_tochase);
		ScoreHandlerVecinosInvasores score_handler = FindObjectOfType<ScoreHandlerVecinosInvasores> ();
		score_handler.sum_score (-8);


	}
	public void destroyScript()
	{
		Destroy (this);
	}
	// Update is called once per frame
	void Update () {

		if (Time.timeScale==1) 
		{
			followAnimal ();


			if (last_pos.Equals(transform.position) && animal_tochase==null) {
				this.gameObject.SetActive(false);
				destroyScript ();
			}
			last_pos= transform.position;
		}

	}
}
