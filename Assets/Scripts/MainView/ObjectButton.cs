using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectButton : MonoBehaviour {
    public Text ID;
    public Text status;
    public Image warning;

    private Animator global_animator;
    private Server server;
    private Component component;

    // Use this for initialization
    void Start () {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        server = GameObject.Find("DisplayArea/Database/Server").GetComponent<Server>();
        ID.text = name;
        this.GetComponent<Button>().onClick.AddListener(()=>{
            GameObject.Find("Canvas/DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent(name);
            global_animator.SetTrigger("EnterDetailView");
        });
    }
	
	// Update is called once per frame
	void Update () {
        string component_status = server.GetComponentById(name).status;
        if (component_status == "Normal")
        {
            this.GetComponent<Image>().color = new Color(0, 1, 0, 0.8f);
            warning.color = new Color(1, 1, 1, 0.0f);
        }
        else if (component_status == "Warning")
        {
            this.GetComponent<Image>().color = new Color(1, 1, 0.4f, 0.8f);
            warning.color = new Color(1, 1, 1, 1.0f);
        }
        else if (component_status == "Off") {
            this.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.6f);
            warning.color = new Color(1, 1, 1, 0.0f);
        }
        else if(component_status == "Danger")
        {
            this.GetComponent<Image>().color = new Color(0.8f, 0, 0, 0.8f);
            warning.color = new Color(1, 1, 1, 1.0f);
        }
        status.text = "Status: " + component_status;
    } 
}
