using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Implementation for HTTP-Request. RestAPIRequest und LinkedDataRequest inheritates from HttpRequest.
/// </summary>
public class HttpRequest: MonoBehaviour {

	public WWW wwwRest; // RestAPI connection test variable
	public WWW wwwLinkedData; // LinkedData connection test variable

	/// <summary>
	/// Single Http GET request. Only parameter in url possible. 
	/// </summary>
	/// <returns>The url of the request. With parameters when needed.</returns>
	/// <param name="inputURL">Return the answer as string.</param>
	public string getHTTPReq (string inputURL) {
		string url = inputURL;
		// GET
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
		while (!www.isDone) {
			WaitForSeconds w = new WaitForSeconds(0.1f);
		}
		return www.text;
	}

	/// <summary>
	/// Single Http POST request. Parameter are in the post dictonary.
	/// </summary>
	/// <param name="url">The url of the request without parameter</param>
	/// <param name="post">A Dictonary with parameters for the request.</param>
	public void postHTTPReq(string url, Dictionary<string, string> post)
	{
		WWWForm form = new WWWForm();
		foreach (KeyValuePair<string, string> post_arg in post)
		{
			form.AddField(post_arg.Key, post_arg.Value);
		}
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForRequest(www));
		while (!www.isDone) {
			WaitForSeconds w = new WaitForSeconds(0.1f);
		}
		//Debug.Log (www.text);
		//return www.text;
	}

	/// <summary>
	/// Call a test request to RestAPI. The evaluation is in RestAPIRequest.cs to check server status with async behavior. 
	/// </summary>
	/// <param name="inputURL">The url of the request.</param>
	public void checkRestServerConnection (string inputURL) {
		wwwRest = new WWW(inputURL);
	}

	/// <summary>
	/// Call a test request to LinkedData. The evaluation is in LinkedDataRequest.cs to check server status with async behavior.
	/// </summary>
	/// <param name="inputURL">The url of the request.</param>
	public void checkLinkedDataServerConnection (string inputURL) {
		wwwLinkedData = new WWW (inputURL);
	}

	/// <summary>
	/// Auswertungs the check.
	/// </summary>
	/// <param name="type">RestAPI or LinkedData.</param>
	/// <param name="value">0 --> green, 1 --> orange, 2 --> red.</param></param></param></param>
	/// <param name="database">The databaseobject to change the value.</param>
	public void setServerStatus (string type, int value, Database database) {
		switch (type) {
		case "opc": 
			database.StatusOPCUAServer = value;
			//Debug.Log ("Status OPC UA Server: " + value);
			break;
		case "linked":
			database.StatusLinkedDataServer = value;
			//Debug.Log("Status LinkedData: " + value);
			break;
		default:
			Debug.Log ("No matching server type ...");
			break;
		}
	}

		
	/// <summary>
	/// Wait for the answer of the GET request.
	/// </summary>
	/// <returns>Returns the www object with the server answer.</returns>
	/// <param name="www">WWW object, who sends the GET request.</param>
	public IEnumerator WaitForRequest(WWW www) {
		yield return www;
	}
}
