using UnityEngine;
using System.Collections;

/// <summary>
/// An object of these class contains a property, value and unit of an LinkedData attribute.
/// </summary>
public class LinkedDataObjectStruct {
	private string property;
	public string Property {get {return property;}}
	private string value; 
	public string Value {get {return value;}}
	private string unit;
	public string Unit {get {return unit;}}

	// Construktor
	public LinkedDataObjectStruct (string prop, string val, string un) {
		property = prop;
		value = val; 
		unit = un; 
	}

	/// <summary>
	/// Get all variable values.
	/// </summary>
	/// <returns>Returns all local stored values.</returns>
	public string getAll () {
		return "property: " + property + "; value: " + value + "; unit: " + unit;
	}
}