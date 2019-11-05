using ExpressionEngine.Helpers;

namespace ExpressionEngine.Parallelization
{
    public static class ExpressionOptimizer
    {
        public static Node Optimize(Node node)
        {
            node = ReplaceSubtract(node);
            
            var current = node as NodeBinary;
            if (current == null)
                return node;
            int bFactor = BalanceFactor(current);
            if (bFactor > 1) {
                if (BalanceFactor(current.Left) > 0) {
                    current = RotateLL(current);
                }
                else {
                    current = RotateLR(current);
                }
            }
            else if (bFactor < -1) {
                if (BalanceFactor(current.Right) > 0) {
                    current = RotateRL(current);
                }
                else {
                    current = RotateRR(current);
                }
            }

            return current;
        }

        private static Node ReplaceSubtract(Node node)
        {
            if (node is NodeBinary nodeBinary) {
                if (nodeBinary.Operation == Operation.Subtract) {
                    nodeBinary.Operation = Operation.Add;
                    nodeBinary.Right = new NodeUnary(nodeBinary.Right, Operation.Minus);
                }

                ReplaceSubtract(nodeBinary.Left);
                ReplaceSubtract(nodeBinary.Right);
            }

            if (node is NodeUnary nodeUnary) {
                if (nodeUnary.Right is NodeNumber nodeNumber)
                    nodeNumber.Number = -nodeNumber.Number;
                else
                    ReplaceSubtract(nodeUnary.Right);
            }

            return node;
        }

        /*private static Node OptimizeDivision(Node node)
        {
            if (!(node is NodeBinary nodeBinary) || nodeBinary.Operation != Operation.Divide)
                return node;
            
        }*/

        private static int BalanceFactor(Node current)
        {
            var left = current.GetLeft()?.GetHeight() ?? 0;
            var right = current.GetRight()?.GetHeight() ?? 0;
            int bFactor = left - right;
            return bFactor;
        }

        private static Node RotateRight(Node node)
        {
            if()
        }

        private static NodeBinary RotateRR(NodeBinary parent)
        {
            Node pivot = parent.Right;
            parent.Right = pivot.Lhs;
            pivot.Lhs = parent;
            return pivot;
        }

        private static NodeBinary RotateLL(NodeBinary parent)
        {
            Node pivot = parent.Left;
            parent.Left = pivot.Rhs;
            pivot.Rhs = parent;
            return pivot;
        }

        private static NodeBinary RotateLR(NodeBinary parent)
        {
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private static NodeBinary RotateRL(NodeBinary parent)
        {
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}