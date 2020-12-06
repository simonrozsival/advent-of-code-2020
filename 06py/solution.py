from functools import reduce

with open("../day-06-csharp/in.txt") as f:
    groups = [g.replace("\n", " ").split() for g in f.read().split("\n\n")]

solution1 = sum(len({c for c in " ".join(g) if c != " "}) for g in groups)
print(f"Puzzle 1: {solution1}")

solution2 = sum(len(reduce(lambda acc, l: {c for c in l if c in acc}, g)) for g in groups)
print(f"Puzzle 2: {solution2}")
