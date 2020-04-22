using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        /// <summary>
        /// Evaluates the expression provided by the user
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string Evaluate(string expression)
        {
            double result;
            string newExpression = expression;
            if (expression.Contains("^")) // Handles exponents
            {
                newExpression = ProcessExponent(newExpression);
            }
            if (newExpression.Contains("//")) // Handles floor division
            {
                result = Math.Floor(Convert.ToDouble(new DataTable().Compute(StripExtraDiv(newExpression), null)));
                return Math.Floor(result).ToString();
            }
            return Convert.ToDouble(new DataTable().Compute(newExpression, null)).ToString();
        }

        /// <summary>
        /// Splits the user input based on the delimeters. Used for custom computing
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<string> CustomSplit(string text)
        {
            var delimiters = new char[] { '^', '(', ')', '*', '-', '+', '%', '/' };
            List<string> result = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                if (Array.IndexOf(delimiters, text[i]) == -1)
                {
                    string left = "";
                    string right = "";
                    for (int j = i - 1; j > 0; j--)
                    {
                        if (Array.IndexOf(delimiters, text[j]) > -1)
                        {
                            break;
                        }
                        else
                        {
                            left = text[j] + left;
                        }
                    }
                    int incremented = -1;
                    for (int k = i; k < text.Length; k++)
                    {
                        if (Array.IndexOf(delimiters, text[k]) > -1)
                        {
                            break;
                        }
                        else
                        {
                            right += text[k];
                            incremented++;
                        }
                    }
                    result.Add(left + right);
                    i += incremented;
                }
                else
                {
                    result.Add(text[i].ToString());
                }
            }
            return result.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        /// <summary>
        /// Handles exponent arithmetic as DataTable.compute cannot calculate it
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string ProcessExponent(string expression)
        {
            List<string> expr = CustomSplit(expression);
            List<string> calculated = new List<string>();
            string newExpression = "";
            for (int i = 0; i < expr.Count; i++)
            {
                if (expr[i] == "^")
                {
                    calculated.RemoveAt(calculated.Count - 1);
                    calculated.Add(Math.Pow(double.Parse(expr[i - 1]), double.Parse(expr[i + 1])).ToString());
                    i++;
                }
                else
                {
                    calculated.Add(expr[i]);
                }
            }
            foreach (var val in calculated)
            {
                newExpression += val;
            }
            return newExpression;
        }

        /// <summary>
        /// Strips the extra / in //
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                MessageBox.Show("Invalid input.", "Error");
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