using System;

namespace ExpressionEngine.Helpers
{
    public static class NodeHelpers
    {
        public static Node GetRight(this Node node)
        {
            if (node is NodeBinary nodeBinary)
                return nodeBinary.Right;
            if (node is NodeUnary nodeUnary)
                return nodeUnary.Right;
            return null;
        }
        
        public static Node SetRight(this Node node, Node value)
        {
            if (node is NodeBinary nodeBinary)
                nodeBinary.Right = value;
            if (node is NodeUnary nodeUnary)
                nodeUnary.Right = value;
            return node;
        }
        
        public static Node GetLeft(this Node node)
        {
            if (node is NodeBinary nodeBinary)
                return nodeBinary.Left;
            /*if (node is NodeUnary nodeUnary)
                return nodeUnary.Rhs;*/
            return null;
        }
        
        public static Node SetLeft(this Node node, Node value)
        {
            if (node is NodeBinary nodeBinary)
                nodeBinary.Left = value;
            return node;
        }

        public static int GetHeight(this Node node)
        {
            if (node is NodeBinary nodeBinary)
                return Math.Max(nodeBinary.Right.GetHeight(), nodeBinary.Left.GetHeight()) + 1;
            if (node is NodeUnary nodeUnary)
                return nodeUnary.GetHeight();
            return 0;
        }
    }
}