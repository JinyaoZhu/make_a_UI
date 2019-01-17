using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour {

	public Slider slider;
    public GameObject background;
	public Text valueText;
	public Text nameText;
    public Text warningText;

    private string barUnit;
    private bool is_active;

    // get current value of the bar
    public float GetCurrentValue() {
        return slider.value;
    }

    // return state of the bar
    public bool IsActive() {
        return is_active;
    }
    // set active state
    public void SetActive(bool state) {
        is_active = state;
    }
    // set name of the bar
    public void SetName(string text) {
        nameText.text = text;
    }
    // set unit of the bar value
    public void SetUnit(string text) {
        barUnit = text;
    }
    // set max value
    public void SetMaxValue(float value) {
        slider.maxValue = value;
    }
    // set min value 
    public void SetMinValue(float value) {
        slider.minValue = value;
    }
    // set current value
    public void SetValue(float value) {
        slider.value = value;
    }

	// Use this for initialization
	void Start () {
        warningText.gameObject.SetActive(false);
        slider.interactable = false;
    }

    // Update is called once per frame
    float delta = 0.3f;
    void Update () {

        if (is_active)
        {
            slider.value += delta;

            if (slider.value >= slider.maxValue) delta = -delta;
            if (slider.value <= slider.minValue) delta = -delta;

            UpdateValueBar();

            background.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            valueText.color = new Color(0,0,0,1);
            nameText.color = new Color(0,0,0,1);
        }
        else {
            slider.value = 0;
            warningText.gameObject.SetActive(false);
            background.GetComponent<Image>().color = new Color(.5f,.5f,.5f,.8f);
            valueText.color = new Color(.5f, .5f, .5f, .8f);
            nameText.color = new Color(.5f, .5f, .5f, .8f);
        }
    }
    // update value bar in each loop
    private void UpdateValueBar() {
        valueText.text = slider.value.ToString("F1") + barUnit;

        if (slider.value >= 0.9 * slider.maxValue)
        {
            warningText.text = "FULL!";
            warningText.gameObject.SetActive(true);
        }
        else if (slider.value <= 0.1 * slider.maxValue)
        {
            warningText.text = "EMPTY!";
            warningText.gameObject.SetActive(true);
        }
        else {
            warningText.gameObject.SetActive(false);
        }
    }
}