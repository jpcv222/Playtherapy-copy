using UnityEngine;
using System.Collections;

public class LightEnabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Goes in");
        if (gameObject.transform.IsChildOf(GameObject.Find("FGC_Male_Char_Brad_Lite").transform))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
