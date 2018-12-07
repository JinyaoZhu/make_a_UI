using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkServerStatus : MonoBehaviour {

    public Transform extrasGUI;
    private Ping opcUAPing;
    private RestAPIRequest restAPIRequest;
    private Database database;
    private string opcUAURL;
    private char delimiter = ':';
    private string opcUA_IP;
    private float slowTime = 100; // millisec to wait for a slow server
    private float pingIntervalStart = 0; // sec, time the runnning interval started
    private float pingInterval = 5; // sec, interval for pinging servers
    private int opcUAPingStatus = 2;  // 0 if OK 1 if slow, 2 if not OK

    private Color32 green = new Color32(34, 173, 54,100);
    private Color32 yellow = new Color32(255, 255, 0,100);
    private Color32 red = new Color32(255, 0, 0, 100);

    // Use this for initialization
    void Start() {
        database = GameObject.Find("Database").GetComponent<Database>();
        restAPIRequest = GameObject.Find("Communicator").GetComponent<RestAPIRequest>();
        //linkedDataRequest = GameObject.Find("Communicator").GetComponent<linkedDataRequest>();
        opcUAURL = restAPIRequest.OPCUAurl;
        string[] substrings;
        substrings = opcUAURL.Split(delimiter);
        opcUA_IP = substrings[1].Substring(2);
        // create ping object to OPC UA Server
        Debug.Log("checkServer: ping started for " + opcUA_IP);
        opcUAPing = new Ping(opcUA_IP);
        pingIntervalStart = Time.time;
    }

    // Update is called once per frame
    void Update() {

        // check status of OPC UA server

        // wait some seconds after ping is done to start a new one
        if (Time.time - pingIntervalStart > pingInterval) {
            if (opcUAPing.isDone && opcUAPing.time < slowTime && opcUAPing.time != -1) {
                //Debug.Log("checkServer: connection to OPC UA server is fine Zeit=" + opcUAPing.time);
                opcUAPingStatus = 0;
            } else if (opcUAPing.isDone && opcUAPing.time > slowTime) {
                //Debug.Log("checkServer: OPC UA server has bad ping Zeit=" + opcUAPing.time);
                opcUAPingStatus = 1;
            } else if (opcUAPing.isDone && opcUAPing.time == -1) {
                //Debug.Log("checkServer: OPC UA server is unreachable Zeit=" + opcUAPing.time);
                opcUAPingStatus = 2;
            } else {
                //Debug.Log("checkServer: OPC UA server is unreachable: Zeit="+opcUAPing.time);
                opcUAPingStatus = 2;
            }

            // create new ping object to OPC UA Server
            opcUAPing.DestroyPing();
            opcUAPing = new Ping(opcUA_IP);
            pingIntervalStart = Time.time;
			/*
            if (opcUAPingStatus == 0 && database.StatusOPCUAServer == 0) {
                extrasGUI.Find("StatusSign2").GetComponent<Image>().color = new Color(0, 255, 0);
            } else if ((opcUAPingStatus == 1 || opcUAPingStatus == 1) && database.StatusOPCUAServer == 1) {
                extrasGUI.Find("StatusSign2").GetComponent<Image>().color = new Color(255, 255, 0);
            } else if (opcUAPingStatus == 2 || database.StatusOPCUAServer == 2) {
                extrasGUI.Find("StatusSign2").GetComponent<Image>().color = new Color(255, 0, 0);
            }*/

			if (database.StatusOPCUAServer == 0) {
				extrasGUI.Find("StatusSign2").GetComponent<Image>().color = green;
				//Debug.Log ("OPC UA: " + database.StatusOPCUAServer);
			} else if(database.StatusOPCUAServer == 1) {
				extrasGUI.Find("StatusSign2").GetComponent<Image>().color = yellow;
				//Debug.Log ("OPC UA: " + database.StatusOPCUAServer);
			} else {
				extrasGUI.Find("StatusSign2").GetComponent<Image>().color = red;
				//Debug.Log ("OPC UA: " + database.StatusOPCUAServer);
			}

            // check status of LinkedData server

            if (database.StatusLinkedDataServer == 0) {
                extrasGUI.Find("StatusSign1").GetComponent<Image>().color = green;
            } else if(database.StatusLinkedDataServer == 1) {
                extrasGUI.Find("StatusSign1").GetComponent<Image>().color = yellow;
            } else {
                extrasGUI.Find("StatusSign1").GetComponent<Image>().color = red;
            }
        }
    }
}
