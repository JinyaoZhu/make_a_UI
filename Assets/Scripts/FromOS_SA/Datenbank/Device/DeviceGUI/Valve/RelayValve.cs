using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modeling a relay valve.
/// </summary>
public class RelayValve : Valve {
	LinearFunctionInput validator = new LinearFunctionInput();
	public override string DeviceType { get { return "RelayValve"; } }

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
	public RelayValve(string plantTag) : base(plantTag) { }

	// TODO: 
	public void CheckInteraktion(List<Vector2> storedInteraction) {
		// Validate
		string result = validator.ValidateInteraktion_dxdy_diff(storedInteraction, deltaX, deltaY, epsilon);
		// TODO: Go on with the result.
	}
}