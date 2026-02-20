using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string? userInput;
            Calculator calculator = new Calculator();
            do
            {
                bool endApp = false;
                int calculatorUsed = 0;
                Console.WriteLine("===== Calculator Console App=====");
                Console.WriteLine("What would you like to do? \n\t1 - Calculation operation\n\t2 - Calculation records\n\t" +
                    "3 - Delete calculation records\n\t4 - Exit calculation console app");
                userInput = Console.ReadLine();
                //to check the user input a number or not
                while (userInput == null || !Regex.IsMatch(userInput, "[1-4]"))
                {
                    Console.WriteLine("Please choose from 1 to 4");
                    userInput = Console.ReadLine();
                }

                switch (userInput)
                {
                    case "1":
                        bool usePrevious = false;
                        while (!endApp)
                        {
                            //Decalare variables and set to empty
                            //Use nullable types(with ?)to match types of System.Console.ReadLine()
                            string? firstNumb = "";
                            string? secondNumb = "";
                            double result = 0;
                            Console.Clear();

                            //The first number to be entered
                            double cleanNumber1 = 0;
                            if (!usePrevious)
                            {
                                Console.WriteLine("Please enter the first number: ");
                                firstNumb = Console.ReadLine();
                                while (!double.TryParse(firstNumb, out cleanNumber1))
                                {
                                    Console.WriteLine("This is not a number value. Please type a number.");
                                    firstNumb = Console.ReadLine();
                                }
                            }
                            else
                            {
                                cleanNumber1 = calculator.ResultList.Last().Answer;
                                Console.WriteLine($"Previous result: {cleanNumber1}");
                            }

                            //Choose an operator
                            Console.WriteLine("\t+ - Add\n\t- - Subtract\n\t* - Multiply\n\t/ - Divide\n\t^ - Power\n\tr - Square root\n\tS - Sin\n\tC - Cosine\n\tT - Tangent");
                            Console.WriteLine("Choose your Option?");

                            string? op = Console.ReadLine();
                            //validate the input is not null and match the paterns
                            while (op == null || !Regex.IsMatch(op, @"[\+|\-|\*|/|^|r|s|c|t]"))
                            {
                                Console.WriteLine("Error: Unrecognized value");
                            }
                            op.Trim().Substring(0, 1);

                            //The second number to be entered
                            double cleanNumber2 = 0;
                            if (Regex.IsMatch(op, @"[\+|\-|\*|/|^]"))
                            {
                                Console.Write("Enter the second number: ");
                                secondNumb = Console.ReadLine();
                                while (!double.TryParse(secondNumb, out cleanNumber2))
                                {
                                    Console.WriteLine("This is not a number, please enter a number");
                                    secondNumb = Console.ReadLine();
                                }
                            }

                            try
                            {
                                result = calculator.DoCalculation(cleanNumber1, cleanNumber2, op);
                                calculatorUsed++;
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result an arithmatic error.");
                                    Console.WriteLine("\n");
                                    break;
                                    
                                }
                                else Console.WriteLine("Your result: {0:0.##}\n", result);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Oh no! An exception occured trying to do math operation.\n - Details: " + e.Message);
                            }
                            Console.WriteLine("------------------------\n");
                            Console.WriteLine($"Calculator was used {calculatorUsed} times");
                            Console.WriteLine("Press 'x' to discard previous result and start new operaion: ");
                            Console.Write("Press 'n' and Enter to go to menu, or press any other key and Enter to resume working on previous result: ");

                            string? endOption = Console.ReadLine();
                            if (endOption == "n")
                            {
                                endApp = true;
                            }
                            else if (endOption != "x")
                            {
                                usePrevious = true;
                            }
                            else
                            {
                                usePrevious = false;
                            }
                            Console.WriteLine("\n");
                        }
                        calculator.Finish();
                        break;
                    case "2":
                        Console.Clear();
                        if (calculator.ResultList.Count == 0)
                        {
                            Console.WriteLine("===Currently there are no calculation records===");
                        }
                        else
                        {
                            Console.WriteLine("===ALL THE CALCULATION HISTORY===");
                            foreach (var result in calculator.ResultList)
                            {
                                result.Display();
                            }
                        }
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to delete all the calculation history? (Y/N) ");
                        string? answr = Console.ReadLine();
                        while (answr == null || (answr != "y" && answr != "n"))
                        {
                            Console.WriteLine("Please choose the correct option. (y/n)");
                            answr = Console.ReadLine();
                        }
                        if(answr == "y")
                        {
                            calculator.ResultList.Clear();
                            Console.Clear();
                            Console.WriteLine("All the calculation records are deleted successfully. Please enter.");
                            Console.ReadLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Erorr: Please choose from 1 - 4");
                        break;
                }
            } while (userInput != "4");
        }
    }
}