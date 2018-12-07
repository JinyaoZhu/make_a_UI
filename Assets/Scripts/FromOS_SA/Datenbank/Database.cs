using UnityEngine;
using System;
using System.Collections.Generic;
using SimpleJSON;

/// <summary>
/// The hole database object. All information are stored in an object of these class. 
/// </summary>
public class Database : MonoBehaviour{
    private linkedDataRequest linkedData;
    private RestAPIRequest restAPI;
	private IstOPCUANodeIds istNodeIds;
	public IstOPCUANodeIds IstNodeIds {get { return istNodeIds;}}
	private SollOPCUANodeIds sollNodeIds;
	public SollOPCUANodeIds SollNodeIds {get { return sollNodeIds;}}
	private MappingSubmodule mappingSubmodule = new MappingSubmodule();
	private JSONNode knownDatatypes;
	private int statusOPCUAServer = 2;
	public int StatusOPCUAServer {get {return statusOPCUAServer; } set {statusOPCUAServer = value; }}
	private int statusLinkedDataServer = 2;
	public int StatusLinkedDataServer {get {return statusLinkedDataServer; } set {statusLinkedDataServer = value; }}

	private Dictionary<string, Dictionary<string, DeviceGUI>> moduleDatabase; // Database
	public Dictionary<string , Dictionary<string, DeviceGUI>> ModuleDatabase {get { return moduleDatabase;}}
	private Dictionary<string, DeviceGUI> dataDictionary; // locale object to build one module and add it later to moduleDatabase
	public Dictionary<string, DeviceGUI> DataDictionary { get { return dataDictionary; } }

	private DeviceValidator devVali = new DeviceValidator ();

	/// <summary>
	/// Awake this instance.
	/// </summary>
    void Awake() {
        //get referecences to communicator scripts for restAPI and linkedData request
        linkedData = GameObject.Find("Communicator").GetComponent<linkedDataRequest>();
        restAPI = GameObject.Find("Communicator").GetComponent<RestAPIRequest>();

		// invoke static nodeid lists
		istNodeIds = new IstOPCUANodeIds(restAPI);
		sollNodeIds = new SollOPCUANodeIds(istNodeIds.NodeIdCrawlerResult);

        //initialise dictionary
        try {
			moduleDatabase = new Dictionary<string, Dictionary<string, DeviceGUI>>();
       	} catch (Exception e) {
            Debug.Log("exception at new dataDictionary occurd: " + e.Message);
        }
    }

	/// <summary>
	/// Resets the database.
	/// </summary>
	public void ResetDatabase () {
		// reset the daterbase
		moduleDatabase = new Dictionary<string, Dictionary<string, DeviceGUI>>();
	}

	public void AddModuleToDatabase(string module) {
		dataDictionary = new Dictionary<string, DeviceGUI> (); // Initialize new sub dictionary object
		List<LinkedDataObject> listAllDevices = new List<LinkedDataObject> (); //New LinkedData object for LinkedData request
		listAllDevices = linkedData.getAllModuleProcessDevices (module); // Start init LinkedData request
		foreach (LinkedDataObject myDevice in listAllDevices) {
			dataDictionary.Add (myDevice.PlantTag, BuildDatabaseEntry (myDevice, module)); // add to local db.
		}
		moduleDatabase.Add (module, dataDictionary); // Add to main database
		restAPI.openSubscriptionOnOPCUA (module);
	}

	/// <summary>
	/// Deletes the module in the databasse.
	/// </summary>
	/// <param name="module">Module name.</param>
  	public void DeleteModuleInDatabasse (string module) {
    	// Remove the object with the key 'module'
		try {
			moduleDatabase.Remove(module);
			Debug.Log ("Remove module " + module + " from moduleDatabase");
		} catch (Exception e) {
			Debug.Log ("Remove module " + module + " failed!! Errorcode: " + e.Message);
		}
  	}

	/// <summary>
	/// Evaluate the valve value.
	/// </summary>
	/// <param name="data">Database object</param>
	public void EvaluationValve (DatabaseObject data) {
		if (data.OPCUA_data._Offen ^ data.OPCUA_data._Geschlossen) {
			if (data.OPCUA_data._Offen) {
				data.OPCUA_data.Value = "Open";
				//Debug.Log ("Open! " + localDBObject.LinkedData_data.PlantTag);
			} else {
				data.OPCUA_data.Value = "Closed";
				//Debug.Log ("Closed! " + localDBObject.LinkedData_data.PlantTag);
			}
		} else {
			data.OPCUA_data.Value = "Error";
		}
		Debug.Log ("Evaluated valve <" + data.Tag + "> value: " + data.OPCUA_data.Value);
	}
		
	/// <summary>
	/// Build new database entry based on LinkedData and OPC UA.
	/// </summary>
	/// <returns>New database object</returns>
	/// <param name="ldResponse">Linked Data response object.</param>
	/// <param name="module">Name of the module</param>
	private DeviceGUI BuildDatabaseEntry (LinkedDataObject ldResponse, String module) {
		// Validate the Device and get the new device back.
		Device local = devVali.Validator (ldResponse.PlantTag, module, istNodeIds); 
		local.LinkedData_data = ldResponse; // Safe LinkedData
		// Now OPC UA Data:
		if (istNodeIds.TagToNodeId [module].ContainsKey (ldResponse.PlantTag)) {
			// There are opc ua inforamtion
			local.InitOPCUAData (restAPI.getNode (istNodeIds.TagToNodeId [module] [ldResponse.PlantTag].Core), restAPI.getNode (sollNodeIds.SollNodeId [module] [ldResponse.PlantTag]));	
		} else if (local.DeviceType == "Tank") {
			// There are opc ua information for the submoduls
			local.InitOPCUAData (istNodeIds, sollNodeIds, restAPI);
		} else {
			local = new HandValve(local.Tag);
			local.InitOPCUAData (null, null);
		}

		try {
			return (DeviceGUI)local; // return the object		
		} catch (Exception e) {
			Debug.Log(local.LinkedData_data.PlantTag + ": TypeCast faild -> " + e.Message);
			DeviceGUI newLocal = new DeviceGUI (local.LinkedData_data.PlantTag);
			return newLocal;
		}
	}

	public DeviceGUI getDeviceByTag (string tag) {
		foreach (KeyValuePair <string, Dictionary<string, DeviceGUI>> entry in moduleDatabase) {
			foreach(KeyValuePair <string, DeviceGUI> devices in entry.Value) {
				if (tag == devices.Key) {
					return devices.Value;
					break;
				}
			}
		}

		return null;
	}
}
