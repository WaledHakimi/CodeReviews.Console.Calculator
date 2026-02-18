using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorLibrary
{
    public class Result
    {
        public double Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public string Operations { get; set; } = "";
        public double Answer { get; set; }
        public void Display()
        {
            Console.WriteLine($"Operand1 = {Operand1} Operations : {Operations} Operand2 = {Operand2} = Result = {Answer}");
        }
    }
}
