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
                .Select((x, index) => new ProcessorElement(index))
                .ToList();
        }
    }

    public class ProcessorElement
    {
        public static Dictionary<CommandType, int> CommandsComplexityMap = new Dictionary<DataFlow.CommandType, int>() {
            [CommandType.Add] = 1,
            [CommandType.Subtr] = 1,
            [CommandType.Minus] = 1,
            [CommandType.Mult] = 2,
            [CommandType.Divide] = 4,
            [CommandType.Input] = 1,
        };

        public ProcessorElement(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int TicksLeft { get; private set; }
        private int _ticks;
        public bool IsFree => TicksLeft == 0;

        public Command Command { get; private set; }
        public List<Operant> Operants { get; private set; }

        public void SetCommand(Command command, List<Operant> operants)
        {
            if (TicksLeft > 0)
                throw new InvalidOperationException("Processor element hasn`t calculated previous");
            TicksLeft = CommandsComplexityMap[command.Type];
            _ticks = 0;
            Command = command;
            Operants = operants;
        }

        public Operant Tick()
        {
            Console.WriteLine($"Proc Elem #{Id}: Tick: {_ticks}, Command: {Command.Id}, {Command.Type}");
            TicksLeft --;
            _ticks++;
            if (TicksLeft != 0)
                return null;
            return CommandCalculator.Calculate(Command, Operants);
        }
    }
}