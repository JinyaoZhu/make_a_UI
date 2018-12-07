/// <summary>
/// Modelling linear fluid level sensor.
/// </summary>
public class LinearFluidLevelSensor : Sensor {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Device plant tag.</param>
	public LinearFluidLevelSensor(string conTag) : base(conTag) {}
	public override string DeviceType { get { return "LinearFluidLevelSensor"; } }
}
