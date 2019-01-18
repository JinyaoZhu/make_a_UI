using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{

    public Slider slider;
    public Text displayValue;
    public GameObject fillArea;
    public Button button;

    private string text_head = "";
    private string text_unit = "";
    private bool is_active = false;

    // set slider active state
    public void SetActive(bool state) {
        is_active = state;
    }

    // set text head, eg: "Value = "
    public void SetHead(string text) {
        text_head = text;
    }
    // set text unit, eg: "L" or "RPM"
    public void SetUnit(string text) {
        text_unit = text;
    }

    public void SetValue(float value) {
        slider.value = value;
    }

    public float GetValue() {
        return slider.value;
    }

    // Use this for initialization
    void Start()
    {
        displayValue.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            slider.interactable = true;
            UpdateSlider();
        }
        else {
            slider.interactable = false;
            slider.value = slider.minValue;
            displayValue.text = "";
            button.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1);
        }


    }

    private void UpdateSlider() {
        displayValue.text = text_head + slider.value + text_unit;

        if (slider.value == slider.minValue)
        {
            button.GetComponent<Image>().color = new Color(0.0f, 0.5f, 1.0f, 0.3f);
        }
        else if (slider.value == slider.maxValue)
        {
            button.GetComponent<Image>().color = new Color(0.0f, 0.5f, 1.0f, 1.0f);
        }
        else
        {
            button.GetComponent<Image>().color = new Color(0.0f, 0.5f, 1.0f, 0.8f);
        }
    }
}