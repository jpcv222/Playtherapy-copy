using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeedbackPiano : MonoBehaviour
{
    [SerializeField] MeshRenderer greenNote;
    [SerializeField] MeshRenderer redNote;
    [SerializeField] float travelTime;
    [SerializeField] float speed;

    private bool isGreenMoving;
    private bool isRedMoving;
    private float elapsedTime;
    private Vector3 greenStartPosition;
    private Vector3 redStartPosition;
    private Vector3 currentPosition;

    // Use this for initialization
    void Start()
    {
        isGreenMoving = false;
        isRedMoving = false;
        elapsedTime = 0f;
        greenStartPosition = greenNote.transform.position;
        redStartPosition = redNote.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGreenMoving)
        {
            if (elapsedTime < travelTime)
            {
                elapsedTime += Time.deltaTime;
                currentPosition = greenNote.gameObject.transform.position;
                currentPosition.y += speed * Time.deltaTime;
                greenNote.gameObject.transform.position = currentPosition;
            }
            else
            {
                HideGreen();
            }
        }
        else if (isRedMoving)
        {
            if (elapsedTime < travelTime)
            {
                elapsedTime += Time.deltaTime;
                currentPosition = redNote.transform.position;
                currentPosition.y -= speed * Time.deltaTime;
                redNote.transform.position = currentPosition;
            }
            else
            {
                HideRed();
            }
        }
    }

    public void ShowGreen()
    {
        HideRed();
        greenNote.transform.position = greenStartPosition;
        greenNote.enabled = true;
        elapsedTime = 0f;
        isGreenMoving = true;
        isRedMoving = false;
    }

    public void ShowRed()
    {
        HideGreen();
        redNote.transform.position = redStartPosition;
        redNote.enabled = true;
        elapsedTime = 0f;
        isRedMoving = true;
    }

    public void HideGreen()
    {
        isGreenMoving = false;
        greenNote.enabled = false;
    }

    public void HideRed()
    {
        isRedMoving = false;
        redNote.enabled = false;
    }
}
