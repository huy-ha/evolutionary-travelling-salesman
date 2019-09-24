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
    // Plot path
    // Try to minimize evaluations
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config("config.txt");
            TSPSolver solver;
            switch (config.Get(Config.String.Solver))
            {
                default:
                    solver = new TSPSolver();
                    break;
            }
            solver.Configure(config);
            solver.Run();
        }
    }
}
