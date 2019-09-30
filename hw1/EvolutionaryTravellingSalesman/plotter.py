import matplotlib.pyplot as plt
import re
import os
import sys
import argparse
import math
import numpy as np


def read_file(filename):
    return [float(x) for x in open(filename, "r").readlines()]


def read_path(filename, generation):
    file = open(filename, "r")
    last_line = None
    curr_generation = -1
    while True:
        try:
            curr_generation = int(re.findall('\d+', file.readline())[0])
        except:
            break
        last_line = file.readline()
        if generation is not -1 and curr_generation >= generation:
            break
    file.close()
    cities = [city.split(' ') for city in last_line.split('|')]
    x = []
    y = []
    for city in cities:
        x.append(float(city[0]))
        y.append(float(city[1]))
    return x, y, curr_generation


def plot_path(args):
    generation = -1
    if args.generation is not None:
        generation = args.generation
    run = int(args.runs[0][0])
    dir = get_dir_name(run)
    x, y, actual_generation = read_path(
        "{}/BestSalesMan.txt".format(dir), generation)
    plt.plot(x, y)
    title = "Genetic Travelling Salesman Path, generation {}".format(
        actual_generation)
    if args.title is not None:
        title = args.title
    plt.title(title)
    plt.show()


def get_dir_name(runNumber):
    runs = [runs for runs in os.listdir(
        'output') if os.path.isdir(os.path.join('output', runs))]
    try:
        for run in runs:
            if run.find("run{}".format(runNumber)) is not -1:
                return "output/{}".format(run)
    except:
        print("Run {} not found!".format(runNumber))


def average_runs(runs):
    evals = []
    maxCosts = []
    maxCostsErr = []
    minCosts = []
    minCostsErr = []
    avgCosts = []

    for run in runs:
        runEvals = []
        runMaxCosts = []
        runMinCosts = []
        runAvgCosts = []
        for i in run:
            dirname = get_dir_name(int(i))
            runEvals.append(read_file("{}/Evaluations.txt".format(dirname)))
            runMaxCosts.append(read_file("{}/MaxCosts.txt".format(dirname)))
            runMinCosts.append(read_file("{}/MinCosts.txt".format(dirname)))
            runAvgCosts.append(read_file("{}/AvgCosts.txt".format(dirname)))
        maxCosts.append(np.mean(runMaxCosts, axis=0))
        maxCostsErr.append(
            np.std(runMaxCosts, axis=0) / math.sqrt(len(i)))
        minCosts.append(np.mean(runMinCosts, axis=0))
        minCostsErr.append(
            np.std(runMinCosts, axis=0) / math.sqrt(len(i)))
        avgCosts.append(np.mean(runAvgCosts, axis=0))
        evals.append(np.mean(runEvals, axis=0))
    return evals, maxCosts, minCosts, avgCosts, maxCostsErr, minCostsErr


def plot_learning_curve(args):
    runs = args.runs
    labels = args.labels

    logconfig = args.log
    title = args.title
    colors = ['red', 'lime', 'cyan', 'blue', 'yellow', 'orange']
    if title is None:
        title = "Genetic Travelling Salesman"
    evals, maxCost, minCost, avgCost, maxCostErr, minCostErr = average_runs(
        runs)
    for i in range(len(labels)):
        if logconfig is not None and logconfig == "y":
            evals[i] = [math.log(x) for x in evals[i]]
        if config == "max":
            plt.errorbar(evals[i], maxCost[i], yerr=maxCostErr[i],
                         label=labels[i], color=colors[i], ecolor=colors[i], errorevery=1000)
        elif config == "min":
            plt.errorbar(evals[i], minCost[i], yerr=maxCostErr[i],
                         label=labels[i], color=colors[i], ecolor=colors[i], errorevery=1000)
        elif config == "avg":
            plt.plot(evals[i], avgCost[i], label=labels[i])
    plt.legend()
    plt.title(title)
    plt.ylabel('Costs')
    plt.xlabel('Log of Evaluations')
    if args.xlimit is not None:
        plt.xlim(0, int(args.xlimit))
    plt.show()


if __name__ == "__main__":
    # example command: python plotter.py -c max -r 26 27 -r 28 29 -l swap insert
    parser = argparse.ArgumentParser(description='Plot a TSP run')
    parser.add_argument('-r', '--runs', nargs='+', action='append',
                        help='runs to average', required=False)
    parser.add_argument('-l', '--labels', nargs='+',
                        help='labels for each group of run', required=False)
    parser.add_argument(
        '-c', '--config', help='max, min or avg', required=True)
    parser.add_argument(
        '-lg', '--log', help='log or naw (y/n)', required=False)
    parser.add_argument(
        '-t', '--title', nargs='+', help='title of output graph', required=False)
    parser.add_argument(
        '-xl', '--xlimit', help='truncate x axis', required=False)

    parser.add_argument(
        '-g', '--generation', help='generation to plot path', required=False)

    args = parser.parse_args()
    config = args.config
    if args.title is not None:
        args.title = ' '.join(args.title)
    if config == "min" or config == "max" or config == "avg":
        plot_learning_curve(args)
    elif config == "path":
        plot_path(args)


"""
Report configs:
Insert Hill Climber:
 - Shortest: python plotter.py -c min -r 26 30 31 32 33 -l insert-hill-climber -t Shortest_Insert_Hill_Climber
 - Longest: python plotter.py -c min -r 27 34 35 36 37 -l insert-hill-climber -t Longest_Insert_Hill_Climber

 Swap Hill Climber:
 - Shortest: python plotter.py -c min -r 38 39 40 41 42 -l swap-hill-climber -t Shortest_Swap_Hill_Climber
 - Longest: python plotter.py -c min -r 43 44 45 46 47 -l swap-hill-climber -t Longest_Swap_Hill_Climber

Compare hill climbers:
 - Shortest: python plotter.py -c min -r 38 39 40 41 42 -r 26 30 31 32 33 -l swap-hill-climber insert-hill-climber -t Shortest_TSP_Solver -lg y
 - Longest: python plotter.py -c min -r 43 44 45 46 47 -r 27 34 35 36 37 -l swap-hill-climber insert-hill-climber -t Longest_TSP_Solver -lg y

 Random Search: 
 - Shortest: python plotter.py -c min -r 25 56 57 58 59 -l random-search -t Shortest_Random_Search
 - Longest: python plotter.py -c min -r 24 52 53 54 55 -l random-search -t Longest_Random_Search

Multiple Inheritance Insert List
- Shortest: python plotter.py -c min -r 51 -l insert-multilist-shortest
 Summary:
  - Shortest: python plotter.py -c min -r 38 39 40 41 42 -r 26 30 31 32 33 -r 25 56 57 58 59 -l swap-hill-climber insert-hill-climber random-search -t TSP_Solver_Comparison -lg y

Plot Path:
python plotter.py -c path -r <run-number>
"""
