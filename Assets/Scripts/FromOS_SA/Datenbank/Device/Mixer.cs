using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelling a mixer.
/// </summary>
public class Mixer : Device {
	LinearFunctionInput validator = new LinearFunctionInput();
	public override string DeviceType { get { return "Mixer"; } }

	// Parameter for horizontal interaction: --
	private float epsilon = 50; // +-50 px
	private float deltaX = 200; // 200 px movment in x direction
	private float deltaY = 200; // 200 px aberration in y direction

	/// <summary>
	/// Initializes a new instance of the <see cref="Heater"/> class.
	/// </summary>
	/// <param name="plantTag">Plant tag of the device. Have to be unique on module level.</param>
	public Mixer(string plantTag) : base(plantTag) { }

    /// <summary>
    /// Checks if interaction matches to Mixer.  
    /// </summary>
    /// <param name="storedInteraction"></param>
    /// <returns></returns>
    public override opcuaNode CheckInteraction(List<Vector2> storedInteraction) {
		// Validate input
		string result = validator.ValidateInteraktion_dxdy_same(storedInteraction, deltaX, deltaY, epsilon);

        // Build retun value
		opcuaNode local = new opcuaNode ("", "", "");
        // TODO: In eigene Funktion auslagern, wegen codedopplung
		switch (result) {
		case "negativ":
			sollWert.setValue ("false");
			local = sollWert;
			break;
		case "positiv":
			sollWert.setValue ("true");
			local = sollWert;
			break;
		}
			
		//writeValue = true;
		return local;
	}
}
