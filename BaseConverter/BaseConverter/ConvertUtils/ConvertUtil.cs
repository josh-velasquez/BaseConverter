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
            "Step 1: Starting with the very last digit or otherwise known as the least significant digit, raise it to the power of its index starting from 0",
                "Step 2: You then take this value and multiply it to the value of the bit at that specific index",
            "Step 3: Do this repeatedly until you reach the very first digit or otherwise known as the most significant bit",
                "Step 4: Once you have the values for each bits calculated, add all of the values up which will yield in the decimal base equivalence of the binary value"};
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="binary"></param>
        /// <returns>Tuple<List<Tuple<step,answer>>, finalAnswer></returns>
        public Tuple<List<Tuple<string, string>>, string> BinaryToDecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            string answer;
            string step;
            double finalAnswer = 0.0;
            for (int i = 0; i < binary.Length; i++)
            {
                double tempAnswer = (Math.Pow(2, i) * int.Parse(binary[i].ToString()));
                answer = tempAnswer.ToString();
                step = "(2^" + i + ")*" + binary[binary.Length - i - 1];
                finalAnswer += tempAnswer;
                steps.Add(Tuple.Create(step, answer));
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

        public string[] printDecimalToHexadecimalInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> DecimalToHexadecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }

        public string[] printHexadecimalToUnaryInstructions()
        {
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> HexadecimalToUnary(string binary)
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
            return new string[] { };
        }

        public Tuple<List<Tuple<string, string>>, string> HexadecimalToDecimal(string binary)
        {
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            return Tuple.Create(steps, "");
        }
    }
}