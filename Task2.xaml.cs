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
using OxyPlot.Annotations;

namespace MonteCarloMethod
{
    /// <summary>
    /// Логика взаимодействия для Task2.xaml
    /// </summary>
    public partial class Task2 : Window
    {
        private int n = 4; // номер варианта
        private PlotModel plotModel = new PlotModel { Title = "График функции f(x)" };
        private ScatterSeries scatterSeriesInner = new();
        private ScatterSeries scatterSeriesOuter = new();

        public Task2()
        {
            InitializeComponent();

            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "X", Minimum = 0 };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Y", Minimum = 0 };
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            var functionSeries = new LineSeries { Title = "f(x)" };

            for (double x = 0.0; x <= 5.5; x += 0.1)
            {
                double y = f(x);
                functionSeries.Points.Add(new DataPoint(x, y));
            }
            plotModel.Series.Add(functionSeries);

            var verticalLine = new LineAnnotation
            {
                Type = LineAnnotationType.Vertical,
                X = 5,
                Color = OxyColors.Blue,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };
            plotModel.Annotations.Add(verticalLine);

            PlotView.Model = plotModel;
            PlotView.InvalidatePlot();
        }
        private double f(double x)
        {
            return Math.Sqrt(11 - 4 * Math.Pow(Math.Sin(x), 2));
        }

        private Tuple<double, List<(double, double)>, List<(double, double)>> AreaCalculation(int N)
        {
            double a = 3.31663, b = 5.0;

            List<(double, double)> random_points = new List<(double, double)>();
            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * b;
                double y = rnd.NextDouble() * a;
                random_points.Add((x, y));
            }

            List<(double, double)> inner_points = new();
            foreach ((double, double) point in random_points)
            {
                if (point.Item2 < f(point.Item1))
                {
                    inner_points.Add(point);
                }
            }

            double area = (inner_points.Count / (double)N) * a * b;
            return new Tuple<double, List<(double, double)>, List<(double, double)>>(area, random_points, inner_points);
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
                answer_textbox.Text = $"Интеграл: {Math.Round(answer.Item1, 4)}";

                double absolute_error = Math.Abs(14.8598209229187 - answer.Item1);
                absolute_error_textbox.Text = $"Абсолютная погрешность: {Math.Round(absolute_error, 4)}";

                double relative_error = absolute_error / 14.8598209229187;
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
