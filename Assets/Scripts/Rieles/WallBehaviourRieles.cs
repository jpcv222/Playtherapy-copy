using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviourRieles : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(parent);
        }
    }
}
