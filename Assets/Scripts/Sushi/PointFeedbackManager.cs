using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointFeedbackManager : MonoBehaviour {

    //Panel that will be modified
    public GameObject pointsPanel;
    //Text that will be modified
    public GameObject pointsText;

    //Points and miss image
    public GameObject pointsImg;
    public GameObject missImg;

    //Time that lasts the panel and text in turning green or red
    public float appearTime;
    //Time that lasts the panel and text in turning white again
    public float disappearTime;
    //Current time of the transition
    float animTime;
    //Current status of the animation
    //0 for idle, 1 for turning green, 2 for turning red
    int animStatus;


	// Use this for initialization
	void Start () {
        if (appearTime == 0.0f)
            appearTime = 0.3f;
        if (disappearTime == 0.0f)
            disappearTime = 1.0f;
        animTime = 0.0f;
        animStatus = 0;
	}
	
    public void GreenPoint()
    {
        animStatus = 1;
        animTime = 0.0f;
        ResetColor();
    }

    public void RedPoint()
    {
        animStatus = 2;
        animTime = 0.0f;
        ResetColor();
    }

    void ResetColor()
    {
        //Changes the panel color to white
        Color panelColor = pointsPanel.GetComponent<Image>().color;
        panelColor.g = 1.0f;
        panelColor.b = 1.0f;
        panelColor.r = 1.0f;

        //Applies the new panel color
        pointsPanel.GetComponent<Image>().color = panelColor;


        //Changes the alpha value of the images
        Color pointImgColor = pointsImg.GetComponent<Image>().color;
        pointImgColor.a = 0.0f;
        pointsImg.GetComponent<Image>().color = pointImgColor;

        Color missImgColor = missImg.GetComponent<Image>().color;
        missImgColor.a = 0.0f;
        missImg.GetComponent<Image>().color = missImgColor;
    }

	// Update is called once per frame
	void Update () {
        if (animStatus != 0)
        {
            if (animTime <= appearTime)
            {
                Color panelColor = pointsPanel.GetComponent<Image>().color;
                Color pointImgColor = pointsImg.GetComponent<Image>().color;
                Color missImgColor = missImg.GetComponent<Image>().color;

                switch (animStatus)
                {
                    case 1:
                        panelColor.b -= (1.0f * Time.deltaTime / appearTime);
                        panelColor.r -= (1.0f * Time.deltaTime / appearTime);
                        pointImgColor.a += (1.0f * Time.deltaTime / appearTime);
                        break;
                    case 2:
                        panelColor.g -= (1.0f * Time.deltaTime / appearTime);
                        panelColor.b -= (1.0f * Time.deltaTime / appearTime);
                        missImgColor.a += (1.0f * Time.deltaTime / appearTime);
                        break;
                    default:
                        break;
                }
                //Applies the new panel color
                pointsPanel.GetComponent<Image>().color = panelColor;
                pointsImg.GetComponent<Image>().color = pointImgColor;
                missImg.GetComponent<Image>().color = missImgColor;

            } else if (animTime <= appearTime + disappearTime) {
                Color panelColor = pointsPanel.GetComponent<Image>().color;
                Color pointImgColor = pointsImg.GetComponent<Image>().color;
                Color missImgColor = missImg.GetComponent<Image>().color;

                switch (animStatus)
                {
                    case 1:
                        panelColor.b += (1.0f * Time.deltaTime / disappearTime);
                        panelColor.r += (1.0f * Time.deltaTime / disappearTime);
                        pointImgColor.a -= (1.0f * Time.deltaTime / disappearTime);
                        break;
                    case 2:
                        panelColor.g += (1.0f * Time.deltaTime / disappearTime);
                        panelColor.b += (1.0f * Time.deltaTime / disappearTime);
                        missImgColor.a -= (1.0f * Time.deltaTime / disappearTime);
                        break;
                    default:
                        break;
                }
                //Applies the new panel color
                pointsPanel.GetComponent<Image>().color = panelColor;
                pointsImg.GetComponent<Image>().color = pointImgColor;
                missImg.GetComponent<Image>().color = missImgColor;
            } else
            {
                ResetColor();
                animStatus = 0;
            }
            animTime += Time.deltaTime;
        }    	
	}
}
