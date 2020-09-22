using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftPlatformBehaviour : MonoBehaviour
{
    private Collider platformCollider;
    private bool sent = false;

    void Start()
    {
        platformCollider = gameObject.GetComponent<Collider>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AvatarPlatform")
        {
            if (!sent && platformCollider.bounds.Contains(other.bounds.min) && platformCollider.bounds.Contains(other.bounds.max))
            {
                GameManagerTiroLibre.gm.ShiftDone();
                sent = true;
            }
        }
    }

    private void OnEnable()
    {
        sent = false;
    }
}
