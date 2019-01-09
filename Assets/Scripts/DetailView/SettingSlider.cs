using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour {

	public Slider slider;
	public Text displayValue;
	public GameObject fillArea;
	public Button button;

	// Use this for initialization
	void Start () { }

	// Update is called once per frame
	void Update () {
		displayValue.text = "VALUE = " + slider.value;

		if (slider.value == slider.minValue) {
			button.GetComponent<Image> ().color =  new Color(0.0f,0.5f,1.0f, 0.3f);
		} else if (slider.value == slider.maxValue) {
			button.GetComponent<Image> ().color =  new Color(0.0f,0.5f,1.0f, 1.0f);
		} else {
			button.GetComponent<Image> ().color =  new Color(0.0f,0.5f,1.0f, 0.8f);
		}
	}
}