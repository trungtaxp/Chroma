using System;
using Chroma.Service;

namespace Chroma.Form1
{
    public partial class Main
    {
        private DeviceConfig rohdeSchwarzConfig;
        private DeviceConfig keithleyConfig;
        private DeviceConfig chromaConfig;

        private void LoadDeviceConfigs()
        {
            rohdeSchwarzConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_NAME") ?? "Rohde & Schwarz",
                ConnectionString = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_CONN") ?? "TCPIP0::192.168.1.30::5200::SOCKET"
            };

            keithleyConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("KEITHLEY_NAME") ?? "Keithley",
                ConnectionString = Environment.GetEnvironmentVariable("KEITHLEY_CONN") ?? "GPIB0::16::INSTR"
            };

            chromaConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("CHROMA_NAME") ?? "Chroma",
                ConnectionString = Environment.GetEnvironmentVariable("CHROMA_CONN") ?? "GPIB0::7::INSTR"
            };
        }
    }
}