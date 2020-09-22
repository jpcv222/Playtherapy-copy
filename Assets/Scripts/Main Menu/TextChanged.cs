using UnityEngine;
using System.Collections;

public class TextChanged : MonoBehaviour
{
    public GameObject inputField;

	public void print()
    {
        Debug.Log(inputField.GetComponent<RectTransform>().rect.height);
    }
}
