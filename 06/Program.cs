using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var inputFile = new StreamReader("./in.txt");
var input = inputFile.ReadToEnd().Split("\n").ToList();

Console.WriteLine($"Puzzle 1: {solvePuzzle1(input)}");
Console.WriteLine($"Puzzle 2: {solvePuzzle2(input)}");

// --- Puzzle 1

int solvePuzzle1(List<string> input) {
  int total = 0;
  var currentGroup = new SortedSet<char>();
  input.Add(""); // extra empty line to flush the last group

  foreach (var line in input) {
    if (line == "") {
      total += currentGroup.Count;
      currentGroup.Clear();
    } else {
      foreach (var c in line.ToCharArray()) currentGroup.Add(c);
    }
  }

  return total;
}

// --- Puzzle 2

int solvePuzzle2(List<string> input) {
  int total = 0;
  List<char> currentGroup = null;
  input.Add(""); // extra empty line to flush the last group

  foreach (var line in input) {
    if (line == "") {
      total += currentGroup?.Count ?? 0;
      currentGroup = null;
    } else {    
      if (currentGroup == null) {
        currentGroup = new List<char>(line.ToCharArray());
      } else {
        currentGroup = currentGroup.Where(c => line.Contains(c)).ToList();
      }
    }
  }

  return total;
}
