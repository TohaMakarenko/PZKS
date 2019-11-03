using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine
{
    public class NodeFunctionCall : Node
    {
        public NodeFunctionCall(string functionName, Node[] arguments)
        {
            FunctionName = functionName;
            Arguments = arguments;
        }

        public string FunctionName { get; }
        public Node[] Arguments { get; }

        public override double Eval(IContext ctx)
        {
            // Evaluate all arguments
            var argVals = new double[Arguments.Length];
            for (int i = 0; i < Arguments.Length; i++) {
                argVals[i] = Arguments[i].Eval(ctx);
            }

            // Call the function
            return ctx.CallFunction(FunctionName, argVals);
        }
    }
}