using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingViewManager : MonoBehaviour {

    public Button loginButton;
    public Button serverButton;
    public Button generalButton;
    public Button logoutButton;
    public Button legalButton;

    public Button returnButton;
    public Button exitYButton;
    public Button exitNButton;
    public Button logoutYButton;
    public Button logoutNButton;

    public RectTransform ServerContent;
    public RectTransform LoginContent;
    public RectTransform GeneralConent;
    public RectTransform LogoutConfirm;
    public RectTransform ExitConfirm;
    public RectTransform LegalContent;



    Animator global_animator;

    void Start () {
        global_animator = GameObject.Find("DisplayArea").GetComponent<Animator>();

        loginButton.onClick.AddListener(loginContentClicked);
        serverButton.onClick.AddListener(serverContentClicked);
        generalButton.onClick.AddListener(generalSettingsClicked);
        logoutButton.onClick.AddListener(logoutClicked);
        legalButton.onClick.AddListener(legalClicked);

        returnButton.onClick.AddListener (onReturnClick);
        exitYButton.onClick.AddListener (exitYesClicked);
        exitNButton.onClick.AddListener (exitNoClicked);
        logoutYButton.onClick.AddListener (logoutYesClicked);
        logoutNButton.onClick.AddListener (logoutNoClicked);
    }

     void loginContentClicked () {
        ServerContent.gameObject.SetActive (false);
        LogoutConfirm.gameObject.SetActive (false);
        GeneralConent.gameObject.SetActive (false);
        ExitConfirm.gameObject.SetActive (false);
        LegalContent.gameObject.SetActive(false);
        LoginContent.gameObject.SetActive (true);
    }

     void serverContentClicked () {
        LoginContent.gameObject.SetActive (false);
        LogoutConfirm.gameObject.SetActive (false);
        GeneralConent.gameObject.SetActive (false);
        ExitConfirm.gameObject.SetActive (false);
        LegalContent.gameObject.SetActive(false);
        ServerContent.gameObject.SetActive (true);

        // load server settings from Request Objects and put them into gui

    }

     void generalSettingsClicked () {
        ServerContent.gameObject.SetActive (false);
        LogoutConfirm.gameObject.SetActive (false);
        LoginContent.gameObject.SetActive (false);
        ExitConfirm.gameObject.SetActive (false);
        LegalContent.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive (true);
    }

     void logoutClicked () {
        ServerContent.gameObject.SetActive (false);
        LogoutConfirm.gameObject.SetActive (true);
        LoginContent.gameObject.SetActive (false);
        ExitConfirm.gameObject.SetActive (false);
        GeneralConent.gameObject.SetActive (false);
        LegalContent.gameObject.SetActive(false);
    }
    // void feedbackClicked()
    // {
    //     ServerContent.gameObject.SetActive(false);
    //     LogoutConfirm.gameObject.SetActive(false);
    //     LoginContent.gameObject.SetActive(false);
    //     ExitConfirm.gameObject.SetActive(false);
    //     GeneralConent.gameObject.SetActive(false);
    //     LegalContent.gameObject.SetActive(false);
    // }
    void legalClicked()
    {
        ServerContent.gameObject.SetActive(false);
        LogoutConfirm.gameObject.SetActive(false);
        LoginContent.gameObject.SetActive(false);
        ExitConfirm.gameObject.SetActive(false);
        GeneralConent.gameObject.SetActive(false);
        LegalContent.gameObject.SetActive(true);
    }
    void exitNoClicked () {
        ExitConfirm.gameObject.SetActive (false);
    }

     void exitYesClicked () {
        ExitConfirm.gameObject.SetActive (false);
        ServerContent.gameObject.SetActive (false);
        LoginContent.gameObject.SetActive (false);
        GeneralConent.gameObject.SetActive (false);
        LogoutConfirm.gameObject.SetActive (false);
        LegalContent.gameObject.SetActive(false);

        global_animator.SetTrigger ("ExitSettingView");
    }

     void logoutNoClicked () {
        LogoutConfirm.gameObject.SetActive (false);
    }

     void logoutYesClicked () {
        LogoutConfirm.gameObject.SetActive (false);
        // TODO:
        // now is equal to exit
        exitYesClicked();
    }

    void onReturnClick () {
        ExitConfirm.gameObject.SetActive (true);
    }
}