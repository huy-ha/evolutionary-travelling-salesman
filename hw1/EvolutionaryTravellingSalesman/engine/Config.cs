using System.Collections.Generic;
using System;
using System.Linq;

namespace EvolutionaryTravellingSalesman
{
    public class Config
    {
        public enum String
        {
            InputFilePath,
            Solver,
            Selector,
            Reproducer
        };
        public enum Float
        {
            ElitistPercentage,
            InitMutationFactor,
            MutationFactorDecay,
            Temperature,
            TemperatureDecay,
            ReproductionPercentage
        };
        public enum Bool { Optimize };
        public enum Int
        {
            PopulationCount,
            GenerationCount,
            LogFrequency
        };
        private Dictionary<Float, float> m_floats = new Dictionary<Float, float>();
        private Dictionary<Int, int> m_ints = new Dictionary<Int, int>();
        private Dictionary<String, string> m_strings = new Dictionary<String, string>();
        private Dictionary<Bool, bool> m_bools = new Dictionary<Bool, bool>();

        public Config(string configFilePath)
        {
            //Default values 
            m_strings.Add(String.InputFilePath, "inputs/tsp.txt");
            m_strings.Add(String.Solver, "TSPSolver");
            m_strings.Add(String.Selector, "TruncateSelector");
            m_strings.Add(String.Reproducer, "AsexualSwapReproducer");

            m_bools.Add(Bool.Optimize, true);

            m_ints.Add(Int.GenerationCount, 100000);
            m_ints.Add(Int.PopulationCount, 100);
            m_ints.Add(Int.LogFrequency, 10);

            m_floats.Add(Float.ElitistPercentage, 0.02f);
            m_floats.Add(Float.InitMutationFactor, 0.5f);
            m_floats.Add(Float.MutationFactorDecay, 0.9999f);
            m_floats.Add(Float.Temperature, 0.9f);
            m_floats.Add(Float.TemperatureDecay, 0.9999f);
            m_floats.Add(Float.ReproductionPercentage, 0.5f);
            // Read in config file 
            string[] configLines = System.IO.File.ReadAllLines(configFilePath);
            if (configLines.Select(line => ParseConfigLine(line)).Any(val => !val))
            {
                throw new Exception("Bad Config File Format");
            }
        }

        private bool ParseConfigLine(string line)
        {
            string field = line.Substring(0, line.IndexOf(':'));
            string value = line.Substring(line.IndexOf(':') + 1);
            Float floatField;
            Int intField;
            String stringField;
            Bool boolField;
            if (Enum.TryParse(field, true, out floatField))
            {
                m_floats[floatField] = float.Parse(value);
                return true;
            }
            else if (Enum.TryParse(field, true, out intField))
            {
                m_ints[intField] = int.Parse(value);
                return true;
            }
            else if (Enum.TryParse(field, true, out stringField))
            {
                m_strings[stringField] = value;
                return true;
            }
            else if (Enum.TryParse(field, true, out boolField))
            {
                m_bools[boolField] = bool.Parse(value);
                return true;
            }
            return false;
        }

        #region Getters
        public float Get(Float field)
        {
            return m_floats[field];
        }

        public int Get(Int field)
        {
            return m_ints[field];
        }

        public string Get(String field)
        {
            return m_strings[field];
        }

        public bool Get(Bool field)
        {
            return m_bools[field];
        }
        #endregion

        public override string ToString()
        {
            string output = "";
            foreach (var pair in m_strings)
            {
                output += pair.Key + ":" + pair.Value + "\n";
                if (pair.Key == String.InputFilePath)
                    output += "\n";
            }
            output += "\n";
            foreach (var pair in m_bools)
                output += pair.Key + ":" + pair.Value + "\n";
            output += "\n";
            foreach (var pair in m_ints)
                output += pair.Key + ":" + pair.Value + "\n";
            output += "\n";
            foreach (var pair in m_floats)
                output += pair.Key + ":" + pair.Value + "\n";
            return output;
        }
    }
}