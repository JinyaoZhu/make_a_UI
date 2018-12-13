using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Close : MonoBehaviour {

public void Quit()
    {
        Debug.Log("has quit the App");
            Application.Quit();

    }
}
