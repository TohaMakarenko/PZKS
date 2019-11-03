using System;

namespace ExpressionEngine
{
    // NodeUnary for unary operations such as Negate
    class NodeUnary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeUnary(Node rhs, Operation operation)
        {
            Rhs = rhs;
            Operation = operation;
        }

        public Node Rhs { get; }
        public Operation Operation { get; } // operation

        public override double Eval(IContext ctx)
        {
            // Evaluate RHS
            var rhsVal = Rhs.Eval(ctx);

            // Evaluate and return
            var result = Calculator.ExecuteUnary(Operation, rhsVal);
            return result;
        }
    }
}
