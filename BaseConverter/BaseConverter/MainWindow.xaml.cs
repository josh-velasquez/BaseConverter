using BaseConverter.ConvertUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace BaseConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class Question
        {
            public bool done;
            public int counter;
            public Tuple<List<Tuple<string, string>>, string> steps;
        }

        private Question currentQuestion;

        private ConvertUtil convertUtil = new ConvertUtil();

        public MainWindow()
        {
            InitializeComponent();
            foreach (var val in Enum.GetValues(typeof(Conversion)))
            {
                conversionCombobox.Items.Add(GetDescription(val));
            }
            conversionCombobox.SelectedIndex = 0;
        }

        private void OnConvertClick(object sender, RoutedEventArgs e)
        {
            if (value.Text == "" || value.Text == "Value")
            {
                ShowMessageBox("Input Error", "Invalid input!");
                return;
            }
            instructionsBox.Items.Clear();
            try
            {
                switch (GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString()))
                {
                    case Conversion.UnaryToBinary:
                        PopulateInstructionBox(convertUtil.printUnaryToBinaryInstructions());
                        promptUserGuess(convertUtil.UnaryToBinary(value.Text));
                        break;

                    case Conversion.UnaryToDecimal:
                        PopulateInstructionBox(convertUtil.printUnaryToDecimalInstructions());
                        promptUserGuess(convertUtil.UnaryToDecimal(value.Text));
                        break;

                    case Conversion.UnaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.printUnaryToHexadecimalInstructions());
                        promptUserGuess(convertUtil.UnaryToHexadecimal(value.Text));
                        break;

                    case Conversion.BinaryToUnary:
                        PopulateInstructionBox(convertUtil.printBinaryToUnaryInstructions());
                        promptUserGuess(convertUtil.BinaryToUnary(value.Text));
                        break;

                    case Conversion.BinaryToDecimal:
                        PopulateInstructionBox(convertUtil.printBinaryToDecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToDecimal(value.Text));
                        break;

                    case Conversion.BinaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.printBinaryToHexadecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToHexadecimal(value.Text));
                        break;

                    case Conversion.DecimalToUnary:
                        PopulateInstructionBox(convertUtil.printDecimalToUnaryInstructions());
                        promptUserGuess(convertUtil.DecimalToUnary(value.Text));
                        break;

                    case Conversion.DecimalToBinary:
                        PopulateInstructionBox(convertUtil.printDecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.DecimalToBinary(value.Text));
                        break;

                    case Conversion.DecimalToHexadecimal:
                        PopulateInstructionBox(convertUtil.printDecimalToHexadecimalInstructions());
                        promptUserGuess(convertUtil.DecimalToHexadecimal(value.Text));
                        break;

                    case Conversion.HexadecimalToUnary:
                        PopulateInstructionBox(convertUtil.printHexadecimalToUnaryInstructions());
                        promptUserGuess(convertUtil.HexadecimalToUnary(value.Text));
                        break;

                    case Conversion.HexadecimalToBinary:
                        PopulateInstructionBox(convertUtil.printHexadecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.HexadecimalToBinary(value.Text));
                        break;

                    case Conversion.HexadecimalToDecimal:
                        PopulateInstructionBox(convertUtil.printHexadecimalToDecimalInstructions());
                        promptUserGuess(convertUtil.HexadecimalToDecimal(value.Text));
                        break;
                }
            }
            catch (Exception)
            {
                ShowMessageBox("Input Error", "Invalid input!");
            }
        }

        private void ShowMessageBox(string title, string message)
        {
            MessageBox.Show(message, title);
        }

        private void PopulateInstructionBox(string[] steps)
        {
            foreach (string step in steps)
            {
                instructionsBox.Items.Add(step);
            }
        }

        private void promptUserGuess(Tuple<List<Tuple<string, string>>, string> solution)
        {
            stepAnswer.Content = "Your turn to guess! What is the result of the first step?";
            submitAnswer.Click += new RoutedEventHandler(CheckUserGuess);
            currentQuestion = new Question()
            {
                done = false,
                steps = solution,
                counter = 0
            };
        }

        private void CheckUserGuess(object sender, EventArgs e)
        {
            if (currentQuestion.Equals(default(Question))) // Check if question is null or done
            {
                return; // no question
            }
            if (currentQuestion.done)
            {
                if (userGuess.Text == currentQuestion.steps.Item2)
                {
                    stepAnswer.Content = "Congrats! You got it right!";
                    stepAnswer.Foreground = Brushes.LightGreen;
                    stepsBox.Items.Add("Final Answer: " + currentQuestion.steps.Item2);
                    currentQuestion = null;
                }
                else
                {
                    stepAnswer.Content = "Wrong! Please try again.";
                    stepAnswer.Foreground = Brushes.Red;
                }
                return;
            }
            CheckIfDone();
            var stepAndAnswer = currentQuestion.steps.Item1[currentQuestion.counter];
            if (userGuess.Text == stepAndAnswer.Item2) // check if its the right answer
            {
                stepAnswer.Content = "Correct!";
                stepAnswer.Foreground = Brushes.LightGreen;
                currentQuestion.counter++;
                stepsBox.Items.Add(stepAndAnswer.Item1 + " = " + stepAndAnswer.Item2);
                CheckIfDone();
            }
            else
            {
                stepAnswer.Content = "Wrong! Try again!";
                stepAnswer.Foreground = Brushes.Red;
            }
        }

        private void CheckIfDone()
        {
            if (currentQuestion.counter == (currentQuestion.steps.Item1.Count))
            {
                stepAnswer.Content = "Correct! What's the final answer?";
                currentQuestion.done = true;
                return;
            }
        }

        private static T GetEnum<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }

        private void ClearEverything()
        {
            value.Text = "";
            stepsBox.Items.Clear();
            userGuess.Text = "";
            instructionsBox.Items.Clear();
        }

        private static string GetDescription(object e)
        {
            return e
            .GetType()
            .GetMember(e.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description
        ?? e.ToString();
        }
    }
}