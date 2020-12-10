using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var inputFile = new StreamReader("./in.txt");
var adapters =
    inputFile.ReadToEnd()
        .Split("\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .OrderBy(joltage => joltage)
        .Prepend(0)
        .ToArray();

var targetJoltage = adapters.Max() + 3;

Console.WriteLine($"Puzzle 1: {solvePuzzle1()}");
Console.WriteLine($"Puzzle 2: {solvePuzzle2UsingDynamicProgramming()}");

Func<(long, long), bool> differBy(long n)
    => p => p.Item2 - p.Item1 == n;

long solvePuzzle1() {
    var pairs = adapters.Zip(adapters.Skip(1));
    var differBy1 = pairs.Where(differBy(1)).Count();
    var differBy3 = pairs.Where(differBy(3)).Count() + 1;

    return differBy1 * differBy3;
}

long solvePuzzle2UsingDynamicProgramming() {
    var posibilitiesForAdapter = new long[targetJoltage + 1];
    posibilitiesForAdapter[targetJoltage] = 1;
    foreach (var i in adapters.Reverse()) {
        posibilitiesForAdapter[i] =
            posibilitiesForAdapter[i + 1]
            + posibilitiesForAdapter[i + 2]
            + posibilitiesForAdapter[i + 3];
    }

    return posibilitiesForAdapter[0];
}
