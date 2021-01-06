using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

// Luka Emrashvili - 823355800

namespace Assignment2
{
    class InfInt : IComparable<InfInt>
    {
        public List<int> Integer { get; private set; }    // auto-implemented property for getting and setting List of Integers
        public bool IsNegative { get; private set; }     // auto-implemented property for getting and setting number's sign.

        public InfInt(List<int> number)            // constructor, which has list of integers as a parameter.
        {
            if (number[0] == 0 && number.Count != 1)     // Check for the sign, in our case, if it is negative, it has preceding zero.
            {
                this.IsNegative = true;           // set the bool to be true.
                number.RemoveAt(0);              // Remove that preceding zero.
                this.Integer = number;          // initialize Integer with given list of integers.
            }
            else                          // Otherwise, it will be positive, so:
            {
                this.IsNegative = false;      // set the bool to be false.
                this.Integer = number;       // initialize Integer with given list of integers.
            }
        }


        public int CompareTo(InfInt other)        // method for comparing to InfInt objects
        {
            int size1 = this.Integer.Count;      // get the sizes of each InfInt
            int size2 = other.Integer.Count;

            if (size1 == size2)              // if the sizes are the same check for each digit.
            {
                for (int i = 0; i < size1; ++i)
                {
                    if (this.Integer[i] != other.Integer[i])    // if they have any different digit
                    {
                        return this.Integer[i].CompareTo(other.Integer[i]);    // get the result of their comparison
                    }
                }

            }

            if (size1 > size2)     // if first has greater size return 1
                return 1;
            else if (size1 < size2)   // if second has greater size return -1
                return -1;
            else                  // otherwise, they are identical and return 0
                return 0;
        }
        public List<int> Addition(InfInt other)            // Method for adding two InfInt objects. it returns List of Ints.
        {
            int size1 = this.Integer.Count;          // Get sizes of each InfInt object.
            int size2 = other.Integer.Count;
            int num;                             // int num is used for storing digit values 
            int carry = 0;                       // for storing carry, initialized to zero at first.
            List<int> Sum = new List<int>();     // List 

            int comp = this.CompareTo(other);     // Compare two objects using CompareTo() method and store the result in comp.

            if (this.IsNegative == false && other.IsNegative == false)    // if both InfInts are Positive
            {

                if (comp >= 0)     // If first is greater than or equal to second 
                {
                    this.Integer.Reverse();    // Reverse each objects.
                    other.Integer.Reverse();

                    for (int i = 0; i <= (size1 - size2); ++i)   // Append zeros to the shorter InfInt.
                    {
                        other.Integer.Add(0);
                    }
                    this.Integer.Add(0);    // Append 1 zero to the longer InfInt, it will be useful in case of carry.
                    for (int i = 0; i <= size1; ++i)
                    {
                        if (this.Integer[i] + other.Integer[i] + carry < 10)      // If the sum of two digits and a carry is less than 10,
                        {
                            num = this.Integer[i] + other.Integer[i] + carry;      // the digit of the result will be equal to their sum.
                            carry = 0;                            // set carry to zero.
                            Sum.Add(num);                   // Add this digit to the Result.
                        }
                        else                                    // If its greater than 10
                        {
                            num = this.Integer[i] + other.Integer[i] - 10 + carry;    // Subtract 10 from the sum
                            carry = 1;                      // set carry to 1.
                            Sum.Add(num);                 // Add this digit to the Result.
                        }

                    }


                    Sum.Reverse();     // Reverse the result back.
                    if (Sum[0] == 0)    // If the leading one is zero after reversing, get rid of it.
                    {
                        Sum.RemoveAt(0);
                    } 
                    return Sum;      // return result.
                }
                else           // if second is greater than first, repeat the same as above but vice versa.
                {
                    this.Integer.Reverse();       
                    other.Integer.Reverse();

                    for (int i = 0; i <= (size2 - size1); ++i)
                    {
                        this.Integer.Add(0);
                    }
                    other.Integer.Add(0);
                    for (int i = 0; i <= size2; ++i)
                    {

                        if (this.Integer[i] + other.Integer[i] + carry < 10)
                        {
                            num = this.Integer[i] + other.Integer[i] + carry;
                            carry = 0;
                            Sum.Add(num);
                        }
                        else
                        {
                            num = this.Integer[i] + other.Integer[i] - 10 + carry;
                            carry = 1;
                            Sum.Add(num);
                        }

                    }
                    Sum.Reverse();    
                    if (Sum[0] == 0)    
                    {
                        Sum.RemoveAt(0);
                    }
                    return Sum;     
                }


            }
            else if (this.IsNegative == true && other.IsNegative == true)       // If both of the InfInts are negative
            {
                this.IsNegative = false;               // set the both InFInts to be positive
                other.IsNegative = false;

                Console.Write("-");              // Write - when the result will be returned
                return this.Addition(other);     // return the addition of these two positive numbers. 

            }
            else if (this.IsNegative == true && other.IsNegative == false)    // If first is negative and second is positive
            {
                this.IsNegative = false;                    // set the first to be positive
                return other.Subtraction(this);           // return subtraction of first from second.
            }
            else                             // if first is positive and second negative
            {
                other.IsNegative = false;                // set the second to be positive
                return this.Subtraction(other);             // return subtraction of second from first.
            }
        }

