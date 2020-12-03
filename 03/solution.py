
def read_all(name):
  with open(name) as f:
    return [l.strip() for l in f.read().splitlines()]

test_input = read_all("test.txt") if True else None
puzzle_input = read_all("in.txt") if True else None

# ------------------

def puzzle1(lines):
  trees = 0
  left = 0
  for line in lines:
    if line[left % len(line)] == "#":
      trees = trees + 1
    left = left + 3

  return str(trees)


# ------------------


def puzzle2(lines):
  result = 1
  slopes = [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]

  for slope_x, slope_y in slopes:
    top = 1
    left = 0
    trees = 0

    for top in range(len(lines)):
      if top % slope_y != 0:
        continue

      line = lines[top]
      if line[left % len(line)] == "#":
        trees = trees + 1
      left = left + slope_x

    result = result * trees

  return str(result)




# ------------------

if test_input is not None:
  print("### TEST ###")

  result = puzzle1(test_input)
  print("---")
  print("> puzzle 1 result: " + result)
  print("---")


  result = puzzle2(test_input)
  print("---")
  print("> puzzle 2 result: " + result)
  print("---")
  print()

  print("### TEST END ###")
  print()
  print()

if puzzle_input is not None:
  result = puzzle1(puzzle_input)
  print("---")
  print("> puzzle 1 result: " + result)
  print("---")
  print()

  result = puzzle2(puzzle_input)
  print("---")
  print("> puzzle 2 result: " + result)
  print("---")