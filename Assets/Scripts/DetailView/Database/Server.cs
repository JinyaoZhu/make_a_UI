using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    public Component Tank1;
    public Component Tank2;
    public Component Ventile1;

    public GameObject Tank1Prefab;
    public GameObject Tank2Prefab;
    public GameObject Ventile1Prefab;

    // Use this for initialization
    void Awake () {
        Tank1 = new Component();
        Tank2 = new Component();
        Ventile1 = new Component();
        Tank1Init(Tank1);
        Tank2Init(Tank2);
        Ventil1Init(Ventile1);
    }

    // Update is called once per frame
    float delta1_1 = 0.3f;
    float delta1_2 = 0.3f;
    float delta2_1 = 0.2f;
	void Update () {
        Tank1Update();
        Tank2Update();
    }

    private void Tank1Update() {
        if (!Ventile1.powerState) return;
        Tank1.valueBarsValue[0] += delta1_1;
        if (Tank1.valueBarsValue[0] >= Tank1.valueBarsMaxValue[0]) delta1_1 = -delta1_1;
        if (Tank1.valueBarsValue[0] <= Tank1.valueBarsMinValue[0]) delta1_1 = -delta1_1;
    }

    private void Tank2Update()
    {
        if (!Ventile1.powerState) return;
        Tank2.valueBarsValue[0] += delta1_2;
        if (Tank2.valueBarsValue[0] >= Tank2.valueBarsMaxValue[0]) delta1_2 = -delta1_2;
        if (Tank2.valueBarsValue[0] <= Tank2.valueBarsMinValue[0]) delta1_2 = -delta1_2;

        Tank2.valueBarsValue[1] += delta2_1;
        if (Tank2.valueBarsValue[1] >= Tank2.valueBarsMaxValue[1]) delta2_1 = -delta2_1;
        if (Tank2.valueBarsValue[1] <= Tank2.valueBarsMinValue[1]) delta2_1 = -delta2_1;
    }


    private void Tank1Init(Component component) {
        component.componentName = "Tank1";
        component.status = "normal";
        component.buttonsIsActive = new bool[] { true, false, false, false };
        component.buttonsState = new bool[] { true, false, false, false };
        component.buttonsName = new string[] { "F1", "NULL", "NULL", "NULL" };
        component.valueBarsIsActive = new bool[] { true, false };
        component.valueBarsValue = new float[] { 50, 0 };
        component.valueBarsMaxValue = new float[] { 100, 100 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "T1", "NULL" };
        component.valueBarsUnit = new string[] { "L", "L" };
        component.settingBarIsActive = true;
        component.settingBarName = "Max Level:";
        component.settingBarUnit = "L";
        component.settingBarValue = 0;
        component.powerIsActive = false;
        component.powerState = false;
        component.model = Tank1Prefab;
    }

    private void Tank2Init(Component component)
    {
        component.componentName = "Tank2";
        component.status = "normal";
        component.buttonsIsActive = new bool[] { true, true, false, false };
        component.buttonsState = new bool[] { true, true, false, false };
        component.buttonsName = new string[] { "F1", "F2", "NULL", "NULL" };
        component.valueBarsIsActive = new bool[] { true, true };
        component.valueBarsValue = new float[] { 50, 0 };
        component.valueBarsMaxValue = new float[] { 100, 100 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "T1", "T2" };
        component.valueBarsUnit = new string[] { "L", "L" };
        component.settingBarIsActive = false;
        component.settingBarName = "NULL";
        component.settingBarUnit = "";
        component.settingBarValue = 0;
        component.powerIsActive = false;
        component.powerState = false;
        component.model = Tank2Prefab;
    }

    private void Ventil1Init(Component component)
    {
        component.componentName = "Ventil1";
        component.status = "normal";
        component.buttonsIsActive = new bool[] { false, false, false, false };
        component.buttonsState = new bool[] { false, false, false, false };
        component.buttonsName = new string[] { " ", " ", " ", " " };
        component.valueBarsIsActive = new bool[] { false, false };
        component.valueBarsValue = new float[] { 50, 0 };
        component.valueBarsMaxValue = new float[] { 100, 100 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "T1", "T2" };
        component.valueBarsUnit = new string[] { "L", "L" };
        component.settingBarIsActive = false;
        component.settingBarName = " ";
        component.settingBarUnit = " ";
        component.settingBarValue = 0;
        component.powerIsActive = true;
        component.powerState = true;
        component.model = Ventile1Prefab;
    }
}
