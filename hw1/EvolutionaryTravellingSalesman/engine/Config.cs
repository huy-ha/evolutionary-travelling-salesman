using System.Collections.Generic;
using System;
public class Config
{
    public enum String { InputFilePath, Solver };

    public enum Float
    {
        ElitistPercentage,
        InitMutationFactor,
        MutationFactorDecay,
        Temperature,
        TemperatureDecay
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
        // Read in config file 

        // Fill m_values with values from config file
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
        throw new NotImplementedException();
    }
}