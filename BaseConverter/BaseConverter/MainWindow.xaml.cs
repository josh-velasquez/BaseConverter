using BaseConverter.ConvertUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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
            public Tuple<List<Tuple<string, string, StepType>>, string> steps;
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
                        PopulateInstructionBox(convertUtil.PrintUnaryToBinaryInstructions());
                        //    promptUserGuess(convertUtil.UnaryToBinary(newValue));
                        break;

                    case Conversion.UnaryToDecimal:
                        PopulateInstructionBox(convertUtil.PrintUnaryToDecimalInstructions());
                        //    promptUserGuess(convertUtil.UnaryToDecimal(newValue));
                        break;

                    case Conversion.UnaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.PrintUnaryToHexadecimalInstructions());
                        // promptUserGuess(convertUtil.UnaryToHexadecimal(newValue));
                        break;

                    case Conversion.BinaryToUnary:
                        PopulateInstructionBox(convertUtil.PrintBinaryToUnaryInstructions());
                        //    promptUserGuess(convertUtil.BinaryToUnary(newValue));
                        break;

                    case Conversion.BinaryToDecimal:
                        PopulateInstructionBox(convertUtil.PrintBinaryToDecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToDecimal(newValue));
                        break;

                    case Conversion.BinaryToHexadecimal:
                        PopulateInstructionBox(convertUtil.PrintBinaryToHexadecimalInstructions());
                        promptUserGuess(convertUtil.BinaryToHexadecimal(newValue));
                        break;

                    case Conversion.DecimalToUnary:
                        PopulateInstructionBox(convertUtil.PrintDecimalToUnaryInstructions());
                        //   promptUserGuess(convertUtil.DecimalToUnary(newValue));
                        break;
                        
                    case Conversion.DecimalToBinary:
                        PopulateInstructionBox(convertUtil.PrintDecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.DecimalToBinary(newValue));
                        break;

                    case Conversion.DecimalToHexadecimal:
                        PopulateInstructionBox(convertUtil.PrintDecimalToHexadecimalInstructions());
                        promptUserGuess(convertUtil.DecimalToHexadecimal(newValue));
                        break;

                    case Conversion.HexadecimalToUnary:
                        PopulateInstructionBox(convertUtil.PrintHexadecimalToUnaryInstructions());
                        // promptUserGuess(convertUtil.HexadecimalToUnary(newValue));
                        break;

                    case Conversion.HexadecimalToBinary:
                        PopulateInstructionBox(convertUtil.PrintHexadecimalToBinaryInstructions());
                        promptUserGuess(convertUtil.HexadecimalToBinary(newValue));
                        break;

                    case Conversion.HexadecimalToDecimal:
                        PopulateInstructionBox(convertUtil.PrintHexadecimalToDecimalInstructions());
                        promptUserGuess(convertUtil.HexadecimalToDecimal(newValue));
                        break;
                }
            }
            catch (Exception)
            {
                ShowMessageBox("Input Error", "Invalid input!");
            }
        }

        private void test(Tuple<List<Tuple<string, string, StepType>>, string> result)
        {
            foreach (var step in result.Item1)
            {
                if (step.Item3 == StepType.SubStep)
                {
                    Debug.WriteLine("\tStep: " + step.Item1 + " Ans: " + step.Item2 + " Type: " + step.Item3);
                }
                else
                {
                    Debug.WriteLine("Step: " + step.Item1 + " Ans: " + step.Item2 + " Type: " + step.Item3);
                }
            }
            Debug.WriteLine("FINAL RESULT: " + result.Item2);
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
        private void promptUserGuess(Tuple<List<Tuple<string, string, StepType>>, string> solution)
        {
            stepAnswer.Content = "Your turn to guess! What is the result of the first step?";
            currentQuestion = new Question()
            {
                done = false,
                steps = solution,
                counter = 0
            };
        }

        private int GetIndexSolution()
        {
            int counter = currentQuestion.counter;
            while (currentQuestion.steps.Item1[counter].Item3 != StepType.Solution)
            {
                counter++;
            }
            return counter;
        }

        private void ScrollResultsToBottom()
        {
            var border = (Border)VisualTreeHelper.GetChild(stepsBox, 0);
            var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            scrollViewer.ScrollToBottom();
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

        private void OnCheckUserGuess(object sender, RoutedEventArgs e)
        {
            if (currentQuestion == null) // Check if question is null or done
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
                    ScrollResultsToBottom();
                }
                else
                {
                    stepAnswer.Content = "Wrong! Please try again.";
                    stepAnswer.Foreground = Brushes.Red;
                }
                return;
            }
            var stepAndAnswer = currentQuestion.steps.Item1;
            int indexOfSoln = GetIndexSolution();
            if (userGuess.Text.ToUpper() == stepAndAnswer[indexOfSoln].Item2) // check if its the right answer
            {
                stepAnswer.Content = "Correct! What's the next answer?";
                stepAnswer.Foreground = Brushes.LightGreen;
                stepsBox.Items.Add(stepAndAnswer[currentQuestion.counter].Item1 + " = " + stepAndAnswer[currentQuestion.counter].Item2);
                if (stepAndAnswer[currentQuestion.counter].Item3 == StepType.MainStep)
                {
                    currentQuestion.counter++;
                    while (stepAndAnswer[currentQuestion.counter].Item3 != StepType.Solution)
                    {
                        stepsBox.Items.Add("\t" + stepAndAnswer[currentQuestion.counter].Item1 + " = " + stepAndAnswer[currentQuestion.counter].Item2);
                        currentQuestion.counter++;
                    }
                    stepsBox.Items.Add(stepAndAnswer[currentQuestion.counter].Item1 + " = " + stepAndAnswer[currentQuestion.counter].Item2);
                }
                userGuess.Text = "";
                currentQuestion.counter++;
                ScrollResultsToBottom();
                // Check if guessing the final answer
                if (currentQuestion.counter == (currentQuestion.steps.Item1.Count))
                {
                    stepAnswer.Content = "Correct! What's the final answer?";
                    currentQuestion.done = true;
                    return;
                }
            }
            else
            {
                stepAnswer.Content = "Wrong! Try again!";
                stepAnswer.Foreground = Brushes.Red;
            }
        }

        private void OnHelpClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
    }
}