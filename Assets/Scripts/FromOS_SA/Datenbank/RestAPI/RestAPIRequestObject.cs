using UnityEngine;
using System.Collections;

/// <summary>
/// Rest API request object for serialisation.
/// </summary>
[System.Serializable]
public class RestAPIRequestObject {
	// maybe more variables ... 
	public string NodeId;
	public string Value;
	public string DataType;

	// special variables for Ventile 
	private bool _offen = false; // open -> 1 | closed -> 0
	public bool _Offen {get{ return _offen;} set {_offen = value;}}
	private bool _geschlossen = false; // closed -> 1 | open -> 0
	public bool _Geschlossen {get{ return _geschlossen;} set {_geschlossen = value;}}
}
