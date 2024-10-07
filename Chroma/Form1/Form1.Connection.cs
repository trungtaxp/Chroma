namespace Chroma
{
    public partial class Form1
    {
        private async void ConnectToAllDevices()
        {
            var rohdeSchwarzConnection = new RohdeSchwarzConnection(rohdeSchwarzConfig, rohdeSchwarzCommands, rohdeSchwarzGroupBox);
            var keithleyConnection = new KeithleyConnection(keithleyConfig, keithleyCommands, keithleyGroupBox);
            var chromaConnection = new ChromaConnection(chromaConfig, chromaCommands, chromaGroupBox);

            await rohdeSchwarzConnection.ConnectAsync();
            await keithleyConnection.ConnectAsync();
            await chromaConnection.ConnectAsync();
        }
    }
}