using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Panel : MonoBehaviour {
    Animator global_animator;
    string inputtext;
    public Button btn;
    Image picture_v;
    Image picture_t;
    Image picture_p;

    // Use this for initialization
    void Start ()
    {
      
    }
	
	// Update is called once per frame
	public void changePictrues (string searchText ) {

        inputtext = searchText;

        

       
        // update picture---------------------------------------------------------------------------------------------------------

      
        switch (inputtext)
        {
             
           
            case "p":
                picture_p = this.GetComponent<Image>();
                picture_p.sprite = Resources.Load("pic/Pumpe_t", typeof(Sprite)) as Sprite;
           
                break;
            case "v":
                
                picture_v = this.GetComponent<Image>();
                picture_v.sprite = Resources.Load("pic/valve_t", typeof(Sprite)) as Sprite;
               
                break;
            case "t":
                picture_t = this.GetComponent<Image>();
                picture_t.sprite = Resources.Load("pic/tank_t", typeof(Sprite)) as Sprite;
               
                break;

        }



        // in to detail view-------------------------------------------------------------------------------------------------------
        btn = GameObject.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(delegate () {

            switch (inputtext)
            {

                case "t":
                    GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank1");
                    global_animator.SetTrigger("EnterDetailView");
                    break;

                case "p":
                    GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Tank2");
                    global_animator.SetTrigger("EnterDetailView");
                    break;

                case "v":

                    Debug.Log("按下换界面按钮");
                    GameObject.Find("DisplayArea/DetailView").GetComponent<DetailViewManager>().SetCurrentComponent("Ventile1");
                    global_animator.SetTrigger("EnterDetailView");
                    break;
            }
        });
       
	}
}
