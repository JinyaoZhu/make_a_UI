using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour {

    public RectTransform settingsHUD;
    public RectTransform serverContent;
    public RectTransform loginContent;
    public RectTransform generalConent;
    public RectTransform logoutjudge;
    public RectTransform judge;
    public void LoginContentClicked()
    {
        serverContent.gameObject.SetActive(false);
        logoutjudge.gameObject.SetActive(false);
        generalConent.gameObject.SetActive(false);
        judge.gameObject.SetActive(false);
        loginContent.gameObject.SetActive(true);
    }

    public void ServerContentClicked()
    {
        loginContent.gameObject.SetActive(false);
        logoutjudge.gameObject.SetActive(false);
        generalConent.gameObject.SetActive(false);
        judge.gameObject.SetActive(false);
        serverContent.gameObject.SetActive(true);

        // load server settings from Request Objects and put them into gui

    }
    public void GeneralSettingsClicked()
    {
        serverContent.gameObject.SetActive(false);
        logoutjudge.gameObject.SetActive(false);
        loginContent.gameObject.SetActive(false);
        judge.gameObject.SetActive(false);
        generalConent.gameObject.SetActive(true);
    }
    public void LogoutClicked()
    {
        serverContent.gameObject.SetActive(false);
        logoutjudge.gameObject.SetActive(true);
        loginContent.gameObject.SetActive(false);
        judge.gameObject.SetActive(false);
        generalConent.gameObject.SetActive(false);
    }
    public void judgeClicked()
    {
        //serverContent.gameObject.SetActive(false);
       // loginContent.gameObject.SetActive(false);
     //   generalConent.gameObject.SetActive(false);
      //  logoutjudge.gameObject.SetActive(false);
        judge.gameObject.SetActive(true);
        
    }
    public void neinClicked()
    {
        judge.gameObject.SetActive(false);
        
    }
        public void jaClicked()
    {
        judge.gameObject.SetActive(false);
        serverContent.gameObject.SetActive(false);
        loginContent.gameObject.SetActive(false);
        generalConent.gameObject.SetActive(false);
        logoutjudge.gameObject.SetActive(false);

        
    }
    public void logoutneinClicked()
    {
        logoutjudge.gameObject.SetActive(false);
        
    }
        public void logoutjaClicked()
    {
        logoutjudge.gameObject.SetActive(false);
        

        
    }
}
