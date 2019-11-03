using System;

namespace ExpressionEngine
{
    public static class Calculator
    {
        public static double ExecuteBinary(Operation operation, double right, double left)
        {
            switch (operation) {
                case Operation.Add:
                    return right + left;
                case Operation.Minus:
                    throw new InvalidOperationException("Minus is unary operation");
                case Operation.Subtract:
                    return right - left;
                case Operation.Multiply:
                    return right * left;
                case Operation.Divide:
                    return right / left;
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