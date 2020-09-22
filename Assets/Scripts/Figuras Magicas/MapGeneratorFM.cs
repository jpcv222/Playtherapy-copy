using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorFM : MonoBehaviour {

	public int mapIndex;
	public Map[] maps;


	public Transform tilePrefab;
	public Transform[] obstaclePrefabs;

	public Transform navMeshFloor;
	public Transform navMeshPrefab;

	[Range (0,1)]
	public float outlinePercent;
	public Vector2 maxMapSize;


	List<Coord> allTileCoords;
	Queue<Coord> shufledTileCoords;
	Queue<Coord> shufledOpenTileCoords;
	public float tileSize;
	Map current_map;
	Transform[,] tileMap;

	void Start(){

		GenerateMap ();
	}



	public void GenerateMap()
	{


		current_map = maps [mapIndex];
		tileMap = new Transform[current_map.mapSize.x, current_map.mapSize.y];


		System.Random prng = new System.Random (current_map.seed);
		GetComponent<BoxCollider> ().size = new Vector3 (current_map.mapSize.x * tileSize, 0.05f, current_map.mapSize.y * tileSize);

		// Generating Coords
		allTileCoords = new List<Coord> ();
		for (int x = 0; x < current_map.mapSize.x; x++) {
			for (int y = 0; y < current_map.mapSize.y; y++) {

				allTileCoords.Add(new Coord(x,y));
			}
		}
		shufledTileCoords = new Queue<Coord>(UtilitiesFM.ShuffleArray(allTileCoords.ToArray(),current_map.seed));


		//creating map holder
		string holderName = "Generated Map";

		if (transform.FindChildByRecursive(holderName)) {
			DestroyImmediate (transform.FindChildByRecursive (holderName).gameObject);
		}


		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		//Spanning Tiles
		Vector3 tilePosition;
		for (int x = 0; x < current_map.mapSize.x; x++) {

			for (int y = 0; y < current_map.mapSize.y; y++) {
				tilePosition = CoordToPosition(x,y);
				Transform newtile = Instantiate (tilePrefab, tilePosition, Quaternion.Euler (Vector3.right * 90)) as Transform; 
				newtile.localScale = Vector3.one * (1 - outlinePercent)*tileSize;
				newtile.parent = mapHolder;
				tileMap [x,y] = newtile;
			}
		}

		//Spawning obstacles 
		bool[,] obstacleMap= new bool[(int)current_map.mapSize.x,(int)current_map.mapSize.y];
		int obstacle_count =(int) (current_map.mapSize.x * current_map.mapSize.y * current_map.obstaclePercent);
		int currentObstacleCount=0;

		List<Coord> allOpenCoords = new List<Coord>(allTileCoords);

		for (int i = 0; i < obstacle_count; i++) {



			Coord randomCoord = GetRandomCoord ();
			obstacleMap [randomCoord.x, randomCoord.y] = true;
			currentObstacleCount++;
			if (randomCoord != current_map.mapCenter && MapIsFullyAccesible (obstacleMap, currentObstacleCount)) {
				Vector3 obstaclePosition = CoordToPosition (randomCoord.x, randomCoord.y);


				//float obstacleHeight = Mathf.Lerp (current_map.minObstacleHeight, current_map.maxObstacleHeight,(float) prng.NextDouble ());
				Transform obstaclePrefab = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];
				Transform newObstacle = Instantiate (obstaclePrefab, obstaclePosition + Vector3.up*0.5f*tileSize , Quaternion.identity) as Transform;
				newObstacle.parent = mapHolder;
				newObstacle.localScale = new Vector3((1 - outlinePercent)*tileSize,tileSize,(1 - outlinePercent)*tileSize);
				 
				Renderer obstacleRenderer = newObstacle.GetComponent<Renderer> ();
				Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
				float colorPercent = randomCoord.y / (float)current_map.mapSize.y;
				obstacleMaterial.color = Color.Lerp (current_map.foregroundColour, current_map.backgroundColour, colorPercent);
				obstacleRenderer.sharedMaterial = obstacleMaterial;

				allOpenCoords.Remove (randomCoord);


			} else {
				obstacleMap [randomCoord.x, randomCoord.y] = false;
				currentObstacleCount--;
			}

		}
		shufledOpenTileCoords = new Queue<Coord>(UtilitiesFM.ShuffleArray(allOpenCoords.ToArray(),current_map.seed));


		//Adding nav mesk mask


		Transform maskLeft = Instantiate (navMeshPrefab, Vector3.left *(current_map.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform; 
		maskLeft.parent = mapHolder;
		maskLeft.localScale = new Vector3 ((maxMapSize.x-current_map.mapSize.x)/2f,1,current_map.mapSize.y)*tileSize;
		maskLeft.gameObject.AddComponent<BoxCollider> ();

		Transform maskRight = Instantiate (navMeshPrefab, Vector3.right *(current_map.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform; 
		maskRight.parent = mapHolder;
		maskRight.localScale = new Vector3 ((maxMapSize.x-current_map.mapSize.x)/2f,1,current_map.mapSize.y)*tileSize;
		maskRight.gameObject.AddComponent<BoxCollider> ();

		Transform maskTop = Instantiate (navMeshPrefab, Vector3.forward *(current_map.mapSize.y + maxMapSize.y) / 4f *tileSize, Quaternion.identity) as Transform; 
		maskTop.parent = mapHolder;
		maskTop.localScale = new Vector3 (maxMapSize.x,1,(maxMapSize.y-current_map.mapSize.y)/2f)*tileSize;
		maskTop.gameObject.AddComponent<BoxCollider> ();

		Transform maskBotton = Instantiate (navMeshPrefab, Vector3.back *(current_map.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity) as Transform; 
		maskBotton.parent = mapHolder;
		maskBotton.localScale = new Vector3 (maxMapSize.x,1,(maxMapSize.y-current_map.mapSize.y)/2f)*tileSize;
		maskBotton.gameObject.AddComponent<BoxCollider> ();

		navMeshFloor.localScale = new Vector3 (maxMapSize.x, maxMapSize.y) * tileSize;
	}

	bool MapIsFullyAccesible(bool[,] obstacleMap, int currentObstacleCount)
	{
		bool[,] mapFlags = new bool[obstacleMap.GetLength (0), obstacleMap.GetLength (1)];
		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (current_map.mapCenter);
		mapFlags [current_map.mapCenter.x, current_map.mapCenter.y] = true;

		int accessibleTileCount = 1;

		while(queue.Count>0)
		{
			Coord tile = queue.Dequeue ();

			for (int x = -1; x <=1; x++) {
				for (int y = -1; y <=1; y++) {
					int neightbourX = tile.x + x;
					int neightbourY = tile.y + y;
					if (x==0 || y ==0) {
						if (neightbourX>=0 && neightbourX<obstacleMap.GetLength(0) && neightbourY>=0 && neightbourY<obstacleMap.GetLength(1))
						{
							if (!mapFlags[neightbourX,neightbourY] && !obstacleMap[neightbourX,neightbourY]) {
								mapFlags [neightbourX, neightbourY] = true;
								queue.Enqueue (new Coord (neightbourX, neightbourY));
								accessibleTileCount++;
							}
						}
					}

				}
			}

		}

		int targetAccesibleTileCount = (int)(current_map.mapSize.x * current_map.mapSize.y - currentObstacleCount);

		return targetAccesibleTileCount== accessibleTileCount;
	}

	Vector3 CoordToPosition(int x, int y)
	{
		return new Vector3 (-current_map.mapSize.x * 0.5f + x+ 0.5f, 0, -current_map.mapSize.y * 0.5f + y+ 0.5f)*tileSize;

	}

	public Coord GetRandomCoord()
	{
		Coord randomCoord = shufledTileCoords.Dequeue();
		shufledTileCoords.Enqueue (randomCoord);
		return randomCoord;
	}
	public Transform GetRandomOpenTile()
	{
		Coord randomCoord = shufledOpenTileCoords.Dequeue();
		shufledOpenTileCoords.Enqueue (randomCoord);
		return tileMap[randomCoord.x,randomCoord.y];
	}
	[System.Serializable]
	public struct Coord
	{
		public int x;
		public int y;

		public Coord(int _x,int _y)
		{

			x= _x;
			y = _y;
		}

		public static bool operator == (Coord c1,Coord c2)
		{
			return c1.x == c2.x && c1.y == c2.y;
		}
		public static bool operator != (Coord c1,Coord c2)
		{
			return !(c1==c2);
		}
	}


	[System.Serializable]
	public class Map{

		public Coord mapSize;
		[Range (0,1)]
		public float obstaclePercent;
		public int seed;
		
		public float minObstacleHeight;
		public float maxObstacleHeight;
		public Color foregroundColour;
		public Color backgroundColour;

		public Coord mapCenter{
			get {
				return new Coord (mapSize.x / 2, mapSize.y / 2);
			}
		}

	}


}
