from functools import reduce

with open("../day-06-csharp/in.txt") as f:
    grouped_lines = f.read().split("\n\n")
    grouped_words = [g.replace("\n", " ") for g in grouped_lines]
    groups = [[set(w) for w in g.split()] for g in grouped_words]

solution1 = sum(len(set.union(*g)) for g in groups)
print(f"Puzzle 1: {solution1}")

solution2 = sum(len(set.intersection(*g)) for g in groups)
print(f"Puzzle 2: {solution2}")
