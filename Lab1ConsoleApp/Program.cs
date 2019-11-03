using System;
using ExpressionEngine;

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
                    Console.WriteLine(result != null ? "OK" : "FAILED");
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}