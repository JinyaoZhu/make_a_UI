using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Modeling a device in process control.
/// </summary>
public class Device {
	protected string tag; // Module Tag
    public string Tag { get { return tag; } set { tag = value; } }
    // ###################################################################################################
	protected LinkedDataObject linkedData_data = new LinkedDataObject(); // LinkedData object
    public LinkedDataObject LinkedData_data { get { return linkedData_data; } set { linkedData_data = value; } }
    // ###################################################################################################

    public virtual string DeviceType { get { return "Device"; }}
	protected bool submodule;
	public bool Submodule {get {return submodule;} set {submodule = value;}}

	protected opcuaNode istWert;
	public opcuaNode IstWert {get {return istWert; }}
	protected opcuaNode sollWert;
	public opcuaNode SollWert {get {return sollWert; }}

	protected int writeValue = 0;
	public int WriteValue {get {return writeValue; } set {writeValue = value; }}
	protected bool valueWritten = false;
	public bool ValueWritten {get {return valueWritten; } set {valueWritten = value; }}

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Plant tag of the device. Have to be unique on module level.</param>
    public Device(string conTag) {
        tag = conTag;
        submodule = false;
    }

	/// <summary>
	/// Init the opc ua data. Get all information. Soll- and IstWert. 
	/// </summary>
	/// <param name="result">Result.</param>
	public virtual void InitOPCUAData (RestAPIRequestObject resultIst, RestAPIRequestObject resultSoll) {
		safeInformation (resultIst, resultSoll);
	}

	/// <summary>
	/// For submodules.
	/// </summary>
	public virtual void InitOPCUAData (IstOPCUANodeIds istIds, SollOPCUANodeIds sollIds, RestAPIRequest rest) {
		
	}

	/// <summary>
	/// For sensors.
	/// </summary>
	/// <param name="resultIst">Result ist.</param>
	public virtual void InitOPCUAData (RestAPIRequestObject resultIst) {
		istWert = new opcuaNode (resultIst.NodeId, resultIst.Value, resultIst.DataType);
	}

	/// <summary>
	/// Same like initOPCUAData, but no double code in inharitaded classes.
	/// </summary>
	/// <param name="resultIst">Result ist.</param>
	/// <param name="resultSoll">Result soll.</param>
	protected void safeInformation (RestAPIRequestObject resultIst, RestAPIRequestObject resultSoll) {
		if (resultIst == null) {
			istWert = new opcuaNode (null, null, null);
		} else istWert = new opcuaNode (resultIst.NodeId, resultIst.Value, resultIst.DataType);
		if (resultIst == null) {
			sollWert = new opcuaNode (null, null, null);
		} else sollWert = new opcuaNode (resultSoll.NodeId, resultSoll.Value, resultSoll.DataType);	
	}

    /// <summary>
    /// Returns the istWert opc ua node of the device in a list. Other Devices may override this function.    
    /// </summary>
    /// <returns></returns>
	public virtual List<opcuaNode> getSubscriptionInfo () {
		List<opcuaNode> nodeList = new List<opcuaNode>();
		nodeList.Add (istWert);
		return nodeList;
	}

    /// <summary>
    /// Set the value of the IstWert variable.
    /// </summary>
    /// <param name="newValue"> New value </param>
	public virtual void SetNewIstWert(string newValue) {
		istWert.setValue(newValue);
		// TODO: Theortisch auf Zeitstempel vergleiche :S
		if (writeValue >= 1) {
			valueWritten = true;
		}
	}

    /// <summary>
    /// Save the new subscriptions value. New value brought by opc ua subscriptions via SocketIO.   
    /// </summary>
    /// <param name="response">Subscription response</param>
	public virtual void SetNewIstWert(SocketResponse response) {

	}

    /// <summary>
    /// Checks if interaction matches to device. May override in child classes.  
    /// </summary>
    /// <param name="storedInteraction">List of 2D-Vectors</param>
    /// <returns></returns>
	public virtual opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
		// TODO: Fix that shit
		return new opcuaNode("", "", "");
	}
}