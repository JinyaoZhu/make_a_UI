using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opcuaNode {
	private string nodeid;
	public string Nodeid {get {return nodeid; } }
	private string value;
	public string Value {get {return value; }}
	public string dataType;
	public string DataType {get {return dataType;}}

	public opcuaNode (string nId, string val, string dTyp) {
		nodeid = nId;
		value = val;
		dataType = dTyp;
	} 

	public void setValue (string val) {
		value = val;
	}

}
