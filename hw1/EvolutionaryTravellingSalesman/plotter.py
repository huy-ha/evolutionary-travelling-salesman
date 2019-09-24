import matplotlib.pyplot as plt
import re
# TODO plot error bars
# TODO plot log of data

def read_file(filename):
    return [float(x) for x in open(filename, "r").readlines()]


def read_path(filename, generation):
    file = open(filename, "r")
    while True:
        curr = int(re.findall('\d+', file.readline())[0])
        if curr >= generation:
            break
    cities = [city.split(' ') for city in file.readline().split('|')]
    x = []
    y = []
    for city in cities:
        x.append(float(city[0]))
        y.append(float(city[1]))
    return x, y


def plot_costs():
    maxCosts = read_file("output/MaxCosts.txt")
    minCosts = read_file("output/MinCosts.txt")
    avgCosts = read_file("output/AvgCosts.txt")

    plt.plot(maxCosts, label="Max Costs")
    plt.plot(minCosts, label="Min Costs")
    plt.plot(avgCosts, label="Average Costs")
    plt.legend()
    plt.title("Genetic Travelling Salesman")
    plt.ylabel('Costs')
    plt.xlabel('Generation')
    plt.show()


def plot_path(generation=0):
    x, y = read_path("output/BestSalesMan.txt", generation)
    plt.plot(x, y)
    plt.title("Genetic Travelling Salesman Path, generation {}".format(generation))
    plt.show()


if __name__ == "__main__":
    # plot_costs()
    plot_path(generation=49999)
