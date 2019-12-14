using System;
using System.Collections.Generic;
using System.Linq;

namespace DataFlow
{
    public class Processor
    {
        public List<ProcessorElement> Elements { get; private set; }
        public Processor(int elemtsNumber)
        {
            Elements = Enumerable.Range(0, elemtsNumber)
                .Select(x => new ProcessorElement())
                .ToList();
        }
    }

    public class ProcessorElement
    {
        public static Dictionary<CommandType, int> CommandsComplexityMap = new Dictionary<DataFlow.CommandType, int>()
        {
            [CommandType.Add] = 1,
            [CommandType.Subtract] = 1,
            [CommandType.Minus] = 1,
            [CommandType.Multiply] = 1,
            [CommandType.Divide] = 1,
            [CommandType.Input] = 1,
        };

        public int TicksLeft { get; private set; }
        public bool IsFree => TicksLeft == 0;

        public Command Command { get; private set; }
        public List<Operant> Operants { get; private set; }

        public void SetCommand(Command command, List<Operant> operants)
        {
            if (TicksLeft > 0)
                throw new InvalidOperationException("Processor element hasn`t calculated previous");
            TicksLeft = CommandsComplexityMap[command.Type];
            Command = command;
            Operants = operants;
        }

        public Operant Tick()
        {
            TicksLeft -= 1;
            if (TicksLeft != 0)
                return null;
            return CommandCalculator.Calculate(Command, Operants);
        }
    }
}
