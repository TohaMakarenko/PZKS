using System;
using System.Collections.Generic;

namespace DataFlow
{
    public class CommandsMemory
    {
        public List<Command> SRAM { get; private set; } = new List<Command>();
        public List<Command> ActivatedCommands { get; private set; } = new List<Command>();
        public List<Command> NotActiveCommands { get; private set; } = new List<Command>();
        public List<Operant> Operants { get; private set; } = new List<Operant>();
    }
}
