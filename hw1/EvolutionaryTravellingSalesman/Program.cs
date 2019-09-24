using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    // TODO do circle cities test file
    // TODO implement Dijkstra's to know limit
    // TODO implement random search to know baseline
    // TODO implement find shortest path by changing fitness calculation of TSP
    // TODO config file
    // TODO approximate time left
    // TODO stopping condition when no improvement in half the time
    // TODO Hierarchical tree based on spatial
    // TODO How to do tight linkage??
    // TODO How to do diversity????
    // TODO change to how many evaluations rather than generations
    // Plot path
    // Try to minimize evaluations
    class Program
    {
        static void Main(string[] args)
        {
            string configFilePath = "config.txt";
            if (args.Length > 0)
                configFilePath = args[0];
            var config = new Config(configFilePath);
            Console.WriteLine(config);
            // TSPSolver solver;
            // switch (config.Get(Config.String.Solver))
            // {
            //     case "MultipleInheritancePriorityTSPSolver":
            //         solver = new MultipleInheritancePriorityTSPSolver();
            //         break;
            //     case "TSPSolver":
            //     default:
            //         solver = new TSPSolver();
            //         break;
            // }
            // solver.Configure(config);
            // solver.Run();
        }
    }
}
