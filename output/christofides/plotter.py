import matplotlib.pyplot as plt
import re
import os
import sys
import argparse
import math
import numpy as np

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
    # print(len(maxCostsErr))
    # print(len(maxCostsErr[0]))
    # print(len(maxCostsErr[0][0]))
    # exit()
    return evals, maxCosts, minCosts, avgCosts, maxCostsErr, minCostsErr


if __name__ == "__main__":

    file = open("tsp.tour", "r")
    lines = file.readlines()
    x = []
    y = []
    cities = [line.split(' ')[-2:] for line in lines]
    for city in cities:
        x.append(float(city[0]))
        y.append(float(city[1]))
    plt.plot(x, y)
    plt.title("Christofides Path")
    plt.show()
