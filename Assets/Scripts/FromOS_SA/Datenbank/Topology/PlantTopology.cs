using System.Collections;
using System.Collections.Generic;

public class PlantTopology {
	private Dictionary<string, Dictionary<string,TopologyObject>> local = new Dictionary<string, Dictionary<string,TopologyObject>>();
	private Dictionary<string, TopologyObject> sub = new Dictionary<string, TopologyObject>();

	private void BuildMischen () {
		local.Clear(); // Clean up
		sub.Clear(); // Safe - Clean up
		// B201
		sub.Add("L201", new TopologyObject("bottom"));
		sub.Add("L202", new TopologyObject("top"));
		sub.Add("L210", new TopologyObject("ontop"));
		local.Add ("B201", sub);
		sub.Clear(); // Clean up
		// B202
		sub.Add("L203", new TopologyObject("bottom"));
		sub.Add("L211", new TopologyObject("ontop"));
		local.Add ("B202", sub);
		sub.Clear(); // Clean up
		// B203
		sub.Add("L204", new TopologyObject("bottom"));
		sub.Add("L212", new TopologyObject("ontop"));
		local.Add ("B203", sub);
		sub.Clear(); // Clean up
		// B204
		sub.Add("L204", new TopologyObject("bottom"));
		sub.Add("L205", new TopologyObject("middle"));
		sub.Add("L213", new TopologyObject("top"));
		sub.Add("L214", new TopologyObject("ontop"));
		local.Add ("B204", sub);
	}

	private void BuildReaktor () {
		local.Clear(); // Clean up
		sub.Clear(); // Safe - Clean up
		// B301
		sub.Add("L303", new TopologyObject("bottom"));
		sub.Add("L302", new TopologyObject("middle"));
		sub.Add("L301", new TopologyObject("top"));
		sub.Add("L304", new TopologyObject("ontop"));
		//sub.Add ("3M4", new TopologyObject (""));
		sub.Add("T301", new TopologyObject("bottom"));
		//sub.Add("W303", new TopologyObject(""));
		sub.Add("R301", new TopologyObject("ontop"));
		local.Add ("B301", sub);
	}

	public Dictionary<string, Dictionary<string,TopologyObject>> ModuleSubmodels (string module) {
		
		switch (module) {
		case "Mischen":
			BuildMischen ();
			break;
		case "Reaktor":
			BuildReaktor ();
			break;
		default: 
			break;
		}
		return local;
	}
	// ...
}
