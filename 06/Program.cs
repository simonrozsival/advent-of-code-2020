using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var inputFile = new StreamReader("./in.txt");
var input = inputFile.ReadToEnd()
  .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
  .Select(group => group.Split("\n", StringSplitOptions.RemoveEmptyEntries))
  .ToArray();

Console.WriteLine($"Puzzle 1: {solvePuzzle1(input)}");
Console.WriteLine($"Puzzle 2: {solvePuzzle2(input)}");

int solvePuzzle1(IEnumerable<string[]> groups)
  => groups.Select(union).Sum(union => union.Count);

int solvePuzzle2(IEnumerable<string[]> groups)
  => groups.Select(intersect).Sum(intersection => intersection.Length);

HashSet<char> union(string[] group)
  => group.Aggregate(seed: new HashSet<char>(), unionTwo);

HashSet<char> unionTwo(HashSet<char> acc, string next)
  => acc.Union(next.ToCharArray()).ToHashSet();

char[] intersect(string[] group)
  => group.Aggregate(seed: group[0].ToCharArray(), intersectTwo);

char[] intersectTwo(char[] acc, string next)
  => acc.Where(c => next.Contains(c)).ToArray();
