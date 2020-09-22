using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text text;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>(); // Acessing the RectTransform
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, text.preferredHeight); // Setting the height to equal the height of text
    }
}
