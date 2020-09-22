using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametersScreenManagerMoles : MonoBehaviour, IParametersManager
{
    private List<Leap.Finger.FingerType> leftFingers;
    private List<Leap.Finger.FingerType> rightFingers;
    private GameManagerMoles.GameModeMoles gameMode;

    public GameObject parametersPanel;
    public GameObject parametersScenaryPanel;
    public Dropdown dropdownGameType;
    public Slider sliderGameType;
    public Text textGameType;
    public Slider sliderTimeBetweenReps;
    public Text textTimeBetweenReps;
    public Slider sliderMoleUptime;
    public Text textMoleUptime;
    public GameObject panelMode;
    public Dropdown dropdownGameMode;
    public GameObject panelFingers;
    public GameObject[] togglesFingers;
    public GameObject panelGrab;
    public Slider sliderMinGrabStrenght;
    public Text textMinGrabStrenght;
    public int scenary;

    private void Start()
    {
        scenary = 1;
        leftFingers = new List<Leap.Finger.FingerType>();
        rightFingers = new List<Leap.Finger.FingerType>();

        int c1 = 0;
        int c2 = 0;

        for (int i = 0; i < togglesFingers.Length; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
            {
                if (i < togglesFingers.Length / 2)
                {
                    leftFingers.Add(togglesFingers[i].GetComponent<ToggleFinger>().fingerType);
                    c1++;
                }
                else
                {
                    rightFingers.Add(togglesFingers[i].GetComponent<ToggleFinger>().fingerType);
                    c2++;
                }
            }
        }

        gameMode = GameManagerMoles.GameModeMoles.Touch;
    }

    public void StartGame()
    {
        bool withTime = false;
        float time = 0;
        int repetitions = 0;
        float timeBetweenReps = sliderTimeBetweenReps.value * 0.1f;
        float moleUptime = sliderMoleUptime.value * 0.1f;
        float minGrabStrenght = sliderMinGrabStrenght.value * 0.1f;

        if (dropdownGameType.value == 0)
        {
            withTime = true;
            time = sliderGameType.value * 30;
        }
        else
        {
            repetitions = (int)sliderGameType.value;
        }        
        
        if (GameManagerMoles.gm != null)
        {
            GameManagerMoles.gm.SetScenary(scenary);
            GameManagerMoles.gm.StartGame(withTime, time, repetitions, leftFingers, rightFingers, timeBetweenReps, moleUptime,
                gameMode, minGrabStrenght);
            if (parametersPanel != null)
                parametersPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Could not start game");
        }
    }

    public void OnGameTypeChanged()
    {
        if (dropdownGameType.value == 0)
        {
            sliderGameType.minValue = 2f;
            sliderGameType.maxValue = 20f;
        }
        else
        {
            sliderGameType.minValue = 10;
            sliderGameType.maxValue = 80;
        }

        sliderGameType.value = sliderGameType.minValue;
    }

    public void OnGameTypeSliderValueChanged()
    {
        if (dropdownGameType.value == 0)
        {
            float time = sliderGameType.value * 30f;
            textGameType.text = ((int)time / 60).ToString("00") + ":" + ((int)time % 60).ToString("00") + " mins";
        }
        else
        {
            textGameType.text = sliderGameType.value.ToString();
        }
    }
    public void OnTimeBetweenRepsSliderValueChanged()
    {
        textTimeBetweenReps.text = (sliderTimeBetweenReps.value * 0.1).ToString("0.0 segs");
    }

    public void OnMoleUptimeSliderValueChanged()
    {
        textMoleUptime.text = (sliderMoleUptime.value * 0.1).ToString("0.0 segs");
    }

    public void OnGameModeChanged()
    {
        if (dropdownGameMode.value == 0)
        {
            gameMode = GameManagerMoles.GameModeMoles.Touch;
            panelGrab.SetActive(false);
            panelFingers.SetActive(true);
        }
        else if (dropdownGameMode.value == 1)
        {
            gameMode = GameManagerMoles.GameModeMoles.Grab;
            panelFingers.SetActive(false);
            panelGrab.SetActive(true);
        }

        panelMode.SetActive(false);
        panelMode.SetActive(true);
    }

    public void OnMinGrabStrenghtSliderValueChanged()
    {
        textMinGrabStrenght.text = (sliderMinGrabStrenght.value * 10).ToString() + "%";
    }

    public void OnToggleFingerValueChanged()
    {
        int counter = 0;
        bool left = false;
        for (int i = 0; i < togglesFingers.Length / 2; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
                counter++;
        }

        left = !(counter == 0);
        //toggleLeftHand.isOn = !(counter == 0);

        counter = 0;
        bool right = false;
        for (int i = togglesFingers.Length / 2; i < togglesFingers.Length; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
                counter++;
        }

        right = !(counter == 0);
        //toggleRightHand.isOn = !(counter == 0);
        
        counter = 0;
        int index = 0;
        for (int i = 0; i < togglesFingers.Length; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
            {
                counter++;
                index = i;
            }
        }

        if (counter == 1)
            togglesFingers[index].GetComponent<Toggle>().interactable = false;
        else
            for (int i = 0; i < togglesFingers.Length; i++)
                togglesFingers[i].GetComponent<Toggle>().interactable = true;

        int c1 = 0;
        int c2 = 0;

        leftFingers.Clear();
        rightFingers.Clear();

        for (int i = 0; i < togglesFingers.Length; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
            {
                if (i < togglesFingers.Length / 2)
                {
                    leftFingers.Add(togglesFingers[i].GetComponent<ToggleFinger>().fingerType);
                    c1++;
                }
                else
                {
                    rightFingers.Add(togglesFingers[i].GetComponent<ToggleFinger>().fingerType);
                    c2++;
                }
            }
        }
    }

    public void AllLeftDisable()
    {
        for (int i = 0; i <=4; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
            {
                togglesFingers[i].GetComponent<Toggle>().isOn = false;

            }
            else
            {
                togglesFingers[i].GetComponent<Toggle>().isOn = true;
            }
        }
    }
    public void AllRightDisable()
    {
        for (int i = 5; i <= 9; i++)
        {
            if (togglesFingers[i].GetComponent<Toggle>().isOn)
            {
                togglesFingers[i].GetComponent<Toggle>().isOn = false;

            }
            else
            {
                togglesFingers[i].GetComponent<Toggle>().isOn = true;
            }
        }
    }
    public void AllDisableComponents()
    {
        parametersPanel.SetActive(false);
        parametersScenaryPanel.SetActive(true);
    }

    public void ChangeScenary(int value)
    {
        scenary = value;
        parametersPanel.SetActive(true);
        parametersScenaryPanel.SetActive(false);
    }


}
