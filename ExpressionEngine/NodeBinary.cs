using System;

namespace ExpressionEngine
{
    // NodeBinary for binary operations such as Add, Subtract etc...
    class NodeBinary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeBinary(Node lhs, Node rhs, Operation operation)
        {
            Lhs = lhs;
            Rhs = rhs;
            Operation = operation;
        }

        public Node Lhs { get; }   // Left hand side of the operation
        public Node Rhs { get; }   // Right hand side of the operation
        public Operation Operation { get; } // operation

        public override double Eval(IContext ctx)
        {
            // Evaluate both sides
            var lhsVal = Lhs.Eval(ctx);
            var rhsVal = Rhs.Eval(ctx);

            // Evaluate and return
            var result = Calculator.ExecuteBinary(Operation, lhsVal, rhsVal);
            return result;
        }
    }
}
