using System;
using System.Collections.Generic;

namespace DataFlow
{
    public class DataFlowHelper
    {
        public static void PrintSystem(DataFlowSystem system)
        {
            Console.WriteLine("OPERANTS:");
            PrintOperantsTable(system.CommandsMemory.Operants);
            Console.WriteLine();
            Console.WriteLine("NOT ACTIVE COMMANDS:");
            PrintCommandsTable(system.CommandsMemory.NotActiveCommands);
            Console.WriteLine();
            Console.WriteLine("ACTIVATED COMMANDS:");
            PrintCommandsTable(system.CommandsMemory.ActivatedCommands);
            Console.WriteLine();
            Console.WriteLine("SRAM:");
            PrintCommandsTable(system.CommandsMemory.SRAM);
        }

        public static void PrintSystemResult(DataFlowSystemResult result)
        {
            Console.WriteLine($"DF SYSTEM RESULT: {result.Result}, TICKS: {result.Ticks}");
        }

        public static void PrintOperantsTable(IEnumerable<Operant> operants)
        {
            Console.WriteLine($"NextCommandId\t Order\t Value");
            foreach (var operant in operants)
                Console.WriteLine($"{operant.NextCommandId}\t\t {operant.Order}\t {operant.Value}");
        }

        public static void PrintCommandsTable(IEnumerable<Command> commands)
        {
            Console.WriteLine($"Id\t Type\t\t IsActive\t Order\t NextCommandId\t");
            foreach (var command in commands)
                Console.WriteLine($"{command.Id}\t {command.Type}\t\t {command.IsActive}\t\t {command.Order}\t {command.NextCommandId}");
        }
    }
}
