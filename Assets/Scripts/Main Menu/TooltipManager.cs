using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Vector3 offset;
    public GameObject label;
    public Text labelText;
    public string tooltip;


	public void Show()
    {
        StartCoroutine(DelayedShow());
    }

    private IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(0.5f);
        labelText.text = tooltip;
        label.transform.position = Input.mousePosition + new Vector3(0.5f, 0.5f, 0);
        label.SetActive(true);
    }

    public void Hide()
    {
        StartCoroutine(DelayedHide());
    }

    private IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(0.5f);
        label.SetActive(false);
    }
}
