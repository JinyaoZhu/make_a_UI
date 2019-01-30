using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    public Component Tank1 = new Component();
    public Component Tank2 = new Component();
    public Component Ventile1 = new Component();
    public Component Ventile2 = new Component();
    public Component Pumpe1 = new Component();
    public Component Mischer1 = new Component();

    public GameObject Tank1Prefab;
    public GameObject Tank2Prefab;
    public GameObject Ventile1Prefab;
    public GameObject Ventile2Prefab;
    public GameObject Pumpe1Prefab;
    public GameObject Mischer1Prefab;

    public Component GetComponentById(string id) {
        switch (id) {
            case "T001": return Tank1;
            case "T002": return Tank2;
            case "V001": return Ventile1;
            case "V002": return Ventile2;
            case "M001": return Mischer1;
            case "P001": return Pumpe1;
            default:break;
        }
        return null;
    }

    // Use this for initialization
    void Start () {
        Tank1Init(Tank1, Tank1Prefab);
        Tank2Init(Tank2, Tank2Prefab);
        Ventil1Init(Ventile1, Ventile1Prefab);
        Ventil2Init(Ventile2, Ventile2Prefab);
        Pumpe1Init(Pumpe1, Pumpe1Prefab);
        Mischer1Init(Mischer1, Mischer1Prefab);
    }

    // Update is called once per frame
    float delta1_1 = 0.3f;
    float delta1_2 = 0.3f;
    float delta2_1 = 0.2f;

    float current_time;

	void Update () {
        current_time += Time.deltaTime;
        Tank1Update();
        Tank2Update();
        Pumpe1Update();
        Mischer1Update();
        VentileUpdate();
    }

    private void Tank1Update() {
        if (!Ventile1.powerState) return;
        Tank1.valueBarsValue[0] += delta1_1;
        if (Tank1.valueBarsValue[0] >= Tank1.settingBarValue) delta1_1 = -delta1_1;
        if (Tank1.valueBarsValue[0] <= Tank1.valueBarsMinValue[0]) delta1_1 = -delta1_1;

        if (Tank1.valueBarsValue[0] <= 0.1 * Tank1.valueBarsMaxValue[0])
            Tank1.status = "Warning";
        else if (Tank1.valueBarsValue[0] >= 0.9 * Tank1.valueBarsMaxValue[0])
            Tank1.status = "Warning";
        else
            Tank1.status = "Normal";
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

    private void Pumpe1Update() {
        if (!Pumpe1.powerState)
            Pumpe1.status = "Off";
        else
            Pumpe1.status = "Normal";
    }

    private void Mischer1Update()
    {
        if (!Mischer1.powerState)
        {
            Mischer1.status = "Off";
            Mischer1.valueBarsValue[0] = 0;
            return;
        }
        else
            Mischer1.status = "Normal";


        Mischer1.valueBarsValue[0] = Mischer1 .settingBarValue + Mathf.Sin(10.0f*current_time);
        Mischer1.valueBarsValue[1] = 25 + 2 * Mathf.Sin(0.5f*current_time);
    }

    private void VentileUpdate() {
        if (!Ventile1.powerState)
        {
            Ventile1.status = "Off";
        }
        else
            Ventile1.status = "Normal";

        if (!Ventile2.powerState)
        {
            Ventile2.status = "Off";
        }
        else
            Ventile2.status = "Normal";
    }


    private void Tank1Init(Component component, GameObject model_3d) {
        component.componentName = "Tank1";
        component.id = "T001";
        component.status = "Normal";
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
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 60;
        component.powerIsActive = false;
        component.powerState = false;
        component.model = model_3d;
    }

    private void Tank2Init(Component component, GameObject model_3d)
    {
        component.componentName = "Tank2";
        component.id = "T002";
        component.status = "Danger";
        component.buttonsIsActive = new bool[] { true, true, false, false };
        component.buttonsState = new bool[] { true, true, true, false };
        component.buttonsName = new string[] { "F1", "F2", "F3", "NULL" };
        component.valueBarsIsActive = new bool[] { true, true };
        component.valueBarsValue = new float[] { 50, 0 };
        component.valueBarsMaxValue = new float[] { 100, 100 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "T1", "T2" };
        component.valueBarsUnit = new string[] { "L", "L" };
        component.settingBarIsActive = false;
        component.settingBarName = "";
        component.settingBarUnit = "";
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 0;
        component.powerIsActive = false;
        component.powerState = false;
        component.model = model_3d;
    }

    private void Ventil1Init(Component component, GameObject model_3d)
    {
        component.componentName = "Ventil1";
        component.id = "V001";
        component.status = "Normal";
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
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 0;
        component.powerIsActive = true;
        component.powerState = true;
        component.model = model_3d;
    }

    private void Ventil2Init(Component component, GameObject model_3d)
    {
        component.componentName = "Ventil2";
        component.id = "V002";
        component.status = "Normal";
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
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 0;
        component.powerIsActive = true;
        component.powerState = false;
        component.model = model_3d;
    }

    private void Mischer1Init(Component component, GameObject model_3d)
    {
        component.componentName = "Mischer1";
        component.id = "M001";
        component.status = "Off";
        component.buttonsIsActive = new bool[] { true, true, true, true };
        component.buttonsState = new bool[] { true, false, false, false };
        component.buttonsName = new string[] { "Heating", "Cooling", "FN1", "FN2" };
        component.valueBarsIsActive = new bool[] { true, true };
        component.valueBarsValue = new float[] { 50, 25 };
        component.valueBarsMaxValue = new float[] { 100, 50 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "Vel.", "Temp." };
        component.valueBarsUnit = new string[] { "/rpm", "/C" };
        component.settingBarIsActive = true;
        component.settingBarName = "Vel.";
        component.settingBarUnit = "/rpm";
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 0;
        component.powerIsActive = true;
        component.powerState = false;
        component.model = model_3d;
    }

    private void Pumpe1Init(Component component, GameObject model_3d)
    {
        component.componentName = "Pumpe1";
        component.id = "P001";
        component.status = "Normal";
        component.buttonsIsActive = new bool[] { true, true, false, false };
        component.buttonsState = new bool[] { true, false, false, false };
        component.buttonsName = new string[] { "F1", "F2", " ", " " };
        component.valueBarsIsActive = new bool[] { true, false };
        component.valueBarsValue = new float[] { 30, 0 };
        component.valueBarsMaxValue = new float[] { 100, 100 };
        component.valueBarsMinValue = new float[] { 0, 0 };
        component.valueBarsName = new string[] { "Vel.", " " };
        component.valueBarsUnit = new string[] { "/rpm",  " " };
        component.settingBarIsActive = false;
        component.settingBarName = " ";
        component.settingBarUnit = " ";
        component.settingBarMaxValue = 100;
        component.settingBarMinValue = 0;
        component.settingBarValue = 0;
        component.powerIsActive = true;
        component.powerState = false;
        component.model = model_3d;
    }
}
