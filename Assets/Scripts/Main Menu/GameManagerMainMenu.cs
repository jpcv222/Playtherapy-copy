using UnityEngine;
using System.Collections;

public class GameManagerMainMenu : MonoBehaviour
{
    public static GameManagerMainMenu gm;

    public GameObject[] minigameBackgrounds;
    public GameObject currentBackground;

    // Use this for initialization
    void Start()
    {
        if (gm == null)
        {
            gm = this.gameObject.GetComponent<GameManagerMainMenu>();
        }
    }

    public GameObject findBackground(string name)
    {
        GameObject r = null;

        foreach (GameObject go in minigameBackgrounds)
        {
            if (go.name == name)
            {
                r = go;
                break;
            }
        }

        return r;
    }
}
