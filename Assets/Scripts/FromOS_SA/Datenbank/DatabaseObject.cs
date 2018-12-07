using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A single database object. Contains all information. Filled by linkedData, single OPC UA request and subscriptions on OPC UA Server.
/// </summary>
public class DatabaseObject{

	private string tag; // Module Tag
    public string Tag { get { return tag;} set { tag = value; } }
	// ###################################################################################################
	private RestAPIRequestObject oPCUA_data = new RestAPIRequestObject(); // OPC UA data object
    public RestAPIRequestObject OPCUA_data { get { return oPCUA_data; } set { oPCUA_data = value; } }
	private LinkedDataObject linkedData_data = new LinkedDataObject(); // LinkedData object
    public LinkedDataObject LinkedData_data { get { return linkedData_data; } set { linkedData_data = value; } }
	// ###################################################################################################
	private string guiTemplateType; // store gui template type
    public string GuiTemplateType { get { return guiTemplateType; } set { guiTemplateType = value; } }
	private bool currentlyDetected; // objecte currently detectable = true; false = not detectable
    public bool CurrentlyDetected { get { return currentlyDetected; } set { currentlyDetected = value; } }
	private bool newValue; // true = new value | false = old value
    public bool NewValue { get { return newValue; } set { newValue = value; } }
	private bool currentlyVisible; // true = visable | false = not visiable
    public bool CurrentlyVisible { get { return currentlyVisible; } set { currentlyVisible = value; } }
	private Vector3 modellPosition; // Postition of the 3D-object in the scene
	public Vector3 ModellPosition {get { return modellPosition;} set {modellPosition.x = value[0]; modellPosition.y = value[1]; modellPosition.z = value[2]; }}
	private Transform buttonTransform;
	public Transform ButtonTransfrom {get {return buttonTransform;} set {buttonTransform = value;}}
	private Dictionary<string, List<string>> sensors = new Dictionary<string, List<string>>();
	public Dictionary<string, List<string>> Sensors { get { return sensors; } set { sensors = value; }}
	private bool submodule;
	public bool Submodule {get {return submodule;} set {submodule = value;}}


    // constructor
	public DatabaseObject(string conTag) {
        tag = conTag;
		submodule = false;
    }
}
