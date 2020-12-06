from functools import reduce

with open("./in.txt") as f:
    groups = [[l for l in g.split("\n") if l != ""] for g in f.read().split("\n\n")]

def solve_puzzle_1():
    return sum(len({c for c in " ".join(g) if c != " "}) for g in groups)

def solve_puzzle_2():
    return sum(len(reduce(lambda acc, l: {c for c in l if c in acc}, g)) for g in groups)

print(solve_puzzle_1())
print(solve_puzzle_2())
