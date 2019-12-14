using System;
using System.Linq;
using System.Collections.Generic;

namespace DataFlow
{
    public static class CommandCalculator
    {        
        public static Operant Calculate(Command command, List<Operant> operants)
        {
            var right = operants.FirstOrDefault(x => x.Order == OperantOrder.Right)?.Value;
            var left = operants.FirstOrDefault(x => x.Order == OperantOrder.Right)?.Value;
            double? result = null;

            switch (command.Type)
            {
                case CommandType.Add:
                    result = right + left;
                    break;
                case CommandType.Minus:
                    result = -right;
                    break;
                case CommandType.Subtract:
                    result = right - left;
                    break;
                case CommandType.Multiply:
                    result = right * left;
                    break;
                case CommandType.Divide:
                    result = right / left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if(!result.HasValue)
                throw new InvalidOperationException("Result is null");
            var operant = new Operant() {
                NextCommandId = command.NextCommandId,
                Order = command.Order,
                Value = result.Value
            };
            return operant;
        }
    }
}
