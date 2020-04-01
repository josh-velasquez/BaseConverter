using BaseConverter.ConvertUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        /// <summary>
        /// Contains the list of steps to the question
        /// </summary>
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
            string newValue = value.Text.ToUpper();
            try
            {
                switch (GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString()))
                {
                    case Conversion.UnaryToBinary:
                        PopulateInstructionBox(convertUtil.printUnaryToBinaryInstructions());
                        promptUserGuess(convertUtil.UnaryToBinary(newValue));
                        break;

                    case Conversion.UnaryToDecimal:
                        PopulateInstructionBox(convertUtil.printUnaryToDecimalInstructions());
                        promptUserGuess(convertUtil.UnaryToDecimal(newValue));
                        break;

                    case Conversion.UnaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.printUnaryToHexadecimalInstructions());
                        promptUserGuess(convertUtil.UnaryToHexadecimal(newValue));
                        break;

                    case Conversion.BinaryToUnary:
                        PopulateInstructionBox(convertUtil.printBinaryToUnaryInstructions());
                        promptUserGuess(convertUtil.BinaryToUnary(newValue));
                        break;

                    case Conversion.BinaryToDecimal:
                        PopulateInstructionBox(convertUtil.printBinaryToDecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToDecimal(newValue));
                        break;

                    case Conversion.BinaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.printBinaryToHexadecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToHexadecimal(newValue));
                        break;

                    case Conversion.DecimalToUnary:
                        PopulateInstructionBox(convertUtil.printDecimalToUnaryInstructions());
                        promptUserGuess(convertUtil.DecimalToUnary(newValue));
                        break;

                    case Conversion.DecimalToBinary:
                        PopulateInstructionBox(convertUtil.printDecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.DecimalToBinary(newValue));
                        break;

                    case Conversion.DecimalToHexadecimal:
                        PopulateInstructionBox(convertUtil.printDecimalToHexadecimalInstructions());
                        promptUserGuess(convertUtil.DecimalToHexadecimal(newValue));
                        break;

                    case Conversion.HexadecimalToUnary:
                        PopulateInstructionBox(convertUtil.printHexadecimalToUnaryInstructions());
                        promptUserGuess(convertUtil.HexadecimalToUnary(newValue));
                        break;

                    case Conversion.HexadecimalToBinary:
                        PopulateInstructionBox(convertUtil.printHexadecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.HexadecimalToBinary(newValue));
                        break;

                    case Conversion.HexadecimalToDecimal:
                        PopulateInstructionBox(convertUtil.printHexadecimalToDecimalInstructions());
                        DebugPrintSteps(convertUtil.HexadecimalToDecimal(value.Text));
                        promptUserGuess(convertUtil.HexadecimalToDecimal(newValue));
                        break;
                }
            }
            catch (Exception)
            {
                ShowMessageBox("Input Error", "Invalid input!");
            }
        }

        /// <summary>
        /// Displays a pop up message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        private void ShowMessageBox(string title, string message)
        {
            MessageBox.Show(message, title);
        }

        /// <summary>
        /// Populates the instructions box
        /// </summary>
        /// <param name="steps"></param>
        private void PopulateInstructionBox(string[] steps)
        {
            foreach (string step in steps)
            {
                instructionsBox.Items.Add(step);
            }
        }

        /// <summary>
        /// Prompts the user for the intial guess. Initializes the question
        /// </summary>
        /// <param name="solution"></param>
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

        /// <summary>
        /// Checks the user's guess and sees if its correct or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckUserGuess(object sender, EventArgs e)
        {
            if (currentQuestion.Equals(default(Question))) // Check if question is null or done
            {
                return; // no question
            }
            if (currentQuestion.done)
            {
                if (userGuess.Text.ToUpper() == currentQuestion.steps.Item2)
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
            if (userGuess.Text.ToUpper() == stepAndAnswer.Item2) // check if its the right answer
            {
                stepAnswer.Content = "Correct! What's the next answer?";
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

        /// <summary>
        /// Checks if the current question is on the final answer
        /// </summary>
        private void CheckIfDone()
        {
            if (currentQuestion.counter == (currentQuestion.steps.Item1.Count))
            {
                stepAnswer.Content = "Correct! What's the final answer?";
                currentQuestion.done = true;
                return;
            }
        }

        /// <summary>
        /// Gets the enum value based on the enum description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the description of the conversion enum
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
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

        private void DebugPrintSteps(Tuple<List<Tuple<string, string>>, string> solution)
        {
            Debug.WriteLine("FINAL ANSWER: " + solution.Item2);
            foreach (var data in solution.Item1)
            {
                Debug.WriteLine("STEP: " + data.Item1 + " ANS: " + data.Item2);
            }
        }

        /// <summary>
        /// Resets and clears all the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            value.Text = "";
            stepsBox.Items.Clear();
            userGuess.Text = "";
            instructionsBox.Items.Clear();
            currentQuestion = null;
            stepAnswer.Content = "";
            stepAnswer.Foreground = Brushes.White;
        }
    }
}