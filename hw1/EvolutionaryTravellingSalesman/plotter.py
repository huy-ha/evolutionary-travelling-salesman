import matplotlib.pyplot as plt
import re
import os
import sys
import argparse
from math import log
import numpy as np

# TODO plot error bars
# TODO collect information for dot plot


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


def plot_costs(output_dir, plotmaxcost=False, plotmincost=True, plotavgcost=False):
    evaluations = read_file("{}/Evaluations.txt".format(output_dir))
    if plotmaxcost:
        maxCosts = read_file("{}/MaxCosts.txt".format(output_dir))
        plt.plot(evaluations, maxCosts, label="Max Costs",)
    if plotmincost:
        minCosts = read_file("{}/MinCosts.txt".format(output_dir))
        plt.plot(evaluations, minCosts,  label="Min Costs")
    if plotavgcost:
        avgCosts = read_file("{}/AvgCosts.txt".format(output_dir))
        plt.plot(evaluations, avgCosts,  label="Average Costs")

    plt.legend()
    plt.title("Genetic Travelling Salesman")
    plt.ylabel('Costs')
    plt.xlabel('Evalutions')
    plt.show()


def plot_path(output_dir, generation=-1):
    x, y, actual_generation = read_path(
        "{}/BestSalesMan.txt".format(output_dir), generation)
    plt.plot(x, y)
    plt.title("Genetic Travelling Salesman Path, generation {}".format(
        actual_generation))
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
    minCosts = []
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
        minCosts.append(np.mean(runMinCosts, axis=0))
        avgCosts.append(np.mean(runAvgCosts, axis=0))
        evals.append(np.mean(runEvals, axis=0))
    return evals, maxCosts, minCosts, avgCosts


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
    args = parser.parse_args()
    runs = args.runs
    labels = args.labels
    config = args.config
    logconfig = args.log
    evals, maxCost, minCost, avgCost = average_runs(runs)
    for i in range(len(labels)):
        if logconfig is not None and logconfig == "y":
            evals[i] = [log(x) for x in evals[i]]
        if config == "max":
            plt.plot(evals[i], maxCost[i], label=labels[i])
        elif config == "min":
            plt.plot(evals[i], minCost[i], label=labels[i])
        elif config == "avg":
            plt.plot(evals[i], avgCost[i], label=labels[i])
    plt.legend()
    plt.title("Genetic Travelling Salesman")
    plt.ylabel('Costs')
    plt.xlabel('Evalutions')
    plt.show()
