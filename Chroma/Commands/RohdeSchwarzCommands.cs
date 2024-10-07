namespace Chroma
{
    public class RohdeSchwarzCommands : ICommands
    {
        public string MeasureVoltage() => "MEAS:VOLT?";
        public string MeasureCurrent() => "MEAS:CURR?";
        public string SetVoltageRange() => "SOUR:VOLT:RANG";
        public string SetCurrentRange() => "SOUR:CURR:RANG";
        public string SetOutputState() => "OUTP:STAT";
        public string Identify() => "*IDN?";
        public string Reset() => "*RST";
        public string ClearStatus() => "*CLS";
        public string ReadStatusByte() => "*STB?";
        public string SelfTest() => "*TST?";
        public string FetchData() => "FETCH:DATA?";
    }
}