using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    private Minigame minigame;

    public void Load()
    {
        string sceneName = gameObject.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log("Scene loaded");
		createGameSession ();
    }

    public void createGameSession()
    {
		Debug.Log ("Entra a createGameSession");
        if (minigame != null)
        {
            GameObject tso = GameObject.Find("TherapySession");
			Debug.Log ("Busca objeto TherapySession");

            if (tso != null)
            {
                GameSession gs = new GameSession(minigame.Id);
                tso.GetComponent<TherapySessionObject>().addGameSession(gs);
				Debug.Log ("Crea nuevo GameSession");
            }
            else
            {
                Debug.Log("TherapySession not found");
            }
        }
    }

    public Minigame Minigame
    {
        get
        {
            return minigame;
        }

        set
        {
            minigame = value;
        }
    }
}
