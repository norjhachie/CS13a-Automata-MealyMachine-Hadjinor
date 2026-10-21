using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealyMachine_CstyleComments
{
    internal class Program
    {
        static string currentState = "q0";

        static string Transition(char input, out int output)
        {
            output = 0;

            switch (currentState)
            {
                case "q0":
                    if (input == '/')
                        currentState = "q1";
                    else
                        currentState = "qd";
                    break;

                case "q1":
                    if (input == '*')
                        currentState = "q2";
                    else
                        currentState = "qd";
                    break;

                case "q2":
                    if (input == 'a' || input == '/')
                        currentState = "q2";
                    else if (input == '*')
                        currentState = "q3";
                    else
                        currentState = "qd";
                    break;

                case "q3":
                    if (input == '/')
                    {
                        currentState = "q4";
                        output = 1;
                    }
                    else if (input == '*')
                        currentState = "q3";
                    else if (input == 'a')
                        currentState = "q2";
                    else
                        currentState = "qd";
                    break;

                case "q4":
                    // Any character after the closing */ is invalid.
                    currentState = "qd";
                    break;

                case "qd":
                    // Dead state: remain here for every input.
                    currentState = "qd";
                    break;
            }

            return currentState;
        }

        static void Main()
        {
            Console.WriteLine("==================================");
            Console.WriteLine(" MEALY MACHINE: C-STYLE COMMENTS");
            Console.WriteLine("==================================");
            Console.WriteLine("Alphabet: a, *, /");
            Console.WriteLine("Example: /*a*/");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Enter a string (or type 'exit' to quit): ");
                string input = Console.ReadLine() ?? "";

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;
                }

                currentState = "q0";
                string outputSequence = "";

                Console.WriteLine("\nState Transition Trace:");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Initial State: q0");

                foreach (char ch in input)
                {
                    string previousState = currentState;
                    int output;

                    if (ch != 'a' && ch != '*' && ch != '/')
                    {
                        currentState = "qd";
                        outputSequence += "0";

                        Console.WriteLine(
                            $"{previousState} -- {ch}/0 --> qd"
                        );
                        continue;
                    }

                    Transition(ch, out output);
                    outputSequence += output;

                    Console.WriteLine(
                        $"{previousState} -- {ch}/{output} --> {currentState}"
                    );
                }

                Console.WriteLine("\nResults:");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Output Sequence: " + outputSequence);
                Console.WriteLine("Final State: " + currentState);

                if (currentState == "q4")
                {
                    Console.WriteLine("Result: ACCEPTED");
                    Console.WriteLine("Valid C-style comment!");
                }
                else
                {
                    Console.WriteLine("Result: REJECTED");
                    Console.WriteLine("Invalid C-style comment!");
                }

                Console.WriteLine();
            }
        }
    }
}
