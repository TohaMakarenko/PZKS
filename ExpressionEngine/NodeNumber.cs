﻿namespace ExpressionEngine
{
    // NodeNumber represents a literal number in the expression
    public class NodeNumber : Node
    {
        public NodeNumber(double number)
        {
            Number = number;
        }

        public double Number { get; set; }

        public override double Eval(IContext ctx)
        {
            // Just return it.  Too easy.
            return Number;
        }
    }
}
