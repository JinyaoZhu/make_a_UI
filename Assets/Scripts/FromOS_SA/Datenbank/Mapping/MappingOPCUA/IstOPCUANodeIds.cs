using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

/// <summary>
/// An Object of these class contains all static information to connect to OPC UA.
/// Has to be automated in future. 
/// </summary>
public class IstOPCUANodeIds {
	// Verlinkung Socket
	private Dictionary<string, JSONObject> socket = new Dictionary<string, JSONObject>();
	public Dictionary<string, JSONObject> Socket {get {return socket; }}
	// Für Verbindung LinkedData zu OPC UA
	// Node to Tag - Mapping
	private Dictionary<string, StaticOPCUANode> nodeIdToTag = new Dictionary<string, StaticOPCUANode>();
	public Dictionary<string, StaticOPCUANode> NodeIdToTag {get {return nodeIdToTag; }}
	// Tag to Node - Mapping
	private Dictionary<string, Dictionary<string, StaticOPCUANode>> tagToNodeId = new Dictionary<string, Dictionary<string, StaticOPCUANode>>();
	public Dictionary<string, Dictionary<string, StaticOPCUANode>> TagToNodeId {get {return tagToNodeId; }}
	// ###################################################################################################
	// MISCHEN
	// ###################################################################################################
	private Dictionary<string, StaticOPCUANode> mischenTtoN = new Dictionary<string, StaticOPCUANode>();
	private JSONObject mischenSocket = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject MischenSocket {get {return mischenSocket; }}
	// ###################################################################################################
	// ABFÜLLEN
	// ###################################################################################################
	private Dictionary<string, StaticOPCUANode> abfuellenTtoN = new Dictionary<string, StaticOPCUANode>();
	private JSONObject abfüllenSocket = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject AbfüllenSocket {get {return abfüllenSocket; }}
	// ###################################################################################################
	// FILTER
	// ###################################################################################################
	private Dictionary<string, StaticOPCUANode> filterTtoN = new Dictionary<string, StaticOPCUANode>();
	private JSONObject filterSocket = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject FilterSocket {get {return filterSocket; }}
	// ###################################################################################################
	// REAKTOR
	// ###################################################################################################
	private Dictionary<string, StaticOPCUANode> reaktorTtoN = new Dictionary<string, StaticOPCUANode>();
	private JSONObject reaktorSocket = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject ReaktorSocket {get {return reaktorSocket; }}
	// ###################################################################################################
	// TEST
	// ###################################################################################################
	private Dictionary<string, StaticOPCUANode> testTtoN = new Dictionary<string, StaticOPCUANode>();
	private JSONObject testSocket = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject TestSocket {get {return testSocket; }}
	// ###################################################################################################
	// MAPPING
	// ###################################################################################################
	private RestAPIRequest restAPI; 
	private JSONNode nodeIdCrawlerResult;
	public  JSONNode NodeIdCrawlerResult {get {return nodeIdCrawlerResult; }}

	// Constructor
	public IstOPCUANodeIds (RestAPIRequest restAPIExtern) {
		restAPI = restAPIExtern;
		//nodeIdCrawlerResult = restAPI.nodeIdCrawler ();

		try {
			nodeIdCrawlerResult = restAPI.nodeIdCrawler ();
			//Debug.Log(nodeIdCrawlerResult);
		}catch(Exception e) {
			Debug.Log("Exeption in get mapping data from OPC UA Server: " + e.Message);
		}

		BuildMischen ();
		buildAbfuellen ();
		BuildFilter ();
		buildReaktor ();
	} 

	/// <summary>
	/// Build static stuff for module 'Abfüllen' .
	/// </summary>
	private void buildAbfuellen () {
		// Mapping
		//Dictionary<string, string> mappingAbfuellen = new Dictionary<string, string> ();
		// ...


		// Abfüllen
		nodeIdToTag.Add("ns=1;i=1916", new StaticOPCUANode ("Abfüllen", "4B4"));
		abfuellenTtoN.Add("4B4", new StaticOPCUANode ("Abfüllen", "ns=1;i=1916"));
		abfüllenSocket.Add("ns=1;i=1916");

		nodeIdToTag.Add("ns=1;i=1917", new StaticOPCUANode ("Abfüllen", "4B6"));
		abfuellenTtoN.Add("4B6", new StaticOPCUANode ("Abfüllen", "ns=1;i=1917"));
		abfüllenSocket.Add("ns=1;i=1917");

		nodeIdToTag.Add("ns=1;i=1918", new StaticOPCUANode ("Abfüllen", "4B7"));
		abfuellenTtoN.Add("4B7", new StaticOPCUANode ("Abfüllen", "ns=1;i=1918"));
		abfüllenSocket.Add("ns=1;i=1918");

		// Ventiel müssen noch besprochen werden 

		nodeIdToTag.Add("ns=1;i=1919", new StaticOPCUANode ("Abfüllen", "V402_G"));
		nodeIdToTag.Add("ns=1;i=1920", new StaticOPCUANode ("Abfüllen", "V402_O"));
		abfuellenTtoN.Add("V402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1919#ns=1;i=1920"));
		abfüllenSocket.Add("ns=1;i=1919");
		abfüllenSocket.Add("ns=1;i=1920");

