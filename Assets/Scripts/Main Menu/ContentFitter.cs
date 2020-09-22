using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContentFitter : MonoBehaviour
{
    private RectTransform contentRectTransform;
    public RectTransform inputBoxRectTransform;    
    public RectTransform inputTextRectTransform;
    public Text inputText;

    private float defaultHeight;
    private float lastHeight;

    public Scrollbar scrollbar;

    private float lastScrollbarSize;
    private float epsilon;

    void Start()
    {
        contentRectTransform = gameObject.GetComponent<RectTransform>(); // Acessing the RectTransform
        defaultHeight = contentRectTransform.rect.height;
        lastHeight = defaultHeight;
        lastScrollbarSize = scrollbar.size;
        epsilon = 0.0001f;
    }

    void Update()
    {
        //inputTextRectTransform.sizeDelta = new Vector2(inputTextRectTransform.rect.width, inputText.preferredHeight);

        if ((inputText.preferredHeight > lastHeight) || (inputText.preferredHeight <= lastHeight && inputText.preferredHeight > defaultHeight))
        {
            inputBoxRectTransform.sizeDelta = new Vector2(inputBoxRectTransform.rect.width, inputText.preferredHeight + 10);
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, inputBoxRectTransform.rect.height);
            lastHeight = inputText.preferredHeight + 10;
        }
        else
        {
            inputBoxRectTransform.sizeDelta = new Vector2(inputBoxRectTransform.rect.width, defaultHeight);
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, inputBoxRectTransform.rect.height);
            lastHeight = defaultHeight;
            Vector2 tempVector = contentRectTransform.anchoredPosition;
            tempVector.y = 0f;
            contentRectTransform.anchoredPosition = tempVector;
            //Debug.Log(contentRectTransform.anchoredPosition);
        }
        /*
        if (Mathf.Abs(lastScrollbarSize - scrollbar.size) > epsilon)
        {
            Debug.Log("Entra a scrollbar");
            Debug.Log(scrollbar.size + "  ---  " + lastScrollbarSize);
            scrollbar.value = 0;
        }

        lastScrollbarSize = scrollbar.size;
        */
    }
}
