using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelling a hand valve.
/// </summary>
public class HandValve : Valve {



	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
	public HandValve(string plantTag) : base(plantTag) { }

	/// <summary>
	/// Handvalves no opc ua data. Override valve function
	/// </summary>
	/// <param name="result">Result.</param>
	/// <param name="resultIst">Result ist.</param>
	/// <param name="resultSoll">Result soll.</param>
	public override void InitOPCUAData (RestAPIRequestObject resultIst, RestAPIRequestObject resultSoll) {
        // TODO: Fix this! Logical error? Copy and Paste shit? 
        safeInformation (resultIst, resultSoll);
	}

	public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
		//opcuaNode local = new opcuaNode ("", "", "");
		return new opcuaNode ("", "", "");
	}
}