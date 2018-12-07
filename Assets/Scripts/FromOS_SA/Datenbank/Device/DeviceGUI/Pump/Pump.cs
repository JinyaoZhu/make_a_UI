using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modeling a pump.
/// </summary>
public class Pump : DeviceGUI {
    /// <summary>
    /// Holds functions to determine your device.
    /// </summary>
    LinearFunctionInput validator = new LinearFunctionInput();

    public override string DeviceType { get { return "Pump"; } }

    // Parameter for horizontal interaction: |
    private float epsilon = 50; // +-50 px
	// Later on normal x becomes y and normal y becomes x! Transformation!
    private float deltaX = 200; // 200 px aberration in x direction
    private float deltaY = 50; // threshold: 50 px movment in y direction. 

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
    public Pump(string plantTag) : base(plantTag) { }

    /// <summary>
    /// Check if interaction request matches to pump interaction gesture.
    /// </summary>
    /// <param name="storedInteraction">List of 2D vectors. Holds the complete interaction.</param>
    /// <returns>Returns a opc ua node. Contains all data to write the new value on the opc ua server.</returns>
	public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
        // Swap X with Y values to use standard function in validator
        List<Vector2> inverse = new List<Vector2>();
        foreach (Vector2 element in storedInteraction) {
            inverse.Add(new Vector2(element.y, element.x));
        }

        // Validate. Notice: deltaX and deltaY in inverse context. 
        string result = validator.ValidateInteraktion_dxdy_diff(inverse, deltaX, deltaY, epsilon);

		opcuaNode local = new opcuaNode ("", "", ""); 

		// TODO: Codedopplung
		switch (result) {
		    case "negativ": // negative touch movment
			    sollWert.setValue ("false");
			    local = sollWert;
				writeValue++;
				break;
		    case "positiv": // positive touch movment.
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
}
