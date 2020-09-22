using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerAnimals : MonoBehaviour {

	public int radius= 10;
	public GameObject[] array_types_animals;
	// Use this for initialization

	public void crearAnimales(int numero_animales=10)
	{
		GameObject[] array_animals_scene = GameObject.FindGameObjectsWithTag ("Animals");


		if (array_animals_scene.Length>0) {

			foreach (var item in array_animals_scene) {
				Destroy (item);
			}
		}
		GameObject animal;
		for (int i = 0; i < numero_animales; i++) {


			int random = Mathf.RoundToInt (Random.Range (0, array_types_animals.Length));

			animal =Instantiate(array_types_animals[random], Vector3.zero, Quaternion.identity);
			animal.transform.position = new Vector3 (Random.Range (-(radius-4), (radius-4)), 1, Random.Range (-(radius-4), (radius-4)));


			string animal_name;


			switch (random) {

			case 0:
				animal_name = "sheep";
				break;
			case 1 :
				animal_name="pig";
				break;
			case 2 :
				animal_name="hen";
				break;

			default:
				animal_name="sheep";
				break;
			}


			animal.name=animal_name;
			animal.AddComponent<AnimalData> ();
			animal.transform.parent = this.transform;
			animal.transform.localRotation = Quaternion.Euler (Vector3.forward * 90);
			animal.tag = "Animals";
		}


	}
	// Update is called once per frame
	void Update () {
		
	}
}
