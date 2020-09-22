using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TooltipNivel : MonoBehaviour {
    public Slider rend;
    public Text tooltip;
    private Vector3 pos;


    // Use this for initialization
    void Start()
    {
        Debug.Log("koko");
        //rend = GetComponent<Slider>();
        Debug.Log(rend);
        pos = new Vector3(5, 5, 5);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnPointerEnter()
    {
            if(pos!= Input.mousePosition)
        {
            pos = Input.mousePosition;
            tooltip.enabled = true;
            tooltip.transform.position = Input.mousePosition;
            tooltip.text = "Nivel del ";
        }

    }

    public void OnPointerExit()
    {
        Debug.Log("mouse overe");
        tooltip.enabled = false;

        //rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }
}
