using System;

namespace ExpressionEngine
{
    public static class Calculator
    {
        public static double ExecuteBinary(Operation operation, double left, double right)
        {
            switch (operation) {
                case Operation.Add:
                    return left + right;
                case Operation.Minus:
                    throw new InvalidOperationException("Minus is unary operation");
                case Operation.Subtract:
                    return left - right;
                case Operation.Multiply:
                    return left * right;
                case Operation.Divide:
                    return left / right;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }

        public static double ExecuteUnary(Operation operation, double value)
        {
            switch (operation) {
                case Operation.Minus:
                    return -value;
                case Operation.Add:
                case Operation.Subtract:
                case Operation.Multiply:
                case Operation.Divide:
                    throw new InvalidOperationException($"Operation {operation} is binary");
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
    }
}