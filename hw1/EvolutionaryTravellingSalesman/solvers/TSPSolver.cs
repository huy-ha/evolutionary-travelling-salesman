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
        protected delegate void OnConfigureHandler(Config config);
        protected event OnConfigureHandler OnConfigure;
        private Config m_config;
        public Config config
        {
            get => m_config;
            set
            {
                m_config = value;
                OnConfigure?.Invoke(m_config);
            }

        }
        protected LinkedList<TravellingSalesman> population;
        int currentGeneration = -1;
        public enum Data { MinCost, AverageCost, MaxCost, BestSalesMan, WorstSalesMan };
        private Dictionary<Data, List<float>> m_data = new Dictionary<Data, List<float>>();
        private Dictionary<Data, string> m_outputStrings = new Dictionary<Data, string>();
        protected IEnumerable<TravellingSalesman.City> cities;
        #endregion

        #region Configuration 
        protected bool findShortestPath;
        protected int generationCount;

        protected int populationCount;
        #endregion
        public TSPSolver()
        {
            generationCount = config.Get(Config.Int.GenerationCount);
            populationCount = config.Get(Config.Int.PopulationCount);
            m_data.Add(Data.AverageCost, new List<float>(generationCount));
            m_data.Add(Data.MaxCost, new List<float>(generationCount));
            m_data.Add(Data.MinCost, new List<float>(generationCount));
            m_outputStrings.Add(Data.BestSalesMan, "");
            m_outputStrings.Add(Data.WorstSalesMan, "");
        }

        public TSPSolver Configure(Config initConfig)
        {
            config = initConfig;
            findShortestPath = config.Get(Config.Bool.Optimize);
            return this;
        }

        protected void Reset()
        {
            // Make sure that solver name is set corrected
            string solverName = SolverName;
            cities = File.ReadAllLines(config.Get(Config.String.InputFilePath))
                                        .Select(line =>
                                        {
                                            var coors = line.Split("\t", 2);
                                            var x = float.Parse(coors[0]);
                                            var y = float.Parse(coors[1]);
                                            return new TravellingSalesman.City(x, y);
                                        });
            //Initialize population
            population = new LinkedList<TravellingSalesman>();
            for (int i = 0; i < config.Get(Config.Int.PopulationCount); i++)
            {
                population.AddLast(new TravellingSalesman(findShortestPath, cities));
            }
        }

        // Evolves the population of TSP for the specified number of generations
        public virtual async Task Run()
        {
            Reset();

            Stopwatch epochStopWatch = new Stopwatch();
            epochStopWatch.Start();
            int generationCount = config.Get(Config.Int.GenerationCount);
            for (currentGeneration = 0; currentGeneration < generationCount; currentGeneration++)
            {
                Stopwatch generationStopWatch = new Stopwatch();
                generationStopWatch.Start();
#if DEBUG
                Console.WriteLine("\nGeneration " + currentGeneration);
#endif
                await Evolve();
                generationStopWatch.Stop();
                RecordStats();
                // TODO calculate time left
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
            var bestSalesMan = population.Aggregate((salesman1, salesman2) => salesman1.Cost > salesman2.Cost ? salesman1 : salesman2);
            // sales man with highest cost
            var worstSalesMan = population.Aggregate((salesman1, salesman2) => salesman1.Cost < salesman2.Cost ? salesman1 : salesman2);
            float averageCost = population.Average(salesman => salesman.Cost);
            m_data[Data.MinCost].Add(bestSalesMan.Cost);
            m_data[Data.AverageCost].Add(averageCost);
            m_data[Data.MaxCost].Add(worstSalesMan.Cost);
#if DEBUG
            Console.WriteLine("Average: " + averageCost + ", Max: " + worstSalesMan.Cost + ", Min: " + bestSalesMan.Cost);
#endif
            if (currentGeneration == config.Get(Config.Int.GenerationCount) - 1 ||
            currentGeneration % config.Get(Config.Int.LogFrequency) == 0)
            {
                m_outputStrings[Data.BestSalesMan] += "Generation " + currentGeneration + "\n";
                m_outputStrings[Data.BestSalesMan] += bestSalesMan.PrintPath() + "\n";
                m_outputStrings[Data.WorstSalesMan] += "Generation " + currentGeneration + "\n";
                m_outputStrings[Data.WorstSalesMan] += worstSalesMan.PrintPath() + "\n";
            }
        }

        public void SaveStats()
        {
            System.IO.File.WriteAllText("output/Config.txt", config.ToString());
            System.IO.File.WriteAllText("output/BestSalesMan.txt", m_outputStrings[Data.BestSalesMan]);
            System.IO.File.WriteAllText("output/WorstSalesMan.txt", m_outputStrings[Data.WorstSalesMan]);
            System.IO.File.WriteAllText("output/MaxCosts.txt", string.Join("\n", m_data[Data.MaxCost]));
            System.IO.File.WriteAllText("output/MinCosts.txt", string.Join("\n", m_data[Data.MinCost]));
            System.IO.File.WriteAllText("output/AvgCosts.txt", string.Join("\n", m_data[Data.AverageCost]));
        }
    }
}
