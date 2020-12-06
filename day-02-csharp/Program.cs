using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

var inputs = ReadAllLines().Select(Parse).ToList();
int validFirstStar = inputs.Where(IsValidMinMax).Count();
int validSecondStar = inputs.Where(IsValidXor).Count();

Console.WriteLine("First part of the assignment: {0}", validFirstStar);
Console.WriteLine("Second part of the assignment: {0}", validSecondStar);

static IEnumerable<string> ReadAllLines() {
    var lines = new List<string>();
    string line;
    while ((line = Console.ReadLine()) != null) {
        lines.Add(line);
    }

    return lines;
}

static bool IsValidXor(Input input) {
    var first = input.password[input.policy.first - 1] == input.policy.letter;
    var second = input.password[input.policy.second - 1] == input.policy.letter;
    return first ^ second;
}

static bool IsValidMinMax(Input input) {
    var count = input.password.ToCharArray().Where(c => c == input.policy.letter).Count();
    return count >= input.policy.first && count <= input.policy.second;
}

static Input Parse(string line) {
    var split = line.Split(':');
    var policy = ParsePolicy(split[0]);
    var password = split[1].Trim();

    return new(policy, password);
}

static LetterPolicy ParsePolicy(string input) {
    var match = Regex.Match(input, "([0-9]+)-([0-9]+) ([a-z])");
    var firstPosition = int.Parse(match.Groups[1].Value);
    var secondPosition = int.Parse(match.Groups[2].Value);
    var letter = match.Groups[3].Value[0];

    return new(firstPosition, secondPosition, letter);
}

record LetterPolicy(int first, int second, char letter);
record Input(LetterPolicy policy, string password);
