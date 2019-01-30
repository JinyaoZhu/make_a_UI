using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewManager : MonoBehaviour {

	public Button settingButton;
	public Button searchButton;
	public Button plantViewButton;

    Animator global_animator;

    public void DemoDisableObjectButton() {
        GameObject.Find("DisplayArea/MainView/MainArea/P001").SetActive(false);
        GameObject.Find("DisplayArea/MainView/MainArea/T001").SetActive(false);
        GameObject.Find("DisplayArea/MainView/MainArea/T002").SetActive(false);
        GameObject.Find("DisplayArea/MainView/MainArea/V001").SetActive(false);
        GameObject.Find("DisplayArea/MainView/MainArea/V002").SetActive(false);
        GameObject.Find("DisplayArea/MainView/MainArea/M001").SetActive(false);
    }

    public void DemoEnableObjectButton()
    {
        GameObject.Find("DisplayArea/MainView/MainArea/P001").SetActive(true);
        GameObject.Find("DisplayArea/MainView/MainArea/T001").SetActive(true);
        GameObject.Find("DisplayArea/MainView/MainArea/T002").SetActive(true);
        GameObject.Find("DisplayArea/MainView/MainArea/V001").SetActive(true);
        GameObject.Find("DisplayArea/MainView/MainArea/V002").SetActive(true);
        GameObject.Find("DisplayArea/MainView/MainArea/M001").SetActive(true);
    }

    // Use this for initialization
    void Start () {
		global_animator = GameObject.Find("DisplayArea").GetComponent<Animator> ();
		settingButton.onClick.AddListener (onSettingClick);
		searchButton.onClick.AddListener (onSearchClick);
		plantViewButton.onClick.AddListener (onPlantClick);
    }

	// Update is called once per frame
	void Update () {

	}

	void onSettingClick () {
		global_animator.SetTrigger ("EnterSettingView");
	}

	void onSearchClick () {
		global_animator.SetTrigger ("EnterSearchView");
	}

    void onPlantClick()
    {
        global_animator.SetTrigger("EnterPlantView");
    }

}