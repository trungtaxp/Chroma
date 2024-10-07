using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ivi.Visa;
using IviVisaExtended;

namespace Chroma
{
    public partial class Form1
    {
        private async Task ShowRohdeSchwarzDataAsync(ICommands commands)
        {
            try
            {
                if (commands is RohdeSchwarzCommands rohdeSchwarzCommands)
                {
                    var formattedIO = (IMessageBasedFormattedIO)_ConnectDrive;
                    formattedIO.Write(rohdeSchwarzCommands.FetchData() + "\n");
                    var dataResponse = await Task.Run(() => formattedIO.ReadString());
                    var dataPoints = dataResponse.Split(',').Select(double.Parse).ToArray();

                    Chart dataChart = new Chart
                    {
                        Location = new System.Drawing.Point(10, 110),
                        Size = new System.Drawing.Size(400, 300)
                    };
                    functionGroupBox.Controls.Add(dataChart);

                    var series = new Series
                    {
                        Name = "Data",
                        Color = System.Drawing.Color.Blue,
                        ChartType = SeriesChartType.Line
                    };

                    dataChart.Series.Add(series);

                    for (int i = 0; i < dataPoints.Length; i++)
                    {
                        series.Points.AddXY(i, dataPoints[i]);
                    }

                    dataChart.Invalidate();
                }
                else
                {
                    MessageBox.Show("FetchData is not supported by this device.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error");
            }
        }
    }
}