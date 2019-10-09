using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EvolutionaryTravellingSalesman
{
    class TSPSolver
    {
        #region Variable Declarations
        protected delegate void OnLogHandler();
        protected event OnLogHandler OnLog;
        private static string m_solverName = "DEFAULT";
        public static string SolverName
        {
            get
            {
                if (m_solverName == "DEFAULT")
                    throw new NotImplementedException();
                return m_solverName;
            }
            protected set
            {
                m_solverName = value;
            }
        }


        protected Config config;
        protected LinkedList<TravellingSalesman> population;
        int currentGeneration = -1;
        public enum Data { MinCost, AverageCost, MaxCost, Evaluations, BestSalesMan, WorstSalesMan };
        private Dictionary<Data, List<float>> m_floatData = new Dictionary<Data, List<float>>();
        private Dictionary<Data, List<int>> m_intData = new Dictionary<Data, List<int>>();
        private Dictionary<Data, string> m_outputStrings = new Dictionary<Data, string>();
        protected IEnumerable<City> cities;
        #endregion

        #region Configuration 
        protected bool findShortestPath;
        protected int generationCount;

        protected int populationCount;

        int lastBestSalesmanGeneration = -1;
        float lastBestSalesmanCost = -1;
        float lastBestSalesmanFitness = -1;
        #endregion

        public TSPSolver(Config initConfig)
        {
            config = initConfig;
            findShortestPath = config.Get(Config.Bool.Optimize);
            generationCount = config.Get(Config.Int.GenerationCount);
            populationCount = config.Get(Config.Int.PopulationCount);
            m_floatData.Add(Data.AverageCost, new List<float>(generationCount));
            m_floatData.Add(Data.MaxCost, new List<float>(generationCount));
            m_floatData.Add(Data.MinCost, new List<float>(generationCount));
            m_intData.Add(Data.Evaluations, new List<int>());
            m_outputStrings.Add(Data.BestSalesMan, "");
            m_outputStrings.Add(Data.WorstSalesMan, "");
        }

        protected void Reset()
        {
            // Make sure that solver name is set corrected
            if (this.GetType() != typeof(TSPSolver))
            {
                string solverName = SolverName;
            }
            cities = File.ReadAllLines(config.Get(Config.String.InputFilePath))
                                        .Select(line =>
                                        {
                                            var coors = line.Split("\t", 2);
                                            var x = float.Parse(coors[0]);
                                            var y = float.Parse(coors[1]);
                                            return new City(x, y);
                                        });
            //Initialize population
            population = new LinkedList<TravellingSalesman>();
            for (int i = 0; i < config.Get(Config.Int.PopulationCount); i++)
            {
                population.AddLast(new TravellingSalesman(cities));
            }
        }

        // Evolves the population of TSP for the specified number of generations
        public virtual async Task Run()
        {
            Reset();

            Stopwatch epochStopWatch = new Stopwatch();
            epochStopWatch.Start();
            int generationCount = config.Get(Config.Int.GenerationCount);
            int saveOutputFrequency = config.Get(Config.Int.OutputSaveFrequency);
            for (currentGeneration = 0; currentGeneration < generationCount; currentGeneration++)
            {

                await Evolve();
                RecordStats();
                if (currentGeneration % saveOutputFrequency == 0) SaveStats();
            }
            epochStopWatch.Stop();
            var ts = epochStopWatch.Elapsed;
            Console.WriteLine(
                "[" + SolverName + "] " +
                "Total Epoch time: " +
                ts.Hours + "h" +
                ts.Minutes + "m" +
                ts.Seconds + "s");
            SaveStats();
        }

        public virtual async Task Evolve()
        {
            throw new NotImplementedException();
        }

        public void RecordStats()
        {
            // sales man with lowest cost
            var bestSalesMan = population.Aggregate((salesman1, salesman2) => salesman1.Fitness() > salesman2.Fitness() ? salesman1 : salesman2);
            // sales man with highest cost
            var worstSalesMan = population.Aggregate((salesman1, salesman2) => salesman1.Fitness() < salesman2.Fitness() ? salesman1 : salesman2);
            float averageCost = population.Average(salesman => salesman.Cost);
            m_floatData[Data.MinCost].Add(bestSalesMan.Cost);
            m_floatData[Data.AverageCost].Add(averageCost);
            m_floatData[Data.MaxCost].Add(worstSalesMan.Cost);
            m_intData[Data.Evaluations].Add(TravellingSalesman.evaluations);
            if (lastBestSalesmanFitness < bestSalesMan.Fitness())
            {
                lastBestSalesmanGeneration = currentGeneration;
                lastBestSalesmanFitness = bestSalesMan.Fitness();
                lastBestSalesmanCost = bestSalesMan.Cost;
                Console.WriteLine("Best Cost: {0} | Generation: {1} | {2}", lastBestSalesmanCost, lastBestSalesmanGeneration, System.DateTime.Now);
                OnLog?.Invoke();
            }
            if (currentGeneration == config.Get(Config.Int.GenerationCount) - 1 ||
            currentGeneration % config.Get(Config.Int.PathSaveFrequency) == 0)
            {
                m_outputStrings[Data.BestSalesMan] += "Generation " + currentGeneration + "\n";
                m_outputStrings[Data.BestSalesMan] += bestSalesMan.PrintPath() + "\n";
                m_outputStrings[Data.WorstSalesMan] += "Generation " + currentGeneration + "\n";
                m_outputStrings[Data.WorstSalesMan] += worstSalesMan.PrintPath() + "\n";
            }
        }

        public void SaveStats()
        {
            System.IO.File.WriteAllText(Program.outputFolder + "/Config.txt", config.ToString());
            System.IO.File.WriteAllText(Program.outputFolder + "/BestSalesMan.txt", m_outputStrings[Data.BestSalesMan]);
            System.IO.File.WriteAllText(Program.outputFolder + "/WorstSalesMan.txt", m_outputStrings[Data.WorstSalesMan]);
            System.IO.File.WriteAllText(Program.outputFolder + "/MaxCosts.txt", string.Join("\n", m_floatData[Data.MaxCost]));
            System.IO.File.WriteAllText(Program.outputFolder + "/MinCosts.txt", string.Join("\n", m_floatData[Data.MinCost]));
            System.IO.File.WriteAllText(Program.outputFolder + "/AvgCosts.txt", string.Join("\n", m_floatData[Data.AverageCost]));
            System.IO.File.WriteAllText(Program.outputFolder + "/Evaluations.txt", string.Join("\n", m_intData[Data.Evaluations]));
        }
    }
}