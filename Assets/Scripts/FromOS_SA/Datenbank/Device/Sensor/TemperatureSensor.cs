/// <summary>
/// Modeling a temperature sensor.
/// </summary>
public class TemperatureSensor : Sensor {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Device plant tag.</param>
	public TemperatureSensor(string conTag) : base(conTag) {}
	public override string DeviceType { get { return "TemperatureSensor"; } }
}
