using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButton3D : MonoBehaviour {

    GameObject displayArea;
    public Button button;

    Animator global_animator;

    // Use this for initialization
    void Start () {
        displayArea = GameObject.Find("DisplayArea");
        global_animator = displayArea.GetComponent<Animator> ();
        button.onClick.AddListener(goDetail);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void goDetail() {
        global_animator.SetTrigger("EnterDetailView");
    }
}
