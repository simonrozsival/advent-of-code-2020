using System;
using System.IO;
using System.Linq;

var inputFile = new StreamReader("./in.txt");
var adapters =
    inputFile.ReadToEnd()
        .Split("\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .OrderBy(a => a)
        .ToArray();

var margin = 3;
var targetJoltage = adapters.Max() + margin;

// Puzzle 1

var current = 0L;
var oneJolts = 0L;
var threeJolts = 0L;

while (targetJoltage - current > margin) {
    var next = adapters.Where(r => r > current).Where(r => r - current <= margin).Min();
    if (next - current == 1) oneJolts++;
    else if (next - current == 3) threeJolts++;
    current = next;
}

if (targetJoltage - current == 1) oneJolts++;
else if (targetJoltage - current == 3) threeJolts++;

var puzzle1 = oneJolts * threeJolts;
Console.WriteLine($"Puzzle 1: {puzzle1}");

// Puzzle 2

var cache = new long[adapters.Max()];

long NumberOfPossibleAdapterArrangements(long from) {
    if (from < cache.Length && cache[from] > 0) return cache[from];
    if (targetJoltage - from <= margin) return 1;

    var nextCandidates = adapters.Where(r => r > from).Where(r => r - from <= margin);
    var result = nextCandidates.Select(NumberOfPossibleAdapterArrangements).Sum();

    cache[from] = result;
    return result;
}

var puzzle2 = NumberOfPossibleAdapterArrangements(from: 0);
Console.WriteLine($"Puzzle 2: {puzzle2}");
