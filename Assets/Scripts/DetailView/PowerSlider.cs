using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{

    public Slider slider;
    public GameObject sliderHandle;
    public Text text;
    public Button powerButton;

    private bool is_active = false;
    private bool is_button_pressed;
    private float last_slider_value;
    private bool power_state;

    // set slider active state
    public void SetActive(bool state)
    {
        is_active = state;
    }

    public void SetPowerState(bool state) {
        if (state)
        {
            slider.value = 1.0f;
            text.text = "POWER IS ON";
            power_state = true;
            powerButton.GetComponent<Image>().color = new Color(0.0f, 0.9f, 0.0f, 0.8f);
        }
        else {
            slider.value = 0.0f;
            text.text = "SWAP TO POWER ON";
            power_state = false;
            powerButton.GetComponent<Image>().color = new Color(0.9f, 0.0f, 0.0f, 0.8f);
        }

        last_slider_value = slider.value;
    }

    public bool GetPowerState() {
        return power_state;
    }

    // Use this for initialization
    void Start()
    {
        // powerButton.OnPointerUp.AddListener (onButtonReleased);
        // powerButton.OnPointerDown.AddListener (onButtonPressed);
        last_slider_value = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            slider.interactable = true;
            UpdateSlider();
        }
        else
        {
            slider.interactable = false;
            text.text = " ";
            powerButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1);
        }
    }

    private void UpdateSlider()
    {
        is_button_pressed = sliderHandle.GetComponent<SliderButtonState>().buttonPressed;

        if (Math.Abs(last_slider_value - slider.value) > 0.5)
            slider.value = last_slider_value;

        last_slider_value = slider.value;

        if (!is_button_pressed)
        {
            if ((slider.value > 0.5f) && (slider.value < 0.95f)) slider.value += 0.05f;

            if (slider.value > 0.05f && slider.value <= 0.5f) slider.value -= 0.05f;

            if (slider.value >= 0.95f)
            {
                slider.value = 1.0f;
                text.text = "POWER IS ON";
                power_state = true;
                powerButton.GetComponent<Image>().color = new Color(0.0f, 0.9f, 0.0f, 0.8f);
            }

            if (slider.value <= 0.05f)
            {
                slider.value = 0.0f;
                text.text = "SWAP TO POWER ON";
                power_state = false;
                powerButton.GetComponent<Image>().color = new Color(0.9f, 0.0f, 0.0f, 0.8f);
            }
        }
    }
}
