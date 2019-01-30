using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ModelHeader : MonoBehaviour {

    public Text modelName;
    public Text modelStatus;

    // set model name
    public void SetName(string text) {
        modelName.text = "NAME: " + text;
    }

    // set model status
    public void SetStatus(string text)
    {
        modelStatus.text =text;
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (modelStatus.text == "Normal" || modelStatus.text == "normal")
        {
            modelStatus.color = new Color(0, 1, 0, 1);
        }
        else if (modelStatus.text == "Warning")
        {
            modelStatus.color = new Color(1.0f, 1.0f, .4f, 1.0f);
        }
        else if (modelStatus.text == "Off")
        {
            modelStatus.color = new Color(.8f, .8f, .8f, 0.8f);
        }
        else if (modelStatus.text == "Danger")
        {
            modelStatus.color = new Color(1, 0, 0, 1);
        }
    }
}
