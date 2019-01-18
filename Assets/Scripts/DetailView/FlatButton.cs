using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatButton : MonoBehaviour
{

    public Button button;

    private int loop_counter = 0;
    private bool is_active = false;
    private bool button_state;

    // set button text can be call from other script
    public void SetButtonText(string text)
    {
        button.GetComponentInChildren<Text>().text = text;
    }
    // set active state, call from other
    public void SetActive(bool state)
    {
        is_active = state;
    }
    // set button state
    public void SetButtonState(bool state) {
        if (state) {
            button.GetComponent<Image>().color = new Color(.0f, .5f, 1.0f, 1.0f);
            button_state = true;
            loop_counter = 1;
        }
        else
        {
            loop_counter = 0;
            button.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1.0f);
            button_state = false;
        }
    }
    // get current button state
    public bool GetButtonState() {
        return button_state;
    }


    // Use this for initialization
    void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
        button.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            button.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1.0f);
            SetButtonText(" ");
        }
    }

    
    void OnButtonClick()
    {
        // toggle button color
        if (loop_counter == 1) // button off
        {
            loop_counter = 0;
            button.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1.0f);
            button_state = false;
        }
        else // button on
        {
            loop_counter++;
            button.GetComponent<Image>().color = new Color(.0f, .5f, 1.0f, 1.0f);
            button_state = true;
        }
    }


}
