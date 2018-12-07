using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System; 
//using System.Diagnostics;
//using System.Xml.Serialization;
// List
using System.Collections.Generic;
//using XmlNodes;

/// <summary>
/// Use an Object of these class for LinkedData requests. These class is a child class of HttpReqeust.
/// </summary>
public class linkedDataRequest : HttpRequest {

	//public InputField requestField;
	private string serverUrl = "eatld.et.tu-dresden.de";
	public string ServerUrl {get {return serverUrl; } set {serverUrl = value; }}

	private string preURL = "";
	private string endURL = "&should-sponge=&format=text%2Fhtml&timeout=0&debug=on";

	private Database database;

	private float startTime;
	private bool linkedDataChecked;
	private float zyklusLD;

	int counter = 0; 
	string ausgabe = "";
	float counterTimer;
	int bla; 
	float blup;

	// Constructor
	public linkedDataRequest() {
		buildPreURL();	
	}
		
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		counterTimer = Time.time; 
		zyklusLD =  Time.time;
		testLinkedData ();
		database = GameObject.Find("Database").GetComponent<Database>();
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		// Invoke checking RestAPI & OPCUA server status
		StartCoroutine("CheckLinkedDataServerStatus");
	}

	void Update () {
		if ((Time.time - zyklusLD) < 5) {
			if (wwwLinkedData.isDone) {
				if (wwwLinkedData.bytesDownloaded == 0) {
					setServerStatus ("linked", 2, database);
					linkedDataChecked = true;
				}else if (linkedDataChecked == false) {
					if ((Time.time - startTime) < 0.12) {
						setServerStatus ("linked", 0, database);
						linkedDataChecked = true;
						//TestAusgabe((Time.time - startTime) + "#", (Time.time - startTime));
					} else if ((Time.time - startTime) >= 0.12 && (Time.time - startTime) <= 5) {
						setServerStatus ("linked", 1, database);
						linkedDataChecked = true;
						//TestAusgabe((Time.time - startTime) + "#", (Time.time - startTime));
					} else {
						setServerStatus ("linked", 2, database);
						linkedDataChecked = true;
						//TestAusgabe((Time.time - startTime) + "#", (Time.time - startTime));
					}	
				}
			}
		} else {
			if (linkedDataChecked == false) {
				setServerStatus ("linked", 2, database);		
			}
			zyklusLD = Time.time;
			testLinkedData ();
		}
	}

	/*private void TestAusgabe (string input, float wert) {
		if ((Time.time - counterTimer) < 60) {
			ausgabe = ausgabe + input;
			bla++;
			blup = blup + wert;
		} else {
			counterTimer = Time.time;
			ausgabe = ausgabe + (blup/bla) + "#";
			Debug.Log (ausgabe);
			bla = 0;
			blup = 0;
			ausgabe = "";
		}
		counter++;
	}*/

	public IEnumerator CheckLinkedDataServerStatus() {
		// check allways
		while (true) {
			// wait for 10 sec. ...
			yield return new WaitForSeconds(5);
			// .. and check server status
			testLinkedData();
		}
	}

	private void buildPreURL () {
		preURL = "http://" + serverUrl + "/sparql?default-graph-uri=&query=";
	}

	/// <summary>
	/// Get Attributes of single process device.
	/// </summary>
	/// <param name="requestAnswer">HTTP response after SPARQL request</param>
	/// <param name="currentObject">Current LinkdDataObject</param>
	/// <param name="loopcounter">Actual loop counter </param>
	public LinkedDataObject getSingleProcessDeviceDecoder(LinkedDataObject currentObject) {
		string inputHtml = getSingleProcessDeviceSPARQLRequest(currentObject.SparqlURL); // get Linked Data request
		string requestAnswer = getHTTPReq (preURL +  System.Uri.EscapeDataString(inputHtml) + endURL); // request linked data server and get answer
		//Debug.Log("RequestAnswer: " + requestAnswer);
		XmlDocument xml = new XmlDocument(); // new xml object
		xml.LoadXml (requestAnswer); // translate string in xml
		// TODO Serialisierung?
		XmlNodeList dataNode = xml.SelectNodes("/table/tr/td"); // parse xml document
		//Debug.Log ("XMLNODELIST: " + dataNode.Item(0).InnerText);
		//Debug.Log ("Länge XMLNODELIST: " + dataNode.Count);

		// Save 
		if ((dataNode.Count % 3) == 0) {
			for(int i = 0; i < dataNode.Count; i = i + 3){
				currentObject.addAttribute(dataNode.Item(i).InnerText,dataNode.Item(i+1).InnerText,dataNode.Item(i+2).InnerText);
				//linkedDataModule[loopcounter] = currentObject;
			};
			//Debug.Log("Listenelement: " + processDevice.getLinkedDataObject()[5].getAll());
		} else {
			Debug.Log ("Error in XML!");
		}
		return currentObject;
	}
		
	/// <summary>
	/// Extract und save all standard information of all ProzessDevices.
	/// </summary>
	/// <returns><c>true</c>, if all module process devices was gotten, <c>false</c> otherwise.</returns>
	/// <param name="requestAnswer">HTTP request in a string</param>
	public List<LinkedDataObject> getAllModuleProcessDevices (string module) {
		List<LinkedDataObject> linkedDataModule = new List<LinkedDataObject>();

		// Aus Überschichtsgründen!
		string requestURL = getAllModuleProcessDevicesSPARQLRequest(module);
		string requestAnswer = getHTTPReq (preURL +  System.Uri.EscapeDataString(requestURL) + endURL);
		XmlDocument xml = new XmlDocument();
		xml.LoadXml(requestAnswer);
		// Alternative: xml.LoadXml(getHTTP(getAllProcessDevicesSPARQLRequest()));
		XmlNodeList dataNode = xml.SelectNodes("/table/tr/td");
		if((dataNode.Count % 5) == 0) {
			for (int i = 0; i < dataNode.Count; i = i + 5) {
				LinkedDataObject processDevice = new LinkedDataObject();
				//Debug.Log("Setting all general variables!");
				processDevice.setGeneralVariable(dataNode.Item(i).InnerText,dataNode.Item(i+1).InnerText,dataNode.Item(i+2).InnerText,dataNode.Item(i+3).InnerText,dataNode.Item(i+4).InnerText);
				// TODO: Backbone ausgeschlossen, da sich hier prozesselemente wiederholen: Q1
				if (dataNode.Item (i + 1).InnerText != "Backbone") {
					linkedDataModule.Add (processDevice);
				}
			}
			return linkedDataModule; 
		}else {
			Debug.Log("Error in XML!");
			return linkedDataModule; 
		}
	}

	private void testLinkedData() {
		startTime = Time.time;
		linkedDataChecked = false;
		string url = getAllModuleProcessDevicesSPARQLRequest("Abfüllen");
		checkLinkedDataServerConnection (preURL + System.Uri.EscapeDataString (url) + endURL);
		//Debug.Log (preURL + System.Uri.EscapeDataString (url) + endURL);
	}

	// ########################################################################################################
	// ALL SPARQL REQUESTS
	// ########################################################################################################

	/// <summary>
	/// Generate a string with a special SPARQL request to get all process devices.
	/// </summary>
	/// <returns>The SPARQL request as a string.</returns>
	private string getAllModuleProcessDevicesSPARQLRequest (string module) {
		//module = "Abfüllen";
		string sparql = "     PREFIX mso: <http://eatld.et.tu-dresden.de/mso/>\n     \n     SELECT DISTINCT str(?modulLabel) str(?modulName) str(?label) str(?plantid) ?device\n     FROM <http://eatld.et.tu-dresden.de/moka>\n     WHERE {\n     \n     ?device a mso:Device;\n     rdfs:label ?label;\n     mso:plantID ?plantid;\n     mso:isPartOfUnit [rdfs:label ?modulLabel; rdfs:comment ?modulName;rdfs:comment \"" + module + "\"@de].\n     \n     FILTER (lang(?label)=\"de\")\n     FILTER (lang(?plantid)=\"de\")\n     FILTER (lang(?modulLabel)=\"de\")\n     FILTER (lang(?modulName)=\"de\")\n     }\n     \n    ORDER BY ?modulLabel ?label ?plantid"; 
		sparql = "PREFIX mso: <http://eatld.et.tu-dresden.de/mso/>\n\nSELECT DISTINCT str(?modulLabel) str(?modulName) str(?label)\nstr(?plantid) ?device\nFROM <http://eatld.et.tu-dresden.de/moka>\nWHERE {\n\n?device a mso:Device;\nrdfs:label ?label;\nmso:plantID ?plantid;\nmso:isPartOfUnit [rdfs:label ?modulLabel ; rdfs:comment ?modulName; rdfs:comment \"" + module + "\"@de].\n\n FILTER (lang(?label)=\"de\")\n FILTER (lang(?plantid)=\"de\")\n}\n\nORDER BY ?label ?plantid"; 
		return sparql;
	}
		
	/// <summary>
	/// Generate a string with a special SPARQL request to get special process device.
	/// </summary>
	/// <returns>The single process device SPARQL request.</returns>
	/// <param name="deviceURL">Unic device URL</param>
	private string getSingleProcessDeviceSPARQLRequest (string deviceURL) {
		string sparql = "PREFIX mso: <http://eatld.et.tu-dresden.de/mso/>\n\nSELECT DISTINCT str(?propertyName) as ?property str(?value) as ?value\nstr(?unit) as ?unit\nFROM <http://eatld.et.tu-dresden.de/moka>\nFROM <http://eatld.et.tu-dresden.de/mso>\nWHERE {\n ?property rdfs:label ?propertyName.\n {\n   <" + deviceURL + "> ?property ?value.\n   FILTER(isLiteral(?value))\n } UNION {\n   <" + deviceURL + "> ?property [\n       a mso:PhysicalQuantity;\n\tmso:numericalValue ?value;\n\tmso:physicalUnit ?unit\n    ]\n }\n FILTER(str(?value)!=\"\")\n FILTER( lang(?propertyName)=\"de\")\n}";
		return sparql; 
	}
}
