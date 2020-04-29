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

namespace finalGPACalculator
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

        private void showCalenderButton(object sender, RoutedEventArgs e)
        {
            Calculator Calculator = new Calculator();
            Calculator.Show();
        }

        private void showGPAButton(object sender, RoutedEventArgs e)
        {
            GPACalculator GPACalc = new GPACalculator();
            GPACalc.Show();
        }
    }
}
