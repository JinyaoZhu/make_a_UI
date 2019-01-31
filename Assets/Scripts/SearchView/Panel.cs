using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Panel : MonoBehaviour {
    
    public Button btn;
    public Image component_img;

    private Animator global_animator;
    private string current_selected = " ";

    // Use this for initialization
    void Start ()
    {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();
        component_img.color = new Color(1,1,1,0.0f);
        // in to detail view-------------------------------------------------------------------------------------------------------
        //btn.onClick.AddListener(() => {

        //    Debug.Log("Go to detail");
        //    switch (inputtext)
        //    {
        //        case "T":
        //            GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank1");
        //            break;

        //        case "P":
        //            GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank2");
        //            break;

        //        case "V":
        //            GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Ventile1");
        //            break;
        //        default: break;
        //    }
        //    global_animator.SetTrigger("Search2Detail");
        //});

        btn.onClick.AddListener(() => {
            if (current_selected.Contains("Dummy") || current_selected.Contains("dummy"))
                return;
            GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent(current_selected);
            global_animator.SetTrigger("Search2Detail");
        });
    }

    // Update is called once per frame
    public void changePictrues(string searchText)
    {
        current_selected = searchText;
        // update picture---------------------------------------------------------------------------------------------------------
        component_img.color = new Color(1, 1, 1, 1);
        switch (searchText.Substring(0, 1).ToUpper())
        {

            case "P":
                component_img.sprite = Resources.Load("pumpe_t", typeof(Sprite)) as Sprite;
                break;
            case "V":
                component_img.sprite = Resources.Load("valve_t", typeof(Sprite)) as Sprite;
                break;
            case "T":
                component_img.sprite = Resources.Load("tank_t", typeof(Sprite)) as Sprite;
                break;
            case "M":
                component_img.sprite = Resources.Load("Mischer_1", typeof(Sprite)) as Sprite;
                break;
            default: break;
        }
     }
}
