using System;
using DataFlow;
using ExpressionEngine;
using ExpressionEngine.Helpers;
using ExpressionEngine.Parallelization;
using ExpressionEngine.Visualization;

namespace Lab1ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var procElementsNumber = 4;

            while (true) {
                Console.WriteLine("Enter expression:");
                var expression = Console.ReadLine();
                try {
                    var result = Parser.Parse(expression);
                    if (result == null) {
                        Console.WriteLine("FAILED");
                        continue;
                    }

                    Console.WriteLine("OK");
                    Console.WriteLine($"Result : {result.Eval()}");
                    Console.WriteLine($"Height : {result.GetHeight()}");
                    result.Print();

                    Console.WriteLine("Optimized:");
                    var optimized = ExpressionOptimizer.Optimize(result);
                    Console.WriteLine($"Result : {optimized.Eval()}");
                    Console.WriteLine($"Height : {optimized.GetHeight()}");
                    optimized.Print();

                    Console.WriteLine("Data Flow:");

                    PrintHeader("SEQUENTIAL");

                    var sequentialSystem = DataFlowSystem.BuildSystem(optimized, 1);
                    DataFlowHelper.PrintSystem(sequentialSystem);
                    var sequentialSystemResult = sequentialSystem.Start();
                    DataFlowHelper.PrintSystemResult(sequentialSystemResult);


                    PrintHeader("PARALLEL");

                    var parallelSystem = DataFlowSystem.BuildSystem(optimized, procElementsNumber);
                    DataFlowHelper.PrintSystem(parallelSystem);
                    var parallelSystemResult = parallelSystem.Start();
                    DataFlowHelper.PrintSystemResult(parallelSystemResult);

                    Console.WriteLine();

                    var acceleration = (double) sequentialSystemResult.Ticks / parallelSystemResult.Ticks;
                    var efficient = (double) acceleration / procElementsNumber;
                    PrintHeader($"ACCELERATION: {acceleration}, EFFICIENT: {efficient}");
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
        }

        static void PrintHeader(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"\r\n{str}\r\n");
            Console.ResetColor();
        }
    }
}