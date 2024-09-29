using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
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
using System.Windows.Shapes;

namespace MonteCarloMethod
{
    /// <summary>
    /// Логика взаимодействия для Task4.xaml
    /// </summary>
    public partial class Task4 : Window
    {
        private int n = 4; // номер варианта
        private PlotModel plotModel = new PlotModel { Title = "График функции f(x)" };
        private ScatterSeries scatterSeriesInner = new();
        private ScatterSeries scatterSeriesOuter = new();
        private double A = 15.0;
        private double B = 7.0;
        public Task4()
        {
            InitializeComponent();
            plotModel = new PlotModel { Title = "Circle and Random Points" };

            // Создание окружности

            var circleSeries = new LineSeries { Color = OxyColors.Blue, MarkerType = MarkerType.None };
            for (double theta = 0; theta <= 2 * Math.PI; theta += 0.01)
            {
                double r = Math.Sqrt(A * Math.Cos(theta) * Math.Cos(theta) + B * Math.Sin(theta) * Math.Sin(theta));
                double x = r * Math.Cos(theta);
                double y = r * Math.Sin(theta);
                circleSeries.Points.Add(new DataPoint(x, y));
            }
            plotModel.Series.Add(circleSeries);

            // Настройка осей
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -Math.Sqrt(A), Maximum = Math.Sqrt(A) });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -Math.Sqrt(B), Maximum = Math.Sqrt(B) });

            PlotView.Model = plotModel;
        }

        private Tuple<double, List<(double, double)>, List<(double, double)>> AreaCalculation(int N)
        {
            double a = Math.Sqrt(B), b = Math.Sqrt(A);

            List<(double, double)> random_points = new List<(double, double)>();
            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * 2 * b - b;
                double y = rnd.NextDouble() * 2 * a - a;
                random_points.Add((x, y));
            }

            List<(double, double)> inner_points = new();
            foreach ((double, double) point in random_points)
            {
                double x = point.Item1, y = point.Item2;
                if (A * Math.Pow(x, 2) + B * Math.Pow(y, 2) > Math.Pow(Math.Pow(x, 2) + Math.Pow(y, 2), 2)) 
                {
                    inner_points.Add(point);
                }
            }

            double area = (inner_points.Count / (double)N) * a * b * 4;
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
                answer_textbox.Text = $"Площадь: {Math.Round(answer.Item1, 4)}";

                double absolute_error = Math.Abs(11 * Math.PI - answer.Item1);
                absolute_error_textbox.Text = $"Абсолютная погрешность: {Math.Round(absolute_error, 2)}";

                double relative_error = absolute_error / 11 * Math.PI;
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
