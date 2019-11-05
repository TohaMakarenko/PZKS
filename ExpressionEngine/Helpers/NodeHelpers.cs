namespace ExpressionEngine.Helpers
{
    public static class NodeHelpers
    {
        public static Node GetRight(this Node node)
        {
            if (node is NodeBinary nodeBinary)
                return nodeBinary.Rhs;
            if (node is NodeUnary nodeUnary)
                return nodeUnary.Rhs;
            return null;
        }
        
        public static Node GetLeft(this Node node)
        {
            if (node is NodeBinary nodeBinary)
                return nodeBinary.Lhs;
            /*if (node is NodeUnary nodeUnary)
                return nodeUnary.Rhs;*/
            return null;
        }
    }
}