using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using var inputFile = new StreamReader("./in.txt");
var input = inputFile.ReadToEnd()
    .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(group => group.Split("\n", StringSplitOptions.RemoveEmptyEntries))
    .ToArray();

Console.WriteLine($"Puzzle 1: {solvePuzzle1(input)}");
Console.WriteLine($"Puzzle 2: {solvePuzzle2(input)}");

int solvePuzzle1(IEnumerable<string[]> groups)
    => groups.Select(group => group.Aggregate(
            seed: Enumerable.Empty<char>(),
            func: (acc, next) => acc.Union(next)))
        .Sum(union => union.Count());

int solvePuzzle2(IEnumerable<string[]> groups)
    => groups.Select(group => group.Aggregate(
            seed: group.FirstOrDefault() ?? Enumerable.Empty<char>(),
            func: (acc, next) => acc.Where(next.Contains)))
        .Sum(intersection => intersection.Count());
