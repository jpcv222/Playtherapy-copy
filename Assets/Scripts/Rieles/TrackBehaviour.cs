using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBehaviour : MonoBehaviour
{
    public GameObject[] lane1;
    public GameObject[] lane2;
    public GameObject[] lane3;

    private ArrayList validIndexes;
    private int trainCounter = 0;

    // Use this for initialization
    void Start ()
    {

    }

    public int SpawnWithObstacles(bool useTrains)
    {
        int i, choice;
        validIndexes = new ArrayList(GameManagerRieles.gm.GetValidIndexes());

        if (!useTrains)
            validIndexes.Remove(0);

        switch (TrackSpawner.ts.trackType)
        {
            case TrackSpawner.TrackType.ThreeLane:
                {
                    ObstacleBehaviourRieles obr;
                    i = Random.Range(0, validIndexes.Count);
                    choice = (int)validIndexes[i];

                    lane1[choice].SetActive(true);

                    obr = lane1[choice].GetComponentInChildren<ObstacleBehaviourRieles>();
                    if (obr != null && obr.type == ObstacleBehaviourRieles.ObstacleType.Train)
                        trainCounter++;

                    i = Random.Range(0, validIndexes.Count);
                    choice = (int)validIndexes[i];

                    lane2[choice].SetActive(true);

                    obr = lane2[choice].GetComponentInChildren<ObstacleBehaviourRieles>();
                    if (obr != null && obr.type == ObstacleBehaviourRieles.ObstacleType.Train)
                        trainCounter++;

                    if (trainCounter == 2)
                        validIndexes.Remove(0);

                    i = Random.Range(0, validIndexes.Count);
                    choice = (int)validIndexes[i];

                    lane3[choice].SetActive(true);

                    obr = lane2[choice].GetComponentInChildren<ObstacleBehaviourRieles>();
                    if (obr != null && obr.type == ObstacleBehaviourRieles.ObstacleType.Train)
                        trainCounter++;

                    break;
                }
            case TrackSpawner.TrackType.SingleLane:
                {
                    i = Random.Range(0, validIndexes.Count);
                    choice = (int)validIndexes[i];

                    lane1[choice].SetActive(true);

                    break;
                }
            default:
                break;
        }

        return trainCounter;        
    }

    public void SpawnWithoutObstacles()
    {
        int choice = 3; // coins index

        switch (TrackSpawner.ts.trackType)
        {
            case TrackSpawner.TrackType.ThreeLane:
                {
                    lane1[choice].SetActive(true);
                    lane2[choice].SetActive(true);
                    lane3[choice].SetActive(true);

                    break;
                }
            case TrackSpawner.TrackType.SingleLane:
                {
                    lane1[choice].SetActive(true);

                    break;
                }
            default:
                break;
        }
    }
}
