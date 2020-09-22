using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVariantsTiroLibre : MonoBehaviour
{
    public Toggle[] toggles;

    public void EnsureAtLeastOneSelected()
    {
        int counter = 0;
        int pos = 0;

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                counter += 1;
                pos = i;
            }
        }

        if (counter == 1)
        {
            toggles[pos].interactable = false;
        }
        else
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = true;
            }
        }
    }
}