		nodeIdToTag.Add("ns=1;i=1921", new StaticOPCUANode ("Abfüllen", "V404_G"));
		nodeIdToTag.Add("ns=1;i=1922", new StaticOPCUANode ("Abfüllen", "V404_O"));
		abfuellenTtoN.Add("V404", new StaticOPCUANode ("Abfüllen", "ns=1;i=1921#ns=1;i=1922"));
		abfüllenSocket.Add("ns=1;i=1921");
		abfüllenSocket.Add("ns=1;i=1922");

		nodeIdToTag.Add("ns=1;i=1923", new StaticOPCUANode ("Abfüllen", "V405_G"));
		nodeIdToTag.Add("ns=1;i=1924", new StaticOPCUANode ("Abfüllen", "V405_O"));
		abfuellenTtoN.Add("V405", new StaticOPCUANode ("Abfüllen", "ns=1;i=1923#ns=1;i=1924"));
		abfüllenSocket.Add("ns=1;i=1923");
		abfüllenSocket.Add("ns=1;i=1924");

		nodeIdToTag.Add("ns=1;i=1925", new StaticOPCUANode ("Abfüllen", "V406_G"));
		nodeIdToTag.Add("ns=1;i=1926", new StaticOPCUANode ("Abfüllen", "V406_O"));
		abfuellenTtoN.Add("V406", new StaticOPCUANode ("Abfüllen", "ns=1;i=1925#ns=1;i=1926"));
		abfüllenSocket.Add("ns=1;i=1925");
		abfüllenSocket.Add("ns=1;i=1926");

		nodeIdToTag.Add("ns=1;i=1927", new StaticOPCUANode ("Abfüllen", "V407_G"));
		nodeIdToTag.Add("ns=1;i=1928", new StaticOPCUANode ("Abfüllen", "V407_O"));
		abfuellenTtoN.Add("V407", new StaticOPCUANode ("Abfüllen", "ns=1;i=1927#ns=1;i=1928"));
		abfüllenSocket.Add("ns=1;i=1927");
		abfüllenSocket.Add("ns=1;i=1928");

		//

		nodeIdToTag.Add("ns=1;i=1929", new StaticOPCUANode ("Abfüllen", "L410"));
		abfuellenTtoN.Add("L410", new StaticOPCUANode ("Abfüllen", "ns=1;i=1929"));
		abfüllenSocket.Add("ns=1;i=1929");

		nodeIdToTag.Add("ns=1;i=1930", new StaticOPCUANode ("Abfüllen", "L401"));
		abfuellenTtoN.Add("L401", new StaticOPCUANode ("Abfüllen", "ns=1;i=1930"));
		abfüllenSocket.Add("ns=1;i=1930");

