using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System; 
using SocketIO;
using System.Collections.Generic;
using SimpleJSON;

/// <summary>
/// Child class of HttpRequest. Handle all OPC UA stuff via a RestAPI.
/// </summary>
public class RestAPIRequest : HttpRequest {

	private SocketIOComponent socket;
	private IstOPCUANodeIds istNodeIds;// = new IstOPCUANodeIds();
	private List<string> actualSubscriptions = new List<string> ();

	//private string oPCUAurl =  "http://10.4.53.181:3700";
	private string oPCUAurl =  "http://10.4.53.172:3700";
	// TODO: Warum bitte zwei mal die selbe URL?
	public string OPCUAurl {get {return oPCUAurl;} set {oPCUAurl = value; }}
	//private string oPCUASubscriptionUrl = "10.4.53.181:3700";
	private string oPCUASubscriptionUrl = "10.4.53.172:3700";
	public string OPCUASubscriptionUrl {get {return oPCUASubscriptionUrl; } set {oPCUASubscriptionUrl = value; }}
	//database
	private Database database;
	private SollOPCUANodeIds sollNodeIds;// = new SollOPCUANodeIds(); 

	// Variables for RestAPI server check
	private float startTimeRest; // Starttime of the test.
	private bool restChecked; // Flag when Status is checked or not checked. 
	private float zyklusRest; // Stores the local time of the RestAPI test 
	private int statusRestAPI = 0;

	// Variables for OPC UA server check
	private float startTimeOPC;
	private bool opcChecked;
	private bool opcChecking;
	private float zyklusOPC;
	private RestAPIRequestObject testOPCConnection;
	private int statusOPCUAServer = 0;

	// matching
	private float zyklusMatching; 

	Boolean testWriteNode = false;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		zyklusRest =  Time.time; 
		zyklusOPC = Time.time;
		zyklusMatching = 0;

		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		// Test OPC UA Server connection
		// connect to database		
		database = GameObject.Find("Database").GetComponent<Database>();
		istNodeIds = database.IstNodeIds;
		sollNodeIds = database.SollNodeIds;

