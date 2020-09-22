﻿using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
public class MapGenerator : MonoBehaviour {


	public enum DrawMode
	{
		NoiseMap,
		ColourMap,
		Mesh,
		FalloffMap
	}

	public DrawMode drawMode;
    public Noise.NormalizeMode normalizeMode;
	public bool useFlatShading;
	[Range(0,6)] public int editorLOD;
	public float noiseScale;
	public int octaves;

	[Range(0,1)] public float persistance;
	public float lacunarity;
	public bool autoUpdate;
	public Vector2 offset;
	public float meshHeightMultiplier;
	public int seed;
	public AnimationCurve meshHeightCurve;

	public bool useFalloff;
	public TerrainType[] regions;
	static MapGenerator instance;
	Queue<MapthreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapthreadInfo<MapData>>();
	Queue<MapthreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapthreadInfo<MeshData>>();

	float[,] fallofMap;

	void Awake()
	{
		fallofMap = FalloffGenerator.GenerateFalloffMap (mapChunkSize);
	}
	public static int mapChunkSize {
		get {

			if (instance == null) {
				instance = FindObjectOfType<MapGenerator> ();
			}
			if (instance.useFlatShading) {

				//return 95; 
				return 95;
			} else {
				//return 239; 
				return 239;
			}


		}
	}
	public void DrawMapInEditor()
	{
		MapData mapData = GenerateMapData(Vector2.zero);
		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (mapData.heightMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap (mapData.colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == DrawMode.Mesh) {
			display.DrawMesh (MeshGenerator.GeneratTerrainMesh (mapData.heightMap,meshHeightMultiplier,meshHeightCurve,editorLOD,useFlatShading), TextureGenerator.TextureFromColourMap (mapData.colourMap, mapChunkSize, mapChunkSize));  
		}else if (drawMode == DrawMode.FalloffMap) {
			display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap (mapChunkSize)));
		}
		 
	}

public void RequestMapData(Vector2 centre,Action<MapData> callback)
{
	ThreadStart threadStart= delegate{
		MapDataThread (centre, callback); 

	};
	new Thread(threadStart).Start();



}
void MapDataThread(Vector2 centre,Action<MapData> callback)
{
	MapData mapData = GenerateMapData(centre);
	lock (mapDataThreadInfoQueue){
		mapDataThreadInfoQueue.Enqueue(new MapthreadInfo<MapData>(callback,mapData));

	}
	
}
public void RequestMeshData(MapData mapData, int lod ,Action<MeshData> callback)
{
	ThreadStart threadStart= delegate{
			MeshDataThread (mapData,lod,callback); 

	};
	new Thread(threadStart).Start();



}
void MeshDataThread(MapData mapData ,int lod ,Action<MeshData> callback)
{
		MeshData meshData = MeshGenerator.GeneratTerrainMesh(mapData.heightMap,meshHeightMultiplier,meshHeightCurve,lod,useFlatShading);
	lock(meshDataThreadInfoQueue)
	{

		meshDataThreadInfoQueue.Enqueue(new MapthreadInfo<MeshData>(callback,meshData));
	}
}
void Update(){

	if(mapDataThreadInfoQueue.Count>0){
		for(int i =0 ; i<mapDataThreadInfoQueue.Count;i++)
		{
			MapthreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
			threadInfo.callback(threadInfo.parameter);
		}



	}
	if(meshDataThreadInfoQueue.Count>0){
		for(int i =0 ; i<meshDataThreadInfoQueue.Count;i++)
		{
			MapthreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
		}



	}



}
	public MapData GenerateMapData(Vector2 centre){

		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize+2, mapChunkSize+2,seed, noiseScale,octaves,persistance,lacunarity,centre +offset, normalizeMode);

		Color[] colourMap = new Color[mapChunkSize*mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				if (useFalloff) 
				{
					noiseMap [x, y] = Mathf.Clamp01(noiseMap [x, y] - fallofMap [x, y]);

				}


				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight>=regions[i].height) {
						colourMap [y * mapChunkSize + x] = regions [i].colour;
						
					}
                    else
                    {
                        break;
                    }
				}
			}
		}

		MapData mapData = new MapData (noiseMap, colourMap);
		return mapData;
		


	}
	void OnValidate()
	{
		
		if (lacunarity<1) {
			lacunarity = 1;
		}
		if (octaves<0) {
			octaves = 0;
		}
		fallofMap = FalloffGenerator.GenerateFalloffMap (mapChunkSize);
	}

	struct MapthreadInfo<T>{
		public readonly Action<T> callback;
		public readonly T parameter;

		public MapthreadInfo (Action<T> callback, T parameter)
	{
		this.callback = callback;
		this.parameter = parameter;
	}

	}
}
[System.Serializable]
public struct TerrainType{

	public string name;
	public float height;
	public Color colour;
}

public struct MapData{

	public float[,] heightMap;
	public Color[] colourMap;
	public MapData (float[,] heightMap, Color[] colourMap)
	{
		this.heightMap = heightMap;
		this.colourMap = colourMap;
	}
	
}