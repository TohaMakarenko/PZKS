using System;
using System.Linq;
using System.Collections.Generic;

namespace DataFlow
{
    public class Distributor
    {
        public CommandsMemory CommandsMemory { get; }
        public Distributor(CommandsMemory commandsMemory)
        {
            CommandsMemory = commandsMemory;
        }

        public void DistributeOperants(List<Operant> operants)
        {
            if (!operants.Any())
                return;

            CommandsMemory.Operants.AddRange(operants);
            foreach (var operant in operants)
            {
                var activatedCommand = CommandsMemory.ActivatedCommands
                    .FirstOrDefault(x => x.Id == operant.NextCommandId);
                Command notActivatedCommand = null;
                if (activatedCommand == null)
                    notActivatedCommand = CommandsMemory.NotActiveCommands
                        .FirstOrDefault(x => x.Id == operant.NextCommandId);
                var commandOperantsCount = CommandsMemory.Operants
                    .Where(x => x.NextCommandId == operant.NextCommandId)
                    .Count();
                var command = activatedCommand ?? notActivatedCommand;
                if (GetCommandRequiredOperantsCount(command.Type) == commandOperantsCount)
                {
                    if (activatedCommand != null)
                    {
                        CommandsMemory.ActivatedCommands.Remove(activatedCommand);
                        CommandsMemory.SRAM.Add(activatedCommand);
                    }
                    else if (notActivatedCommand != null)
                    {
                        CommandsMemory.NotActiveCommands.Remove(activatedCommand);
                        CommandsMemory.SRAM.Add(notActivatedCommand);
                    }
                }

                if (notActivatedCommand != null)
                {
                    CommandsMemory.NotActiveCommands.Remove(activatedCommand);
                    CommandsMemory.ActivatedCommands.Add(notActivatedCommand);
                }
            }
        }

        private int GetCommandRequiredOperantsCount(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.Add:
                case CommandType.Divide:
                case CommandType.Subtract:
                case CommandType.Multiply:
                    return 2;
                case CommandType.Input:
                case CommandType.Minus:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Required operants count for command type {commandType} is not defined");
            }
        }
    }
}
