/// <summary>
/// Modelling a sensor.
/// </summary>
public class Sensor : Device {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="conTag">Device plant tag.</param>
	public Sensor(string conTag) : base(conTag) {}
	public override string DeviceType { get { return "Sensor"; } }

	public override void InitOPCUAData (RestAPIRequestObject resultIst) {
		istWert = new opcuaNode (resultIst.NodeId, resultIst.Value, resultIst.DataType);
	}
}
