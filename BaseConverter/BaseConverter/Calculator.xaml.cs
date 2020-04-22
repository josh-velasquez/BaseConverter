using System;
using System.Data;
using System.Windows;

namespace BaseConverter
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void OnCheckUserGuess(object sender, RoutedEventArgs e)
        {
        }

        private void LeftBracketClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("(");
        }

        private void RightBracketClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText(")");
        }

        private void SevenClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("7");
        }

        private void EightClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("8");
        }

        private void NineClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("9");
        }

        private void PowerClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("^");
        }

        private void FourClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("4");
        }

        private void FiveClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("5");
        }

        private void SixClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("6");
        }

        private void DivideClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("/");
        }

        private void OneClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("1");
        }

        private void TwoClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("2");
        }

        private void ThreeClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("3");
        }

        private void MultiplyClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("*");
        }

        private void PeriodClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText(".");
        }

        private void ZeroClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("0");
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("+");
        }

        private void SubtractClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("-");
        }

        private void ModuloClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("%");
        }

        private void FloorDivideClick(object sender, RoutedEventArgs e)
        {
            inputBox.AppendText("//");
        }

        private string Evaluate(string expression)
        {
            double result;

            if (expression.Contains("^") {
                return Convert.ToDouble(new DataTable().Compute(ProcessExponent(expression), null)).ToString()
            }
            if (expression.Contains("//"))
            {
                result = Math.Floor(Convert.ToDouble(new DataTable().Compute(StripExtraDiv(expression), null)));
                return Math.Floor(result).ToString();
            }
            return Convert.ToDouble(new DataTable().Compute(expression, null)).ToString();
        }

        private string ProcessExponent(string expression)
        {

            return ":";
        }

        private string StripExtraDiv(string expression)
        {
            return expression.Replace("//", "/");
        }

        private void EqualClick(object sender, RoutedEventArgs e)
        {
            try
            {
                resultBox.Text = Evaluate(inputBox.Text).ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Invalid input. Error: " + exception, "Error");
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            resultBox.Clear();
            inputBox.Clear();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                inputBox.Text = inputBox.Text.Remove(inputBox.Text.Length - 1, 1);
            }
        }
    }
}