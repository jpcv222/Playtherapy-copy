using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class InputLineDetails
{
    public bool enable;
    public string inputLabelText;
    public string inputText;

    public InputLineDetails(bool enable, string inputLabelText, string inputText)
    {
        this.enable = enable;
        this.inputLabelText = inputLabelText;
        this.inputText = inputText;
    }
}

public class EventButtonDetails
{
    public string buttonText;
    public UnityAction action;

    public EventButtonDetails(string buttonText, UnityAction action)
    {
        this.buttonText = buttonText;
        this.action = action;
    }
}

public class ModalPanelDetails
{
    public string displayText;
    public Sprite iconImage;
    public EventButtonDetails button1Details;
    public EventButtonDetails button2Details;
    public EventButtonDetails button3Details;
    public bool inputPanel;
    public InputLineDetails input1Details;
    public InputLineDetails input2Details;
    public InputLineDetails input3Details;
    public InputLineDetails input4Details;
}

public class ModalPanel : MonoBehaviour
{
    public Text displayText;
    public Image iconImage;
    public Button button1;
    public Button button2;
    public Button button3;

    public Text button1Text;
    public Text button2Text;
    public Text button3Text;

    public GameObject inputPanel;

    public GameObject inputLabel1;
    public GameObject inputLabel2;
    public GameObject inputLabel3;
    public GameObject inputLabel4;    

    public GameObject input1;
    public GameObject input2;
    public GameObject input3;
    public GameObject input4;

    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    public void Choice(ModalPanelDetails details)
    {        
        modalPanelObject.SetActive(true);
        Debug.Log("tiki");

        iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        displayText.text = details.displayText;

        if (details.iconImage)
        {
            iconImage.sprite = details.iconImage;
            iconImage.gameObject.SetActive(true);
        }

        if (inputPanel)
        {
            inputPanel.SetActive(details.inputPanel);

            if (details.inputPanel)
            {
                inputLabel1.SetActive(details.input1Details.enable);
                input1.SetActive(details.input1Details.enable);

                if (details.input1Details.enable)
                {
                    inputLabel1.GetComponent<Text>().text = details.input1Details.inputLabelText;
                    input1.GetComponent<InputField>().text = details.input1Details.inputText;
                }

                inputLabel2.SetActive(details.input2Details.enable);
                input2.SetActive(details.input2Details.enable);

                if (details.input2Details.enable)
                {
                    inputLabel2.GetComponent<Text>().text = details.input2Details.inputLabelText;
                    input2.GetComponent<InputField>().text = details.input2Details.inputText;
                }

                inputLabel3.SetActive(details.input3Details.enable);
                input3.SetActive(details.input3Details.enable);

                if (details.input3Details.enable)
                {
                    inputLabel3.GetComponent<Text>().text = details.input3Details.inputLabelText;
                    input3.GetComponent<InputField>().text = details.input3Details.inputText;
                }

                inputLabel4.SetActive(details.input4Details.enable);
                input4.SetActive(details.input4Details.enable);

                if (details.input4Details.enable)
                {
                    inputLabel4.GetComponent<Text>().text = details.input4Details.inputLabelText;
                    input4.GetComponent<InputField>().text = details.input4Details.inputText;
                }
            }
        }       

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(ClosePanel);
        button1.onClick.AddListener(details.button1Details.action);
        button1Text.text = details.button1Details.buttonText;
        button1.gameObject.SetActive(true);

        if (details.button2Details != null)
        {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(ClosePanel);
            button2.onClick.AddListener(details.button2Details.action);
            button2Text.text = details.button2Details.buttonText;
            button2.gameObject.SetActive(true);
        }

        if (details.button3Details != null)
        {
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(ClosePanel);
            button3.onClick.AddListener(details.button3Details.action);
            button3Text.text = details.button3Details.buttonText;
            button3.gameObject.SetActive(true);
        }
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
