using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    public Text text;

    private float defaultHeight;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>(); // Acessing the RectTransform
        defaultHeight = rectTransform.rect.height;
    }

    void Update()
    {
        if (text.preferredHeight > defaultHeight)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width*0, text.preferredHeight + 10); // Setting the height to equal the height of text
        }
    }
}
