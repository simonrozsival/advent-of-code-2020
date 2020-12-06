import sys

input = [l.strip() for l in sys.stdin.readlines()]

# ------------------

def merge_input():
  lines = []
  acc = ""
  for line in input:
    if line == "":
      lines.append(acc.strip())
      acc = ""
    else:
      acc = acc + " " + line
  lines.append(acc.strip())
  return lines

def to_passport(line):
  pairs = line.split(" ")
  keyVals = [pair.split(":") for pair in pairs]
  return {pair[0]: pair[1] for pair in keyVals}

# ------------------

def is_valid1(passport):
  required_keys = set(["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"])
  keys = passport.keys()
  valid = len(keys) >= 7 and required_keys.issubset(set(keys))
  return valid

def puzzle1():
  raw_passsports = merge_input()
  passports = map(to_passport, raw_passsports)
  valid = list(filter(is_valid1, passports))
  return len(valid)

# ------------------

valid_hgt_cm = lambda val: 150 <= int(val) <= 193
valid_hgt_in = lambda val: 59 <= int(val) <= 76
valid_hgt = lambda val: valid_hgt_cm(val[:-2]) if val[-2:] == "cm" else valid_hgt_in(val[:-2])
validators = {
  "byr": lambda val: 1920 <= int(val) <= 2002,
  "iyr": lambda val: 2010 <= int(val) <= 2020,
  "eyr": lambda val: 2020 <= int(val) <= 2030,
  "hgt": lambda val: len(val) > 2 and valid_hgt(val),
  "hcl": lambda val: len(val) == 7 and val[0] == "#" and len(val[1:].strip("abcdef0123456789")) == 0,
  "ecl": lambda val: val in list(["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]),
  "pid": lambda val: len(val) == 9 and val.isdigit(),
  "cid": lambda val: True
}

def validate(key, val):
  validator = validators[key]
  return validator(val)

def is_valid2(passport):
  return is_valid1(passport) and all(validate(key, passport[key]) for key in passport)

def puzzle2():
  raw_passsports = merge_input()
  passports = map(to_passport, raw_passsports)
  valid = list(filter(is_valid2, passports))
  return len(valid)

# ------------------

result = puzzle1()
print(f"---\n> puzzle 1 result: {result}\n---")

result = puzzle2()
print(f"---\n> puzzle 2 result: {result}\n---")