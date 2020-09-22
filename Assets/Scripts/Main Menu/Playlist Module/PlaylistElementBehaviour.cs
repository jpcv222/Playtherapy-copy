using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaylistElementBehaviour : MonoBehaviour
{
    public Text indexLabel;
    public Text label;
    public Button upButton;
    public Button downButton;
    public Button removeButton;
    public int index;
    private PlaylistScreenManager psm;

    public void Initialize(string name, PlaylistScreenManager psm, int index)
    {
        indexLabel.text = (index + 1) + ".";
        label.text = name;
        this.psm = psm;
        this.index = index;
        UpdateElement();
    }

    public void OnSelected()
    {
        psm.Select(index);
        psm.nameText.text = "Configurando: " + label.text;

    }

    public void OnUpButtonClicked()
    {
        psm.MoveUp(index);
        UpdateElement();
    }

    public void OnDownButtonClicked()
    {
        psm.MoveDown(index);
        UpdateElement();
    }

    public void OnRemoveButtonClicked()
    {
        psm.Remove(index);
        UpdateElement();
    }

    public void UpdateElement()
    {
        indexLabel.text = (index + 1) + ".";


        if (index == 0)
            upButton.interactable = false;
        else
            upButton.interactable = true;

        if (psm.IsLastIndex(index))
            downButton.interactable = false;
        else
            downButton.interactable = true;
    }
}
