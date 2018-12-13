using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class panelscript : MonoBehaviour {

    public GameObject Optionsmenu;
    int counter;
    public void showhidePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            Optionsmenu.gameObject.SetActive(false);
           
        } 
        else
        {
            Optionsmenu.gameObject.SetActive(true);

        }
       
    }
	
}
