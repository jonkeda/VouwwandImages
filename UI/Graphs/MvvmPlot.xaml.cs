using ScottPlot.Plottable;
using System;
using System.Windows;
using System.Windows.Input;

namespace VouwwandImages.UI.Graphs
{
    /// <summary>
    /// Interaction logic for MvvmPlot.xaml
    /// </summary>
    public partial class MvvmPlot
    {
        public MvvmPlot()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(MvvmPlot), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty PlotDataProperty = DependencyProperty.Register(
            nameof(PlotData), typeof(PlotData), typeof(MvvmPlot), new PropertyMetadata(default(PlotData), PlotDataChanged));
        private static void PlotDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MvvmPlot? plot = d as MvvmPlot;
            plot?.PlotChanged(e.NewValue as PlotData);
        }

        private void PlotChanged(PlotData? data)
        {
            if (data == null)
            {
                return;
            }
            Graph.Plot.Clear();
            Graph.Plot.Title(data.Title);
            foreach (ScatterData scatterData in data.ScatterData)
            {
                Graph.Plot.AddScatter(scatterData.DataX, scatterData.DataY, label: scatterData.Text);

            }

            Graph.Plot.Legend(data.HasLegend);

            Graph.Refresh();
        }

        public PlotData PlotData
        {
            get { return (PlotData)GetValue(PlotDataProperty); }
            set { SetValue(PlotDataProperty, value); }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            (double mouseCoordX, double mouseCoordY) = Graph.GetMouseCoordinates();
            double xyRatio = Graph.Plot.XAxis.Dims.PxPerUnit / Graph.Plot.YAxis.Dims.PxPerUnit;

            string text = "";
            var distance = double.MaxValue;
            foreach (var plot in Graph.Plot.GetPlottables())
            {
                ScatterPlot scatterPlot = plot as ScatterPlot;
                if (scatterPlot != null)
                {
                    (double pointX, double pointY, int pointIndex) =
                        scatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

                    var newDistance = CalculateDistance(mouseCoordX, mouseCoordY, pointX, pointY);

                    if (distance > newDistance)
                    {
                        var x = scatterPlot.Xs[pointIndex];
                        var y = scatterPlot.Ys[pointIndex];
                        text = $"x: {x:F2} y: {y:F2}";
                        distance = newDistance;
                    }
                }
            }

            Text = text;
        }

        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
