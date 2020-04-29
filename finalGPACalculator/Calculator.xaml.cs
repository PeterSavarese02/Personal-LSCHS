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

namespace finalGPACalculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        long num1 = 0; // First number in equation 
        long num2 = 0; // Second number in equation
        string oper = ""; // Operator used in equation

        public Calculator()
        {
            InitializeComponent();
        }

        public void ErrorCatcher(string mMessage)
        {
            if (mMessage == "")
            {
                return;
            }

            MessageBox.Show("Error:\n" + mMessage, "Error Catcher", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool IsInteger(double d) // Error checking to prevent program dying
        {
            if (d == (int)d) return true;
            else return false;
        }

        private void handleNumberClick(int val)
        {
            if (!IsInteger(val))
            {
                MessageBox.Show("ERROR\n\n Attempt to handle integer as string!");
            }

            if (num1 == 0) //Checks if first number has been chosen
            {
                num1 = val;
                calcEquText.Text = Convert.ToString(val);
            }
            else if (num2 == 0) // If we have number one but don't have number 2 in equation
            {
                if (oper == "") // An operator isn't chosen, append more to allow more numbers
                {
                    try
                    {
                        num1 = Convert.ToInt32(num1.ToString() + val.ToString());
                        // MessageBox.Show(num1.ToString());
                        calcEquText.AppendText(val.ToString());
                    }
                    catch
                    {
                        ErrorCatcher("Prevented possible overflow of Int32. Your number is above 2,147,483,647!");
                    }
                }
                else
                {
                    num2 = val;
                    calcEquText.AppendText(Convert.ToString(val));
                }
            }
            else if (num1 != 0 && num2 != 0) // If we have both values
            {
                // MessageBox.Show("You have already input two values! Automatically calculated with selected operation");
                try
                {
                    num2 = Convert.ToInt32(num2.ToString() + val.ToString());
                    // MessageBox.Show(num2.ToString());
                    calcEquText.AppendText(val.ToString());
                }
                catch
                {
                    ErrorCatcher("Prevented possible overflow of Int32. Your number is above 2,147,483,647!");
                }
            }
        }

        private void calcNumClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all vars for new eqution
            calcEquText.Text = "";
            oper = "";
            num1 = 0;
            num2 = 0;
        }

        private void numberBtnClick(object sender, RoutedEventArgs e)
        {
            // All numbered buttons in 1 function! Refer to handleNumberClick for info

            Button btn = (Button)sender;
            handleNumberClick((Convert.ToInt32(btn.Content)));
        }

        private void operationBtnClick(object sender, RoutedEventArgs e)
        {
            // Handle all operator buttons in 1 function! 
            bool canContinue = true; // Can we continue to switch for our function
            Button btn = (Button)sender;
            if (oper != "")
            {
                MessageBox.Show("An operation has already been selected for the given equation");
                canContinue = false; // No go
            }

            if (canContinue)
            {
                calcEquText.AppendText(btn.Content.ToString());
                switch (btn.Content)
                {
                    case "+":
                        oper = "+";
                        break;
                    case "-":
                        oper = "-";
                        break;
                    case "x":
                        oper = "*";
                        break;
                    case "÷":
                        oper = "/";
                        break;
                }
            }
        }

        private void calcNumEqual_Click(object sender, RoutedEventArgs e)
        {
            switch (oper)
            {
                case "+":
                    calcEquText.Text = (num1 + num2).ToString();
                    break;
                case "-":
                    calcEquText.Text = (num1 - num2).ToString();
                    break;
                case "*":
                    calcEquText.Text = (num1 * num2).ToString();
                    break;
                case "/":
                    if (num1 == 0 || num2 == 0)
                    {
                        MessageBox.Show("Error\n Prevented DivideByZero. Attempt to divide a number by 0");
                        break;
                    }
                    calcEquText.Text = (num1 / num2).ToString();
                    break;
            }

            try
            {
                num1 = Convert.ToInt32(calcEquText.Text);
                num2 = 0;
                oper = "";
            }
            catch
            {
                MessageBox.Show("Error\n Prevented possible overflow of Int32. Your number is above 2,147,483,647!", "Error Catcher", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void calcPerc_Click(object sender, RoutedEventArgs e)
        {
            // If you know math, you know this

            if (calcEquText.Text != "0" || calcEquText.Text != "")
            {
                decimal perTemp = (Convert.ToDecimal(calcEquText.Text) / 100);

                calcEquText.Text = perTemp.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int numberNegPos = Convert.ToInt32(calcEquText.Text);
            if (numberNegPos < 0)
            {
                num1 = Math.Abs(numberNegPos);
                calcEquText.Text = num1.ToString();
            }
            else
            {
                num1 = -numberNegPos;
                calcEquText.Text = num1.ToString();
            }
        }
    }
}
