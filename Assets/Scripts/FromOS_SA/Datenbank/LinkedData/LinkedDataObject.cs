using UnityEngine;
using System.Collections;
// List
using System.Collections.Generic;

/// <summary>
/// An Object of these class contains all LinkedData information for a single process device.
/// </summary>
public class LinkedDataObject {

	private string moduleCode; // Module code of the module in LinkedData.
	public string ModuleCode {get {return moduleCode;}}
	private string moduleDiscription; // Full module name of the module in LinkedData. 
	public string ModuleDiscription {get { return moduleDiscription;}}
	private string plantTag; // Plant tag of the device in LinkedData.
	public string PlantTag {get {return plantTag;}}
	private string plantURL; // Plant URL of the device in LinkedData
	public string PlantURL {get {return plantURL;}}
	private string sparqlURL; // Sparql URL of the device for deeper Sparql request.
	public string SparqlURL {get {return sparqlURL;}}
	private List<LinkedDataObjectStruct> linkedDataAttribute = new List<LinkedDataObjectStruct>(); // Stores more LinkedData infomration
	public List<LinkedDataObjectStruct> LinkedDataAttribute {get {return linkedDataAttribute;}}

	/// <summary>
	/// Adds an new attribute to the linkedDataAttribute.
	/// </summary>
	/// <param name="property">Property type.</param>
	/// <param name="value">Value of the property.</param>
	/// <param name="unit">Unit of the property.</param>
	public void addAttribute (string property, string value, string unit) {
		linkedDataAttribute.Add (new LinkedDataObjectStruct (property, value, unit));
	}

	/// <summary>
	/// Sets the general variable of the LinkedData object.
	/// </summary>
	/// <param name="mC">Module code.</param>
	/// <param name="mD">Module discription.</param>
	/// <param name="pT">Plant tag.</param>
	/// <param name="pU">Plant URL.</param>
	/// <param name="sU">Sparql URL.</param>
	public void setGeneralVariable (string mC, string mD, string pT, string pU, string sU) {
		moduleCode = mC;
		moduleDiscription = mD;
		plantTag = pT;
		plantURL = pU;
		sparqlURL = sU;
	}
}
