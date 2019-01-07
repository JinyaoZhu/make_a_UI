using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchViewManager : MonoBehaviour {

	public GameObject displayArea;
	public Button returnButton;

	Animator global_animator;

	// Use this for initialization
	void Start () {
		global_animator = displayArea.GetComponent<Animator> ();
		returnButton.onClick.AddListener(onReturnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onReturnClick(){
		global_animator.SetTrigger("ExitSearchView");
	}
}