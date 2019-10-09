import random
import math


def create_cities(cities_count=100, filename="cities.txt", seed=0):
    f = open(filename, "w+")
    random.seed(seed)
    for i in range(cities_count):
        f.write("{} {}\n".format(random.random(), random.random()))
    f.close()


def create_circle(cities_count=100, filename="circle.txt"):
    f = open(filename, "w+")
    for i in range(cities_count):
        t = random.random() * math.pi * 2
        f.write("{} {}\n".format(
            math.sin(t), math.cos(t)))
    f.close()


if __name__ == "__main__":
    create_circle()