		// Test serverstatus for the first time
		testRest();
		//socket.On ("message", testSocket);
		socket.On("message", socketMessage);
	}

	void Update () {
		// Check RestAPI
		if ((Time.time - zyklusRest) < 5) {
			if (wwwRest.isDone) {
				if (wwwRest.bytesDownloaded == 0) {
					statusRestAPI = 2;
					statusOPCUAServer = 2; 
					restChecked = true;
					//Debug.Log ("RestAPI Server status: " + statusRestAPI);
				} else if (restChecked == false) {
					if ((Time.time - startTimeRest) < 0.12) {
						statusRestAPI = 0;
						restChecked = true;
						zyklusOPC = Time.time;
						testOPC ();
						//Debug.Log ("RestAPI Server status: " + statusRestAPI);
					} else if ((Time.time - startTimeRest) >= 0.12 && (Time.time - startTimeRest) <= 5) {
						statusRestAPI = 1;
						restChecked = true;
						zyklusOPC = Time.time;
						testOPC ();
						//Debug.Log ("RestAPI Server status: " + statusRestAPI);
					} else {
						statusRestAPI = 2;
						statusOPCUAServer = 2; 
						restChecked = true;
						//Debug.Log ("RestAPI Server status: " + statusRestAPI);
					}	
				}
			}
		} else {
			if (restChecked == false) {
				statusRestAPI = 2;
				statusOPCUAServer = 2;
				//Debug.Log ("RestAPI Server status: " + statusRestAPI);
			}
			zyklusRest = Time.time;
			testRest ();
		}
		// Check OPC UA
		if(opcChecking) {
			if ((Time.time - zyklusOPC) < 5) {
				if (testOPCConnection.NodeId != null && opcChecked == false) {
					if ((Time.time - startTimeOPC) < 0.12) {
						statusOPCUAServer = 0; 
						opcChecked = true;
						//Debug.Log ("OPC UA Server status: " + statusOPCUAServer);
					} else if ((Time.time - startTimeOPC) >= 0.12 && (Time.time - startTimeOPC) <= 5) {
						database.StatusOPCUAServer = 1; 
						opcChecked = true;
						//Debug.Log ("OPC UA Server status: " + statusOPCUAServer);
					} else {
						statusOPCUAServer = 2; 
						opcChecked = true;
						//Debug.Log ("OPC UA Server status: " + statusOPCUAServer);
					}	
				}
			} else {
				if (opcChecked == false) {
					statusOPCUAServer = 2;
					//Debug.Log ("OPC UA Server status: " + statusOPCUAServer);
				}
			}
		}
		if ((Time.time - zyklusMatching) > 5 || zyklusMatching == 0) {
			// Matching OPC UA and LinkedData
			if (statusOPCUAServer == 0 && statusRestAPI == 0) {
				// green
				setServerStatus ("opc", 0, database);
			} else if (statusOPCUAServer > statusRestAPI) {
				setServerStatus ("opc", statusOPCUAServer, database);
			} else if (statusRestAPI == 1) {
				// yellow
				setServerStatus ("opc", 1, database);
			} else if (statusRestAPI == 2) {
				// red
				setServerStatus ("opc", 2, database);
			}
			zyklusMatching = Time.time; 
		}
	}

	/// <summary>
	/// Raises the application quit event.
	/// </summary>
	void OnApplicationQuit() {
		// Close all subscriptions
		Debug.Log("Closing all subscriptions!");
		socket.Emit("unsubscribe");
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		// Invoke checking RestAPI & OPCUA server status
		StartCoroutine("CheckOPCUAServerStatus");
	}
		
	/// <summary>
	/// Checks the OPCUA server and RestAPI status every 10 seconds.
	/// </summary>
	/// <returns></returns>
	public IEnumerator CheckOPCUAServerStatus() {
		// check allways
		while (true) {
			// wait for 10 sec. ...
			yield return new WaitForSeconds(5);
			// .. and check server status
			/*if(database.ModuleDatabaseTest.ContainsKey("Reaktor")) {
				if (database.ModuleDatabaseTest ["Reaktor"].ContainsKey ("B301")) {
					writeValue("Reaktor", "R301", (!testWriteNode).ToString() );
					testWriteNode = !testWriteNode;
				}
			}*/
			testRest();
		}
	}

	/// <summary>
	/// Represents the getNode RestAPI function. Get all information of a single node.
	/// </summary>
	/// <returns>RestAPIRequestObject to store in database.</returns>
	/// <param name="nodeid">The nodeid of the single node</param>
	public RestAPIRequestObject getNode(string nodeid) {
		RestAPIRequestObject localRestAPI = new RestAPIRequestObject();
		try {
			if (nodeid == "hardcodet") {
				localRestAPI = JsonUtility.FromJson<RestAPIRequestObject> (getHTTPReq (buildGetNodeUrl("ns=1;i=1933")));
			} else {
				// Get single node from OPC UA Server
				localRestAPI = JsonUtility.FromJson<RestAPIRequestObject>(getHTTPReq(buildGetNodeUrl(nodeid)));
			}
			return localRestAPI;  // Return response
		} catch (Exception e) {
			Debug.Log ("Error @ getNode! Errortext: " + e.Message);
			return localRestAPI;
		}
	}

	/// <summary>
	/// Gets the value/data type of the node.
	/// </summary>
	/// <returns>The data type.</returns>
	/// <param name="nodeid">The nodeid of the value/data type</param>
	public RestAPIDataValueObject getDataType (string nodeid) {
		RestAPIDataValueObject localDataType = new RestAPIDataValueObject ();
		localDataType = JsonUtility.FromJson<RestAPIDataValueObject>(getHTTPReq(buildGetNodeUrl(nodeid)));
		return localDataType; 
	}

	//TODO: DELETE
	private void testWriteValue () {
		string nodeid = "ns=1;i=1940";
		string dataType = "Boolean";
		string value = "false"; 
		Dictionary<string, string> data = new Dictionary <string, string> ();
		// Build http post 
		string url = buildWriteValueUrl ();
		data.Add ("nodeID", nodeid);
		data.Add ("datatype", dataType);
		data.Add ("value", value);
		// Send http post request
		postHTTPReq (url, data);
		Debug.Log ("Write Value ist raus!");
	}
		
	/// <summary>
	/// Write new value on OPC UA
	/// </summary>
	/// <param name="module">Module</param>
	/// <param name="tag">Devicetag in module</param>
	/// <param name="value">New value</param>
	public void writeValue (string module, string tag, string value) {
		string nodeid = null;
		string dataType = null;
		string newValue = value;
		string url = buildWriteValueUrl ();
		Dictionary<string, string> data = new Dictionary <string, string> ();
		if (database.ModuleDatabase [module].ContainsKey (tag)) {
			nodeid = database.ModuleDatabase [module] [tag].SollWert.Nodeid;
			dataType = getDataType(database.ModuleDatabase [module] [tag].SollWert.DataType).DisplayName.text;
		} else { // submodul
			foreach(KeyValuePair<string, DeviceGUI> entry in database.ModuleDatabase[module]) {
				if (entry.Value.Submodule) {
					if (entry.Value.SubDevices.ContainsKey(tag)) {
						nodeid = entry.Value.SubDevices [tag].SollWert.Nodeid;
						dataType = getDataType(entry.Value.SubDevices [tag].SollWert.DataType).DisplayName.text;
					}
				}
			}
		}

		data.Add ("nodeId", nodeid);
		data.Add ("datatype", dataType);
		data.Add ("value", newValue);

		//Debug.Log ("NodeId: " + nodeid);
		//Debug.Log ("DataType: " + dataType);
		//Debug.Log ("value: " + newValue);

		if (nodeid != null && dataType != null) {
			postHTTPReq (url, data);
		} else {
			Debug.Log ("Error @ writeValue!"); 
		}

			/*if (database.ModuleDatabaseTest [istNodeIds.NodeIdToTag [res.nodeId].Module].ContainsKey (istNodeIds.NodeIdToTag [res.nodeId].Core)) {
				database.ModuleDatabaseTest [istNodeIds.NodeIdToTag [res.nodeId].Module][istNodeIds.NodeIdToTag [res.nodeId].Core].setNewIstWert (res.value);
			} else { // possible submoduls
				foreach(KeyValuePair<string, DeviceGUI> entry in database.ModuleDatabaseTest[istNodeIds.NodeIdToTag [res.nodeId].Module]) {
					if (entry.Value.Submodule) {
						entry.Value.setNewIstWert (res);
					}
				}
			}*/
		 

		/*if (database.ModuleDatabaseTest [module][tag].SollWert.DataType != null) {
			string nodeid = sollNodeIds.SollNodeId[module][tag];
			string dataType = database.ModuleDatabase [module][tag].OPCUA_data.DataType;
			Dictionary<string, string> data = new Dictionary <string, string> ();
			// Build http post parameter
			string url = buildWriteValueUrl ();
			data.Add ("nodeID", nodeid);
			data.Add ("datatype", dataType);
			data.Add ("value", value);
			// Send http post request
			postHTTPReq (url, data);
		} else {
			Debug.Log ("Error @ writeValue! No data type defined! Cannot write value!"); 
		}*/
	}

	public void writeValueNew (string nodeid ,string datatype ,string value) {

		Dictionary<string, string> data = new Dictionary <string, string> ();
		string url = buildWriteValueUrl ();

		data.Add ("nodeId", nodeid);
		data.Add ("datatype", getDataType(datatype).DisplayName.text);
		data.Add ("value", value);

		//Debug.Log ("NodeId: " + nodeid);
		//Debug.Log ("DataType: " + dataType);
		//Debug.Log ("value: " + newValue);

		//if (nodeid != null && dataType != null) {
			postHTTPReq (url, data);
		//} else {
		//	Debug.Log ("Error @ writeValue!"); 
		//}
	}

	// ################################################################################################
	// ################################################################################################
	// SUBSCRIPTION - ANFANG
	// ################################################################################################
	// ################################################################################################

	/// <summary>
	/// Start subscriptions on OPC UA Server.
	/// </summary>
	/// <param name="module">Module name</param>
	public void openSubscriptionOnOPCUA (string module) {
		// Start subscription on sockets
		foreach(KeyValuePair<string,DeviceGUI> entry in database.ModuleDatabase[module]) {
			List<opcuaNode> nodes = entry.Value.getSubscriptionInfo (); 
			foreach (opcuaNode nodeEntry in nodes) {
				try {
					if(nodeEntry.Nodeid != null) {
						//socket.Emit("subscribe",istNodeIds.Socket["Reaktor"]);
						//Debug.Log("Subscribe to: " + nodeEntry.Nodeid);
						//JSONNode loc = JSON.Parse("{nodeid: " + nodeEntry.Nodeid + "}");
						Dictionary <string, string> test = new Dictionary<string, string>();
						test.Add("nodeid",nodeEntry.Nodeid );
						JSONObject loc = new JSONObject(test);
						socket.Emit("subscribe",loc);	
					}
				} catch (Exception e) {
					Debug.Log ("Error @ openSubscriptionOnOPCUA! Errormessage: " + e.Message);
				}
			}
		}

		actualSubscriptions.Add(module);
		Debug.Log ("Subscriptions done! Module: " + module);

		/*try {
			//socket.Emit("subscribe",istNodeIds.Socket["Reaktor"]);
			socket.Emit("subscribe",istNodeIds.Socket[module]);
			actualSubscriptions.Add(module);
			Debug.Log ("Subscriptions done! Module: " + module);
		} catch (Exception e) {
			Debug.Log ("Error @ openSubscriptionOnOPCUA! Errormessage: " + e.Message);
			//socket.Emit("subscribe",istNodeIds.Socket["Test"]);
		}*/
	}

	/// <summary>
	/// Closes subscriptions on OPC UA Server
	/// </summary>
	/// <param name="module">Module name.</param>
	public void closeSubscriptionOPCUA (string module) {
		// Close all subscriptions
		try {
			socket.Emit("unsubscribe");
			// Remove module from actual subscriptions list
			actualSubscriptions.Remove(module);
			// reestablisch rest subscriptions 
			foreach (string restModulesInFokus in actualSubscriptions) {
				openSubscriptionOnOPCUA (restModulesInFokus);
			}	
		} catch (Exception e) {
			Debug.Log ("Error @ closeSubscriptionOPCUA! Errormessage: " + e.Message);
		}
	}

	/// <summary>
	/// Method called by socket.On("message") Event
	/// </summary>
	/// <param name="e"> SocketIOEvent </param>
	private void socketMessage (SocketIOEvent e) {
		SocketResponse res = new SocketResponse ();
		// Serialize socket answer to object
		res = JsonUtility.FromJson<SocketResponse> (e.data.ToString());
		//Debug.Log("Get Socket message: " + res.nodeId + "-" + res.value);
		// Save value in databse
		if (istNodeIds.NodeIdToTag.ContainsKey (res.nodeId)) {
			if (database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module].ContainsKey (istNodeIds.NodeIdToTag [res.nodeId].Core)) {
				database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module][istNodeIds.NodeIdToTag [res.nodeId].Core].SetNewIstWert (res.value);
			} else { // possible submoduls
				foreach(KeyValuePair<string, DeviceGUI> entry in database.ModuleDatabase[istNodeIds.NodeIdToTag [res.nodeId].Module]) {
					if (entry.Value.Submodule) {
						entry.Value.SetNewIstWert (res);
					}
				}
			}
		} 

		/*if (istNodeIds.NodeIdToTag [res.nodeId].Core.EndsWith ("_O") || istNodeIds.NodeIdToTag [res.nodeId].Core.EndsWith ("_G")) {
			// Special case valve
			string tag = istNodeIds.NodeIdToTag [res.nodeId].Core.Substring(0, (istNodeIds.NodeIdToTag [res.nodeId].Core.Length - 2));

			// Storre the new value
			if (istNodeIds.NodeIdToTag [res.nodeId].Core.EndsWith ("_O")) {
				database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module] [tag].OPCUA_data._Offen = database.stringToBool(res.value);
			} else {
				database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module] [tag].OPCUA_data._Geschlossen = database.stringToBool(res.value);
			}
			// Evaluate the new valve value
			database.evaluationValve(database.ModuleDatabase[istNodeIds.NodeIdToTag[res.nodeId].Module][tag]);

			Debug.Log("Special Case Ventile! New value stored! " + database.ModuleDatabase[istNodeIds.NodeIdToTag[res.nodeId].Module][tag].OPCUA_data.Value);
		} else {
			if (istNodeIds.NodeIdToTag[res.nodeId].Core.StartsWith("V")) {
				if (database.stringToBool(res.value)) {
					database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module] [tag].OPCUA_data.Value = "Open";
				} else {
					database.ModuleDatabase [istNodeIds.NodeIdToTag [res.nodeId].Module] [tag].OPCUA_data.Value = "Closed";
				}
			}
			// Normal case. Just write value to Database
			database.ModuleDatabase [istNodeIds.NodeIdToTag[res.nodeId].Module][istNodeIds.NodeIdToTag[res.nodeId].Core].OPCUA_data.Value = res.value;
			Debug.Log ("Get new Value from subscriptions. Write new value >" + res.value + "< to >" + database.ModuleDatabase[istNodeIds.NodeIdToTag[res.nodeId].Module][istNodeIds.NodeIdToTag [res.nodeId].Core].Tag + "<");
		}*/
	} 

	// ################################################################################################
	// ################################################################################################
	// SUBSCRIPTION - ENDE
	// ################################################################################################
	// ################################################################################################

	/// <summary>
	/// Builds the url to create a subscription on OPC UA Server.
	/// </summary>
	/// <returns>The url.</returns>
	private string buildCreateSubscriptionRequest () {
		return oPCUAurl + "/base/create_subscription";
	}

	/// <summary>
	/// Builds the url to close a subscription on OPC UA Server.
	/// </summary>
	/// <returns>The url.</returns>
	private string buildCloseSubscriptionRequest () {
		return oPCUAurl + "/base/close_subscription";
	}

	/// <summary>
	/// Builds the url to get a node from OPC UA Server.
	/// </summary>
	/// <returns>The url.</returns>
	/// <param name="nodeid">The nodeid of the node.</param>
	private string buildGetNodeUrl ( string nodeid) {
		return  oPCUAurl + "/base/getNode?nodeId=%22" + nodeid + "%22";
	}

	/// <summary>
	/// Builds the url to write a value on OPC UA.
	/// </summary>
	/// <returns>The write value URL.</returns>
	private string buildWriteValueUrl () {
	//private string buildWriteValueUrl (string nodeid, string dataType, string value) {
		//return oPCUAurl + "/base/writeValue?nodeID=%22" + nodeid + "%22datatype=%22" + dataType + "%22value=%22" + value + "%22";
		return oPCUAurl + "/base/writeValue"; 
	}

	private void testRest() {
		startTimeRest = Time.time;
		restChecked = false;
		checkRestServerConnection(buildGetNodeUrl("ns=0;i=84"));
	}

	private void testOPC () {
		opcChecked = false;
		opcChecking = true; 
		startTimeOPC = Time.time;
		testOPCConnection= new RestAPIRequestObject();
		testOPCConnection = getNode("ns=0;i=84");
	}

	// ################################################################################################
	// ################################################################################################
	// StuA: cheidelbach - START
	// ################################################################################################
	// ################################################################################################

	public JSONNode nodeIdCrawler () {
		// input : string browsePath
		// http://localhost:3700/base/getSubNodesBrowsePath?browsePath=%2FObjects%2F1%3ASimulators%2F2%3AApplication%2F2%3AGVL_Visu
		// Mit allen sonderzeichen ...
		//RestAPIRequestObject_nodeIdCrawler localCrawler = new RestAPIRequestObject_nodeIdCrawler();
		// TODO: Localhost anpassen
		var localCrawler = JSON.Parse(getHTTPReq(oPCUAurl + "/base/getSubNodesBrowsePath?browsePath=%2FObjects%2F1%3ASimulators%2F2%3AApplication%2F2%3AGVL_Visu&referenceType=Organizes"));

		//localCrawler = JsonUtility.FromJson<RestAPIRequestObject_nodeIdCrawler>(getHTTPReq("http://localhost:3700/base/getSubDict?browsePath=%2FObjects%2F1%3ASimulators%2F2%3AApplication%2F2%3AGVL_Visu"));
		//Debug.Log(localCrawler["Mischer"]["data"]);
		return localCrawler;
	}

	public JSONNode OPCUABaseDataTypeCrawler() {
		var localCrawler = JSON.Parse(getHTTPReq(oPCUAurl + "/base/getSubNodesBrowsePath?browsePath=%2FTypes%2FDataTypes%2FBaseDataType&referenceType=HasSubtype"));
	try {
		return localCrawler["Unknown Module"]["data"];
		} catch (Exception e) {
			Debug.Log ("No BaseDataTypes!");
			return null;
		} 
	}

	// ################################################################################################
	// ################################################################################################
	// StuA: cheidelbach - END
	// ################################################################################################
	// ################################################################################################

}
