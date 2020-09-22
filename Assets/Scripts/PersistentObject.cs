using UnityEngine;
using System.Collections;

public class PersistentObject : MonoBehaviour
{
    // this is always called before Start
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
