using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectFirstObject : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject objectToSelect;

    // Use this for initialization
    void Start()
    {
        if (objectToSelect != null)
            eventSystem.SetSelectedGameObject(objectToSelect);            
    }
}
