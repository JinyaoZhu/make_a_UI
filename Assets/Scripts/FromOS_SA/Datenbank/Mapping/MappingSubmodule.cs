using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Mapping groups of devices to submoduls. 
/// </summary>
public class MappingSubmodule {
	private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> submodule = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
	public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> Submodule {get {return submodule; }}

	public MappingSubmodule () {
		buildAbfüllen ();
        buildFilter();
        buildReaktor();
	}

	private void buildAbfüllen() {
		List<string> local = new List<string> ();
		Dictionary<string, List<string>> dic_in = new Dictionary<string, List<string>>();
		Dictionary<string, Dictionary<string, List<string>>> dic_out = new Dictionary<string, Dictionary<string, List<string>>> ();

		// B401
		local.Add ("L402");
		local.Add ("L401");
		local.Add ("L410");
		dic_in.Add ("levelBinary", local);
		dic_out.Add ("B401", dic_in);

		// B402
		local = new List<string> ();
		dic_in = new Dictionary<string, List<string>>();
		//dic_out = new Dictionary<string, Dictionary<string, List<string>>> ();
		local.Add ("L411");
		dic_in.Add ("levelBinary", local);
		local = new List<string> ();
		local.Add ("L412");
		dic_in.Add ("levelAnalog", local);
		dic_out.Add ("B402", dic_in);

		// Conveyor1
		/*local = new List<string> ();
		dic_in = new Dictionary<string, List<string>>();
		dic_out = new Dictionary<string, Dictionary<string, List<string>>> ();
		local.Add ("4B4");
		local.Add ("4B6");
		local.Add ("4B7");
		dic_in.Add ("levelBinary", local);
		dic_out.Add ("Conveyor1", dic_in);*/

		submodule.Add ("Abfüllen", dic_out);
	}

	private void buildFilter () {
		List<string> local = new List<string> ();
		Dictionary<string, List<string>> dic_in = new Dictionary<string, List<string>>();
		Dictionary<string, Dictionary<string, List<string>>> dic_out = new Dictionary<string, Dictionary<string, List<string>>> ();
		// ... some submodules ...
		submodule.Add ("Filter", dic_out);
	}

	private void buildReaktor () {
		List<string> local = new List<string> ();
		Dictionary<string, List<string>> dic_in = new Dictionary<string, List<string>>();
		Dictionary<string, Dictionary<string, List<string>>> dic_out = new Dictionary<string, Dictionary<string, List<string>>> ();

		//B301
		local.Add ("L303");
		local.Add ("L302");
		local.Add ("L301");
		dic_in.Add ("levelBinary", local);
		local = new List<string> ();
		local.Add ("L304");
		dic_in.Add ("levelAnalog", local);
		local = new List<string> ();
		local.Add ("T301");
		dic_in.Add ("temp", local);
		//local = new List<string> ();
		//local.Add ("R301");
		//dic_in.Add("agitator", local);
		//local = new List<string> ();
		//local.Add ("W303");
		//dic_in.Add("heater", local);
		dic_out.Add ("B301", dic_in);

		submodule.Add ("Reaktor", dic_out);
	}
}
