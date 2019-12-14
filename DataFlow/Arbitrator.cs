using System;
using System.Collections.Generic;
using System.Linq;

namespace DataFlow
{
    public class Arbitrator
    {
        public Processor Processor { get; private set; }
        public CommandsMemory CommandsMemory { get; private set; }
        public Distributor Distributor { get; }

        public Arbitrator(Processor processor, CommandsMemory commandsMemory, Distributor distributor)
        {
            Processor = processor;
            CommandsMemory = commandsMemory;
            Distributor = distributor;
        }

        public List<Operant> Tick() {
            var operants = new List<Operant>();
            foreach(var procElement in Processor.Elements.Where(x => x.IsFree)) {
                var command = CommandsMemory.SRAM.FirstOrDefault();
                if(command == null)
                    break;
                CommandsMemory.SRAM.Remove(command);
                var commandOperants = CommandsMemory.Operants
                    .Where(x => x.NextCommandId == command.Id)
                    .ToList();
                procElement.SetCommand(command, commandOperants);
                CommandsMemory.Operants.RemoveAll(x => commandOperants.Contains(x));
            }
            foreach(var procElement in Processor.Elements.Where(x => !x.IsFree)) {
                var tickResult = procElement.Tick();
                if(tickResult != null)
                    operants.Add(tickResult);
            }
            Distributor.DistributeOperants(operants);
            return operants;
        }
    }
}
