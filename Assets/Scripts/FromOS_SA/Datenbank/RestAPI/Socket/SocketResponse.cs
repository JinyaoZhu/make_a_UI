using UnityEngine;
using System.Collections;

/// <summary>
/// Class to serialize socket answer from OPC UA Server.
/// </summary>
[System.Serializable]
public class SocketResponse {
	public int subscriptionId;
	public string value;
	public string timestamp;
	public string nodeId; 
}