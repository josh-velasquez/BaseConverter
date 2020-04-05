using System;
using System.Collections.Generic;

/// <summary>
/// Contains the conversion algorithms along with their instructions
///
/// Returns Examplanation
/// Tuple<List<Tuple<string, string, StepType>>, string>
/// Tuple<List<Tuple<steps, answer to the step, type of step>>, final answer>
/// To achieve the user prompts for each steps, a tuple that contains a list of tuple
///     contains the steps to arrive to the answer, the answer to that specific step
///     (for checking if the user guess is correct or not), type of step which dictates
///     whether the step added.
///     StepType : MainStep = Just a header that doesn't require a check answer
///             against the users guess (used for clarification)
///     StepType : SubStep = A simple substep in conversion (this checks for users input)
///     StepType : Solution = This indicates the end of a substep (this checks for user input)
///     StepType : SingleSolution = For simple conversion that is just one step
/// </summary>
namespace BaseConverter.ConvertUtils
{
    internal class ConvertUtil
    {
        public string[] PrintUnaryToBinaryInstructions()
        {
            return new string[] { "Unary is based on base 1 numerical system. Binary is based on base 2 numerical system.",
                "To easily do conversions between unary to binary, it is easier to convert unary first to a decimal value and then conver that to binary",
                "Step 1: Count the number of 1's in your value and convert that value to a decimal",
                "Step 2: Once you have the decimal value, floor divide that value by 2 and write down the remainder",
                "Step 3: Take the quotient and further floor divide that by two and again write down the remainder",
                "Step 4: Continue to floor divide the quotient until the remainder is 0",
                "Step 5: Once the quotient equal to 0 is reached, read the remainders from bottom to top (from your recent remainder calculation to the oldest)",
                "Enter your value for the remainder below and see if you got it correct!",
                "",
                "Example: 111111 Unary to Binary",
                "Applying Step 1, we get its decimal equivalent to 6. We then floor divide this value by 2 and take note of the remainder.",
                "So we get the following:",
                "6 % 2 = 0      (Answer = 0)",
                "3 % 2 = 1      (Answer = 1)",
                "1 % 2 = 1      (Answer = 1)",
                "Then we apply Step 5 and simply read the results from bottom to up so we get, 110 in binary",
                "Therefore, 111111 in Unary is equivalent to 110 in binary.",
                "Final Answer = 110"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> UnaryToBinary(string unary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = 0;
            string step;
            string finalAnswer = "";
            for (int i = 0; i < unary.Length; i++)
            {
                value++;
            }
            steps.Add(Tuple.Create("Decimal value ", value.ToString(), StepType.MainStep));
            while (value != 0)
            {
                step = value + " % " + 2;
                value = Math.DivRem(value, 2, out int remainder);
                steps.Add(Tuple.Create(step, remainder.ToString(), StepType.Solution));
                finalAnswer = finalAnswer.Insert(0, remainder.ToString());
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintUnaryToDecimalInstructions()
        {
            return new string[] {"Unary is based on base 1 numerical system",
                "Step 1: Count the number of 1's",
                "Enter your answer below and see if you got it correct!",
                "",
                "Example: 1111 Unary to Decimal",
                "By simply applying Step 1, we get the decimal value of 4",
                "Final Answer = 4"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> UnaryToDecimal(string unary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int finalAnswer = 0;
            for (int i = 0; i < unary.Length; i++)
            {
                finalAnswer++;
            }
            steps.Add(Tuple.Create("", finalAnswer.ToString(), StepType.SingleSolution));
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        public string[] PrintUnaryToHexadecimalInstructions()
        {
            return new string[] { "Unary is based on base 1 numerical system. Hexadecimal is based on base 16 numerical system",
                "To make the conversion between unary to hexadecimal, it is easier to convert the unary value to decimal first then to hexadecimal",
                "Step 1: Convert the unary value to decimal by counting the amount of 1's there are",
                "Step 2: Floor divide the decimal value by 16. Convert the remainder to its hexadecimal equivalent",
                "Step 3: Write down the remainder and floor divide the previous quotient by 16",
                "Step 4: Repeat these steps until the quotient is equal to 0",
                "Step 5: Once you have the values calculated, simply write the remainder values (in hexadecimal) from bottom to top",
                "Enter the remainder for each divison below (converted to hexadecimal) and see if you got it correct!",
                "",
                "Example: 1111111111 Unary to Hexadecimal",
                "Apply Step 1, we get the decimal value of 10. From here we can floor divide until we get the quotient of 0",
                "So we get the following:",
                "10 % 16 = 10       (Answer = A)",
                "Note that 10 in hexadecimal is equivalent to 'A' (checkout the conversion table if you need help)",
                "Since this case we only floor divided once, our final answer is just A",
                "Final Answer = A"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> UnaryToHexadecimal(string unary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = 0;
            string step;
            string finalAnswer = "";
            string remVal;
            for (int i = 0; i < unary.Length; i++)
            {
                value++;
            }
            steps.Add(Tuple.Create("Decimal value", value.ToString(), StepType.MainStep));
            while (value != 0)
            {
                step = value + " % " + 16;
                value = Math.DivRem(value, 16, out int remainder);
                remVal = ConvertDecimalToHex(remainder);
                finalAnswer = finalAnswer.Insert(0, remVal);
                steps.Add(Tuple.Create(step, remVal, StepType.Solution));
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintBinaryToUnaryInstructions()
        {
            return new string[] { "Binary is based on base 2 numerical system. Unary is based on base 1 numerical system",
                "To convert between binary to unary, it is easier to first convert the binary value to decimal and then to unary",
                "Step 1: Starting with the very last digit or otherwise known as the least significant bit (LSB), raise 2 to the power of its index (starting from 0)",
                "Step 2: You then take this value and multiply it to the bit at that specific index",
                "Step 3: Do this repeatedly until you reach the very first digit or otherwise known as the most significant bit (MSB)",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the binary value",
                "Step 5: Count out your decimal starting from 1 and for every value, write down a 1",
                "Enter the decimal value you calculate from the conversion below and then enter its unary value",
                "",
                "Example: 1101 Binary to Unary",
                "Applying Step 1 on the value '1' and index 0 we get (2^0)*1 = 1, where 1 is its decimal value",
                "So we get the following:",
                "(2^0)*1 = 1        (Answer = 1)",
                "(2^1)*0 = 0        (Answer = 0)",
                "(2^2)*1 = 4        (Answer = 4)",
                "(2^3)*1 = 8        (Answer = 8)",
                "Once we have the decimal values, we simply just add all the answers up to get the total decimal equivalent value",
                "1 + 0 + 4 + 8 = 13",
                "Then we simply just count out the decimal value by representing each count by a 1, so we get 1111111111111",
                "Final Answer = 1111111111111"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> BinaryToUnary(string binary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            string step;
            double answer = 0.0;
            string finalAnswer = "";
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                double tempAnswer = Math.Pow(2, binary.Length - 1 - i) * int.Parse(binary[i].ToString());
                step = "(2^" + (binary.Length - 1 - i) + ")*" + binary[i].ToString();
                answer += tempAnswer;
                steps.Add(Tuple.Create(step, tempAnswer.ToString(), StepType.Solution));
            }
            for (int i = 0; i < answer; i++)
            {
                finalAnswer += "1";
            }
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        public string[] PrintBinaryToDecimalInstructions()
        {
            return new string[] { "Binary is based on the base 2 number system. Each position of the digit corresponds to the power they are to be raised in",
                "Step 1: Starting with the very last digit or otherwise known as the least significant digit (LSB), raise 2 to the power of its index (starting from 0)",
                "Step 2: You then take this value and multiply it to the bit at that specific index",
                "Step 3: Do this repeatedly until you reach the very first digit or otherwise known as the most significant bit",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the binary value",
                "Enter the result of each calculated bit below and see if you got it correct!",
                "",
                "Example: 1011 Decimal to Binary",
                "Applying Step 1, we get the bit 1 as the MSB and an index of 0, so we get (2^0)*1 = 1, where 1 is its decimal value",
                "So we get the following:",
                "(2^0)*1 = 1        (Answer = 1)",
                "(2^1)*1 = 2        (Answer = 2)",
                "(2^2)*0 = 0        (Answer = 0)",
                "(2^3)*1 = 8        (Answer = 8)",
                "Once we have the decimal values, we simply just add them up, so we get 11",
                "Final Answer = 11"};
        }

        /// <summary>
        /// Converts binary to decimal
        /// </summary>
        /// <param name="binary"></param>
        /// <returns>Tuple<List<Tuple<step,answer>>, finalAnswer></returns>
        public Tuple<List<Tuple<string, string, StepType>>, string> BinaryToDecimal(string binary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            string step;
            double finalAnswer = 0.0;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                double tempAnswer = Math.Pow(2, binary.Length - 1 - i) * int.Parse(binary[i].ToString());
                step = "(2^" + (binary.Length - 1 - i) + ")*" + binary[i].ToString();
                finalAnswer += tempAnswer;
                steps.Add(Tuple.Create(step, tempAnswer.ToString(), StepType.Solution));
            }
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        public string[] PrintBinaryToHexadecimalInstructions()
        {
            return new string[] { "Binary is based on the base 2 number system. Hexadecimal is based on the base 16 number system",
                "For ever four bits, represents one hexedecimal bit",
                "Step 1: Separate the bits into fours starting from the very last value or the least significant bit",
                "Step 2: Once separated, simply convert each four bits to its decimal counterpart",
                "Step 3: Convert the decimal to its hexadecimal counterpart",
                "Step 4: Repeat Steps 2-3 for each four bits that was separated in the beginning",
                "Step 5: Once calculated, simply read the values from bottom to top (your most recent hexadecimal calculation to the oldest)",
                "Enter each hexadecimal value that you calculated for each four bits below and see if you got it correct!",
                "",
                "Example: 01111100 Binary to Hexadecimal",
                "For the first four bits we get 1100, where we get the following",
                "(2^0)*0 = 0",
                "(2^1)*0 = 0",
                "(2^2)*1 = 4",
                "(2^3)*1 = 8",
                "Then we simply add this value up to get its decimal value, 12, which is equivalent to the hexadecimal value 'C' (checkout the conversion table if you need help)",
                "Answer = C",
                "Then we process the next four bits, which are 0111, where we get the following (Note we reset the index back to 0)",
                "(2^0)*1 = 1",
                "(2^1)*1 = 2",
                "(2^2)*1 = 4",
                "(2^3)*0 = 0",
                "Which we add up leading to its decimal value, 7, which is equivalent to the hexadecimal value '7'",
                "Answer = 7",
                "Then we simply read the resulting hexadecimal values from top to bottom to get 7C",
                "Final Answer = 7C"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> BinaryToHexadecimal(string binary)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            string finalAnswer = "";
            string step;
            string hexBit = "";
            double dec = 0.0;
            string newBinary = binary;
            while (newBinary.Length % 4 != 0)
            {
                newBinary = newBinary.Insert(0, "0");
            }
            for (int i = newBinary.Length - 1; i >= 0; i--)
            {
                hexBit += newBinary[i];
                if (i % 4 == 0) // Separate bits into four
                {
                    hexBit = ReverseString(hexBit);
                    steps.Add(Tuple.Create("Binary Value ", hexBit, StepType.MainStep));
                    for (int j = hexBit.Length - 1; j >= 0; j--)
                    {
                        double tempVal = Math.Pow(2, hexBit.Length - 1 - j) * int.Parse(hexBit[j].ToString());
                        step = "(2^" + (hexBit.Length - 1 - j) + ")*" + hexBit[j].ToString();
                        dec += tempVal;
                        steps.Add(Tuple.Create(step, tempVal.ToString(), StepType.SubStep));
                    }
                    steps.Add(Tuple.Create("Result ", ConvertDecimalToHex((int)dec), StepType.Solution));
                    finalAnswer = finalAnswer.Insert(0, ConvertDecimalToHex((int)dec));
                    hexBit = "";
                    dec = 0.0;
                }
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintDecimalToUnaryInstructions()
        {
            return new string[] { "The unary number system is based on base 1 numerical system.",
                "Each value is denoted by 1",
                "Step 1: Count out your decimal starting from 1 and for every value, write down a 1",
                "Enter the resulting unary value you below and see if you got it correct!",
                "",
                "Example: 6 Decimal to Unary",
                "By applying Step 1, we simply get 111111",
                "Final Answer = 111111"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> DecimalToUnary(string dec)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = int.Parse(dec);
            string finalAnswer = "";
            for (int i = 0; i < value; i++)
            {
                finalAnswer += "1";
            }
            steps.Add(Tuple.Create("", finalAnswer.ToString(), StepType.SingleSolution));
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintDecimalToBinaryInstructions()
        {
            return new string[] { "Binary is based on the base 2 number system",
                "Step 1: Floor divide the decimal value by 2",
                "Step 2: Write down the remainder and floor divide the previous quotient by 2",
                "Step 3: Repeat these steps until the quotient is equal to 0",
                "Step 4: Once the quotient is 0, read the remainders from bottom to top (from your recent remainder calculation to the oldest remainder calculation)",
                "Enter each of the remainders of your calculation below and see if you got it correct!",
                "",
                "Example: 14 Decimal to Binary",
                "By continuously applying Step 1 and Step 2 we get the following:",
                "14 % 2 = 0     (Answer = 0)",
                "7 % 2 = 1      (Answer = 1)",
                "3 % 2 = 1      (Answer = 1)",
                "1 % 2 = 1      (Answer = 1)",
                "Then we simply apply Step 4 and read it from bottom to top we get, 1110",
                "Final Answer = 1110"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> DecimalToBinary(string dec)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = int.Parse(dec);
            string step;
            string finalAnswer = "";
            while (value != 0)
            {
                step = value + " % " + 2;
                value = Math.DivRem(value, 2, out int remainder);
                steps.Add(Tuple.Create(step, remainder.ToString(), StepType.Solution));
                finalAnswer = finalAnswer.Insert(0, remainder.ToString());
            }
            return Tuple.Create(steps, finalAnswer);
        }

        /// <summary>
        /// Instructions for converting decimal to hexadecimal
        /// </summary>
        /// <returns></returns>
        public string[] PrintDecimalToHexadecimalInstructions()
        {
            return new string[] { "Decimal is based on the 10 base number system while hexadecimal is based on the base 16 number system ",
                "Step 1: Floor divide the decimal value by 16. Convert the remainder to its hexadecimal equivalent",
                "Step 2: Write down the remainder and floor divide the previous quotient by 16",
                "Step 3: Repeat these steps until the quotient is equal to 0",
                "Step 4: Once you have the values calculated, simply write the remainder values (in hexadecimal) from bottom to top",
                "Enter the remainder for each divison below (converted to hexadecimal) and see if you got it correct!",
                "",
                "Example: 26 Decimal to Hexadecimal",
                "Applying Step 1 and Step 2 repeatedly we get the following:",
                "26 % 16 = 10       (Answer = A)",
                "1 % 16 = 1         (Answer = 1)",
                "Note that the equivalent value of 10 decimal to hexadecimal is 'A', and so on",
                "Once we have the values calculated, we simply read the results from bottom to top to get 1A",
                "Final Answer = 1A"};
        }

        /// <summary>
        /// Converts decimal to hexadecimal
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public Tuple<List<Tuple<string, string, StepType>>, string> DecimalToHexadecimal(string dec)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = int.Parse(dec);
            string step;
            string finalAnswer = "";
            string remVal;
            while (value != 0)
            {
                step = value + " % " + 16;
                value = Math.DivRem(value, 16, out int remainder);
                remVal = ConvertDecimalToHex(remainder);
                finalAnswer = finalAnswer.Insert(0, remVal);
                steps.Add(Tuple.Create(step, remVal, StepType.Solution));
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintHexadecimalToUnaryInstructions()
        {
            return new string[] { "Hexadecimal is based on the base 16 number system. Unary is based on base 1 numerical system",
                "The easiest way to convert base systems to unary is to convert them to the decimal base systems first",
                "Step 1: Convert your hexadecimal to decimal",
                "Step 2: Count out your decimal starting from 1 and for every value, write down a 1",
                "Enter the decimal equivalent first below and then enter its converted value to unary to see if you got it correct!",
                "",
                "Example 1C Hexadecimal to Unary",
                "By applying step 1, we get the following decimal value of the hexadecimal as 28",
                "Answer = 28",
                "We simply count out the decimal and represent each count as a 1 so we get the following",
                "Final Answer = 1111111111111111111111111111"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> HexadecimalToUnary(string hexadecimal)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            double answer = 0.0;
            string convVal;
            for (int i = hexadecimal.Length - 1; i >= 0; i--)
            {
                convVal = ConvertHexToDecimal(hexadecimal[i].ToString());
                double tempAnswer = Math.Pow(16, hexadecimal.Length - 1 - i) * int.Parse(convVal);
                answer += tempAnswer;
            }
            steps.Add(Tuple.Create("Decimal value", answer.ToString(), StepType.Solution));
            string finalAnswer = "";
            for (int i = 0; i < answer; i++)
            {
                finalAnswer += "1";
            }
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        public string[] PrintHexadecimalToBinaryInstructions()
        {
            return new string[] { "Hexadecimal is based on the base 16 number system. Binary is based on the base 2 number system.",
                "Step 1: Convert each hexadecimal digit to its decimal counterpart to make the conversions easier",
                "Step 2: Starting from the very right value or the least significant value, floor divide it by 2 and write down the remainder",
                "Step 3: Repeat Step 2 with every quotient you calculate until you reach the quotient of 0",
                "Step 4: Once the quotient is 0, form the remainders together from the bottom to up (your last calculated remainder is the very first binary digit and your first calculated remainder is the very last of the binary digit)",
                "Step 5: Once you have every hexadecimal value calculated, combine them altogether from bottom to top (your latest hexadecimal value to the oldest)",
                "Enter the result for each hexadecimal digit in binary form below and see if you got it correct!",
                "NOTE: If your guess is less than four digits long, append 0's before it to make it so (ex. 101 -> 0101)",
                "",
                "Example: 2C Hexadecimal to Binary",
                "By applying Step 1, for the hexadecimal value C, we get the value decimal value 12",
                "We then apply Step 2 and floor divide the decimal value and we get the following",
                "12 % 2 = 0",
                "6 % 2 = 0",
                "3 % 2 = 1",
                "1 % 2 = 1",
                "Where we get the binary value 1100 when reading from bottom to top",
                "Answer = 1100",
                "We then apply Step 1 again and figure out the decimal value for the hexadecimal value 2, which is simply just 2",
                "Applying Step 2 we get the following",
                "2 % 2 = 0",
                "1 % 2 = 1",
                "Where we get the binary value 01 or 0010 when reading from bottom to top",
                "Answer = 0010",
                "We then apply Step 5 and combine the values together from bottom to top which yields 00101100",
                "Final Answer = 00101100"};
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hexadecimal"></param>
        /// <returns>Tuple<List<Tuple<string, string, int>>, string> = Tuple<List<Tuple<step, answer, substeps>>, finalAnswer></returns>
        public Tuple<List<Tuple<string, string, StepType>>, string> HexadecimalToBinary(string hexadecimal)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            string step;
            string convVal;
            string binary = "";
            string finalBinary = "";
            for (int i = hexadecimal.Length - 1; i >= 0; i--)
            {
                // Hex to decimal
                convVal = ConvertHexToDecimal(hexadecimal[i].ToString());
                steps.Add(Tuple.Create(hexadecimal[i].ToString() + " Decimal Value ", convVal, StepType.MainStep));
                int value = int.Parse(convVal);
                // Decimal to binary
                while (value != 0)
                {
                    step = value + " % " + 2;
                    value = Math.DivRem(value, 2, out int remainder);
                    binary = binary.Insert(0, remainder.ToString());
                    steps.Add(Tuple.Create(step, remainder.ToString(), StepType.SubStep));
                }
                // Fill remaining bits with 0
                while (binary.Length != 4)
                {
                    binary = binary.Insert(0, "0");
                }
                steps.Add(Tuple.Create("Result ", binary, StepType.Solution));
                finalBinary = finalBinary.Insert(0, binary);
                binary = "";
            }
            return Tuple.Create(steps, finalBinary);
        }

        public string[] PrintHexadecimalToDecimalInstructions()
        {
            return new string[] { "Hexadecimal is based on the base 16 number system.",
                "Step 1: Starting with the very last value or otherwise known as the least significant bits, raise 16 to the power of its index (starting from 0)",
                "Step 2: You then take this value and multiply it to the decimal value of that hexadecimal at that specific index",
                "Step 3: Do this repeatedly until you reach the very first value or otherwise known as the most significant bits",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the hexadecimal value",
                "Enter the result of each calculated value (in decimal) below and see if you got it correct!",
                "",
                "Example: 6C Hexadecimal to Decimal",
                "By applying Step 1 and Step 2 repeatedly and starting from index 0 we get the following",
                "(16^0)*C = 12      (Answer = 12)",
                "(16^1)*6 = 96      (Answer = 96)",
                "Note that the hexadecimal value 'F' is equivalent to the decimal 12 (checkout the conversion table if you need help)",
                "We then apply Step 4 and simply add all the values up which yields 108",
                "Final Answer = 108"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> HexadecimalToDecimal(string hexadecimal)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            string step;
            double finalAnswer = 0.0;
            string convVal;
            for (int i = hexadecimal.Length - 1; i >= 0; i--)
            {
                convVal = ConvertHexToDecimal(hexadecimal[i].ToString());
                double tempAnswer = Math.Pow(16, hexadecimal.Length - 1 - i) * int.Parse(convVal);
                step = "(16^" + (hexadecimal.Length - 1 - i) + ")*" + convVal;
                finalAnswer += tempAnswer;
                steps.Add(Tuple.Create(step, tempAnswer.ToString(), StepType.Solution));
            }
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        /// <summary>
        /// Converts decimal values to its hexadecimal equivalent (10-15)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ConvertDecimalToHex(int value)
        {
            switch (value)
            {
                case 10:
                    return "A";

                case 11:
                    return "B";

                case 12:
                    return "C";

                case 13:
                    return "D";

                case 14:
                    return "E";

                case 15:
                    return "F";

                default:
                    return value.ToString();
            }
        }

        private static string ConvertHexToDecimal(string value)
        {
            string newValue = value.ToUpper();
            switch (newValue)
            {
                case "A":
                    return 10.ToString();

                case "B":
                    return 11.ToString();

                case "C":
                    return 12.ToString();

                case "D":
                    return 13.ToString();

                case "E":
                    return 14.ToString();

                case "F":
                    return 15.ToString();

                default:
                    if (int.TryParse(value, out int n))
                    {
                        return value;
                    }
                    else
                    {
                        throw new Exception();
                    }
            }
        }

        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}