using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndTherapySession : MonoBehaviour {

    // used for transition between menus
    public TherapySessionObject tso;
	public GameObject minigamesMenuPanel;
	public GameObject observationsPanel;
    public InputField observationsField;
    public string sceneToLoad;

	// Use this for initialization
	void Start () {
        sceneToLoad = "Main Menu";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndTherapy()
    {
		minigamesMenuPanel.SetActive(false);
		observationsPanel.SetActive(true);  
	}

    public void SaveObservations()
    {
        string observations = observationsField.text;

        if (tso)
        {
            tso.SaveObservations(observations);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
