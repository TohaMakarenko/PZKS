using System;

namespace ExpressionEngine
{
    // NodeBinary for binary operations such as Add, Subtract etc...
    class NodeBinary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeBinary(Node left, Node right, Operation operation)
        {
            Left = left;
            Right = right;
            Operation = operation;
        }

        public Node Left { get; set; }   // Left hand side of the operation
        public Node Right { get; set;}   // Right hand side of the operation
        public Operation Operation { get; set; } // operation

        public override double Eval(IContext ctx)
        {
            // Evaluate both sides
            var lhsVal = Left.Eval(ctx);
            var rhsVal = Right.Eval(ctx);

            // Evaluate and return
            var result = Calculator.ExecuteBinary(Operation, lhsVal, rhsVal);
            return result;
        }
    }
}