		nodeIdToTag.Add("ns=1;i=1931", new StaticOPCUANode ("Abfüllen", "L402"));
		abfuellenTtoN.Add("L402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1931"));
		abfüllenSocket.Add("ns=1;i=1931");

		nodeIdToTag.Add("ns=1;i=1932", new StaticOPCUANode ("Abfüllen", "L411"));
		abfuellenTtoN.Add("L411", new StaticOPCUANode ("Abfüllen", "ns=1;i=1932"));
		abfüllenSocket.Add("ns=1;i=1932");

		nodeIdToTag.Add("ns=1;i=1933", new StaticOPCUANode ("Abfüllen", "L412"));
		abfuellenTtoN.Add("L412", new StaticOPCUANode ("Abfüllen", "ns=1;i=1933"));
		abfüllenSocket.Add("ns=1;i=1933");

		nodeIdToTag.Add("ns=1;i=1934", new StaticOPCUANode ("Abfüllen", "M01"));
		abfuellenTtoN.Add("M01", new StaticOPCUANode ("Abfüllen", "ns=1;i=1934"));
		abfüllenSocket.Add("ns=1;i=1934");

		nodeIdToTag.Add("ns=1;i=1936", new StaticOPCUANode ("Abfüllen", "M02"));
		abfuellenTtoN.Add("M02", new StaticOPCUANode ("Abfüllen", "ns=1;i=1936"));
		abfüllenSocket.Add("ns=1;i=1936");

		nodeIdToTag.Add("ns=1;i=1938", new StaticOPCUANode ("Abfüllen", "P401"));
		abfuellenTtoN.Add("P402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1938"));
		abfüllenSocket.Add("ns=1;i=1938");

		socket.Add ("Abfüllen", abfüllenSocket);
		tagToNodeId.Add ("Abfüllen", abfuellenTtoN);
	}

	private void BuildMischen () {
		Dictionary<string, string> mappingMischen = new Dictionary<string, string> ();
		mappingMischen.Add ("P201", "visu_2M1_NA_3514102");
		mappingMischen.Add ("P202", "visu_2M2_NA_3514102");
		mappingMischen.Add ("V201", "visu_V2M3_NA_3515101O");
		mappingMischen.Add ("V202", "visu_V2M4_NA_3515101O");
		mappingMischen.Add ("V203", "visu_V2M5_NA_3515101O");
		mappingMischen.Add ("F201", "visu_F_2B1_NA_3514102");
		mappingMischen.Add ("L201", "visu_L_2B10_NA_3514102");
		mappingMischen.Add ("L211", "visu_L_2B11_NA_3514102");
		mappingMischen.Add ("L212", "visu_L_2B12_NA_3514102");
		mappingMischen.Add ("L213", "visu_L_2B13_NA_3514102");
		mappingMischen.Add ("L214", "visu_L_4B1_liter_NA_3514102");
		mappingMischen.Add ("L202", "visu_L_2B3_NA_3514102");
		mappingMischen.Add ("L203", "visu_L_2B4_NA_3514102");
		mappingMischen.Add ("L204", "visu_L_2B5_NA_3514102");
		mappingMischen.Add ("L205", "visu_L_2B6_NA_3514102");
		mappingMischen.Add ("L206", "visu_L_2B7_NA_3514102");

		// Handle esay mapping
		foreach(KeyValuePair<string, string> entry in mappingMischen) {
			try {
				nodeIdToTag.Add(nodeIdCrawlerResult["Mischen"]["data"][entry.Value], new StaticOPCUANode ("Mischen", entry.Key));
				mischenTtoN.Add(entry.Key, new StaticOPCUANode ("Mischen", nodeIdCrawlerResult["Mischen"]["data"][entry.Value]));			
				mischenSocket.Add((string)nodeIdCrawlerResult["Mischen"]["data"][entry.Value]);	

			}catch(Exception e) { 
				Debug.Log("Exeption in Mapping for Mischer in device: "+ entry.Key +" : " + e.Message);
			}	
		}
		socket.Add ("Mischen", mischenSocket);
		tagToNodeId.Add ("Mischen", mischenTtoN);
	}

	/// <summary>
	/// Build static stuff for module 'Filter' .
	/// </summary>
	private void BuildFilter () {
		// Mapping
		Dictionary<string, string> mappingFilter = new Dictionary<string, string> ();
		mappingFilter.Add("L101","Visu_L101_Value_NA_3514102");
		mappingFilter.Add("L102","Visu_L102_Value_NA_3514102");
		mappingFilter.Add("L103","Visu_L103_Value_NA_3514102");
		mappingFilter.Add("L104","Visu_L104_Value_NA_3514102");
		mappingFilter.Add("L110","Visu_L110_Value_NA_3514102");
		mappingFilter.Add("L111","Visu_L111_Value_NA_3514102");
		mappingFilter.Add("P101","Visu_P101_value_NA_3514102");
		mappingFilter.Add("P102","Visu_P102_value_NA_3514102");
		mappingFilter.Add("PIC","Visu_PIC_Value_NA_3514102");
		mappingFilter.Add("PROP_V","Visu_PROP_V_Value_NA_3514102");
		mappingFilter.Add("R104","Visu_R104_value_NA_3514102");
		mappingFilter.Add("V102","Visu_V102_value_NA_3514102");
		// valves
		mappingFilter.Add ("V106_O", "Visu_Fbk_V106_open_NA_3515101O");
		mappingFilter.Add ("V109_O", "Visu_Fbk_V109_open_NA_3515101O");
		mappingFilter.Add ("V110_O", "Visu_Fbk_V110_open_NA_3515101O");
		mappingFilter.Add ("V114_O", "Visu_Fbk_V114_open_NA_3515101O");
		mappingFilter.Add ("V115_O", "Visu_Fbk_V115_open_NA_3515101O");
		// closed
		mappingFilter.Add ("V106_G", "Visu_Fbk_V106_close_NA_3515101C");
		mappingFilter.Add ("V109_G", "Visu_Fbk_V109_close_NA_3515101C");
		mappingFilter.Add ("V110_G", "Visu_Fbk_V110_close_NA_3515101C");
		mappingFilter.Add ("V114_G", "Visu_Fbk_V114_close_NA_3515101C");
		mappingFilter.Add ("V115_G", "Visu_Fbk_V115_close_NA_3515101C");

		// valves o/c mapping
		Dictionary<string, string> mappingFilter_Ventile_OC = new Dictionary<string, string> ();
		mappingFilter_Ventile_OC.Add ("V106_G", "V106_O");
		mappingFilter_Ventile_OC.Add ("V109_G", "V109_O");
		mappingFilter_Ventile_OC.Add ("V110_G", "V110_O");
		mappingFilter_Ventile_OC.Add ("V114_G", "V115_O");
		mappingFilter_Ventile_OC.Add ("V115_G", "V115_O");

		// Handle esay mapping
		foreach(KeyValuePair<string, string> entry in mappingFilter) {
			try {
				nodeIdToTag.Add(nodeIdCrawlerResult["Filter"]["data"][entry.Value], new StaticOPCUANode ("Filter", entry.Key));
				//Debug.Log(entry.Key);
				//Debug.Log(entry.Value);
				//Debug.Log(nodeIdCrawlerResult["Filter"]["data"][entry.Value]);
				if(!(mappingFilter_Ventile_OC.ContainsValue(entry.Key) || mappingFilter_Ventile_OC.ContainsKey(entry.Key))) {
					filterTtoN.Add(entry.Key, new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][entry.Value]));			
				} 
				filterSocket.Add((string)nodeIdCrawlerResult["Filter"]["data"][entry.Value]);	
	
			}catch(Exception e) { 
				Debug.Log("Exeption in Mapping for Filter in device: "+ entry.Key +" : " + e.Message);
			}	
		}

		// Hardcodet bad mapping because of valves
		try {
			filterTtoN.Add("V106", new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V106_G"]] + "#" + nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V106_O"]]));
			filterTtoN.Add("V109", new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V109_G"]] + "#" + nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V109_O"]]));
			filterTtoN.Add("V110", new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V110_G"]] + "#" + nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V110_O"]]));
			filterTtoN.Add("V114", new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V114_G"]] + "#" + nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V114_O"]]));
			filterTtoN.Add("V115", new StaticOPCUANode ("Filter", nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V115_G"]] + "#" + nodeIdCrawlerResult["Filter"]["data"][mappingFilter["V115_O"]]));
		} catch (Exception e) {
			Debug.Log("Exeption in Mapping for Filter(Valves): " + e.Message);
		} 

		socket.Add ("Filter", filterSocket);
		tagToNodeId.Add ("Filter", filterTtoN);
	}

	/// <summary>
	/// Build static stuff for module 'Reaktor'.
	/// </summary>
	private void buildReaktor () {

		Dictionary<string, string> mappingReaktor = new Dictionary<string, string> ();
		mappingReaktor.Add("T301","Visu_T301_Value_NA_3514102");
		mappingReaktor.Add("L301","visu_L301_NA_3514102");
		mappingReaktor.Add("L302","visu_L302_NA_3514102");
		mappingReaktor.Add("L303","visu_L303_NA_3514102");
		mappingReaktor.Add("L304","visu_L304_liter_NA_3514102");
		mappingReaktor.Add("P301","visu_P301_NA_3514102");
		mappingReaktor.Add("P302","visu_P302_NA_3514102");
		mappingReaktor.Add("R301","visu_R301_value_NA_3514102");
		// valves - open 
		mappingReaktor.Add ("V305", "visu_V305_NA_35151010");
		mappingReaktor.Add ("V306", "visu_V306_NA_35151010");
		// valves - closed
		// ...

		// valves o/c mapping
		// ...
		/*Dictionary<string, string> mappingFilter_Ventile_OC = new Dictionary<string, string> ();
		mappingFilter_Ventile_OC.Add ("V106_G", "V106_O");*/


		// Handle esay mapping
		foreach(KeyValuePair<string, string> entry in mappingReaktor) {
			try {
				nodeIdToTag.Add(nodeIdCrawlerResult["Reaktor"]["data"][entry.Value], new StaticOPCUANode ("Reaktor", entry.Key));
				//if(!(mappingFilter_Ventile_OC.ContainsValue(entry.Key) || mappingFilter_Ventile_OC.ContainsKey(entry.Key))) {
				reaktorTtoN.Add(entry.Key, new StaticOPCUANode ("Reaktor", nodeIdCrawlerResult["Reaktor"]["data"][entry.Value]));			
				//} 
				reaktorSocket.Add((string)nodeIdCrawlerResult["Reaktor"]["data"][entry.Value]);	

			}catch(Exception e) { 
				Debug.Log("Exeption in Mapping for Reaktor in device: "+ entry.Key +" : " + e.Message);
			}	
		}

		/*nodeIdToTag.Add("ns=1;i=1802", new StaticOPCUANode ("Reaktor", "T301"));
		reaktorTtoN.Add("T301", new StaticOPCUANode ("Reaktor", "ns=1;i=1803"));
		reaktorSocket.Add("ns=1;i=1803");

		nodeIdToTag.Add("ns=1;i=1788", new StaticOPCUANode ("Reaktor", "L301"));
		reaktorTtoN.Add("L301", new StaticOPCUANode ("Reaktor", "ns=1;i=1788"));
		reaktorSocket.Add("ns=1;i=1788");

		nodeIdToTag.Add("ns=1;i=1789", new StaticOPCUANode ("Reaktor", "L302"));
		reaktorTtoN.Add("L302", new StaticOPCUANode ("Reaktor", "ns=1;i=1789"));
		reaktorSocket.Add("ns=1;i=1789");

		nodeIdToTag.Add("ns=1;i=1794", new StaticOPCUANode ("Reaktor", "L303"));
		reaktorTtoN.Add("L303", new StaticOPCUANode ("Reaktor", "ns=1;i=1794"));
		reaktorSocket.Add("ns=1;i=1794");

		nodeIdToTag.Add("ns=1;i=1795", new StaticOPCUANode ("Reaktor", "L304"));
		reaktorTtoN.Add("L304", new StaticOPCUANode ("Reaktor", "ns=1;i=1795"));
		reaktorSocket.Add("ns=1;i=1795");

		nodeIdToTag.Add("ns=1;i=1796", new StaticOPCUANode ("Reaktor", "P301"));
		reaktorTtoN.Add("P301", new StaticOPCUANode ("Reaktor", "ns=1;i=1796"));
		reaktorSocket.Add("ns=1;i=1796");

		nodeIdToTag.Add("ns=1;i=1798", new StaticOPCUANode ("Reaktor", "P302"));
		reaktorTtoN.Add("P302", new StaticOPCUANode ("Reaktor", "ns=1;i=1798"));
		reaktorSocket.Add("ns=1;i=1798");

		nodeIdToTag.Add("ns=1;i=1801", new StaticOPCUANode ("Reaktor", "R301"));
		reaktorTtoN.Add("R301", new StaticOPCUANode ("Reaktor", "ns=1;i=1801"));
		reaktorSocket.Add("ns=1;i=1801");

		// Ventile

		nodeIdToTag.Add("ns=1;i=1803", new StaticOPCUANode ("Reaktor", "V305"));
		reaktorTtoN.Add("V305", new StaticOPCUANode ("Reaktor", "ns=1;i=1803"));
		reaktorSocket.Add("ns=1;i=1803");

		nodeIdToTag.Add("ns=1;i=1805", new StaticOPCUANode ("Reaktor", "V306"));
		reaktorTtoN.Add("V306", new StaticOPCUANode ("Reaktor", "ns=1;i=1805"));
		reaktorSocket.Add("ns=1;i=1805");*/

		//

		socket.Add ("Reaktor", reaktorSocket);
		tagToNodeId.Add ("Reaktor", reaktorTtoN);
	}

	/*private void buildAbfuellen () {
		// Abfüllen
		nodeIdToTag.Add("ns=1;i=1841", new StaticOPCUANode ("Abfüllen", "4B4"));
		abfuellenTtoN.Add("4B4", new StaticOPCUANode ("Abfüllen", "ns=1;i=1841"));
		abfüllenSocket.Add("ns=1;i=1841");

		nodeIdToTag.Add("ns=1;i=1842", new StaticOPCUANode ("Abfüllen", "4B6"));
		abfuellenTtoN.Add("4B6", new StaticOPCUANode ("Abfüllen", "ns=1;i=1842"));
		abfüllenSocket.Add("ns=1;i=1842");

		nodeIdToTag.Add("ns=1;i=1843", new StaticOPCUANode ("Abfüllen", "4B7"));
		abfuellenTtoN.Add("4B7", new StaticOPCUANode ("Abfüllen", "ns=1;i=1843"));
		abfüllenSocket.Add("ns=1;i=1843");

		// Ventiel müssen noch besprochen werden 

		nodeIdToTag.Add("ns=1;i=1844", new StaticOPCUANode ("Abfüllen", "V402_G"));
		nodeIdToTag.Add("ns=1;i=1845", new StaticOPCUANode ("Abfüllen", "V402_O"));
		abfuellenTtoN.Add("V402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1844#ns=1;i=1845"));
		abfüllenSocket.Add("ns=1;i=1844");
		abfüllenSocket.Add("ns=1;i=1845");

		nodeIdToTag.Add("ns=1;i=1846", new StaticOPCUANode ("Abfüllen", "V404_G"));
		nodeIdToTag.Add("ns=1;i=1847", new StaticOPCUANode ("Abfüllen", "V404_O"));
		abfuellenTtoN.Add("V404", new StaticOPCUANode ("Abfüllen", "ns=1;i=1846#ns=1;i=1847"));
		abfüllenSocket.Add("ns=1;i=1846");
		abfüllenSocket.Add("ns=1;i=1847");

		nodeIdToTag.Add("ns=1;i=1848", new StaticOPCUANode ("Abfüllen", "V405_G"));
		nodeIdToTag.Add("ns=1;i=1849", new StaticOPCUANode ("Abfüllen", "V405_O"));
		abfuellenTtoN.Add("V405", new StaticOPCUANode ("Abfüllen", "ns=1;i=1848#ns=1;i=1849"));
		abfüllenSocket.Add("ns=1;i=1848");
		abfüllenSocket.Add("ns=1;i=1849");

		nodeIdToTag.Add("ns=1;i=1850", new StaticOPCUANode ("Abfüllen", "V406_G"));
		nodeIdToTag.Add("ns=1;i=1851", new StaticOPCUANode ("Abfüllen", "V406_O"));
		abfuellenTtoN.Add("V406", new StaticOPCUANode ("Abfüllen", "ns=1;i=1850#ns=1;i=1851"));
		abfüllenSocket.Add("ns=1;i=1850");
		abfüllenSocket.Add("ns=1;i=1851");

		nodeIdToTag.Add("ns=1;i=1852", new StaticOPCUANode ("Abfüllen", "V407_G"));
		nodeIdToTag.Add("ns=1;i=1853", new StaticOPCUANode ("Abfüllen", "V407_O"));
		abfuellenTtoN.Add("V407", new StaticOPCUANode ("Abfüllen", "ns=1;i=1852#ns=1;i=1853"));
		abfüllenSocket.Add("ns=1;i=1852");
		abfüllenSocket.Add("ns=1;i=1853");

		//

		nodeIdToTag.Add("ns=1;i=1854", new StaticOPCUANode ("Abfüllen", "L410"));
		abfuellenTtoN.Add("L410", new StaticOPCUANode ("Abfüllen", "ns=1;i=1854"));
		abfüllenSocket.Add("ns=1;i=1854");

		nodeIdToTag.Add("ns=1;i=1855", new StaticOPCUANode ("Abfüllen", "L401"));
		abfuellenTtoN.Add("L401", new StaticOPCUANode ("Abfüllen", "ns=1;i=1855"));
		abfüllenSocket.Add("ns=1;i=1855");

		nodeIdToTag.Add("ns=1;i=1856", new StaticOPCUANode ("Abfüllen", "L402"));
		abfuellenTtoN.Add("L402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1856"));
		abfüllenSocket.Add("ns=1;i=1856");

		nodeIdToTag.Add("ns=1;i=1857", new StaticOPCUANode ("Abfüllen", "L411"));
		abfuellenTtoN.Add("L411", new StaticOPCUANode ("Abfüllen", "ns=1;i=1857"));
		abfüllenSocket.Add("ns=1;i=1857");

		nodeIdToTag.Add("ns=1;i=1858", new StaticOPCUANode ("Abfüllen", "L412"));
		abfuellenTtoN.Add("L412", new StaticOPCUANode ("Abfüllen", "ns=1;i=1858"));
		abfüllenSocket.Add("ns=1;i=1858");

		nodeIdToTag.Add("ns=1;i=1859", new StaticOPCUANode ("Abfüllen", "M01"));
		abfuellenTtoN.Add("M01", new StaticOPCUANode ("Abfüllen", "ns=1;i=1859"));
		abfüllenSocket.Add("ns=1;i=1859");

		nodeIdToTag.Add("ns=1;i=1861", new StaticOPCUANode ("Abfüllen", "M02"));
		abfuellenTtoN.Add("M02", new StaticOPCUANode ("Abfüllen", "ns=1;i=1861"));
		abfüllenSocket.Add("ns=1;i=1861");

		nodeIdToTag.Add("ns=1;i=1863", new StaticOPCUANode ("Abfüllen", "P401"));
		abfuellenTtoN.Add("P402", new StaticOPCUANode ("Abfüllen", "ns=1;i=1863"));
		abfüllenSocket.Add("ns=1;i=1863");

		socket.Add ("Abfüllen", abfüllenSocket);
		tagToNodeId.Add ("Abfüllen", abfuellenTtoN);
	}

	/// <summary>
	/// Build static stuff for module 'Filter' .
	/// </summary>
	private void buildFilter () {

		// Ventiel!

		nodeIdToTag.Add("ns=1;i=1780", new StaticOPCUANode ("Filter", "V106_G"));
		nodeIdToTag.Add("ns=1;i=1781", new StaticOPCUANode ("Filter", "V106_O"));
		filterTtoN.Add("V106", new StaticOPCUANode ("Filter", "ns=1;i=1780#ns=1;i=1781"));
		filterSocket.Add("ns=1;i=1780");
		filterSocket.Add("ns=1;i=1781");

		nodeIdToTag.Add("ns=1;i=1782", new StaticOPCUANode ("Filter", "V109_G"));
		nodeIdToTag.Add("ns=1;i=1783", new StaticOPCUANode ("Filter", "V109_O"));
		filterTtoN.Add("V109", new StaticOPCUANode ("Filter", "ns=1;i=1782#ns=1;i=1783"));
		filterSocket.Add("ns=1;i=1782");
		filterSocket.Add("ns=1;i=1783");

		nodeIdToTag.Add("ns=1;i=1784", new StaticOPCUANode ("Filter", "V110_G"));
		nodeIdToTag.Add("ns=1;i=1785", new StaticOPCUANode ("Filter", "V110_O"));
		filterTtoN.Add("V110", new StaticOPCUANode ("Filter", "ns=1;i=1784#ns=1;i=1785"));
		filterSocket.Add("ns=1;i=1784");
		filterSocket.Add("ns=1;i=1785");

		nodeIdToTag.Add("ns=1;i=1786", new StaticOPCUANode ("Filter", "V114_G"));
		nodeIdToTag.Add("ns=1;i=1787", new StaticOPCUANode ("Filter", "V114_O"));
		filterTtoN.Add("V114", new StaticOPCUANode ("Filter", "ns=1;i=1786#ns=1;i=1787"));
		filterSocket.Add("ns=1;i=1786");
		filterSocket.Add("ns=1;i=1787");

		nodeIdToTag.Add("ns=1;i=1788", new StaticOPCUANode ("Filter", "V115_G"));
		nodeIdToTag.Add("ns=1;i=1789", new StaticOPCUANode ("Filter", "V115_O"));
		filterTtoN.Add("V115", new StaticOPCUANode ("Filter", "ns=1;i=1788#ns=1;i=1789"));
		filterSocket.Add("ns=1;i=1788");
		filterSocket.Add("ns=1;i=1789");

		nodeIdToTag.Add("ns=1;i=1806", new StaticOPCUANode ("Filter", "V102"));
		filterTtoN.Add("V102", new StaticOPCUANode ("Filter", "ns=1;i=1806"));
		filterSocket.Add("ns=1;i=1806");

		//

		nodeIdToTag.Add("ns=1;i=1790", new StaticOPCUANode ("Filter", "L101"));
		filterTtoN.Add("L101", new StaticOPCUANode ("Filter", "ns=1;i=1790"));
		filterSocket.Add("ns=1;i=1790");

		nodeIdToTag.Add("ns=1;i=1791", new StaticOPCUANode ("Filter", "L102"));
		filterTtoN.Add("L102", new StaticOPCUANode ("Filter", "ns=1;i=1791"));
		filterSocket.Add("ns=1;i=1791");

		nodeIdToTag.Add("ns=1;i=1792", new StaticOPCUANode ("Filter", "L103"));
		filterTtoN.Add("L103", new StaticOPCUANode ("Filter", "ns=1;i=1792"));
		filterSocket.Add("ns=1;i=1792");

		nodeIdToTag.Add("ns=1;i=1793", new StaticOPCUANode ("Filter", "L104"));
		filterTtoN.Add("L104", new StaticOPCUANode ("Filter", "ns=1;i=1793"));
		filterSocket.Add("ns=1;i=1793");

		nodeIdToTag.Add("ns=1;i=1794", new StaticOPCUANode ("Filter", "L110"));
		filterTtoN.Add("L110", new StaticOPCUANode ("Filter", "ns=1;i=1794"));
		filterSocket.Add("ns=1;i=1794");

		nodeIdToTag.Add("ns=1;i=1795", new StaticOPCUANode ("Filter", "L111"));
		filterTtoN.Add("L111", new StaticOPCUANode ("Filter", "ns=1;i=1795"));
		filterSocket.Add("ns=1;i=1795");

		nodeIdToTag.Add("ns=1;i=1797", new StaticOPCUANode ("Filter", "1797"));
		filterTtoN.Add("P101", new StaticOPCUANode ("Filter", "ns=1;i=1871"));
		filterSocket.Add("ns=1;i=1797");

		nodeIdToTag.Add("ns=1;i=1799", new StaticOPCUANode ("Filter", "P102"));
		filterTtoN.Add("P102", new StaticOPCUANode ("Filter", "ns=1;i=1799"));
		filterSocket.Add("ns=1;i=1799");

		nodeIdToTag.Add("ns=1;i=1804", new StaticOPCUANode ("Filter", "R104"));
		filterTtoN.Add("R104", new StaticOPCUANode ("Filter", "ns=1;i=1804"));
		filterSocket.Add("ns=1;i=1804");

		nodeIdToTag.Add("ns=1;i=1800", new StaticOPCUANode ("Filter", "PIC"));
		filterTtoN.Add("PIC", new StaticOPCUANode ("Filter", "ns=1;i=1800"));
		filterSocket.Add("ns=1;i=1800");

		nodeIdToTag.Add("ns=1;i=1802", new StaticOPCUANode ("Filter", "PROP_V"));
		filterTtoN.Add("PROP_V", new StaticOPCUANode ("Filter", "ns=1;i=1802"));
		filterSocket.Add("ns=1;i=1802");

		socket.Add ("Filter", filterSocket);
		tagToNodeId.Add ("Filter", filterTtoN);
	}

	/// <summary>
	/// Build static stuff for module 'Reaktor'.
	/// </summary>
	private void buildReaktor () {
		nodeIdToTag.Add("ns=1;i=1727", new StaticOPCUANode ("Reaktor", "T301"));
		reaktorTtoN.Add("T301", new StaticOPCUANode ("Reaktor", "ns=1;i=1727"));
		reaktorSocket.Add("ns=1;i=1727");

		nodeIdToTag.Add("ns=1;i=1717", new StaticOPCUANode ("Reaktor", "L301"));
		reaktorTtoN.Add("L301", new StaticOPCUANode ("Reaktor", "ns=1;i=1717"));
		reaktorSocket.Add("ns=1;i=1717");

		nodeIdToTag.Add("ns=1;i=1718", new StaticOPCUANode ("Reaktor", "L302"));
		reaktorTtoN.Add("L302", new StaticOPCUANode ("Reaktor", "ns=1;i=1718"));
		reaktorSocket.Add("ns=1;i=1718");

		nodeIdToTag.Add("ns=1;i=1719", new StaticOPCUANode ("Reaktor", "L303"));
		reaktorTtoN.Add("L303", new StaticOPCUANode ("Reaktor", "ns=1;i=1719"));
		reaktorSocket.Add("ns=1;i=1719");

		nodeIdToTag.Add("ns=1;i=1720", new StaticOPCUANode ("Reaktor", "1720"));
		reaktorTtoN.Add("L304", new StaticOPCUANode ("Reaktor", "ns=1;i=1795"));
		reaktorSocket.Add("ns=1;i=1720");

		nodeIdToTag.Add("ns=1;i=1721", new StaticOPCUANode ("Reaktor", "P301"));
		reaktorTtoN.Add("P301", new StaticOPCUANode ("Reaktor", "ns=1;i=1721"));
		reaktorSocket.Add("ns=1;i=1721");

		nodeIdToTag.Add("ns=1;i=1723", new StaticOPCUANode ("Reaktor", "P302"));
		reaktorTtoN.Add("P302", new StaticOPCUANode ("Reaktor", "ns=1;i=1723"));
		reaktorSocket.Add("ns=1;i=1723");

		nodeIdToTag.Add("ns=1;i=1726", new StaticOPCUANode ("Reaktor", "R301"));
		reaktorTtoN.Add("R301", new StaticOPCUANode ("Reaktor", "ns=1;i=1726"));
		reaktorSocket.Add("ns=1;i=1726");

		// Ventile

		nodeIdToTag.Add("ns=1;i=1728", new StaticOPCUANode ("Reaktor", "V305"));
		reaktorTtoN.Add("V305", new StaticOPCUANode ("Reaktor", "ns=1;i=1728"));
		reaktorSocket.Add("ns=1;i=1728");

		nodeIdToTag.Add("ns=1;i=1730", new StaticOPCUANode ("Reaktor", "V306"));
		reaktorTtoN.Add("V306", new StaticOPCUANode ("Reaktor", "ns=1;i=1730"));
		reaktorSocket.Add("ns=1;i=1805");

		//

		socket.Add ("Reaktor", reaktorSocket);
		tagToNodeId.Add ("Reaktor", reaktorTtoN);
	}*/
}
