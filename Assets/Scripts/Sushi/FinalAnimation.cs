using UnityEngine;
using System.Collections;

public class FinalAnimation : MonoBehaviour {

    GameObject cam;
    GameObject table;

    Vector3 deltaRotation;
    Vector3 deltaPosition;

    public int status;
    public float timer;

    public float translateTime;
    public float timePerFloor;
    public int floorsNumber;

	// Use this for initialization
	void Start () {
        Debug.Log("Start");
        status = 0;
        timer = 0.0f;
        translateTime = 0.0f;
        timePerFloor = 0.0f;
        cam = GameObject.Find("Main Camera");
        table = GameObject.Find("SushiContainer");
        deltaPosition = table.transform.position - cam.transform.position - new Vector3(0.0f, -0.5f, 3.0f);
        deltaRotation = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
    }
	
    public void startAnimation(float transTime, float timeFloor, int flNumber)
    {
        timer = 0.0f;
        status = 1;
        translateTime = transTime;
        if (translateTime == 0.0f)
            translateTime = 5.0f;
        timePerFloor = timeFloor;
        if (timePerFloor == 0.0f)
            timePerFloor = 4.0f;
        floorsNumber = flNumber;
        if (floorsNumber == 0)
            floorsNumber = 1;

        Debug.Log(transTime);
        Debug.Log(translateTime);
        Debug.Log(timeFloor);
        Debug.Log(flNumber);
    }

	// Update is called once per frame
	void Update () {
        if (status == 1)
        {
            cam.transform.Rotate(-deltaRotation * (Time.deltaTime / translateTime));
            cam.transform.Translate(deltaPosition * (Time.deltaTime / translateTime));
            if (timer > translateTime)
            {
                Debug.Log("Cambia de estado con " + timer);
                status = 2;
            }
                
            timer += Time.deltaTime;
        } else if (status == 2)
        {
            cam.transform.RotateAround(table.transform.position, Vector3.up, 360.0f * Time.deltaTime / timePerFloor);
            cam.transform.Translate(Vector3.up * 0.3f * Time.deltaTime / timePerFloor);
            if (timer > (translateTime + (timePerFloor * floorsNumber)))
                status = 0;
            timer += Time.deltaTime;
        }

        

        //cam.transform.RotateAround(table.transform.position, Vector3.up, 20 * Time.deltaTime);

    }
}
