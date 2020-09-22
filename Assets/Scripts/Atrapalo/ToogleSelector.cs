using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ToogleSelector : MonoBehaviour {

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
            inputTime.readOnly = true;
            inputTime.text = "";
            Debug.Log("Se activo ToggleRep");
        }
        else
        {
            toggleTime.isOn = true;
            inputTime.readOnly = false;
            Debug.Log("Se activo ToggleRep");
        }
    }
    public void onTimeValueChanged()
    {
        if (toggleTime.isOn)
        {
            toggleRep.isOn = false;
            inputRep.readOnly = true;
            inputRep.text = "";
            Debug.Log("Se activo ToggleTime");
        }
        else
        {
            toggleRep.isOn = true;
            inputRep.readOnly = false;
            Debug.Log("Se activo ToggleTime");
        }
    }
}
