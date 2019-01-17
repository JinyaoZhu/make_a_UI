using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewManager : MonoBehaviour {

	public Button settingButton;
	public Button searchButton;
	public Button plantViewButton;
	public Button objectButton1;
	public Button objectButton2;
	public Button objectButton3;

	Animator global_animator;

	// Use this for initialization
	void Start () {
		global_animator = GameObject.Find("DisplayArea").GetComponent<Animator> ();
		settingButton.onClick.AddListener (onSettingClick);
		searchButton.onClick.AddListener (onSearchClick);
		plantViewButton.onClick.AddListener (onPlantClick);

		objectButton1.onClick.AddListener (LoadTank1);
		objectButton2.onClick.AddListener (LoadTank2);
		objectButton3.onClick.AddListener (LoadTank2);
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

    void LoadTank1 () {
        GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank1");
        global_animator.SetTrigger ("EnterDetailView");
	}

    void LoadTank2()
    {
        GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank2");
        global_animator.SetTrigger("EnterDetailView");
    }


}