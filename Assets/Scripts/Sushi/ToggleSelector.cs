using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSelector : MonoBehaviour
{
    public Toggle toggleRep;
    public Toggle toggleTime;

    public Slider sliderRep;
    public Slider sliderTime;


    public void OnRepValueChanged()
    {
        if (toggleRep.isOn)
        {
            toggleTime.isOn = false;
            sliderTime.interactable = false;
            //Debug.Log("Se activo ToggleRep");
        }
        else
        {
            toggleTime.isOn = true;
            sliderTime.interactable = true;
            //Debug.Log("Se activo ToggleRep");
        }
    }

    public void OnTimeValueChanged()
    {
        if (toggleTime.isOn)
        {
            toggleRep.isOn = false;
            sliderRep.interactable = false;
            //Debug.Log("Se activo ToggleTime");
        }
        else
        {
            toggleRep.isOn = true;
            sliderRep.interactable = true;
            //Debug.Log("Se activo ToggleTime");
        }
    }
}
