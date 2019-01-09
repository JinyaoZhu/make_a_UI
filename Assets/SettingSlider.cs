using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour {

	public Slider slider;
	public Text displayValue;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
      displayValue.text = "VALUE = " + slider.value;
	}
}