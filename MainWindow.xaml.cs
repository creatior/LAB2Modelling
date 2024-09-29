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
using MonteCarloMethod;
using static System.Net.Mime.MediaTypeNames;

// TriangleArea
namespace TriangleArea
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void task1_button_Click(object sender, RoutedEventArgs e)
        {
            Task1 window = new Task1();
            window.Show();
        }

        private void task2_button_Click(object sender, RoutedEventArgs e)
        {
            Task2 window = new Task2();
            window.Show();
        }
        private void task3_button_Click(object sender, RoutedEventArgs e)
        {
            Task3 window = new Task3();
            window.Show();
        }
        private void task4_button_Click(object sender, RoutedEventArgs e)
        {
            Task4 window = new Task4();
            window.Show();
        }
        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
