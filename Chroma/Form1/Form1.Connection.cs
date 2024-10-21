using Chroma.Form1.DeviceConnection;

namespace Chroma.Form1
{
    public partial class Main
    {
        private RohdeSchwarzConnection _rohdeSchwarzConnection;
        private KeithleyConnection _keithleyConnection;
        private ChromaConnection _chromaConnection;

        private async void ConnectToAllDevices()
        {
            _rohdeSchwarzConnection = new RohdeSchwarzConnection(_rohdeSchwarzConfig, _rohdeSchwarzCommands, _rohdeSchwarzGroupBox);
            _keithleyConnection = new KeithleyConnection(_keithleyConfig, _keithleyCommands, _keithleyGroupBox);
            _chromaConnection = new ChromaConnection(_chromaConfig, _chromaCommands, _chromaGroupBox);

            await _rohdeSchwarzConnection.ConnectAsync();
            await _keithleyConnection.ConnectAsync();
            await _chromaConnection.ConnectAsync();
        }
    }
}