using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Numerics;
using System.Security.Cryptography;
using System.Windows.Media.Animation;

namespace TriangleArea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int n = 4; // номер варианта
        private PlotModel plotModel = new PlotModel { Title = "График функции f(x)" };
        private ScatterSeries scatterSeriesInner;
        private ScatterSeries scatterSeriesOuter;


        public MainWindow()
        {
            InitializeComponent();
            
            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "X" };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Y" };
            plotModel.Axes.Add(xAxis); 
            plotModel.Axes.Add(yAxis);

            var functionSeries = new LineSeries { Title = "f(x)" };

            for (double x = 0.0; x <= n; x += 0.2)
            {
                double y = 10 * x / n;
                functionSeries.Points.Add(new DataPoint(x, y));
            }
            for (double x = n; x <= 20; x += 0.2)
            {
                double y = 10 * (x - 20) / (n - 20);
                functionSeries.Points.Add(new DataPoint(x, y));
            }
            plotModel.Series.Add(functionSeries);

            //PlotView.Model = plotModel;
            //List<(double, double)> inner_points = AreaCalculation();

            //ScatterSeries scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            //foreach (var point in inner_points)
            //{
            //    scatterSeries.Points.Add(new ScatterPoint(point.Item1, point.Item2));
            //}

            //plotModel.Series.Add(scatterSeries); PlotView.Model = plotModel;
        }

        private double f(double x)
        {
            if (x < 4)
            {
                return 10.0 * x / n;
            }
            else
            {
                return 10 * (x - 20) / (n - 20);
            }
        }

        private Tuple<double, List<(double, double)>, List<(double, double)>> AreaCalculation(int N)
        {
            double a = 10.0, b = 20.0;

            List<(double, double)> random_points = new List<(double, double)>();
            Random rnd = new Random();
            (double, double) theoretical_MX = (b / 2.0, a / 2.0);
            (double, double) theoretical_DX = (Math.Pow(b, 2.0) / 12.0, Math.Pow(a, 2.0) / 12.0);
            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * b;
                double y = rnd.NextDouble() * a;
                random_points.Add((x, y));
            }
            //double error = 0.1;
            //List<(double, double)> selected_points = new List<(double, double)>();

            //foreach ((double, double) x in random_points)
            //{
            //    (double, double) MX = (x.Item1 / 2.0, x.Item2 / 2.0);
            //    (double, double) DX = (Math.Pow(x.Item1, 2.0) / 12.0, Math.Pow(x.Item2, 2.0) / 12.0);
            //    if (Math.Abs(MX.Item1 - theoretical_MX.Item1) < error 
            //     && Math.Abs(MX.Item2 - theoretical_MX.Item2) < error
            //     && Math.Abs(DX.Item1 - theoretical_DX.Item1) < error
            //     && Math.Abs(DX.Item2 - theoretical_DX.Item2) < error)
            //    {
            //        selected_points.Add(x);
            //    }
            //}
            
            List<(double, double)> inner_points = new();
            foreach ((double, double) point in  random_points)
            {
                if (point.Item2 < f(point.Item1))
                {
                    inner_points.Add(point);
                }
            }

            double area = (inner_points.Count / (double)N) * a * b;
            return new Tuple<double, List<(double, double)>, List<(double, double)>> (area, random_points, inner_points);
        }

        private void calculate_button_Click(object sender, RoutedEventArgs e)
        {

            if (N_textbox.Text != "")
            {
                var seriesToRemoveInner = plotModel.Series.FirstOrDefault(s => s.Title == "Внутренние точки") as ScatterSeries;
                var seriesToRemoveOuter = plotModel.Series.FirstOrDefault(s => s.Title == "Внешние точки") as ScatterSeries;

                if (seriesToRemoveInner != null && seriesToRemoveOuter != null)
                {
                    plotModel.Series.Remove(seriesToRemoveInner);
                    plotModel.Series.Remove(seriesToRemoveOuter);
                }
                int N = Int32.Parse(N_textbox.Text);
                Tuple<double, List<(double, double)>, List<(double, double)>> answer = AreaCalculation(N);
                answer_textbox.Text = $"Площадь треугольника: {Math.Round(answer.Item1, 4)}";

                double absolute_error = Math.Abs(100.0 - answer.Item1);
                absolute_error_textbox.Text = $"Абсолютная погрешность: {Math.Round(absolute_error, 2)}";

                double relative_error = absolute_error / 100.0;
                relative_error_textbox.Text = $"Относительная погрешность: {Math.Round(relative_error, 4)}";

                List<(double, double)> random_points = answer.Item2;
                List<(double, double)> inner_points = answer.Item3;

                List<(double, double)> outer_points = random_points.Except(inner_points).ToList();

                this.scatterSeriesInner = new ScatterSeries { Title = "Внутренние точки", MarkerType = MarkerType.Circle, MarkerFill = OxyColors.Orange, MarkerSize = 4 };
                foreach (var point in inner_points)
                {
                    this.scatterSeriesInner.Points.Add(new ScatterPoint(point.Item1, point.Item2));
                }

                this.scatterSeriesOuter = new ScatterSeries { Title = "Внешние точки", MarkerType = MarkerType.Circle, MarkerFill = OxyColors.Red, MarkerSize = 4 };
                foreach (var point in outer_points)
                {
                    this.scatterSeriesOuter.Points.Add(new ScatterPoint(point.Item1, point.Item2));
                }

                plotModel.Series.Add(scatterSeriesInner);
                plotModel.Series.Add(scatterSeriesOuter);
                PlotView.Model = plotModel;
                PlotView.InvalidatePlot();
            }
        }
    }
}
