using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
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
    /// Логика взаимодействия для Task3.xaml
    /// </summary>
    public partial class Task3 : Window
    {
        private int n = 4; // номер варианта
        private PlotModel plotModel = new PlotModel { Title = "График функции f(x)" };
        private ScatterSeries scatterSeriesInner = new();
        private ScatterSeries scatterSeriesOuter = new();
        private const double R = 4;

        public Task3()
        {
            InitializeComponent();
            plotModel = new PlotModel { Title = "Circle and Random Points" };
            
            // Создание окружности
            var circleSeries = new LineSeries { Color = OxyColors.Blue, MarkerType = MarkerType.None };
            for (double phi = 0; phi <= 2 * Math.PI; phi += 0.01)
            {
                double x = R * Math.Cos(phi);
                double y = R * Math.Sin(phi);
                circleSeries.Points.Add(new DataPoint(x, y));
            }
            plotModel.Series.Add(circleSeries);

            // Настройка осей
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -R, Maximum = R });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -R, Maximum = R });

            PlotView.Model = plotModel;
        }
        private Tuple<double, List<(double, double)>, List<(double, double)>> AreaCalculation(int N)
        {
            List<(double, double)> random_points = new List<(double, double)>();
            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * 2 * R - R;
                double y = rnd.NextDouble() * 2 * R - R;
                random_points.Add((x, y));
            }

            List<(double, double)> inner_points = new();
            foreach ((double, double) point in random_points)
            {
                if (Math.Pow(point.Item1, 2) + Math.Pow(point.Item2, 2) < Math.Pow(R, 2))
                {
                    inner_points.Add(point);
                }
            }
            double pi = 4 * (inner_points.Count / (double)N);
            return new Tuple<double, List<(double, double)>, List<(double, double)>>(pi, random_points, inner_points);
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

                answer_textbox.Text = $"Число пи: {Math.Round(answer.Item1, 4)}";

                double absolute_error = Math.Abs(Math.PI - answer.Item1);
                absolute_error_textbox.Text = $"Абсолютная погрешность: {Math.Round(absolute_error, 4)}";

                double relative_error = absolute_error / Math.PI;
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
