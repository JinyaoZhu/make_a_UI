using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;
using UnityEngine;

/// <summary>
/// Mapping nodeid's for desired values.
/// </summary>
public class SollOPCUANodeIds {
	// Dictonary for desired values.
	private Dictionary<string, Dictionary<string, string>> sollNodeId = new Dictionary<string,Dictionary<string, string>>();
	public Dictionary<string, Dictionary<string, string>> SollNodeId {get {return sollNodeId; }}
	private JSONNode nodeCrawler;

	/// <summary>
	/// Initializes a new instance of the <see cref="SollOPCUANodeIds"/> class.
	/// </summary>
	/// <param name="externNodeCrawler">Extern node crawler.</param>
	public SollOPCUANodeIds (JSONNode externNodeCrawler) {
		nodeCrawler = externNodeCrawler;
		buildAbfuellen ();
		buildFilter ();
		buildReaktor ();
		buildMischen ();
	}

	/// <summary>
	/// Maps the module 'Abfüllen'
	/// </summary>
	private void buildAbfuellen () {
		Dictionary< string, string> local = new Dictionary <string, string>();

		local.Add("V02", "ns=1;i=1940");
		local.Add("V04", "ns=1;i=1941");
		local.Add("V05", "ns=1;I=1942");
		local.Add("V06", "ns=1;I=1943");
		local.Add("V07", "ns=1;I=1944");

		local.Add("M01", "ns=1;I=1935");
		local.Add("M02", "ns=1;I=1937");

		sollNodeId.Add("Abfüllen", local);
	}

	/// <summary>
	/// Mapps the module 'Mischen'
	/// </summary>
	private void buildMischen () {
		// mapping - input
		Dictionary<string, string> mappingMischen = new Dictionary<string, string> ();
		// Fill array
		mappingMischen.Add ("P201", "visu_2M1_NA_3514103");
		mappingMischen.Add ("P202", "visu_2M2_NA_3514103");
		mappingMischen.Add ("V201", "visu_V2M3_NA_3514103");
		mappingMischen.Add ("V202", "visu_V2M4_NA_3514103");
		mappingMischen.Add ("V203", "visu_V2M5_NA_3514103");
		// Map nodeid to tag
		buildSollNodeIdDict ("Mischen", mappingMischen);
	}

	/// <summary>
	/// Maps the module 'Filter'
	/// </summary>
	private void buildFilter () {
		// mapping - input
		Dictionary<string, string> mappingFilter = new Dictionary<string, string> ();
		// Fill array
		mappingFilter.Add("P101","Visu_P101_Ctrl_NA_3514103");
		mappingFilter.Add("P102","Visu_P102_Ctrl_NA_3514103");
		mappingFilter.Add("PROP_V","Visu_PROP_V_Ctrl_NA_3514103");
		mappingFilter.Add("R104","Visu_P102_Ctrl_NA_3514103");
		mappingFilter.Add("V102","Visu_V102_Ctrl_NA_3514103");
		mappingFilter.Add("V103","Visu_V103_Ctrl_NA_3514103");
		mappingFilter.Add("V106","Visu_V106_Ctrl_NA_3514103");
		mappingFilter.Add("V109","Visu_V109_Ctrl_NA_3514103");
		mappingFilter.Add("V110","Visu_V110_Ctrl_NA_3514103");
		mappingFilter.Add("V114","Visu_V114_Ctrl_NA_3514103");
		mappingFilter.Add("V115","Visu_V115_Ctrl_NA_3514103");
		// Map nodeid to tag
		buildSollNodeIdDict ("Filter", mappingFilter);
	}

	/// <summary>
	/// Maps the module 'Reaktor'
	/// </summary>
	private void buildReaktor () {
		// mapping - input
		Dictionary<string, string> mappingReaktor = new Dictionary<string, string> ();
		// Fill array
		mappingReaktor.Add("P301","visu_P301_NA_3514103");
		mappingReaktor.Add("P302","visu_P302_NA_3514103");
		mappingReaktor.Add("R301","visu_R301_Ctrl_NA_3514103");
		mappingReaktor.Add("V305","visu_V305_NA_3514103");
		mappingReaktor.Add("V306","visu_V306_NA_3514103");
		// Map  nodeid to tag
		buildSollNodeIdDict ("Reaktor", mappingReaktor);
	}

	/// <summary>
	/// Mapps the 3D modul model to the 'Soll-Wert' opc ua nodes.
	/// </summary>
	/// <param name="module">Name of the module </param>
	/// <param name="mapping">Mapping between 3D modell and OPC UA Nodes.</param>
	private void buildSollNodeIdDict(string module, Dictionary<string, string> mapping) {
		Dictionary< string, string> local = new Dictionary <string, string>();
		// iterating through dictinary
		foreach(KeyValuePair<string, string> entry in mapping) {
			// Pokemon - Gotta catch 'em all ...
			try {
				local.Add(entry.Key, nodeCrawler[module]["data"][entry.Value]);
			}catch(Exception e) {
				Debug.Log("Exception during mapping input nodes for " + module + " at device: "+ entry.Key +" : " + e.Message);
			}
		}
		sollNodeId.Add (module, local);
	}
}
