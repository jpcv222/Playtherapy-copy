using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametersScreenManagerRieles : MonoBehaviour, IParametersManager
{
    public GameObject parametersPanel;
    public Dropdown dropdownGameType;
    public Slider sliderGameType;
    public Text textGameType;
    public Toggle toggleJog;
    public Slider sliderJogThreshold;
    public Text textJogThreshold;
    public Slider sliderJogTime;
    public Text textJogTime;
    public Toggle toggleCrouch;
    public Slider sliderCrouchThreshold;
    public Text textCrouchThreshold;
    public Toggle toggleJump;
    public Slider sliderJumpThreshold;
    public Text textJumpThreshold;
    public Toggle toggleShifts;
    public Slider sliderDifficulty;
    public Text textDifficulty;

    private void Update()
    {
        if (toggleJog.isOn == true && toggleJump.isOn == false && toggleCrouch.isOn == false)
        {
            dropdownGameType.value = 0;
            dropdownGameType.interactable = false;
            sliderDifficulty.interactable = false;
        }
        else
        {
            dropdownGameType.interactable = true;
            sliderDifficulty.interactable = true;
        }
    }
    public void StartGame()
    {
        bool withTime = false;
        float time = 0;
        int repetitions = 0;
        bool useJog = toggleJog.isOn;
        float jogThreshold = sliderJogThreshold.value * 0.01f;
        float jogTime = sliderJogTime.value * 0.1f;
        bool useCrouch = toggleCrouch.isOn;
        float crouchThreshold = sliderCrouchThreshold.value * 0.01f;
        bool useJump = toggleJump.isOn;
        float jumpThreshold = sliderJumpThreshold.value * 0.01f;
        bool useShifts = toggleShifts.isOn;
        int difficulty = (int)sliderDifficulty.value;

        if (dropdownGameType.value == 0)
        {
            withTime = true;
            time = sliderGameType.value * 30;
        }
        else
        {
            repetitions = (int)sliderGameType.value;
        }
        
        if (GameManagerRieles.gm != null)
        {
            GameManagerRieles.gm.StartGame(withTime, time, repetitions, useJog, jogThreshold, jogTime, useCrouch, crouchThreshold, useJump, jumpThreshold, useShifts, difficulty);
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
            toggleShifts.interactable = true;
        }
        else
        {

            sliderGameType.minValue = 10;
            sliderGameType.maxValue = 80;
            //toggleShifts.interactable = false;
            //toggleShifts.isOn = false;
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

    public void OnJogThresholdSliderValueChanged()
    {
        textJogThreshold.text = sliderJogThreshold.value + " cm";
    }

    public void OnJogTimeSliderValueChanged()
    {
        textJogTime.text = (sliderJogTime.value * 0.1).ToString("0.0") + " seg";
    }

    public void OnCrouchThresholdSliderValueChanged()
    {
        textCrouchThreshold.text = sliderCrouchThreshold.value + " cm";
    }

    public void OnJumpThresholdSliderValueChanged()
    {
        textJumpThreshold.text = sliderJumpThreshold.value + " cm";
    }

    public void OnDifficultySliderValueChanged()
    {
        if (sliderDifficulty.value == 1)
            textDifficulty.text = "Fácil";
        else if (sliderDifficulty.value == 2)
            textDifficulty.text = "Normal";
        else if (sliderDifficulty.value == 3)
            textDifficulty.text = "Difícil";
        else
            Debug.Log("error setting difficulty");
    }

   
}
