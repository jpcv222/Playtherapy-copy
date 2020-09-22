using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public static TrackSpawner ts;

    public enum TrackType { SingleLane, ThreeLane };
    public TrackType trackType;

    public GameObject parent;
    public GameObject emptyTrack;
    public GameObject gameTrack;
    public GameObject emptyTrack2;
    public GameObject gameTrack2;
    public int initialTracks;
    public Vector3 firstTrackPosition;

    private GameObject trackToSpawn;
    private Vector3 lastTrackPosition;
    private float trackLength;
    private int trainCounter;
    private bool useTrains;
    private bool nextEmptyTrack;

    // Use this for initialization
    void Start ()
    {
        if (ts == null)
            ts = gameObject.GetComponent<TrackSpawner>();

        useTrains = true;
        nextEmptyTrack = false;
	}

    public void SpawnInitialTracks()
    {
        if (trackType == TrackType.SingleLane)
            trackToSpawn = emptyTrack2;
        else
            trackToSpawn = emptyTrack;

        GameObject obj = Instantiate(trackToSpawn, parent.transform) as GameObject;
        obj.transform.position = firstTrackPosition;

        lastTrackPosition = firstTrackPosition;
        trackLength = obj.GetComponentInChildren<Collider>().bounds.size.z;

        for (int i = 1; i < initialTracks; i++)
        {
            lastTrackPosition.z += trackLength;
            Instantiate(trackToSpawn, parent.transform).transform.position = lastTrackPosition;
        }
    }

    public void SpawnNextTrack()
    {
        if (trackType == TrackType.SingleLane)
            trackToSpawn = gameTrack2;
        else
            trackToSpawn = gameTrack;

        lastTrackPosition.z += trackLength;
        GameObject obj = Instantiate(trackToSpawn, parent.transform) as GameObject;
        obj.transform.position = lastTrackPosition;

        if (!nextEmptyTrack)
        {
            useTrains = obj.GetComponent<TrackBehaviour>().SpawnWithObstacles(useTrains) != 2;
            nextEmptyTrack = GameManagerRieles.gm.difficulty == 1;
        }
        else
        {
            obj.GetComponent<TrackBehaviour>().SpawnWithoutObstacles();
            nextEmptyTrack = false;
        }

    }

    public float TrackLength
    {
        get
        {
            return trackLength;
        }

        set
        {
            trackLength = value;
        }
    }
}
