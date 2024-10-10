using Chroma.Form1.DeviceConnection;

namespace Chroma.Form1
{
    public partial class Main
    {
        private async void ConnectToAllDevices()
        {
            var rohdeSchwarzConnection = new RohdeSchwarzConnection(_rohdeSchwarzConfig, _rohdeSchwarzCommands, _rohdeSchwarzGroupBox);
            var keithleyConnection = new KeithleyConnection(_keithleyConfig, _keithleyCommands, _keithleyGroupBox);
            var chromaConnection = new ChromaConnection(_chromaConfig, _chromaCommands, _chromaGroupBox);

            await rohdeSchwarzConnection.ConnectAsync();
            await keithleyConnection.ConnectAsync();
            await chromaConnection.ConnectAsync();
        }
    }
}