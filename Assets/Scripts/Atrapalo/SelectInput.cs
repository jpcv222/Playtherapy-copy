using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SelectInput : MonoBehaviour {

    public Toggle toggleRep;
    public Toggle toggleTime;


    public InputField inputRep;
    public InputField inputTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onRepValueChanged()
    {
        if (toggleRep.isOn)
        {
            toggleTime.isOn = false;
            inputTime.interactable = false;
            Debug.Log("Se activo ToggleRep");
        }
        else
        {
            toggleTime.isOn = true;
            inputTime.interactable = true;
            Debug.Log("Se activo ToggleRep");
        }
    }
    public void onTimeValueChanged()
    {
        if (toggleTime.isOn)
        {
            toggleRep.isOn = false;
            inputRep.interactable = false;
            Debug.Log("Se activo ToggleTime");
        }
        else
        {
            toggleRep.isOn = true;
            inputRep.interactable = true;
            Debug.Log("Se activo ToggleTime");
        }
    }
}
