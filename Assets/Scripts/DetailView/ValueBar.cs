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

	public float currentvalue;

	// Use this for initialization
	void Start () {
		nameText.text = name;
		slider.maxValue = maxValue;
		slider.minValue = minValue;
		slider.value = startValue;
	}

    // Update is called once per frame
    float delta = 0.1f;
	void Update () {

		slider.value += delta;

		if (slider.value >= slider.maxValue) delta = -delta;
        if (slider.value <= slider.minValue) delta = -delta;

        valueText.text = slider.value.ToString ("F1") + " L";

		currentvalue = slider.value;
	}
}