/// <summary>
/// Modelling a binary sensor.
/// </summary>
public class BinarySensor : Sensor {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Device plant tag.</param>
	public BinarySensor(string conTag) : base(conTag) {}
	public override string DeviceType { get { return "BinarySensor"; } }
}
