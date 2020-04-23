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
            // Start window in the center screen
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Populate dropdown menu
            foreach (var val in Enum.GetValues(typeof(Conversion)))
            {
                conversionCombobox.Items.Add(GetDescription(val));
            }
            conversionCombobox.SelectedIndex = 0;
            ShowInstructions();
        }

        /// <summary>
        /// When convert button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConvertClick(object sender, RoutedEventArgs e)
        {
            stepsBox.Items.Clear();
            instructionsBox.Items.Clear();
            if (value.Text == "" || !convertUtil.IsValidInput(GetSelectedConversion(), value.Text))
            {
                ShowMessageBox("Input Error", "Invalid input! Please enter a valid value.");
                return;
            }
            PopulateInstructionBox(convertUtil.GetInstructions(GetSelectedConversion()));
            try
            {
                stepAnswer.Text = "";
                PromptUserGuess(convertUtil.GetSteps(GetSelectedConversion(), value.Text.ToUpper()));
            }
            catch (Exception)
            {
                ShowMessageBox("Input Error", "Invalid input! Make sure you have no white spaces between your values and is a valid value.");
            }
        }

        /// <summary>
        /// Gets the conversion value from the dropdown menu
        /// </summary>
        /// <returns></returns>
        private Conversion GetSelectedConversion()
        {
            return GetEnum<Conversion>(conversionCombobox.SelectedItem.ToString());
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
            return newValue;
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
                if (step.Contains("Answer"))
                {
                    instructionsBox.Items.Add(new ListBoxItem { Content = step, Background = Brushes.LightGreen });
                }
                else
                {
                    instructionsBox.Items.Add(step);
                }
            }
        }

        /// <summary>
        /// Prompts the user for the intial guess. Initializes the question
        /// </summary>
        /// <param name="solution"></param>
        private void PromptUserGuess(Tuple<List<Tuple<string, string, StepType>>, string> solution)
        {
            stepAnswer.Foreground = Brushes.White;
            stepAnswer.Text = "Your turn to guess! Enter your answer below.";
            currentQuestion = new Question()
            {
                done = false,
                steps = solution,
                counter = 0,
                conversionType = GetSelectedConversion()
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
            ResetEverything();
        }

        /// <summary>
        /// Clears everything and resets everything
        /// </summary>
        private void ResetEverything()
        {
            value.Text = "";
            stepsBox.Items.Clear();
            userGuess.Text = "";
            instructionsBox.Items.Clear();
            currentQuestion = null;
            stepAnswer.Text = "";
            stepAnswer.Foreground = Brushes.White;
            workspaceBox.Clear();
            PopulateInstructionBox(convertUtil.GetInstructions(GetSelectedConversion()));
        }

        /// <summary>
        /// Check user guess click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckUserGuess(object sender, RoutedEventArgs e)
        {
            CheckUserGuess();
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
            table.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            table.Show();
        }

        /// <summary>
        /// Captures enter button press on answer box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ShowMessageBox("Error", "No conversion available. Please enter a conversion value and click the convert button.");
                return; // no question
            }
            if (currentQuestion.done || currentQuestion.steps.Item1[0].Item3 == StepType.SingleSolution)
            {
                if (userGuess.Text.ToUpper() == currentQuestion.steps.Item2 || userGuess.Text.ToUpper() == AppendPrefix(currentQuestion.conversionType, currentQuestion.steps.Item2))
                {
                    stepAnswer.Text = "Congrats! You got it right!";
                    stepAnswer.Foreground = Brushes.LightGreen;
                    stepsBox.Items.Add("Final Answer: " + currentQuestion.steps.Item2);
                    currentQuestion = null;
                    ScrollResultsToBottom();
                }
                else
                {
                    stepAnswer.Text = "Wrong! Please try again.";
                    stepAnswer.Foreground = Brushes.Red;
                }
                userGuess.Text = "";
                return;
            }
            var stepAndAnswer = currentQuestion.steps.Item1;
            int indexOfSoln = GetIndexSolution();
            if (userGuess.Text.ToUpper() == stepAndAnswer[indexOfSoln].Item2 || userGuess.Text.ToUpper() == AppendPrefix(currentQuestion.conversionType, stepAndAnswer[indexOfSoln].Item2)) // check if its the right answer
            {
                stepAnswer.Text = "Correct! What's the next answer?";
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
                    stepAnswer.Text = "Correct! What's the final answer?";
                    currentQuestion.done = true;
                    return;
                }
            }
            else
            {
                stepAnswer.Text = "Wrong! Try again!";
                stepAnswer.Foreground = Brushes.Red;
                userGuess.Text = "";
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
            if (currentQuestion == null)
            {
                ShowMessageBox("Error", "No conversion available. Please enter a conversion value and click the convert button.");
                return;
            }
            else if (currentQuestion.done)
            {
                stepAnswer.Text = "What's the final answer?";
                stepAnswer.Foreground = Brushes.White;
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
                stepAnswer.Text = "What's the final answer?";
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
            ResetEverything();
            var randomConversion = convertUtil.GenerateRandomConversion();
            conversionCombobox.SelectedIndex = randomConversion.Item1;
            value.Text = randomConversion.Item2;
        }

        /// <summary>
        /// Updates the conversion instructions box when selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instructionsBox.Items.Clear();
            PopulateInstructionBox(convertUtil.GetInstructions(GetSelectedConversion()));
        }

        /// <summary>
        /// Shows the instructions page
        /// </summary>
        private void ShowInstructions()
        {
            Instructions instructions = new Instructions();
            instructions.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            instructions.Topmost = true;
            instructions.Show();
        }

        /// <summary>
        /// Instruction icon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInstructionsClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowInstructions();
        }

        /// <summary>
        /// Used for debugging
        /// </summary>
        /// <param name="result"></param>
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

        private void OnShowCalculatorClick(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            calculator.Show();
        }
    }
}