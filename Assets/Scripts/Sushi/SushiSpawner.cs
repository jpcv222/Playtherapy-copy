using UnityEngine;
using System.Collections;

public class SushiSpawner : MonoBehaviour {

    int sushiCount;
	int remainingSushiCount;
	float spawnTime;
	bool spawnFlag;
    public GameObject roll;

	// particle when spawns
	public GameObject particlePrefab;

	// Use this for initialization
	void Start () {
        sushiCount = 0;
		spawnTime = 0.0f;
		spawnFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnFlag) {
			if (spawnTime < 0.0f) {
				spawnFlag = false;
				SpawnRoll ();
			}
			spawnTime -= Time.deltaTime;
		} else if (remainingSushiCount > 0) {
			StartSpawn ();
		}
			
	}

	public void MakeSpawn() {
		remainingSushiCount++;
	}

	void StartSpawn() {
		remainingSushiCount--;
		spawnFlag = true;
		spawnTime = 0.25f;
		Vector3 posParent = GameObject.Find("SushiContainer").transform.position;
		int multZ = 0;
		if (sushiCount % 8 < 4)
		{
			multZ = 0;
		} else
		{
			multZ = 1;
		}
		if (particlePrefab) {
			// Instantiate an explosion effect at the gameObjects position and rotation
			Instantiate (particlePrefab, new Vector3(-0.50f + (0.40f * (sushiCount % 4)) + posParent.x, 0.15f + (((sushiCount) / 8) * 0.29f) + posParent.y + 1.0f, 0.14f - (0.40f * multZ) + posParent.z), transform.rotation);
		}

	}

    void SpawnRoll()
    {
        Debug.Log("Instancia roll " + (sushiCount + 1));
        if (!roll)
            return;
        Vector3 posParent = GameObject.Find("SushiContainer").transform.position;
        int multZ = 0;
        if (sushiCount % 8 < 4)
        {
            multZ = 0;
        } else
        {
            multZ = 1;
        }
        Instantiate(roll, new Vector3(-0.50f + (0.40f * (sushiCount % 4)) + posParent.x, 0.15f + (((sushiCount) / 8) * 0.29f) + posParent.y + 1.0f, 0.14f - (0.40f * multZ) + posParent.z), roll.transform.rotation, GameObject.Find("SushiContainer").transform);
        sushiCount++;
    }
}
