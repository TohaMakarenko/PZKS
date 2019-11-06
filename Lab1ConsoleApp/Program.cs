using System;
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
            while (true) {
                Console.WriteLine("Enter expression:");
                var expression = Console.ReadLine();
                try {
                    var result = Parser.Parse(expression);
                    if(result == null) {
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
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}