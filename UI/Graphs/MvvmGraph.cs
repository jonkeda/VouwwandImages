using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VouwwandImages.UI.Graphs
{
    public class ScatterDataCollection : Collection<ScatterData>
    {}

    public class ScatterData
    {
        public ScatterData() {}

        public ScatterData(string text, List<double> dataX, List<double> dataY)
        {
            Text = text;
            DataX = dataX.ToArray();
            DataY = dataY.ToArray();
        }

        public string Text { get; set; }

        public double[] DataX { get; set; } = { };

        public double[] DataY { get; set; } = { };
    }

    public class PlotData
    {
        public PlotData()
        {

        }

        public PlotData(ScatterData scatterData)
        {
            ScatterData.Add(scatterData);
        }

        public PlotData(ScatterDataCollection scatterData)
        {
            ScatterData = scatterData;
        }

        public ScatterDataCollection ScatterData { get; set; } = new ();

        public string Title { get; set; } = "";
        public bool HasLegend { get; set; } = true;
    }
}
