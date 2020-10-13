using System;

namespace Lagranj
{
    class Program
    {
        private static byte amountOfNodes;
        private static double[] arrX;
        private static double[] arrY;
        private static double newX;
        private static double result;

        private static void GetAmountOfNodes()
        {
            bool InputSuccessfull = false;

            do
            {
                Console.Write("How much nodes do we have?: ");
                InputSuccessfull = byte.TryParse(Console.ReadLine(), out amountOfNodes);

                if (amountOfNodes == 0)
                    InputSuccessfull = false;

                if (!InputSuccessfull)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must enter a non-negative digit, which's more than zero! Try again...");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            while (!InputSuccessfull);
        }
        private static void GetXValues()
        {
            Console.WriteLine();
            Console.WriteLine("Input values for all the nodes...");

            for (int i = 0; i < amountOfNodes; i++)
            {
                bool InputSuccessfull = false;

                do
                {
                    Console.Write($"X[{i}] = ");
                    InputSuccessfull = double.TryParse(Console.ReadLine(), out arrX[i]);

                    if (!InputSuccessfull)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your input was not in correct format! Try again...");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                while (!InputSuccessfull);
            }
        }
        private static void GetYValues()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("All right!");
            Console.WriteLine("Now input values the function has in each of the nodes...");
            Console.WriteLine();
            Console.ResetColor();

            for (int i = 0; i < amountOfNodes; i++)
            {
                bool InputSuccessfull = false;

                do
                {
                    Console.Write($"F({arrX[i]}) = ");
                    InputSuccessfull = double.TryParse(Console.ReadLine(), out arrY[i]);

                    if (!InputSuccessfull)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your input was not in correct format! Try again...");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                while (!InputSuccessfull);
            }
        }
        private static void GetNewXValue()
        {
            bool InputSuccessfull = false;

            do
            {
                Console.WriteLine("Enter the new X* value to calculate the value of F(x*).");
                Console.Write("X* = ");
                InputSuccessfull = double.TryParse(Console.ReadLine(), out newX);

                if (!InputSuccessfull)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your input was not in correct format! Try again...");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            while (!InputSuccessfull);
        }

        private static void PrintALine(double length)
        {
            Console.WriteLine();
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        private static void PrintATable()
        {
            Console.Write("|\tX\t|");
            for (int i = 0; i < amountOfNodes; i++)
            {
                Console.Write($"\t{arrX[i]}\t|");
            }

            double lineLength = Console.CursorLeft;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            PrintALine(lineLength);

            Console.Write("|\tX\t|");
            for (int i = 0; i < amountOfNodes; i++)
            {
                Console.Write($"\t{arrX[i]}\t|");
            }

            PrintALine(lineLength);

            Console.Write("|\tY\t|");
            for (int i = 0; i < amountOfNodes; i++)
            {
                Console.Write($"\t{arrY[i]}\t|");
            }

            PrintALine(lineLength);
        }

        private static double[,] table;
        private static double[] d;
        private static void GetTheTable()
        {
            table = new double[amountOfNodes, amountOfNodes];
            d = new double[amountOfNodes];
            double artadryal = 1;
            double gumar = 0;

            for (int i = 0; i < amountOfNodes; i++)
            {
                d[i] = 1;

                for (int j = 0; j < amountOfNodes; j++)
                {
                    if (i == j)
                    {
                        table[i, j] = newX - arrX[i];
                        artadryal *= table[i, j];
                    }
                    else
                    {
                        table[i, j] = arrX[i] - arrX[j];
                    }
                    d[i] *= table[i, j];
                }

                if (d[i] != 0)
                    gumar += (arrY[i] / d[i]);
            }
            result = artadryal * gumar;
        }
        private static void PrintTheDerivedTable()
        {
            Console.Write($"|\td[{0}] = {d[0]}\t ");
            for (int j = 0; j < amountOfNodes; j++)
            {
                Console.Write($"\t{table[0, j]}\t|");
            }

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            PrintALine(Console.CursorLeft);

            for (int i = 0; i < amountOfNodes; i++)
            {
                Console.Write($"|\td[{i}] = {d[i]}\t|");
                for (int j = 0; j < amountOfNodes; j++)
                {
                    Console.Write($"\t{table[i, j]}\t|");
                }
                PrintALine(Console.CursorLeft);
            }
            Console.WriteLine();
        }

        private static void CheckIfNodesContainX(double newX)
        {
            for (int i = 0; i < amountOfNodes; i++)
            {
                if (arrX[i] == newX)
                {
                    result = arrY[i];
                    return;
                }
            }
        }

        static void Main(string[] args)
        {
            GetAmountOfNodes();

            arrX = new double[amountOfNodes];
            arrY = new double[amountOfNodes];
            GetXValues();
            GetYValues();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.WriteLine("Here are your values:");
                PrintATable();
                Console.WriteLine();
                Console.ResetColor();

                GetNewXValue();
                Console.WriteLine();

                GetTheTable();

                Console.ForegroundColor = ConsoleColor.Green;
                PrintTheDerivedTable();
                Console.ResetColor();

                CheckIfNodesContainX(newX);

                string res = String.Format("{0:0.####}", result);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Result = {res}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.Write("Press any key to calculate the function's value for another X*...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}