using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelling a tank.
/// </summary>
public class Tank : DeviceGUI {
	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="conTag">Con tag.</param>
	public Tank(string conTag) : base(conTag) {
		submodule = true; // These class contains submodules
	}

	public override string DeviceType { get { return "Tank"; } }

	//TODO: Eventull Funktion, die einfach alle Werte aus den Submodulen abfragt und strukturiert zurückgibt
	//private Dictionary<string, Device> subDevices = new Dictionary<string, Device>();

	private PlantTopology plantTopo = new PlantTopology(); // Fucking mapping.
    
    /// <summary>
    /// Holds functions to determine your device.
    /// </summary>
    private DeviceValidator devVali = new DeviceValidator(); 

	private  Dictionary<string, TopologyObject> subDevicesTopologie = new Dictionary<string, TopologyObject>(); // Fucking mapping.

    /// <summary>
    /// Add new submodule.
    /// </summary>
    /// <param name="name">Plant tag of the device.</param>
    /// <param name="device">The submodule self.</param>
    private void addSubmodule(string name, Device device) {
		if (!subDevices.ContainsKey (name)) {
			subDevices.Add(name, device);
		}
	}

	/// <summary>
    /// Initialise the sub devices of this tank. This methode overrides the parent class methode.
    /// </summary>
    /// <param name="istIds">Mapping actual values opc ua node id's.</param>
    /// <param name="sollIds">Mapping set values opc ua node id's.</param>
    /// <param name="rest">RestAPI object.</param>
	public override void InitOPCUAData (IstOPCUANodeIds istIds, SollOPCUANodeIds sollIds, RestAPIRequest rest) {
        // Load mapping -> TODO: Has to be fixed ...
		subDevicesTopologie = plantTopo.ModuleSubmodels (linkedData_data.ModuleDiscription) [linkedData_data.PlantTag];
		Device local; // Safepoint
        // loop through dictionary
		foreach(KeyValuePair<string, TopologyObject> entry in subDevicesTopologie) {
            // Get the class of the subdevice
			local = devVali.Validator (entry.Key, linkedData_data.ModuleDiscription, istIds);
            // A sub device with set and actual value.
			if (sollIds.SollNodeId [linkedData_data.ModuleDiscription].ContainsKey (entry.Key)) {
                // init this device.
				local.InitOPCUAData (rest.getNode (istIds.TagToNodeId [linkedData_data.ModuleDiscription] [entry.Key].Core), rest.getNode (sollIds.SollNodeId [linkedData_data.ModuleDiscription] [entry.Key]));
			} else {
                // A sub device with only actual value. TODO: Neue Variable wirklich notwendig?
				Sensor locSensor = (Sensor)local; // Until now: Only sensors. In further software versions you need maybe more cases.
				// Fix it. Looks bad! Problem1: LinkedData doesen't know the position. Problem2: the validation can't see if binary sensor or something else.
				if (subDevicesTopologie [locSensor.Tag].Position == "ontop" && locSensor.GetType ().ToString () == "BinarySensor") {
                    locSensor = new LinearFluidLevelSensor(locSensor.Tag); 
				}
                // init the new sensor
				locSensor.InitOPCUAData (rest.getNode (istIds.TagToNodeId [linkedData_data.ModuleDiscription] [entry.Key].Core));
				local = locSensor;
			} 
			// Add the sub device to dictionary 
			subDevices.Add (local.Tag, local);
		}
	}

	/// <summary>
	/// Returns a list of Submoduls with given type.
	/// </summary>
	/// <returns>List of submoduls</returns>
	/// <param name="type">Device class</param>
	public List<Device> GetSubdevices (String type) {
		// TODO: Change input string -> Device.GetType
		List<Device> localSubDevices = new List<Device>();
		foreach (KeyValuePair<string, Device> entry in subDevices) {
			if (entry.Value.GetType ().ToString () == type) {
				localSubDevices.Add (entry.Value);
			}
		}
		return localSubDevices;
	}

    /// <summary>
    /// Returns a list of the actual value nodes of all sub devices.
    /// </summary>
    /// <returns>List of opc ua node</returns>
    public override List<opcuaNode> getSubscriptionInfo () {
		List<opcuaNode> nodeList = new List<opcuaNode>();
        // loop through dictionary
		foreach (KeyValuePair<string,Device> entry in subDevices) {
			nodeList.Add(entry.Value.IstWert);
		}
		return nodeList;
	}

    /// <summary>
    /// Save new value in the spezific subdevie. Overrides the function in parent class.
    /// </summary>
    /// <param name="response">The SocketIO subscription response.</param>
	public override void SetNewIstWert(SocketResponse response) {
        // loop through dictinary
		foreach (KeyValuePair <string,Device> entry in subDevices) {
			if (entry.Value.IstWert.Nodeid == response.nodeId) {
                // Found the device. Safe the new value.
				entry.Value.IstWert.setValue(response.value);
                // TODO: break?
			}
		}
	}

    /// <summary>
    /// Check if interaction request for sub device of the tank. 
    /// </summary>
    /// <param name="storedInteraction">List of 2D vectors. Holds the complete interaction.</param>
    /// <returns>Returns a opc ua node. Contains all data to write the new value on the opc ua server.</returns>
	public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
		// Check if interaction hints on subdevice. 
		opcuaNode local = new opcuaNode ("", "", "");
		foreach(KeyValuePair<string, Device> entry in subDevices) {
			local =  entry.Value.CheckInteraction (storedInteraction);
            // Hit? Than break and continue.
            if (local.Nodeid != "") break;
		}

		return local;
	}
}
