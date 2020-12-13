from functools import reduce

with open("./in.txt") as file:
    input = [line for line in file.read().split("\n") if line != ""]

instructions = list(map(lambda line: (line[0], int(line[1:])), input))
initial_state = ('E', 0, 0)

turn_left = {
    "N": "W",
    "W": "S",
    "S": "E",
    "E": "N"
}

turn_right = {
    "N": "E",
    "W": "N",
    "S": "W",
    "E": "S"
}

def execute_step(state, instruction):
    (command, steps) = instruction
    (heading, ew, ns) = state

    if command == "F":
        command = heading

    if command == "N":
        return (heading, ew, ns + steps)
    elif command == "S":
        return (heading, ew, ns - steps)
    elif command == "E":
        return (heading, ew + steps, ns)
    elif command == "W":
        return (heading, ew - steps, ns)
    elif command == "R":
        return (turn(heading, steps), ew, ns)
    elif command == "L":
        return (turn(heading, -steps), ew, ns)
    else:
        raise Exception(f"invalid command {command}")

def turn(heading, degrees):
    n = abs(degrees // 90)
    for i in range(n):
        if degrees > 0:
            heading = turn_right[heading]
        else:
            heading = turn_left[heading]

    return heading


def execute_steps(state, instructions):
    return reduce(execute_step, instructions, state)

def distance(state):
    (_, ew, ns) = state
    return abs(ew) + abs(ns)

# puzzle 1
final_state = execute_steps(initial_state, instructions)
print(f"Puzzle 1: {distance(final_state)}")
