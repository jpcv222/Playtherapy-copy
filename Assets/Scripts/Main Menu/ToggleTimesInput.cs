using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTimesInput : MonoBehaviour
{
    public Toggle toggle;
    public GameObject label;
    public GameObject input;

    public void Start()
    {
        toggle.isOn = false;
        Toggle();
    }

	public void Toggle()
    {
        label.SetActive(toggle.isOn);
        input.SetActive(toggle.isOn);
    }
}
