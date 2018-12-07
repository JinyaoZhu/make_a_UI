using System.Collections;
using System.Collections.Generic;

public class OpenSocketObject {
	private string tag;
	public string Tag {get {return tag; }}
	private string module;
	public string Module {get {return module;}}
	private int subscriptionID;
	public int SubscriptionID {get {return subscriptionID;}}

	//constructor
	public OpenSocketObject(string _tag, string _module, int id) {
		tag = _tag;
		module = _module;
		subscriptionID = id; 
	}
}
