using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalWindowMaker : MonoBehaviour
{
    private ModalPanel modalPanel;
    private DBConnection dbConnection;

    private static ModalWindowMaker modalWindowMaker;

    public static ModalWindowMaker Instance()
    {
        if (!modalWindowMaker)
        {
            modalWindowMaker = FindObjectOfType(typeof(ModalWindowMaker)) as ModalWindowMaker;
            if (!modalWindowMaker)
                Debug.LogError("There needs to be one active ModalWindowMaker script on a GameObject in your scene.");
        }

        return modalWindowMaker;
    }

    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        dbConnection = DBConnection.Instance();
    }

    //  Send to the Modal Panel to set up the Buttons and Functions to call
    public void DBConnectionError(string host, string username, string password, string database)
    {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails();
        modalPanelDetails.displayText = "Error al conectar con la Base de Datos.";
        modalPanelDetails.inputPanel = true;
        modalPanelDetails.input1Details = new InputLineDetails(true, "Host", host);
        modalPanelDetails.input2Details = new InputLineDetails(true, "Username", username);
        modalPanelDetails.input3Details = new InputLineDetails(true, "Password", password);
        modalPanelDetails.input4Details = new InputLineDetails(true, "Database", database);
        modalPanelDetails.button1Details = new EventButtonDetails("Reintentar", RetryDBConnectionFunction);
        modalPanelDetails.button2Details = new EventButtonDetails("Salir", Quit);

        modalPanel.Choice(modalPanelDetails);
    }

    void RetryDBConnectionFunction()
    {
        Debug.Log("Retrying connection...");

        string host = modalPanel.input1.GetComponent<InputField>().text;
        string username = modalPanel.input2.GetComponent<InputField>().text;
        string password = modalPanel.input3.GetComponent<InputField>().text;
        string database = modalPanel.input4.GetComponent<InputField>().text;

        dbConnection.Connect(host, username, password, database);

        Debug.Log("Connection retried");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
