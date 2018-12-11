using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour {

    public RectTransform ServerContent;
    public RectTransform LoginContent;
    public RectTransform GeneralConent;
    public RectTransform LogoutConfirm;
    public RectTransform OnClick;
    public void LoginContentClicked()
    {
        ServerContent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(false);
        OnClick.gameObject.SetActive(false);
        LoginContent.gameObject.SetActive(true);
    }

    public void ServerContentClicked()
    {
        LoginContent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(false);
        OnClick.gameObject.SetActive(false);
        ServerContent.gameObject.SetActive(true);

        // load server settings from Request Objects and put them into gui

    }
    public void GeneralSettingsClicked()
    {
        ServerContent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(false);
        LoginContent.gameObject.SetActive(false);
        OnClick.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(true);
    }
    public void LogoutClicked()
    {
        ServerContent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(true);
        LoginContent.gameObject.SetActive(false);
        OnClick.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(false);
    }
    public void OnClicked()
    {
        //serverContent.gameObject.SetActive(false);
       // loginContent.gameObject.SetActive(false);
     //   generalConent.gameObject.SetActive(false);
      //  logoutjudge.gameObject.SetActive(false);
        OnClick.gameObject.SetActive(true);
        
    }
    public void NoClicked()
    {
        OnClick.gameObject.SetActive(false);
        
    }
        public void YesClicked()
    {
        OnClick.gameObject.SetActive(false);
        ServerContent.gameObject.SetActive(false);
        LoginContent.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(false);

        
    }
    public void LogoutNoClicked()
    {
        LogoutConfirm.gameObject.SetActive(false);
        
    }
        public void LogoutYesClicked()
    {
        LogoutConfirm.gameObject.SetActive(false);
        

        
    }
}
