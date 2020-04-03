using System;
using System.Collections.Generic;

namespace BaseConverter.ConvertUtils
{
    internal class ConvertUtil
    {
        public string[] PrintUnaryToBinaryInstructions()
        {
            return new string[] { "Unary is based on base 1 numerical system. Binary is based on base 2 numerical system.",
                "To easily do conversions between unary to binary, it is easier to convert unary first to a decimal value and then conver that to binary",
                "Step 1: Count the number of 1's in your value and convert that value to a decimal",
                "Step 2: Once you have the decimal value, divide that value by 2 and write down the remainder",
                "Step 3: Take the quotient and further divide that by two and again write down the remainder",
                "Step 4: Continue to divide the quotient until the remainder is 0",
                "Step 5: Once the quotient equal to 0 is reached, read the remainders from bottom to top (from your recent remainder calculation to the oldest)",
                "Enter your value for the remainder below and see if you got it correct!"};
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
                value = Math.DivRem(value, 2, out int remainder);
                step = value + " % " + 2;
                steps.Add(Tuple.Create(step, remainder.ToString(), StepType.Solution));
                finalAnswer = finalAnswer.Insert(0, remainder.ToString());
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] PrintUnaryToDecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> UnaryToDecimal(string unary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] PrintUnaryToHexadecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> UnaryToHexadecimal(string unary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] PrintBinaryToUnaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> BinaryToUnary(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] PrintBinaryToDecimalInstructions()
        {
            return new string[] { "Binary is based on the base 2 number system. Each position of the digit corresponds to the power they are to be raised in",
                "Step 1: Starting with the very last digit or otherwise known as the least significant digit, raise 2 to the power of its index (starting from 0)",
                "Step 2: You then take this value and multiply it to the bit at that specific index",
                "Step 3: Do this repeatedly until you reach the very first digit or otherwise known as the most significant bit",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the binary value",
                "Enter the result of each calculated bit below and see if you got it correct!"};
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
                "Enter each hexadecimal value that you calculated for each four bits below and see if you got it correct!"};
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
                "Each value is denoted by 1 (Ex. 3 in decimal is equivalen to 111 in unary",
                "Step 1: Count out your decimal starting from 1 and for every value, write down a 1",
                "Enter the resulting unary value you below and see if you got it correct!"};
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
                "Step 1: Divide the decimal value by 2",
                "Step 2: Write down the remainder and divide the previous quotient by 2",
                "Step 3: Repeat these steps until the quotient is equal to 0",
                "Step 4: Once the quotient is 0, read the remainders from bottom to top (from your recent remainder calculation to the oldest remainder calculation)",
                "Enter each of the remainders of your calculation below and see if you got it correct!"};
        }

        public Tuple<List<Tuple<string, string, StepType>>, string> DecimalToBinary(string dec)
        {
            List<Tuple<string, string, StepType>> steps = new List<Tuple<string, string, StepType>>();
            int value = int.Parse(dec);
            string step;
            string finalAnswer = "";
            while (value != 0)
            {
                value = Math.DivRem(value, 2, out int remainder);
                step = value + " % " + 2;
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
                "Getting the remainder is essential for this calculation. We can use modulo, denoted by % to calculate the remainder",
                "Step 1: Divide the decimal value by 16. Convert the remainder to its hexadecimal equivalent",
                "Step 2: Write down the remainder and divide the previous quotient by 16",
                "Step 3: Repeat these steps until the quotient is equal to 0",
                "Step 4: Once you have the values calculated, simply write the remainder values (in hexadecimal) from bottom to top",
                "Enter the remainder for each divison below (converted to hexadecimal) and see if you got it correct!",
                "HINT: Remember that when your dividend is greater than your divisor, the dividend is the remainder! (ex. 6/16 = 0, and the remainder is 6)"};
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
                value = Math.DivRem(value, 16, out int remainder);
                step = value + " % " + 16;
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
                "Enter the decimal equivalent first below and then enter its converted value to unary to see if you got it correct!"};
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
                "Step 2: Divide the decimal value by 2 and write down the remainder",
                "Step 3: Repeat Step 2 with every quotient you calculate until you reach the quotient of 0",
                "Step 4: Once the quotient is 0, form the remainders together from the bottom to up (your last calculated remainder is the very first binary digit and your first calculated remainder is the very last of the binary digit)",
                "Enter the result for each hexadecimal digit in binary form below and see if you got it correct!",
                "HINT: Each hexedecimal digit corresponds to four binary digits. So if you have two hexadecimal digits, you should get eight binary digits in the end!",
                "NOTE: If your guess is less than four digits long, append 0's before it to make it so (ex. 101 -> 0101"};
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
                    value = Math.DivRem(value, 2, out int remainder);
                    step = value + " % " + 2;
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
                "Enter the result of each calculated value (in decimal) below and see if you got it correct!"};
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
                    return value;
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