using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modeling a valve.
/// </summary>
public class Valve : DeviceGUI {
    LinearFunctionInput validator = new LinearFunctionInput();
    public override string DeviceType { get { return "Valve"; } }

    // Parameter for horizontal interaction: --
	protected float epsilon = 50; // +-50 px
	protected float deltaX = 200; // 200 px movment in x direction
	protected float deltaY = 50; // 50 px aberration in y direction

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
    public Valve(string plantTag) : base(plantTag) { }

    // TODO: 
	public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
        // Validate
        string result = validator.ValidateInteraktion_dxdy_diff(storedInteraction, deltaX, deltaY, epsilon);

		opcuaNode local = new opcuaNode ("", "", "");

		switch (result) {
		case "negativ":
			sollWert.setValue ("false");
			local = sollWert;
			writeValue++;
			break;
		case "positiv":
			sollWert.setValue ("true");
			local = sollWert;
			writeValue++;
			break;
		default:
			//writeValue = false;
			break;
		}
		return local;
    }

    /// <summary>
    /// Init the opc ua of this valve. Overrides the partent class function.
    /// </summary>
    /// <param name="resultIst"></param>
    /// <param name="resultSoll"></param>
	public override void InitOPCUAData (RestAPIRequestObject resultIst, RestAPIRequestObject resultSoll) {
		// Convert bool to valve output
		if (StringToBool(resultIst.Value)) {
			// true = open
			resultIst.Value = "Open";
		} else {
			// false = closed
			resultIst.Value = "Closed";
		}
		safeInformation (resultIst, resultSoll); // safe data
	}

    /// <summary>
    /// Converts String to bool. No System function?!?!?
    /// </summary>
    /// <param name="value">Bool value in string</param>
    /// <returns></returns>
	protected bool StringToBool (string value) {
		if (value == "true") {
			return true;
		} else {
			return false;
		}
	}
}
