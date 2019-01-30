using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlantViewManager : MonoBehaviour {

	public Button returnButton;

    public Text time;
    public GameObject V001;
    public GameObject V002;
    public GameObject T001;
    public GameObject T002;
    public GameObject M001;
    public GameObject P001;

    private Server server;
    private DetailViewManager detailViewmanager;

    Animator global_animator;

	// Use this for initialization
	void Start () {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        server = GameObject.Find("DisplayArea/Database/Server").GetComponent<Server>();
        returnButton.onClick.AddListener(onReturnClick);

        detailViewmanager = GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>();

        V001.GetComponent<Button>().onClick.AddListener(()=> {
            detailViewmanager.SetCurrentComponent("V001");
            global_animator.SetTrigger("Plant2Detail");
        });

        V002.GetComponent<Button>().onClick.AddListener(() => {
            detailViewmanager.SetCurrentComponent("V002");
            global_animator.SetTrigger("Plant2Detail");
        });

        T001.GetComponent<Button>().onClick.AddListener(() => {
            detailViewmanager.SetCurrentComponent("T001");
            global_animator.SetTrigger("Plant2Detail");
        });

        T002.GetComponent<Button>().onClick.AddListener(() => {
            detailViewmanager.SetCurrentComponent("T002");
            global_animator.SetTrigger("Plant2Detail");
        });

        M001.GetComponent<Button>().onClick.AddListener(() => {
            detailViewmanager.SetCurrentComponent("M001");
            global_animator.SetTrigger("Plant2Detail");
        });

        P001.GetComponent<Button>().onClick.AddListener(() => {
            detailViewmanager.SetCurrentComponent("P001");
            global_animator.SetTrigger("Plant2Detail");
        });

    }
	
	// Update is called once per frame
	void Update () {

        setStatusColor(server.Tank1, T001);
        setStatusColor(server.Tank2, T002);
        setStatusColor(server.Ventile1, V001);
        setStatusColor(server.Ventile2, V002);
        setStatusColor(server.Mischer1, M001);
        setStatusColor(server.Pumpe1, P001);

        time.text = System.DateTime.UtcNow.ToString("HH:mm:ss, dd MMMM, yyyy")+"  ";
    }

	void onReturnClick(){
		global_animator.SetTrigger("ExitPlantView");
	}

    private void setStatusColor(Component cmp, GameObject icon) {
        if (cmp.status == "Normal")
        {
            icon.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        else if (cmp.status == "Off")
        {
            icon.GetComponent<Image>().color = new Color(.8f, .8f, .8f, 0.8f);
        }
        else if (cmp.status == "Warning") {
            icon.GetComponent<Image>().color = new Color(1.0f, 1.0f, .4f, 1.0f);
        }
        else if (cmp.status == "Danger") { 
            icon.GetComponent<Image>().color = new Color(1, 0, 0, 1);
        }
    }
}