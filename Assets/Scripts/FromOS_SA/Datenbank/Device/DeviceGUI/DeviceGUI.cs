using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Modelling a process control device with gui. 
/// </summary>
public class DeviceGUI : Device {
    // GUI - Data
    private string guiTemplateType; // store gui template type
    public string GuiTemplateType { get { return guiTemplateType; } set { guiTemplateType = value; } }
    private bool currentlyDetected; // objecte currently detectable = true; false = not detectable
    public bool CurrentlyDetected { get { return currentlyDetected; } set { currentlyDetected = value; } }
    private bool currentlyVisible; // true = visable | false = not visiable
    public bool CurrentlyVisible { get { return currentlyVisible; } set { currentlyVisible = value; } }
    private Vector3 modellPosition; // Postition of the 3D-object in the scene
    public Vector3 ModellPosition { get { return modellPosition; } set { modellPosition.x = value[0]; modellPosition.y = value[1]; modellPosition.z = value[2]; } }
    private Transform buttonTransform;
    public Transform ButtonTransfrom { get { return buttonTransform; } set { buttonTransform = value; } }
    /// <summary>
    /// A dictionary with all sub devices of this device.
    /// </summary>
    protected Dictionary<string, Device> subDevices = new Dictionary<string, Device>();
    public Dictionary<string, Device> SubDevices { get { return subDevices; } set { subDevices = value; } }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Plant tag of the device. Have to be unique on module level.</param>
    public DeviceGUI(string conTag) : base(conTag) {}

	//TODO: DeviceType wird nicht überschrieben!!
}