using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CancelObservations : MonoBehaviour
{
    public GameObject observationsPanel;
    public GameObject minigamesMenuPanel;
    public InputField input;

    public void Cancel()
    {
        observationsPanel.SetActive(false);
        input.text = "";
        minigamesMenuPanel.SetActive(true);
    }
}
