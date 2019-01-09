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

	// Use this for initialization
	void Start () {
		// powerButton.OnPointerUp.AddListener (onButtonReleased);
		// powerButton.OnPointerDown.AddListener (onButtonPressed);
	}

	// Update is called once per frame
	void Update () {
		is_button_pressed = sliderHandle.GetComponent<PowerButtonState> ().buttonPressed;
		Debug.Log (is_button_pressed);
		if (!is_button_pressed) {
			if ((slider.value > 0.5f) && (slider.value < 0.95f)) slider.value += 0.05f;

			if (slider.value > 0.05f && slider.value <= 0.5f) slider.value -= 0.05f;

			if (slider.value >= 0.95f) {
				slider.value = 1.0f;
				text.text = "POWER IS ON";
				powerButton.GetComponent<Image> ().color = Color.green;
			}

			if (slider.value <= 0.05f) {
				slider.value = 0.0f;
				text.text = "SWAP TO POWER ON";
				powerButton.GetComponent<Image> ().color = Color.red;
			}
		}
	}
}