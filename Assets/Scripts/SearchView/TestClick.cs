using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestClick : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        Click();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        Debug.Log("Test Click");
    }
}
