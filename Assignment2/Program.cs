using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// Luka Emrashvili - 823355800

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> FirstOperands = new List<string>();      // Create a list for First Operands.
            List<string> SecondOperands = new List<string>();      // Create a list for Second Operands.
            List<string> Operators = new List<string>();        // Create a list for operators.

            List<InfInt> FirstInfInts = new List<InfInt>();      // Create a list for First InfInt objects.
            List<InfInt> SecondInfInts = new List<InfInt>();      // Create a list for Second InfInt objects.

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\user\Desktop\Fall 2020\COMPE - 361 -Magda Tsintsadze\Assignment2\infint.txt");         // Read the file and store lines in an array.

            for (int i = 0; i < lines.Length; ++i)            // loop through an array of lines.
            {
                if (i % 3 == 0)                        // Differentiate the First Operands and put them in an appropriate list.
                    FirstOperands.Add(lines[i]);
                else if (i % 3 == 1)                   // Repeat the same for Second Operands.
                    SecondOperands.Add(lines[i]);
                else                                   // Repeat the same for Operators.
                    Operators.Add(lines[i]);
            }

            for (int i = 0; i < FirstOperands.Count; ++i)          // Loop through the list of the first operands
            {
                if (FirstOperands[i][0] == '-')                // If the first char of each string is minus,
                {
                    var regex = new Regex(Regex.Escape("-"));
                    FirstOperands[i] = regex.Replace(FirstOperands[i], "0", 1);       // Replace minus with zero.
                }
            }
            for (int i = 0; i < SecondOperands.Count; ++i)          // Repeat the same for second operands
            {
                if (SecondOperands[i][0] == '-')                
                {
                    var regex = new Regex(Regex.Escape("-"));
                    SecondOperands[i] = regex.Replace(SecondOperands[i], "0", 1);
                }
            }

            for (int i = 0; i < FirstOperands.Count; ++i)       // Loop through the First operands 
            {
                InfInt Number = new InfInt(FirstOperands[i].Select(x => x - '0').ToList());    //  transform the string into list of integers, and pass it to InfInt object as an argument.
                FirstInfInts.Add(Number);                                                      //  Add the created Infint objects to the InfInt List
            }
            for (int i = 0; i < SecondOperands.Count; ++i)      // Repeat the same for the Second operands
            {
                InfInt Number = new InfInt(SecondOperands[i].Select(x => x - '0').ToList());
                SecondInfInts.Add(Number);
            }

            // Now, we want to output equations, so we have to change Operands 
            // into their first form, that is, return minuses instead of zeros in fromt of them. 

            for (int i = 0; i < FirstOperands.Count; ++i)          // Loop through the list of the first operands
            {
                if (FirstOperands[i][0] == '0' && FirstOperands[i].Length != 1)      // If the first char of each string is zero and its length is not 1
                {
                    var regex = new Regex(Regex.Escape("0"));
                    FirstOperands[i] = regex.Replace(FirstOperands[i], "-", 1);       // Replace zero with minus.
                }
            }
            for (int i = 0; i < SecondOperands.Count; ++i)          // Repeat the same for second operands
            {
                if (SecondOperands[i][0] == '0' && SecondOperands[i].Length != 1)                
                {
                    var regex = new Regex(Regex.Escape("0"));
                    SecondOperands[i] = regex.Replace(SecondOperands[i], "-", 1);
                }
            }


            // Outputting The Equations with results


            Console.WriteLine();


            for (int i = 0; i < Operators.Count; ++i)      // loop through the operators.
            {
                if ((String.Compare(Operators[i], "+")) == 0)                 // If its addition 
                {
                    Console.Write($"\t{FirstOperands[i]}  +  {SecondOperands[i]} = ");     // print the two operands with operator

                    foreach (int j in FirstInfInts[i].Addition(SecondInfInts[i]))        // loop through the result, which is list of ints.
                    {
                        Console.Write($"{j}");          // print each integer from the List of ints.
                    }
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------");


                }
                else if ((String.Compare(Operators[i], "-")) == 0)       // Repeat the same for subtraction.
                {
                    Console.Write($"\t{FirstOperands[i]}  -  {SecondOperands[i]} = ");

                    foreach (int j in FirstInfInts[i].Subtraction(SecondInfInts[i]))
                    {
                        Console.Write($"{j}");
                    }
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------");

                }
                else if ((String.Compare(Operators[i], "*")) == 0)      // Repeat the same for multiplication.
                {
                    Console.Write($"\t{FirstOperands[i]} * {SecondOperands[i]} = ");

                    foreach (int j in FirstInfInts[i].Multiplication(SecondInfInts[i]))
                    {
                        Console.Write($"{j}");
                    }
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------");

                }
                else      // If the Operator is different, prompt user to use only +, -, or *.
                {
                    Console.WriteLine("\tPlease use only either +, -, or * Operators.");
                }
            }
        }
    }
}
