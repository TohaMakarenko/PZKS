using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine
{
    // Represents a variable (or a constant) in an expression.  eg: "2 * pi"
    public class NodeVariable : Node
    {
        public NodeVariable(string variableName)
        {
            VariableName = variableName;
        }

        public string VariableName { get; }

        public override double Eval(IContext ctx)
        {
            return ctx.ResolveVariable(VariableName);
        }
    }
}
