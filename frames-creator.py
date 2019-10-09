import matplotlib.pyplot as plt
import re

if __name__ == "__main__":
    dir = "output/run50-dotplot-path-animation"
    output_dir = "output/frames/"
    file = open("{}/BestSalesMan.txt".format(dir), "r")
    last_line = None
    while True:
        curr_generation = int(re.findall('\d+', file.readline())[0])
        print("Current Generation: {}".format(curr_generation))
        cities = [city.split(' ') for city in file.readline().split('|')]
        x = []
        y = []
        for city in cities:
            x.append(float(city[0]))
            y.append(float(city[1]))
        plt.plot(x, y)
        plt.title("Generation {}".format(curr_generation))
        plt.savefig("{}g{}.png".format(output_dir, curr_generation))
        plt.clf()
    file.close()
