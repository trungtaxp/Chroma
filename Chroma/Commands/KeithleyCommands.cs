namespace Chroma
{
    public class KeithleyCommands : ICommands
    {
        public string MeasureVoltage() => ":MEAS:VOLT:DC?";
        public string MeasureCurrent() => ":MEAS:CURR?";
        public string SetVoltageRange() => ":SENS:VOLT:RANG";
        public string SetCurrentRange() => ":SENS:CURR:RANG";
        public string SetOutputState() => ":OUTP:STAT";
        public string Identify() => "*IDN?";
        public string Reset() => "*RST";
        public string ClearStatus() => "*CLS";
        public string ReadStatusByte() => "*STB?";
        public string SelfTest() => "*TST?";
    }
}