using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class EnemyFM : MonoBehaviour {






	public System.Action OnDeath;
	public List<Image> array_images;
	public List<Sprite> array_sprites;
	public GameObject my_canvas;
	UnityEngine.AI.NavMeshAgent pathfinder;
	Transform target;


	public List<int> index_images;
	GesturesShapeManager manager;

	public enum State{Iddle, Chasing, Attacking,Dead};
	State currentState;
	float attactDistanceThreshold = 5f;
	float timeBeetweenAttacks=3;
	float nextAttackTime;
	float myCollisionRadius;
	float targetCollisionRadius;


	Animator animator_controller;
	EffectMagicFM effets;
	ScoreHandlerFM score_script;
	// Use this for initialization
	void Awake()
	{
		score_script =  FindObjectOfType<ScoreHandlerFM> ();
		effets = FindObjectOfType<EffectMagicFM> ();
		animator_controller = GetComponent<Animator> ();
		pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		currentState = State.Chasing;
		target = GameObject.Find ("Player").transform;
		StartCoroutine (UpdatePath());

		myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
		targetCollisionRadius = target.GetComponent<CapsuleCollider> ().radius;


		manager = GameObject.FindObjectOfType<GesturesShapeManager> ();
		manager.onShapeRecognition += onRecognizeShape;

		array_sprites = new List<Sprite> ();
		array_images = new List<Image> ();
		index_images = new List<int> ();




	}
	void OnDestroy()
	{
		
	}

	public void addGesture(Sprite gesture_sprite,Color color, int index)
	{
		index_images.Add (index);
		array_sprites.Add (gesture_sprite);
		GameObject new_obj = new GameObject (gesture_sprite.name);
		new_obj.transform.parent = my_canvas.transform;



		Image image_gesture = new_obj.AddComponent<Image> ();


		image_gesture.rectTransform.sizeDelta = new Vector2 (4f, 4f);
		image_gesture.color = color;
		image_gesture.preserveAspect = true;
		image_gesture.rectTransform.localPosition= new Vector3(0,0.5f);
		array_images.Add (image_gesture);

		Sprite my_sprite=gesture_sprite;
		if (my_sprite!=null) {
			image_gesture.sprite = my_sprite;
		}
		orderImageGestures ();

	
	}
	void orderImageGestures()
	{
		if (array_images.Count>0)
		{
			float initY = 1f;
			float initX = 0;
			float anchoImagenes;
			Image tempImage = array_images [0];
			float separationX=0.1f;

			if (array_images.Count > 1) {

				float initialX;
				anchoImagenes = array_images.Count * (tempImage.rectTransform.rect.width*0.5f) + (array_images.Count - 1) * separationX;
				//print ("ancho:" + anchoImagenes);
				initialX = (float)(initX - anchoImagenes * 0.25f);
				//print ("initialX:" + initialX);
				Image item;
				float posX;
				float posY;
				for (int i = 0; i < array_images.Count; i++) {
					item = array_images [i];
					posX = initialX + (separationX+tempImage.rectTransform.rect.width*0.5f)*i;
					posY = initY;
					item.transform.localPosition = new Vector3 (posX, posY, 0);
					//print (item.transform.position);
				}



			} else {
				array_images [0].transform.localPosition = new Vector3(initX,initY,0);
			}
		}




	}
	void onRecognizeShape(PDollarGestureRecognizer.Result gestureData)
	{
		
		if (array_images!=null && array_images.Count>0) {

			if (array_images[0]!=null) 
			{


				if (gestureData.GestureClass== array_images[0].name)
				{

					score_script.sum_score (2);
					effets.startHitMagicIn (transform.position, index_images[0]);
					Image imageGesture=array_images[0];
					array_images.Remove (imageGesture);
					index_images.RemoveAt (0);
					Destroy (imageGesture.gameObject);
					orderImageGestures ();
					//reordenar imagenes gestures

					if (array_images.Count == 0) {

						currentState = State.Dead;
						pathfinder.enabled = false;
						animator_controller.SetTrigger ("is_dead");
						manager.onShapeRecognition -= onRecognizeShape;
						//FindObjectOfType<MapGeneratorFM> ().maps [0].seed = (int)Random.Range(0,20);
						//FindObjectOfType<MapGeneratorFM> ().GenerateMap ();
						if (OnDeath != null) {
							OnDeath ();
						}
						Destroy (this.gameObject, 3);

					} else {
						animator_controller.SetTrigger ("hit");
					}


				}

			}



		}



	}


	void Update () {

		if (currentState!=State.Dead) {
			
			if (Time.time>nextAttackTime) {
				float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

				float minimunDistance = Mathf.Pow (attactDistanceThreshold +myCollisionRadius*5+ targetCollisionRadius, 2);
				if (sqrDstToTarget< minimunDistance)
				{

					nextAttackTime = Time.time + timeBeetweenAttacks;
					//StartCoroutine(Attack());
					score_script.sum_score (-1);
					animator_controller.SetTrigger ("attack");
				}

			}
		}





	}
	IEnumerator Attack()
	{
		if (currentState != State.Dead) {
			
			currentState = State.Attacking;
			pathfinder.enabled = false;


			Vector3 originalPosition = transform.position;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);


			float attackSpeed = 3;
			float percent = 0;
			float interpolation;
			while (percent <= 1) {

				percent += Time.deltaTime * attackSpeed;
				interpolation = (-percent * percent + percent) * 4;
				//transform.position = Vector3.Lerp (originalPosition, attackPosition, interpolation);
				yield return null;
			}


			currentState = State.Chasing;
			pathfinder.enabled = true;
		} else {
			yield return null;
		}


	}

	IEnumerator UpdatePath()
	{
		
		float refreshRatePath = 0.25f;
		while (target!=null)
		{
			if (currentState == State.Chasing) {
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius+attactDistanceThreshold*0.5f);
				pathfinder.SetDestination (targetPosition);

			}
			yield return new WaitForSeconds(refreshRatePath);

		}

	}



	void LateUpdate()
	{
		if (my_canvas!=null) {
			my_canvas.transform.forward = Camera.main.transform.forward;

		}

	}

}
