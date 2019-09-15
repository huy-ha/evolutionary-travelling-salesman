import random

def create_cities(cities_count=100,filename="cities.txt",seed=0):
    f = open(filename,"w+")
    random.seed(seed)
    for i in range(cities_count):
        f.write("{} {}\n".format(random.random(),random.random()))
    f.close() 


if __name__ == "__main__":
    create_cities()