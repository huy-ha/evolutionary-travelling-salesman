import matplotlib.pyplot as plt
import re
import os
import sys
import argparse
import math
import numpy as np


def read_file(filename):
    return [float(x) for x in open(filename, "r").readlines()]


if __name__ == "__main__":
    dirname = "run50-dotplot-path-animation"
    filename = "output/{}/PopulationSnapShots.txt".format(dirname)
    evals_filename = "output/{}/Evaluations.txt".format(dirname)
    populationsnapshot = open(filename, "r")
    evals = read_file(evals_filename)
    currentgeneration = 0
    x = []
    y = []
    while True:
        line = populationsnapshot.readline()
        populationfitness_s = line.split('|')
        if len(populationfitness_s) == 1:
            break
        population = [float(individual)
                      for individual in populationfitness_s]
        for individual in population:
            x.append(evals[currentgeneration])
            y.append(individual)
        currentgeneration += 1
    populationsnapshot.close()
    plt.scatter(x, y)
    plt.title("Dot Plot for a population of Insert Hill Climbers")
    plt.xlabel("Evaluations")
    plt.ylabel("Fitness")
    plt.show()
