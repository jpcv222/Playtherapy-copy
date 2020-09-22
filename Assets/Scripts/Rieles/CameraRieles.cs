using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRieles : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.

    [SerializeField]
    float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.
    Vector3 newCamPos;


    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go)
            target = go.transform;
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        newCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        newCamPos = Vector3.Lerp(transform.position, newCamPos, smoothing * Time.deltaTime);
        transform.position = newCamPos;
    }
}
