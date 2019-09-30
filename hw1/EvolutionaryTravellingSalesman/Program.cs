using System.Threading.Tasks;
using System.IO;
namespace EvolutionaryTravellingSalesman
{
    class Program
    {
        public static string outputFolder = "output";

        static async Task Main(string[] args)
        {
            string configFilePath = "config";
            if (args.Length > 0)
                configFilePath = args[0];
            if (args.Length > 1)
            {
                outputFolder = args[1];
                if (!Directory.Exists(outputFolder))
                {
                    var dir = Directory.CreateDirectory("output/" + outputFolder);
                    outputFolder = dir.FullName;
                }
            }
            var config = new Config("configs/" + configFilePath + ".txt");
            System.Console.WriteLine(config);
            TSPSolver solver;
            switch (config.Get(Config.String.Solver))
            {
                case "ElitesAnnealingTSPSolver":
                    solver = new ElitesAnnealingTSPSolver(config);
                    break;
                case "RandomTSPSolver":
                    solver = new RandomSearchTSPSolver(config);
                    break;
                case "TSPSolver":
                    solver = new TSPSolver(config);
                    break;
                default:
                    throw new System.Exception("Invalid Solver Type");
            }
            await solver.Run();
        }
    }
}
