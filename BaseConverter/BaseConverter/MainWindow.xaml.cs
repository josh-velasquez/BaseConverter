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
            public Conversion conversionType;
        }

        private Question currentQuestion;

        private ConvertUtil convertUtil = new ConvertUtil();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            foreach (var val in Enum.GetValues(typeof(Conversion)))
            {
                conversionCombobox.Items.Add(GetDescription(val));
            }
            conversionCombobox.SelectedIndex = 0;

            // Show instructions page
            Instructions instructions = new Instructions();
            instructions.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            instructions.Topmost = true;
            instructions.Show();
        }

        private void OnConvertClick(object sender, RoutedEventArgs e)
        {
            if (value.Text == "" || !IsValidInput(GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString()), value.Text))
            {
                ShowMessageBox("Input Error", "Invalid input! Please enter a valid value.");
                return;
            }
            string newValue = value.Text.ToUpper();
            try
            {
                switch (GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString()))
                {
                    case Conversion.UnaryToBinary:
                        PromptUserGuess(convertUtil.UnaryToBinary(newValue));
                        break;

                    case Conversion.UnaryToDecimal:
                        PromptUserGuess(convertUtil.UnaryToDecimal(newValue));
                        break;

                    case Conversion.UnaryToHexadecimal:
                        PromptUserGuess(convertUtil.UnaryToHexadecimal(newValue));
                        break;

                    case Conversion.BinaryToUnary:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.BinaryToUnary(newValue));
                        break;

                    case Conversion.BinaryToDecimal:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.BinaryToDecimal(newValue));
                        break;

                    case Conversion.BinaryToHexadecimal:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.BinaryToHexadecimal(newValue));
                        break;

                    case Conversion.DecimalToUnary:
                        PromptUserGuess(convertUtil.DecimalToUnary(newValue));
                        break;

                    case Conversion.DecimalToBinary:
                        PromptUserGuess(convertUtil.DecimalToBinary(newValue));
                        break;

                    case Conversion.DecimalToHexadecimal:
                        PromptUserGuess(convertUtil.DecimalToHexadecimal(newValue));
                        break;

                    case Conversion.HexadecimalToUnary:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.HexadecimalToUnary(newValue));
                        break;

                    case Conversion.HexadecimalToBinary:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.HexadecimalToBinary(newValue));
                        break;

                    case Conversion.HexadecimalToDecimal:
                        newValue = StripPrefix(newValue);
                        PromptUserGuess(convertUtil.HexadecimalToDecimal(newValue));
                        break;
                }
            }
            catch (Exception)
            {
                ShowMessageBox("Input Error", "Invalid input! Make sure you have no white spaces between your values and is a valid value.");
            }
        }

        private bool IsValidInput(Conversion conversionType, string input)
        {
            switch (conversionType)
            {
                case Conversion.UnaryToBinary:

                case Conversion.UnaryToDecimal:

                case Conversion.UnaryToHexadecimal:
                    return IsUnary(input);

                case Conversion.BinaryToUnary:

                case Conversion.BinaryToDecimal:

                case Conversion.BinaryToHexadecimal:
                    return IsBinary(input);

                case Conversion.DecimalToUnary:

                case Conversion.DecimalToBinary:

                case Conversion.DecimalToHexadecimal:
                    return IsDecimal(input);

                case Conversion.HexadecimalToUnary:

                case Conversion.HexadecimalToBinary:

                case Conversion.HexadecimalToDecimal:
                    return IsHexadecimal(input);
            }
            return false;
        }

        /// <summary>
        /// Check if user input is a unary value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsUnary(string value)
        {
            foreach (var chr in value)
            {
                if (chr != '1')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if user input is a binary value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsBinary(string value)
        {
            char[] validCharacters = new char[] { '0', '1' };
            string newValue = value;
            if (value.ToUpper().Contains("0B"))
            {
                newValue = value.Replace("0B", "");
            }
            foreach (var chr in newValue)
            {
                if (!Array.Exists(validCharacters, E => E == chr))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the user input is a decimal value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsDecimal(string value)
        {
            if (int.TryParse(value, out _))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the user input is a hexadecimal value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsHexadecimal(string value)
        {
            char[] validCharacters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            string newValue = value.ToUpper();
            if (newValue.ToUpper().Contains("0X"))
            {
                newValue = value.Replace("0X", "");
            }
            foreach (var chr in newValue)
            {
                if (!Array.Exists(validCharacters, E => E == chr))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Appends prefixes to answer to compare with user guess
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string AppendPrefix(Conversion conversionType, string value)
        {
            string newValue = value;
            switch (conversionType)
            {
                case Conversion.UnaryToBinary:

                case Conversion.DecimalToBinary:

                case Conversion.HexadecimalToBinary:
                    newValue = value.Insert(0, "0B");
                    break;

                case Conversion.UnaryToHexadecimal:

                case Conversion.BinaryToHexadecimal:

                case Conversion.DecimalToHexadecimal:
                    newValue = value.Insert(0, "0X");
                    break;
            }
            Debug.WriteLine(newValue);
            return newValue;
        }

        /// <summary>
        /// Strips prefixes from user's guess (0x,0b)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string StripPrefix(string value)
        {
            string newValue = value;
            if (value.Contains("0X"))
            {
                newValue = value.Replace("0X", "");
            }
            else if (value.Contains("0B"))
            {
                newValue = value.Replace("0B", "");
            }
            return newValue;
        }

        private void DebugAnswer(Tuple<List<Tuple<string, string, StepType>>, string> result)
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
            Debug.WriteLine("Final Answer: " + result.Item2);
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
        private void PromptUserGuess(Tuple<List<Tuple<string, string, StepType>>, string> solution)
        {
            stepAnswer.Foreground = Brushes.White;
            stepAnswer.Content = "Your turn to guess! Enter your answer below.";
            currentQuestion = new Question()
            {
                done = false,
                steps = solution,
                counter = 0,
                conversionType = GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString())
            };
        }

        /// <summary>
        /// Gets the solution index for the step
        /// </summary>
        /// <returns></returns>
        private int GetIndexSolution()
        {
            int counter = currentQuestion.counter;
            while (currentQuestion.steps.Item1[counter].Item3 != StepType.Solution)
            {
                counter++;
            }
            return counter;
        }

        /// <summary>
        /// Scrolls to the bottom of the steps box
        /// </summary>
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
            workspaceBox.Clear();
        }

        private void OnCheckUserGuess(object sender, RoutedEventArgs e)
        {
            CheckUserGuess();
        }

        /// <summary>
        /// Toggles the help window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHelpClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        /// <summary>
        /// Clears the workspace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClearWorkspace(object sender, RoutedEventArgs e)
        {
            workspaceBox.Clear();
        }

        /// <summary>
        /// Shows the conversion table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowConversionTableClick(object sender, RoutedEventArgs e)
        {
            ConversionTable table = new ConversionTable();
            table.Show();
        }

        private void OnEnterPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                CheckUserGuess();
            }
        }

        /// <summary>
        /// Checks the users solution to the algorithms solution
        /// </summary>
        private void CheckUserGuess()
        {
            if (currentQuestion == null) // Check if question is null or done
            {
                return; // no question
            }
            if (currentQuestion.done || currentQuestion.steps.Item1[0].Item3 == StepType.SingleSolution)
            {
                if (userGuess.Text.ToUpper() == currentQuestion.steps.Item2 || userGuess.Text.ToUpper() == AppendPrefix(currentQuestion.conversionType, currentQuestion.steps.Item2))
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
            if (userGuess.Text.ToUpper() == stepAndAnswer[indexOfSoln].Item2 || userGuess.Text.ToUpper() == AppendPrefix(currentQuestion.conversionType, stepAndAnswer[indexOfSoln].Item2)) // check if its the right answer
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

        /// <summary>
        /// Shows the final answer to the question. Resets the question once clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowFinalAnswerClick(object sender, RoutedEventArgs e)
        {
            if (currentQuestion == null)
            {
                ShowMessageBox("Error", "No conversion available. Please enter a conversion value and click the convert button.");
            }
            else
            {
                stepsBox.Items.Add("Final answer: " + currentQuestion.steps.Item2);
                currentQuestion = null;
            }
        }

        /// <summary>
        /// Shows the partial answer to the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowAnswerClick(object sender, RoutedEventArgs e)
        {
            if (currentQuestion == null || currentQuestion.done)
            {
                stepAnswer.Content = "";
                return;
            }
            var stepAndAnswer = currentQuestion.steps.Item1;
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
            if (currentQuestion.counter == (currentQuestion.steps.Item1.Count))
            {
                stepAnswer.Content = "What's the final answer?";
                stepAnswer.Foreground = Brushes.White;
                currentQuestion.done = true;
                return;
            }
        }

        /// <summary>
        /// Generates a random question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGenerateRandomConversionClick(object sender, RoutedEventArgs e)
        {
            var randomConversion = convertUtil.GenerateRandomConversion();
            conversionCombobox.SelectedIndex = randomConversion.Item1;
            value.Text = randomConversion.Item2;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instructionsBox.Items.Clear();
            string newValue = value.Text.ToUpper();

            switch (GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString()))
            {
                case Conversion.UnaryToBinary:
                    PopulateInstructionBox(convertUtil.PrintUnaryToBinaryInstructions());
                    break;

                case Conversion.UnaryToDecimal:
                    PopulateInstructionBox(convertUtil.PrintUnaryToDecimalInstructions());
                    break;

                case Conversion.UnaryToHexadecimal:
                    PopulateInstructionBox(convertUtil.PrintUnaryToHexadecimalInstructions());
                    break;

                case Conversion.BinaryToUnary:
                    PopulateInstructionBox(convertUtil.PrintBinaryToUnaryInstructions());
                    break;

                case Conversion.BinaryToDecimal:
                    PopulateInstructionBox(convertUtil.PrintBinaryToDecimalInstructions());
                    break;

                case Conversion.BinaryToHexadecimal:
                    PopulateInstructionBox(convertUtil.PrintBinaryToHexadecimalInstructions());
                    break;

                case Conversion.DecimalToUnary:
                    PopulateInstructionBox(convertUtil.PrintDecimalToUnaryInstructions());
                    break;

                case Conversion.DecimalToBinary:
                    PopulateInstructionBox(convertUtil.PrintDecimalToBinaryInstructions());
                    break;

                case Conversion.DecimalToHexadecimal:
                    PopulateInstructionBox(convertUtil.PrintDecimalToHexadecimalInstructions());
                    break;

                case Conversion.HexadecimalToUnary:
                    PopulateInstructionBox(convertUtil.PrintHexadecimalToUnaryInstructions());
                    break;

                case Conversion.HexadecimalToBinary:
                    PopulateInstructionBox(convertUtil.PrintHexadecimalToBinaryInstructions());
                    break;

                case Conversion.HexadecimalToDecimal:
                    PopulateInstructionBox(convertUtil.PrintHexadecimalToDecimalInstructions());
                    break;
            }
        }
    }
}