using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelling a heater.
/// </summary>
public class Heater : Device {
	LinearFunctionInput validator = new LinearFunctionInput();
	public override string DeviceType { get { return "Heater"; } }

    // TODO: Care. Ist cup interaction
	private float epsilon = 50; // +-50 px
	private float deltaX = 200; // 200 px movment in x direction
	private float deltaY = 200; // 200 px movment in y direction

	/// <summary>
	/// Initializes a new instance of the <see cref="Heater"/> class.
	/// </summary>
	/// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
	public Heater(string plantTag) : base(plantTag) { }

	// TODO: 
	public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
		// Validate - Care it's cup interaction 
		// string result = validator.ValidateInteraktion_dxdy_diff(storedInteraction, deltaX, deltaY, epsilon);
		// TODO: Go on with the result.
		opcuaNode local = new opcuaNode ("", "", "");

		/*switch (result) {
		case "negativ":
			sollWert.setValue ("false");
			local = sollWert;
			break;
		case "positiv":
			sollWert.setValue ("true");
			local = sollWert;
			break;
		}*/

		return null;
	}
}
