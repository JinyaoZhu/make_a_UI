using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailViewManager : MonoBehaviour {

	public Button returnButton;

	Animator global_animator;

	// Use this for initialization
	void Start () {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        returnButton.onClick.AddListener(OnReturnClick);
        LoadComponent("Tank1");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

	void OnReturnClick(){
		global_animator.SetTrigger("ExitDetailView");
	}

    public void LoadComponent(string name)
    {
        switch (name) {
            case "Tank1":
                LoadTank1();
                break;
            case "Tank2":
                LoadTank2();
                break;
            default: break;
        }
    }

    private void LoadTank2() {
        string my_name = "Tank B";

        FlatButton button1 = transform.Find("Content/Buttons/Upper/Row1/Button1").GetComponent<FlatButton>();
        FlatButton button2 = transform.Find("Content/Buttons/Upper/Row1/Button2").GetComponent<FlatButton>();
        FlatButton button3 = transform.Find("Content/Buttons/Upper/Row2/Button3").GetComponent<FlatButton>();
        FlatButton button4 = transform.Find("Content/Buttons/Upper/Row2/Button4").GetComponent<FlatButton>();
        ValueBar bar1 = transform.Find("Content/Graphics/Upper/Bars/ValueBar1").GetComponent<ValueBar>();
        ValueBar bar2 = transform.Find("Content/Graphics/Upper/Bars/ValueBar2").GetComponent<ValueBar>();
        SettingSlider slider = transform.Find("Content/Buttons/Lower/SettingSlider").GetComponent<SettingSlider>();
        PowerSlider power = transform.Find("Content/Buttons/Lower/PowerSlider").GetComponent<PowerSlider>();
        ModelHeader header = transform.Find("Content/Graphics/Upper/Model/Info").GetComponent<ModelHeader>();

        // set header
        header.SetName(my_name);
        header.SetStatus("normal");

        // set buttons        
        button1.SetButtonText("F1");
        button1.SetButtonState(true);
        button1.SetActive(true);

        button2.SetButtonText("F2");
        button2.SetButtonState(false);
        button2.SetActive(true);

        button3.SetButtonText("NULL");
        button3.SetButtonState(false);
        button3.SetActive(false);

        button4.SetButtonText("NULL");
        button4.SetButtonState(false);
        button4.SetActive(false);

        // set value bar        
        bar1.SetName("T1");
        bar1.SetUnit("L");
        bar1.SetValue(50);
        bar1.SetMaxValue(100);
        bar1.SetMinValue(0);
        bar1.SetActive(true);

        bar2.SetName("T2");
        bar2.SetUnit("L");
        bar2.SetValue(10);
        bar2.SetMaxValue(50);
        bar2.SetMinValue(0);
        bar2.SetActive(true);

        // setting slider
        slider.SetHead("NULL");
        slider.SetUnit("NULL");
        slider.SetActive(false);

        // power slider
        power.SetActive(true);
    }

    private void LoadTank1()
    {
        string my_name = "Tank A";
        FlatButton button1 = transform.Find("Content/Buttons/Upper/Row1/Button1").GetComponent<FlatButton>();
        FlatButton button2 = transform.Find("Content/Buttons/Upper/Row1/Button2").GetComponent<FlatButton>();
        FlatButton button3 = transform.Find("Content/Buttons/Upper/Row2/Button3").GetComponent<FlatButton>();
        FlatButton button4 = transform.Find("Content/Buttons/Upper/Row2/Button4").GetComponent<FlatButton>();
        ValueBar bar1 = transform.Find("Content/Graphics/Upper/Bars/ValueBar1").GetComponent<ValueBar>();
        ValueBar bar2 = transform.Find("Content/Graphics/Upper/Bars/ValueBar2").GetComponent<ValueBar>();
        SettingSlider slider = transform.Find("Content/Buttons/Lower/SettingSlider").GetComponent<SettingSlider>();
        PowerSlider power = transform.Find("Content/Buttons/Lower/PowerSlider").GetComponent<PowerSlider>();
        ModelHeader header = transform.Find("Content/Graphics/Upper/Model/Info").GetComponent<ModelHeader>();

        // set header
        header.SetName(my_name);
        header.SetStatus("normal");

        // set buttons        
        button1.SetButtonText("F1");
        button1.SetButtonState(true);
        button1.SetActive(true);

        button2.SetButtonText("NULL");
        button2.SetButtonState(false);
        button2.SetActive(false);

        button3.SetButtonText("NULL");
        button3.SetButtonState(false);
        button3.SetActive(false);

        button4.SetButtonText("NULL");
        button4.SetButtonState(false);
        button4.SetActive(false);

        // set value bar        
        bar1.SetName("T1");
        bar1.SetUnit("L");
        bar1.SetValue(50);
        bar1.SetMaxValue(100);
        bar1.SetMinValue(0);
        bar1.SetActive(true);

        bar2.SetName("T2");
        bar2.SetUnit("L");
        bar2.SetValue(10);
        bar2.SetMaxValue(50);
        bar2.SetMinValue(0);
        bar2.SetActive(false);

        // setting slider
        slider.SetHead("NULL");
        slider.SetUnit("NULL");
        slider.SetActive(false);

        // power slider
        power.SetActive(true);
    }
}
