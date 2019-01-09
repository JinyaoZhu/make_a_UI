using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour {

	public Slider slider;
	public GameObject sliderHandle;
	public Text text;
	public Button powerButton;

	bool is_button_pressed;
	float lastSliderValue;

	// Use this for initialization
	void Start () {
		// powerButton.OnPointerUp.AddListener (onButtonReleased);
		// powerButton.OnPointerDown.AddListener (onButtonPressed);
		lastSliderValue = slider.value;
	}

	// Update is called once per frame
	void Update () {
		is_button_pressed = sliderHandle.GetComponent<PowerButtonState> ().buttonPressed;
		
        if(Math.Abs(lastSliderValue - slider.value) > 0.5) slider.value = lastSliderValue;
		lastSliderValue = slider.value;

		if (!is_button_pressed) {
			if ((slider.value > 0.5f) && (slider.value < 0.95f)) slider.value += 0.05f;

			if (slider.value > 0.05f && slider.value <= 0.5f) slider.value -= 0.05f;

			if (slider.value >= 0.95f) {
				slider.value = 1.0f;
				text.text = "POWER IS ON";
				powerButton.GetComponent<Image> ().color = new Color(0.0f,0.9f,0.0f, 0.8f);
			}

			if (slider.value <= 0.05f) {
				slider.value = 0.0f;
				text.text = "SWAP TO POWER ON";
				powerButton.GetComponent<Image> ().color = new Color(0.9f,0.0f,0.0f, 0.8f);
			}
		}
	}
}
