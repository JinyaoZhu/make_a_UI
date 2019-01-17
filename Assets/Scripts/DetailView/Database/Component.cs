using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : IEquatable<Component>
{

    public string componentName;
    public string status;

    public bool[] buttonsIsActive; // if the button is active
    public bool[] buttonsState; // is the button on/off
    public string[] buttonsName; // name of the button

    public bool[] valueBarsIsActive; // if the value bar is active
    public float[] valueBarsValue; // value of the bar
    public float[] valueBarsMaxValue; // maximal value 
    public float[] valueBarsMinValue; // minimal value
    public string[] valueBarsName; // name of the bar
    public string[] valueBarsUnit; // unit

    public bool settingBarIsActive;
    public string settingBarName;
    public string settingBarUnit;
    public float settingBarValue;

    public bool powerIsActive;
    public bool powerState;

    public void SetButtonsState(bool[] states) {
        int i = 0;
        foreach (bool state in states) {
            buttonsState[i] = states[i];
            i++;
        }
    }

    public void SetValueBars(float value1, float value2) {
        valueBarsValue[0] = value1;
        valueBarsValue[1] = value2;
    }

    public void SetSettingBar(float value) {
        settingBarValue = value;
    }

    public void SetPowerState(bool state)
    {
        powerState = state;
    }

    // need this for using Ditionary
    public bool Equals(Component other)
    {
        return componentName == other.componentName;
    }
    // need this for using Ditionary
    public override int GetHashCode()
    {
        return componentName.GetHashCode();
    }
}
