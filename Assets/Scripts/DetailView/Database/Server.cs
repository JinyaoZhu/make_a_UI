using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    public Component Tank1 = new Component();
    public Component Tank2 = new Component();

    // Use this for initialization
    void Awake () {
        Tank1Init();
        Tank2Init();
    }

    // Update is called once per frame
    float delta = 0.3f;
	void Update () {
        Tank1Update();
        Tank2Update();
    }

    private void Tank1Update() {
        Tank1.valueBarsValue[0] += delta;
        if (Tank1.valueBarsValue[0] >= Tank1.valueBarsMaxValue[0]) delta = -delta;
        if (Tank1.valueBarsValue[0] <= Tank1.valueBarsMinValue[0]) delta = -delta;
    }

    private void Tank2Update()
    {
        Tank2.valueBarsValue[0] += delta;
        if (Tank2.valueBarsValue[0] >= Tank2.valueBarsMaxValue[0]) delta = -delta;
        if (Tank2.valueBarsValue[0] <= Tank2.valueBarsMinValue[0]) delta = -delta;

        Tank2.valueBarsValue[1] += delta;
        if (Tank2.valueBarsValue[1] >= Tank2.valueBarsMaxValue[1]) delta = -delta;
        if (Tank2.valueBarsValue[1] <= Tank2.valueBarsMinValue[1]) delta = -delta;
    }


    private void Tank1Init() {
        Tank1.componentName = "Tank1";
        Tank1.status = "normal";
        Tank1.buttonsIsActive = new bool[] { true, false, false, false };
        Tank1.buttonsState = new bool[] { true, false, false, false };
        Tank1.buttonsName = new string[] { "F1", "NULL", "NULL", "NULL" };
        Tank1.valueBarsIsActive = new bool[] { true, false };
        Tank1.valueBarsValue = new float[] { 50, 0 };
        Tank1.valueBarsMaxValue = new float[] { 100, 100 };
        Tank1.valueBarsMinValue = new float[] { 0, 0 };
        Tank1.valueBarsName = new string[] { "T1", "NULL" };
        Tank1.valueBarsUnit = new string[] { "L", "L" };
        Tank1.settingBarIsActive = true;
        Tank1.settingBarName = "Max Level:";
        Tank1.settingBarUnit = "L";
        Tank1.settingBarValue = 0;
        Tank1.powerIsActive = true;
        Tank1.powerState = false;
    }

    private void Tank2Init()
    {
        Tank2.componentName = "Tank2";
        Tank2.status = "normal";
        Tank2.buttonsIsActive = new bool[] { true, true, false, false };
        Tank2.buttonsState = new bool[] { true, true, false, false };
        Tank2.buttonsName = new string[] { "F1", "F2", "NULL", "NULL" };
        Tank2.valueBarsIsActive = new bool[] { true, true };
        Tank2.valueBarsValue = new float[] { 50, 0 };
        Tank2.valueBarsMaxValue = new float[] { 100, 100 };
        Tank2.valueBarsMinValue = new float[] { 0, 0 };
        Tank2.valueBarsName = new string[] { "T1", "T2" };
        Tank2.valueBarsUnit = new string[] { "L", "L" };
        Tank2.settingBarIsActive = false;
        Tank2.settingBarName = "NULL";
        Tank2.settingBarUnit = "";
        Tank2.settingBarValue = 0;
        Tank2.powerIsActive = true;
        Tank2.powerState = true;
    }
}
