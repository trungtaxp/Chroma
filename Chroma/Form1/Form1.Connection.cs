using Chroma.Form1.DeviceConnection;

namespace Chroma.Form1
{
    public partial class Main
    {
        private async void ConnectToAllDevices()
        {
            var rohdeSchwarzConnection = new RohdeSchwarzConnection(rohdeSchwarzConfig, _rohdeSchwarzCommands, rohdeSchwarzGroupBox);
            var keithleyConnection = new KeithleyConnection(keithleyConfig, _keithleyCommands, keithleyGroupBox);
            var chromaConnection = new ChromaConnection(chromaConfig, _chromaCommands, chromaGroupBox);

            await rohdeSchwarzConnection.ConnectAsync();
            await keithleyConnection.ConnectAsync();
            await chromaConnection.ConnectAsync();
        }
    }
}