        public List<int> Subtraction(InfInt other)            // Method for Subtracting two InfInt objects. it returns List of Ints.
        {
            int size1 = this.Integer.Count;          // get the sizes of both infInts.
            int size2 = other.Integer.Count;
            int difference;                           //  For storing the singe digit.
            List<int> Difference = new List<int>();     // List of ints for storing those single digits

            int comp = this.CompareTo(other);      // Compare both InfIts

            if (this.IsNegative == false && other.IsNegative == false)    // IF both are Positive
            {
                if (comp > 0)                  // If first greater than second.
                {
                    this.Integer.Reverse();  // reverse both Infints.
                    other.Integer.Reverse();

                    for (int i = 0; i < (size1 - size2); ++i)    // Append second Inf int with zeros
                    {
                        other.Integer.Add(0);
                    }
                    for (int i = 0; i < size1; ++i)          // loop through the digits
                    {
                        if (this.Integer[i] >= other.Integer[i])       // if firts infint's digit is greater than secon's:
                        {
                            difference = this.Integer[i] - other.Integer[i];    // the digit of the result will be simply th difference of them.
                            Difference.Add(difference);               // Add this digit to the final result.
                        } 
                        else                        // if first InfInt's digit is less than secons's:
                        {
                            difference = 10 + this.Integer[i] - other.Integer[i];   // Add 10 to the first digit and than subtract the second one.
                            Difference.Add(difference);                            // Add the resulted igit to the final result.
                            for (int j = i + 1; j < size1; ++j)            // loop throug the first InfInts's remaining digits to borrow one.
                            {
                                if (this.Integer[j] != 0)              // if the digit does not equal to zero, borrow from that digit and break the loop.
                                {
                                    this.Integer[j] = this.Integer[j] - 1;
                                    break;
                                }
                                else                             // if digit equals zero change it to be equal to 9.
                                {
                                    this.Integer[j] = 9;
                                }
                            }
                        }
                    }
                }
                else if (comp < 0)            // IF first Infint is less than second.
                {
                    Console.Write("-");              // Write - when the result will be returned
                    return other.Subtraction(this);     // Return subtraction of first from second.
                }
                else              // If both InfInts are identical add zero to the result simply.
                {
                    Difference.Add(0);
                }
                Difference.Reverse();     // reverse the result
                if (Difference.Count != 1 && Difference[0] == 0)   // If we have preceding zeros in the result get rid of them.
                {
                    var str = String.Join("", Difference);         // change the list of ints to be string.
                    str = str.TrimStart(new char[] { '0' });        // Remove leading zeros from the string.
                    Difference = str.Select(x => x - '0').ToList();   // Change the string back to the list of ints.
                }
                return Difference;   // Return the result.
            }
            else if (this.IsNegative == true && other.IsNegative == true)    // IF both InfInts are negative:
            {

                this.IsNegative = false;       // Set the both to be Positive.
                other.IsNegative = false;

                if (comp > 0)          // IF first is greater than second
                {
                    Console.Write("-");     // Write - when the result will be returned
                    return this.Subtraction(other);       // return subtraction of second from first.
                }
                else                   // otherwise, if second is greater or equal to first
                    return this.Subtraction(other);       // Return subtraction of second from first.
            }
            else if (this.IsNegative == true && other.IsNegative == false)      // If first is negative and second is positive
            {
                this.IsNegative = false;          // Set the first to be postive
                Console.Write("-");             // Write - when the result will be returned
                return this.Addition(other);        // Return their addition.
            }
            else                                  // If first is positive and second is negative
            {
                other.IsNegative = false;            // set the second to be positive
                return this.Addition(other);      // Return their addition.
            }

        }

