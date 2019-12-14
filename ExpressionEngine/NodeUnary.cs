using System;

namespace ExpressionEngine
{
    // NodeUnary for unary operations such as Negate
    public class NodeUnary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeUnary(Node right, Operation operation)
        {
            Right = right;
            Operation = operation;
        }

        public Node Right { get; set; }
        public Operation Operation { get; set; } // operation

        public override double Eval(IContext ctx)
        {
            // Evaluate RHS
            var rhsVal = Right.Eval(ctx);

            // Evaluate and return
            var result = Calculator.ExecuteUnary(Operation, rhsVal);
            return result;
        }
    }
}
