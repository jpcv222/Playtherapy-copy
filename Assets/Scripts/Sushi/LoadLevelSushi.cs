using UnityEngine;
using System.Collections;

public class LoadLevelSushi : MonoBehaviour
{
	public string levelToLoad;

	public void LoadLevelSelected()
	{
		if (TherapySessionObject.tso != null)
		    TherapySessionObject.tso.restartLastSession ();

		UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
	}
}
