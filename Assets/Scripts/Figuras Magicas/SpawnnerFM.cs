using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnnerFM : MonoBehaviour {

	public List<int> gestures_index_used;
	public bool can_spawn;
	public Wave[] waves;
	public EnemyFM[] enemies;
	public Sprite[] gesturesEnabled;
	public Color[] gesturesColor;
	Wave currentWave;
	int currentWaveIndex=-1;
	MapGeneratorFM map;

	public List<GameObject> current_enemies_objects;
	void Awake()
	{
		map = FindObjectOfType<MapGeneratorFM> ();
		can_spawn = false;
		//NextWave ();

	}


	public int enemiesRemainingToSpawn;
	public int enemiesRemainingAlive;
	float nextSpawnTime;

	void Update()
	{

		if (can_spawn==true)
		{
			if (enemiesRemainingToSpawn>0 && Time.time>nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

				StartCoroutine (SpawnEnemy());

			}
		}


	}

	IEnumerator SpawnEnemy()
	{
		float spawnDelay = 1;
		float tileFlahSpeed = 4;
		float spawnTimer=0;


		Transform randomTile = map.GetRandomOpenTile ();
		Material tileMat = randomTile.GetComponent<Renderer> ().material;
		Color initialColour = tileMat.color;
		Color flashColor = Color.red;

		while (spawnTimer< spawnDelay) {

			tileMat.color = Color.Lerp (initialColour, flashColor, Mathf.PingPong (spawnTimer * tileFlahSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
		}

		int random = Random.Range (0, enemies.Length);

		EnemyFM spawnnedEnemy = Instantiate (enemies [random], randomTile.position+Vector3.up, Quaternion.identity) as EnemyFM;
		//spawnnedEnemy.name = "enemy";
		spawnnedEnemy.transform.parent = this.transform;

		Sprite gesture;
		Color color;
		int randomGesture;
		for (int i = 0; i < random+1; i++) {

			randomGesture = Random.Range (0, gestures_index_used.Count);
			randomGesture = gestures_index_used [randomGesture];
			gesture = gesturesEnabled[ randomGesture ];
			color = gesturesColor[randomGesture];
			spawnnedEnemy.addGesture (gesture,color,randomGesture);

		}

		current_enemies_objects.Add (spawnnedEnemy.gameObject);

		spawnnedEnemy.OnDeath += onEnemyDeath;


	}


	public void reset()
	{

		foreach (var item in current_enemies_objects) {
			Destroy (item);
		}

		current_enemies_objects = new List<GameObject> ();
		currentWaveIndex = -1;
		can_spawn = false;
	}

	public void NextWave()
	{
		currentWaveIndex++;
		if (currentWaveIndex <waves.Length ) {
			
			currentWave = waves [currentWaveIndex];
			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;
		}

	}

	[System.Serializable]
	public class Wave{
		public int enemyCount;
		public float timeBetweenSpawn;

	}

	void onEnemyDeath()
	{
		enemiesRemainingAlive--;

		if (enemiesRemainingAlive==0) {

			NextWave ();
		}
	}
		
}
