﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailViewManager : MonoBehaviour {

	public Button returnButton;
    public Button plantButton;

    Animator global_animator;

    // all elements in panel
    private FlatButton[] buttons = new FlatButton[6];
    private ValueBar[] bars = new ValueBar[2];
    private SettingSlider slider;
    private PowerSlider power;
    private ModelHeader header;
    private PlotGraph plotter;
    private Camera stage;
    private GameObject model;

    // server that update and save the component data
    private Server server;

    private Dictionary<string, Component> components = new Dictionary<string, Component>();

    // current component in the panel
    private string currentComponentName;
    private string lastComponentName = "";

    public void SetCurrentComponent(string name) {
        currentComponentName = name;
    }

    // Use this for initialization
    void Start()
    {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        returnButton.onClick.AddListener(()=>{global_animator.SetTrigger("ExitDetailView");});
        plantButton.onClick.AddListener(()=>{global_animator.SetTrigger("Detail2Plant");});

        buttons[0] = transform.Find("Content/Buttons/Upper/Row1/Button1").GetComponent<FlatButton>();
        buttons[1] = transform.Find("Content/Buttons/Upper/Row1/Button2").GetComponent<FlatButton>();
        buttons[2] = transform.Find("Content/Buttons/Upper/Row2/Button3").GetComponent<FlatButton>();
        buttons[3] = transform.Find("Content/Buttons/Upper/Row2/Button4").GetComponent<FlatButton>();
        buttons[4] = transform.Find("Content/Buttons/Upper/Row3/Button5").GetComponent<FlatButton>();
        buttons[5] = transform.Find("Content/Buttons/Upper/Row3/Button6").GetComponent<FlatButton>();

        bars[0] = transform.Find("Content/Graphics/Upper/Bars/ValueBar1").GetComponent<ValueBar>();
        bars[1] = transform.Find("Content/Graphics/Upper/Bars/ValueBar2").GetComponent<ValueBar>();
        slider = transform.Find("Content/Buttons/Lower/SettingSlider").GetComponent<SettingSlider>();
        power = transform.Find("Content/Buttons/Lower/PowerSlider").GetComponent<PowerSlider>();
        header = transform.Find("Content/Graphics/Upper/Model/Info").GetComponent<ModelHeader>();
        plotter = transform.Find("Content/Graphics/Lower/Graph").GetComponent<PlotGraph>();
        stage = transform.Find("Content/Graphics/Upper/Model/Model/Stage").GetComponent<Camera>();

        server = GameObject.Find("DisplayArea/Database/Server").GetComponent<Server>();

        currentComponentName = "";

        components.Add("T001", server.Tank1);
        components.Add("T002", server.Tank2);
        components.Add("V001", server.Ventile1);
        components.Add("V002", server.Ventile2);
        components.Add("M001", server.Mischer1);
        components.Add("P001", server.Pumpe1);
    }

        // Update is called once per frame
    void Update () {

        if (currentComponentName == "") return;

        if (currentComponentName != lastComponentName) {
            InitPanel(components[currentComponentName]);
        }

        lastComponentName = currentComponentName;

        UpdatePanel(components[currentComponentName]);

        UpdateComponent(components[currentComponentName]);
    }

    // update component according to the current panel settings
    private void UpdateComponent(Component component)
    {
        component.SetPowerState(power.GetPowerState());
        component.SetSettingBar(slider.GetValue());
        bool[] buttons_state = new bool[buttons.Length];
        int i = 0;
        foreach(FlatButton button in buttons) { 
            buttons_state[i] = button.GetButtonState();
            i++;
        }
        component.SetButtonsState(buttons_state);
    }


    // initialize panel with component data
    private void InitPanel(Component component) {
        int i;
        // set header
        header.SetName(component.componentName);
      
        // set buttons
        i = 0;
        foreach (FlatButton button in buttons) {
            button.SetButtonText(component.buttonsName[i]);
            button.SetButtonState(component.buttonsState[i]);
            button.SetActive(component.buttonsIsActive[i]);
            i++;
        }

        // set value bar        
        i = 0;
        foreach (ValueBar bar in bars) {
            bar.SetName(component.valueBarsName[i]);
            bar.SetUnit(component.valueBarsUnit[i]);
            bar.SetValue(component.valueBarsValue[i]);
            bar.SetMaxValue(component.valueBarsMaxValue[i]);
            bar.SetMinValue(component.valueBarsMinValue[i]);
            bar.SetActive(component.valueBarsIsActive[i]);
            i++;
        }

        // graph title
        plotter.SetTitle(component.componentName);

        // setting slider
        slider.SetHead(component.settingBarName);
        slider.SetUnit(component.settingBarUnit);
        slider.SetMaxValue(component.settingBarMaxValue);
        slider.SetMinValue(component.settingBarMinValue);
        slider.SetValue(component.settingBarValue);
        slider.SetActive(component.settingBarIsActive);

        // power slider
        power.SetPowerState(component.powerState);
        power.SetActive(component.powerIsActive);

        // 3d model 
        if (model)
        {
            Destroy(model);
        }
        model = Instantiate(component.model, stage.transform, false);
        model.transform.localPosition = new Vector3(0, 0, 1000);
        model.SetActive(true);
    }
    // update panel with component data
    private void UpdatePanel(Component component) {
        int i = 0;
        foreach (ValueBar bar in bars)
        {
              bar.SetValue(component.valueBarsValue[i]);
           i++;
        }
        // change status
        header.SetStatus(component.status);
    }

}
