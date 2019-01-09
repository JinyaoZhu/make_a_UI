using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour {

	public Slider slider;
	public Text valueText;
	public Text nameText;
	public float maxValue;
	public float minValue;
	public float startValue;
	public string  name;

	// Use this for initialization
	void Start () {
		nameText.text = name;
		slider.maxValue = maxValue;
		slider.minValue = minValue;
		slider.value = startValue;
	}

	// Update is called once per frame
	void Update () {

		slider.value += 0.1f;

		if (slider.value >= slider.maxValue) slider.value = slider.minValue;

		valueText.text = slider.value.ToString ("F1") + " L";
	}
}