using System;
using ExpressionEngine;
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
                    result.Print();
                    Console.WriteLine(result != null ? "OK" : "FAILED");
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}