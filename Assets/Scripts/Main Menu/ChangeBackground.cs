using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public GameObject canvasOld;
    public GameObject canvasNew;

    public void ChangeSceneBackground()
    {
        string name = GetComponentInChildren<Text>().text;
        Debug.Log(name);

        if (canvasOld != null)
        {
            canvasOld.SetActive(false);
        }

        if (canvasNew != null)
        {
            canvasNew.SetActive(true);
        }

        //GameManagerMainMenu.gm.currentBackground.SetActive(false);

        //GameObject go = GameManagerMainMenu.gm.findBackground(name);
        //GameManagerMainMenu.gm.currentBackground = go;
        //GameManagerMainMenu.gm.currentBackground.SetActive(true);
    }
}
