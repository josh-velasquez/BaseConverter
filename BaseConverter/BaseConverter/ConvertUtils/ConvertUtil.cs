using System;
using System.Collections.Generic;

namespace BaseConverter.ConvertUtils
{
    internal class ConvertUtil
    {
        public string[] printUnaryToBinaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> UnaryToBinary(string unary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printUnaryToDecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> UnaryToDecimal(string unary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printUnaryToHexadecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> UnaryToHexadecimal(string unary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printBinaryToUnaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> BinaryToUnary(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printBinaryToDecimalInstructions()
        {
            return new string[] { "Binary is based on the 2 base number system.  Each position of the digit corresponds to the power they are to be raised in",
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
        public Tuple<List<Tuple<string, string>>, string> BinaryToDecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            string step;
            double finalAnswer = 0.0;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                double tempAnswer = Math.Pow(2, binary.Length - 1 - i) * int.Parse(binary[i].ToString());
                step = "(2^" + (binary.Length - 1 - i) + ")*" + binary[i].ToString();
                finalAnswer += tempAnswer;
                steps.Add(Tuple.Create(step, tempAnswer.ToString()));
            }
            return Tuple.Create(steps, finalAnswer.ToString());
        }

        public string[] printBinaryToHexadecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> BinaryToHexadecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printDecimalToUnaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> DecimalToUnary(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printDecimalToBinaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> DecimalToBinary(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        /// <summary>
        /// Instructions for converting decimal to hexadecimal
        /// </summary>
        /// <returns></returns>
        public string[] printDecimalToHexadecimalInstructions()
        {
            return new string[] { "Decimal is based on the 10 base number system while hexadecimal is based on the 16 base number system ",
                "Getting the remainder is essential for this calculation. We can use modulo, denoted by % to calculate the remainder",
                "Step 1: Divide the decimal value by 16. Convert the remainder to its hexadecimal equivalent",
                "Step 2: Take the remainder and divide again by 16",
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
        public Tuple<List<Tuple<string, string>>, string> DecimalToHexadecimal(string dec)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
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
                steps.Add(Tuple.Create(step, remVal));
            }
            return Tuple.Create(steps, finalAnswer);
        }

        public string[] printHexadecimalToUnaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> HexadecimalToUnary(string hexadecimal)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printHexadecimalToBinaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> HexadecimalToBinary(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printHexadecimalToDecimalInstructions()
        {
            return new string[] { "Hexadecimal is based on the 16 base number system.",
                "Step 1: Starting with the very last value or otherwise known as the least significant bits, raise 16 to the power of its index (starting from 0)",
                "Step 2: You then take this value and multiply it to the decimal value of that hexadecimal at that specific index",
                "Step 3: Do this repeatedly until you reach the very first value or otherwise known as the most significant bits",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the hexadecimal value",
                "Enter the result of each calculated value (in decimal) below and see if you got it correct!"};
        }

        public Tuple<List<Tuple<string, string>>, string> HexadecimalToDecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            string step;
            double finalAnswer = 0.0;
            string convVal;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                convVal = ConvertHexToDecimal(binary[i].ToString());
                double tempAnswer = Math.Pow(16, binary.Length - 1 - i) * int.Parse(convVal);
                step = "(16^" + (binary.Length - 1 - i) + ")*" + convVal;
                finalAnswer += tempAnswer;
                steps.Add(Tuple.Create(step, tempAnswer.ToString()));
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
    }
}