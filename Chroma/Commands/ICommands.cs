namespace Chroma.Commands
{
    public interface ICommands
    {
        string MeasureVoltage();
        string MeasureCurrent();
        string SetVoltageRange();
        string SetCurrentRange();
        string SetOutputState();
        string Identify();
        string Reset();
        string ClearStatus();
        string ReadStatusByte();
        string SelfTest();
    }
}