using UnityEngine;
using System.Collections;

public class TargetMover : MonoBehaviour
{

    // define the possible states through an enumeration
    public enum status { Up, UpII, Static, Down };

    // store the state
    private status motionState = status.Up;

    // motion parameters
    public float floatingTime = 5.0f;
    public float upTime = 0.5f;
    public float spinSpeed = 180.0f;
    private float initialHeight = 0.0f;
    public float objectiveHeight = 1.5f;
    private float aliveTime = 0.0f;

    private MovementDetectionLibrary.SpawnGameObjects spawner;

    private GameManagerSushi gameM;

    public bool moving = false;

    void Start()
    {
        initialHeight = transform.position.y;
        spawner = GameObject.Find("Spawner").GetComponent<MovementDetectionLibrary.SpawnGameObjects>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManagerSushi>();
    }

    public void StartMoving(Vector3 objective, float fTime, float uTime)
    {
        objectiveHeight = objective.y;
        gameObject.transform.position = new Vector3(objective.x, 0.0f, objective.z + 2.0f);
        floatingTime = fTime;
        upTime = uTime;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
            return;
        // do the appropriate motion based on the motionState
        switch (motionState)
        {
            case status.Up:
                // rotate around the up axix of the gameObject
                gameObject.transform.Translate(Vector3.up * 2.0f * ((objectiveHeight - initialHeight) / upTime) * Time.deltaTime, Space.World);
                break;
            case status.UpII:
                // rotate around the up axix of the gameObject
                gameObject.transform.Translate(Vector3.back * (4.0f / upTime) * Time.deltaTime, Space.World);
                break;
            case status.Static:
                // move up and down over time
                gameObject.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
                gameObject.transform.Rotate(Vector3.right * (180.0f / floatingTime) * Time.deltaTime);
                break;
            case status.Down:
                // move up and down over time
                gameObject.transform.Translate(Vector3.down * 2.0f * ((objectiveHeight - initialHeight) / upTime) * Time.deltaTime, Space.World);
                gameObject.transform.Translate(Vector3.back * (8.0f / upTime) * Time.deltaTime, Space.World);
                break;
        }
        aliveTime += Time.deltaTime;
        if (aliveTime < upTime / 2.0f)
        {
            motionState = status.Up;
        }
        else if (aliveTime < upTime)
        {
            motionState = status.UpII;
        }
        else if (aliveTime < upTime + floatingTime)
        {
            motionState = status.Static;
        }
        else if (aliveTime < upTime + floatingTime + 1.0f)
        {
            motionState = status.Down;
        }
        else
        {
            if (gameM.withTime)
            {
				gameM.NewRepetition();
                if (gameM.currentTime > 0.0f)
                {
                    Debug.Log("Makethingtospawn");
                    spawner.MakeThingToSpawn();
                }
            }
            else
            {
                gameM.NewRepetition();
                if (gameM.GetRepetitions() >= 0)
                {
                    Debug.Log("Makethingtospawn2");
                    spawner.MakeThingToSpawn();
                }
            }

            GameObject.Find("GameManager").GetComponent<PointFeedbackManager>().RedPoint();
            Destroy(gameObject);
        }

    }

    void setObjHeight(float objHeight)
    {
        objectiveHeight = objHeight;
    }
}