using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains usefull methods to handel the mass of different devices.
/// </summary>
public class DeviceValidator {

    /// <summary>
    /// Identifies the class/type of the device and returns it.
    /// </summary>
    /// <param name="plantTag">Plant tag</param>
    /// <param name="module">Name of the module in plant</param>
    /// <param name="istIds">Mapping of opc ua node id's</param>
    /// <returns></returns>
	public Device Validator (string plantTag, string module, IstOPCUANodeIds istIds) {
		Device local;
		switch (plantTag[0].ToString()) {
		case "P":
			switch (plantTag) {
			case "PIC":
				local = new Device (plantTag);
				break;
			case "PROP_V":
				local = new Device (plantTag);
				break; 
			default:
				local = new Pump (plantTag);
				break;
			}
			break;
		case "V":
			// Valve modeld in opc ua
			if (istIds.TagToNodeId[module].ContainsKey (plantTag)) {
				// RelayValve
				if (istIds.TagToNodeId[module][plantTag].Core.Contains ("#")) {
					local = new RelayValve (plantTag);
				} else { // Normal valve
					local = new Valve (plantTag);
				}
			} else { // only modeld in Linked Data
				// Handventil. Achtung codedopplung.
				local = new HandValve (plantTag);
			}
			break;
		case "F":
			// TODO: Fix device typ
			local = new Device (plantTag);
			break;
		case "L":
			// TODO: NIcht eindeutig ob binär oder linear
			local = new BinarySensor (plantTag);
			break;
		case "R":
			local = new Mixer (plantTag); 
			break;
		case "T":
			local = new TemperatureSensor (plantTag); 
			break;
		case "B":
			local = new Tank (plantTag); 
			break;
		case "W":
            // Spechial case: WT 
			if (plantTag [1].ToString () == "T") {
				local = new DeviceGUI (plantTag);
			} else local = new Device (plantTag);
			break;
		default:
			local = new Device (plantTag);
			break; 
		}
		return local;
	}
}