        public List<int> Multiplication(InfInt other)      // Method for Multiplying two InfInt objects. it returns List of Ints.
        {
            int size1 = this.Integer.Count;             // Get sizes of both InfInts
            int size2 = other.Integer.Count;
            int[,] table = new int[size2, 2 * size1 + 2 * size2];     // Create a rectangular array for storing the results of multiplication of single digits.
            int product1;        // for storing single digits of multiplication.
            int num;            // for storing single digits of multiplication.
            int k = 0;         // just constant, used while looping.
            int carry = 0;       // to store carry values.
            int sum = 0;        // to store sum of single digits after multiplication.
            List<int> Null = new List<int> { 0 };    // List of ints, which will be returned in case any of the Infints equals zero.
            List<int> Product = new List<int>();     // List of ints to return result of multiplication.
            
            this.Integer.Reverse();            // Reverse both InfInts
            other.Integer.Reverse();
            this.Integer.Add(0);               // Append first Infint with zero, useful while occurs carry.

            for (int i = 0; i < size2; ++i )          // nested loops for loopin through each digit of each InfInt
            {

                for (int j = 0; j <= size1; ++j)
                {
                    product1 = other.Integer[i] * this.Integer[j] + carry;   // multiplying 2 digits and adding carry (if occurs).
                    if (product1 > 9)          // If product greater than 9
                    {
                        num = product1 % 10;        // digit will be equal the modulos of 10.
                        carry = product1 / 10;       // carry will be equal to the result of division by 10.
                    }
                    else        // otherwise, if the product is less than 10
                    {
                        num = product1;    // digit will be equal to product1
                        carry = 0;         // carry will be set to zero.
                    }

                    table[i, k++] = num;   // Add the final resulted digit to the rectangular array.     
                }
                k = i + 1;    // It is used to shift the result by 1 in the rectangular array on every multiplication of digits.
            }


            for (int i = 0; i <= size1 + size2; ++i)        // Loop through the rectangular array to add the digits.
            {
                for (int j = 0; j < table.GetLength(0); ++j)      // get the length of rows and add digits accordingly
                {
                    sum += table[j, i];
                }

                sum += carry;       // if carry occurs add it to sum.

                if (sum > 9)        // if sum is greater than 9
                {
                    num = sum % 10;        // digit will be equal the modulos of 10.
                    carry = sum / 10;      // carry will be equal to the result of division by 10.
                }
                else
                {
                    num = sum;         // digit will be equal to sum.
                    carry = 0;         // carry will be set to 0.
                }
                sum = 0;              // sum will be initialized to zero after each iteration.
                Product.Add(num);         // add num to the final result.
            }

            Product.Reverse();       // reverse the product.

            if (Product.Count != 1 && Product[0] == 0)    // if there are leading zeros after reversing the InfInt get rid of them the same way as above.
            {
                var str = String.Join("", Product);
                str = str.TrimStart(new char[] { '0' });
                Product = str.Select(x => x - '0').ToList();
            }

            if (this.IsNegative == true && other.IsNegative == false || this.IsNegative == false && other.IsNegative == true)    // if any of the operands is negative:
            {
                Console.Write("-");      // Write - when the result will be returned
            }

            if (this.Integer[0] == 0 || other.Integer[0] == 0)    // if any of the operands is zero, result will be zero.
            {
                Product = Null;    // Null is the above initialized list of Ints containing single zero.
            }
            return Product;
        }
    }
}

