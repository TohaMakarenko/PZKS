using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionEngine;

namespace DataFlow
{
    public class DataFlowSystem
    {
        public static DataFlowSystem BuildSystem(Node tree, int processorElementsNumber)
        {
            var system = new DataFlowSystem();
            system.Processor = new Processor(processorElementsNumber);
            system.CommandsMemory = new CommandsMemory();
            system.Distributor = new Distributor(system.CommandsMemory);
            system.Arbitrator = new Arbitrator(system.Processor, system.CommandsMemory, system.Distributor);

            BuildTables(system, tree, 0, OperantOrder.Right);
            return system;
        }

        private static void BuildTables(DataFlowSystem system, Node node, int nextCommandId, OperantOrder order)
        {
            if (node is NodeBinary nodeBinary) {
                var command = new Command() {
                    Id = system._lastCommandId++,
                    IsActive = false,
                    NextCommandId = nextCommandId,
                    Order = order,
                    Type = (CommandType) nodeBinary.Operation
                };
                system.CommandsMemory.NotActiveCommands.Add(command);
                BuildTables(system, nodeBinary.Right, command.Id, OperantOrder.Right);
                BuildTables(system, nodeBinary.Left, command.Id, OperantOrder.Left);
            }

            if (node is NodeUnary nodeUnary) {
                var command = new Command() {
                    Id = system._lastCommandId++,
                    IsActive = false,
                    NextCommandId = nextCommandId,
                    Order = order,
                    Type = (CommandType) nodeUnary.Operation
                };
                system.CommandsMemory.NotActiveCommands.Add(command);
                BuildTables(system, nodeUnary.Right, command.Id, OperantOrder.Right);
            }

            if (node is NodeNumber nodeNumber) {
                var operant = new Operant {
                    NextCommandId = nextCommandId,
                    Order = order,
                    Value = nodeNumber.Number
                };
                system.Distributor.DistributeOperants(new List<Operant> {operant});
            }
        }

        private int _lastCommandId = 0;
        public Processor Processor { get; protected set; }
        public CommandsMemory CommandsMemory { get; protected set; }
        public Arbitrator Arbitrator { get; protected set; }
        public Distributor Distributor { get; protected set; }
        protected DataFlowSystem() { }

        public DataFlowSystemResult Start()
        {
            var result = new DataFlowSystemResult();
            while (true) {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"TICK: {result.Ticks}");
                Console.ResetColor();

                Console.WriteLine("Processor Elements");
                Arbitrator.Tick();

                DataFlowHelper.PrintSystem(this);
                Console.WriteLine();


                if (!CommandsMemory.NotActiveCommands.Any()
                    && !CommandsMemory.ActivatedCommands.Any()
                    && !CommandsMemory.SRAM.Any()) {
                    result.Result = CommandsMemory.Operants.FirstOrDefault().Value;
                    return result;
                }

                result.Ticks++;
            }
        }
    }
}