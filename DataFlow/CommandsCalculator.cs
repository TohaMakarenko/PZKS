using System;
using System.Linq;
using System.Collections.Generic;

namespace DataFlow
{
    public static class CommandCalculator
    {
        public static Operant Calculate(Command command, List<Operant> operants)
        {
            var left = operants.FirstOrDefault(x => x.Order == OperantOrder.Left)?.Value;
            var right = operants.FirstOrDefault(x => x.Order == OperantOrder.Right)?.Value;
            double? result = null;

            switch (command.Type) {
                case CommandType.Add:
                    result = left + right;
                    break;
                case CommandType.Minus:
                    result = -left;
                    break;
                case CommandType.Subtr:
                    result = left - right;
                    break;
                case CommandType.Mult:
                    result = left * right;
                    break;
                case CommandType.Divide:
                    result = left / right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!result.HasValue)
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