using System;
using Chroma.Form1.DeviceConnection;

namespace Chroma.Form1
{
    public partial class Main
    {
        private DeviceConfig _rohdeSchwarzConfig;
        private DeviceConfig _keithleyConfig;
        private DeviceConfig _chromaConfig;

        private void LoadDeviceConfigs()
        {
            _rohdeSchwarzConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_NAME") ?? "Rohde & Schwarz",
                ConnectionString = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_CONN") ?? "TCPIP0::169.168.1.16::5200::SOCKET"
            };

            _keithleyConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("KEITHLEY_NAME") ?? "Keithley",
                ConnectionString = Environment.GetEnvironmentVariable("KEITHLEY_CONN") ?? "GPIB0::16::INSTR"
            };

            _chromaConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("CHROMA_NAME") ?? "Chroma",
                ConnectionString = Environment.GetEnvironmentVariable("CHROMA_CONN") ?? "GPIB0::7::INSTR"
            };
        }
    }
}