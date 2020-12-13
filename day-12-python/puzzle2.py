from functools import reduce

with open("./in.txt") as file:
    input = [line for line in file.read().split("\n") if line != ""]

instructions = list(map(lambda line: (line[0], int(line[1:])), input))
initial_position = (0, 0)
initial_waypoint = (10, 1)
initial_state = (initial_position, initial_waypoint)

def execute_step(state, instruction):
    (command, steps) = instruction
    (position, waypoint) = state

    if command in ["N", "S", "W", "E"]:
        return (position, move_waypoint(command, steps, waypoint))
    if command == "F":
        return (move_towards(position, waypoint, steps), waypoint)
    elif command == "R":
        return (position, rotate_waypoint(waypoint, steps))
    elif command == "L":
        return (position, rotate_waypoint(waypoint, -steps))
    else:
        raise Exception(f"invalid command {command}")

def move_waypoint(command, steps, waypoint):
    (x, y) = waypoint
    if command == "N":
        return (x, y + steps)
    elif command == "S":
        return (x, y - steps)
    elif command == "E":
        return (x + steps, y)
    elif command == "W":
        return (x - steps, y)

def move_towards(position, waypoint, steps):
    (p_x, p_y) = position
    (wp_x, wp_y) = waypoint
    return (p_x + steps * wp_x, p_y + steps * wp_y)

def rotate_waypoint(waypoint, degrees):
    steps = abs(degrees // 90)
    for i in range(steps):
        if degrees > 0:
            waypoint = (waypoint[1], -waypoint[0])
        else:
            waypoint = (-waypoint[1], waypoint[0])

    return waypoint

def execute_steps(state, instructions):
    return reduce(execute_step, instructions, state)

def distance(state):
    (position, _) = state
    (x, y) = position
    return abs(x) + abs(y)

# puzzle 1
final_state = execute_steps(initial_state, instructions)
print(f"Puzzle 2: {distance(final_state)}")
