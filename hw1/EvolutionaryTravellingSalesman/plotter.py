import matplotlib.pyplot as plt
import re
import os
import sys

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


if __name__ == "__main__":
    if(len(sys.argv) > 1):
        try:
            runNumber = int(sys.argv[1])
        except:
            print("Usage: python {} <run_number>".format(sys.argv[0]))
            exit()
        if runNumber is 0:
            plot_costs('output')
            plot_path('output')
        else:
            runs = [runs for runs in os.listdir(
                'output') if os.path.isdir(os.path.join('output', runs))]
            rundir = None
            try:
                rundir = "output/{}".format(next(run for run in runs if run.find(
                    "run{}".format(sys.argv[1])) is not -1))
            except:
                print("Run {} not found!".format(sys.argv[1]))
                exit()
            plot_costs(rundir)
            plot_path(rundir)
    else:
        print("Usage: python {} <run_number>".format(sys.argv[0]))
        exit()
