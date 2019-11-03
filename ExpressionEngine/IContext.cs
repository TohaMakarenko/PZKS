﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine
{
    public interface IContext
    {
        double ResolveVariable(string name);
        double CallFunction(string name, double[] arguments);
    }
}
