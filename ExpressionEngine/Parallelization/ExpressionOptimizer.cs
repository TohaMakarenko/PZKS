using System;
using ExpressionEngine.Helpers;

namespace ExpressionEngine.Parallelization
{
    public static class ExpressionOptimizer
    {
        public static Node Optimize(Node node)
        {
            node = ReplaceSubtract(node);
            return Balance(node);
        }

        private static Node Balance(Node node)
        {
            if (node is NodeUnary nodeUnary)
                return Balance(nodeUnary.Right);

            var current = node as NodeBinary;
            if (current == null)
                return node;

            while (current.Left.GetHeight() > current.Right.GetHeight() + 1)
            {
                var newCurrent = RotateRight(current);
                if (newCurrent == current)
                    break;
                current = newCurrent;
            }

            while (current.Right.GetHeight() > current.Left.GetHeight() + 1)
            {
                var newCurrent = RotateLeft(current);
                if (newCurrent == current)
                    break;
                current = newCurrent;
            }

            current.Left = Balance(current.Left);
            current.Right = Balance(current.Right);

            return current;
        }

        private static Node ReplaceSubtract(Node node)
        {
            if (node is NodeBinary nodeBinary)
            {
                if (nodeBinary.Operation == Operation.Subtract)
                {
                    nodeBinary.Operation = Operation.Add;
                    nodeBinary.Right = new NodeUnary(nodeBinary.Right, Operation.Minus);
                }

                ReplaceSubtract(nodeBinary.Left);
                ReplaceSubtract(nodeBinary.Right);
            }

            if (node is NodeUnary nodeUnary)
            {
                if (nodeUnary.Right is NodeNumber nodeNumber)
                    nodeNumber.Number = -nodeNumber.Number;
                else
                    ReplaceSubtract(nodeUnary.Right);
            }

            return node;
        }


        private static NodeBinary RotateRight(NodeBinary nodeBinary)
        {
            switch (nodeBinary.Operation)
            {
                case Operation.Add:
                {
                    if (nodeBinary.Left is NodeBinary leftBinary && leftBinary.Operation == Operation.Add)
                    {
                        nodeBinary.Left = leftBinary.Right;
                        leftBinary.Right = nodeBinary;
                        return leftBinary;
                    }

                    break;
                }
                case Operation.Minus:
                case Operation.Subtract:
                    throw new InvalidOperationException($"Invalid operation {nodeBinary.Operation}");
                    break;
                case Operation.Multiply:
                {
                    if (nodeBinary.Left is NodeBinary leftBinary && leftBinary.Operation == Operation.Multiply)
                    {
                        nodeBinary.Left = leftBinary.Right;
                        leftBinary.Right = nodeBinary;
                        return leftBinary;
                    }

                    break;
                }
                case Operation.Divide:
                {
                    if (nodeBinary.Left is NodeBinary leftBinary && leftBinary.Operation == Operation.Divide)
                    {
                        nodeBinary.Left = leftBinary.Right;
                        nodeBinary.Operation = Operation.Multiply;
                        leftBinary.Right = nodeBinary;
                        return leftBinary;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return nodeBinary;
        }

        private static NodeBinary RotateLeft(NodeBinary nodeBinary)
        {
            switch (nodeBinary.Operation)
            {
                case Operation.Add:
                {
                    if (nodeBinary.Right is NodeBinary rightBinary && rightBinary.Operation == Operation.Add)
                    {
                        nodeBinary.Right = rightBinary.Left;
                        rightBinary.Left = nodeBinary;
                        return rightBinary;
                    }

                    break;
                }
                case Operation.Minus:
                case Operation.Subtract:
                    throw new InvalidOperationException($"Invalid operation {nodeBinary.Operation}");
                    break;
                case Operation.Multiply:
                {
                    if (nodeBinary.Right is NodeBinary rightBinary && rightBinary.Operation == Operation.Multiply)
                    {
                        nodeBinary.Right = rightBinary.Left;
                        rightBinary.Left = nodeBinary;
                        return rightBinary;
                    }

                    break;
                }
                case Operation.Divide:
                {
                    {
                        if (nodeBinary.Right is NodeBinary rightBinary && rightBinary.Operation == Operation.Divide)
                        {
                            nodeBinary.Right = rightBinary.Right;
                            nodeBinary.Operation = Operation.Multiply;
                            rightBinary.Right = rightBinary.Left;
                            rightBinary.Left = nodeBinary;
                            return rightBinary;
                        }
                    }
                    {
                        if (nodeBinary.Right is NodeBinary rightBinary && rightBinary.Operation == Operation.Multiply)
                        {
                            nodeBinary.Right = rightBinary.Left;
                            rightBinary.Operation = Operation.Divide;
                            rightBinary.Left = nodeBinary;
                            return rightBinary;
                        }
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return nodeBinary;
        }
    }
}