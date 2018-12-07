using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to build static dictionarys for OPC UA requests.
/// </summary>
public class StaticOPCUANode {
	private string module;
	public string Module {get {return module; }}
	private string core; // NodeId or Tag
	public string Core {get {return core;}}

	// Constructor
	public StaticOPCUANode (string _module, string _core) {
		module = _module;
		core = _core;
	}
}
