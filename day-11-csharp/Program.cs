using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Evolve = System.Func<State, int, int, State[][], State>;

using var inputFile = new StreamReader("./in.txt");
var grid =
    inputFile.ReadToEnd()
        .Split("\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(line => line.ToCharArray().Select(c => (State)c))
        .ToGrid();

State NextStepPuzzle1(State c, int row, int column, State[][] grid) {
    var occupiedNeighbors = grid.OccupiedImmediateNeighbors(row, column);
    return (c, occupiedNeighbors) switch {
        (State.Floor, _) => State.Floor,
        (State.Empty, 0) => State.Occupied,
        (State.Occupied, >=4) => State.Empty,
        _ => c,
    };
}

var puzzle1 = grid.EvolveUntilStable(NextStepPuzzle1).Sum(row => row.Count(c => c == State.Occupied));
Console.WriteLine($"Puzzle 1: {puzzle1}");

State NextStepPuzzle2(State c, int row, int column, State[][] grid) {
    var occupiedNeighbors = grid.ClosestOccupiedNeighbors(row, column);
    return (c, occupiedNeighbors) switch {
        (State.Floor, _) => State.Floor,
        (State.Empty, 0) => State.Occupied,
        (State.Occupied, >=5) => State.Empty,
        _ => c,
    };
}

var puzzle2 = grid.EvolveUntilStable(NextStepPuzzle2).Sum(row => row.Count(c => c == State.Occupied));
Console.WriteLine($"Puzzle 2: {puzzle2}");

static class Extensions {
    public static void Print(this State[][] grid) {
        foreach (var row in grid) {
            foreach (var col in row) {
                Console.Write((char)col);
            }
            Console.WriteLine();
        }
    }

    public static IEnumerable<(int r, int c)> Directions() {
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i == 0 && j == 0) continue;
                yield return (i, j);
            }
        }
    }

    public static int OccupiedImmediateNeighbors(this State[][] grid, int row, int column)
        => Directions()
            .Where(d => grid.FitsWithinBounds(row, column, d))
            .Select(d => grid[row + d.r][column + d.c])
            .Count(c => c == State.Occupied);

    public static int ClosestOccupiedNeighbors(this State[][] grid, int row, int column)
        => Directions()
            .Select(d => {
                int r = row;
                int c = column;
                while (grid.FitsWithinBounds(r, c, d)) {
                    if (grid[r + d.r][c + d.c] != State.Floor) {
                        return grid[r + d.r][c + d.c];
                    }

                    r += d.r;
                    c += d.c;
                }

                return State.Empty;
            })
            .Count(c => c == State.Occupied);

    public static State[][] EvolveUntilStable(this State[][] grid, Evolve nextStep) {
        var next = grid.Evolve(nextStep);
        while (grid.Differs(next)) {
            grid = next;
            next = grid.Evolve(nextStep);
        }

        return grid;
    }

    public static bool FitsWithinBounds(this State[][] grid, int row, int column, (int r, int c) offset)
        => row + offset.r >= 0
            && row + offset.r < grid.Length
            && column + offset.c >= 0
            && column + offset.c < grid[0].Length;

    public static State[][] Evolve(this State[][] grid, Evolve nextStep) 
        => grid.Select((r, i) => r.Select((c, j) => nextStep(c, i, j, grid))).ToGrid();

    public static bool Differs(this State[][] first, State[][] second) {
        for (int i = 0; i < first.Length; i++) {
            for (int j = 0; j < first[i].Length; j++) {
                if (first[i][j] != second[i][j]) return true;
            }
        }

        return false;
    }

    public static State[][] ToGrid(this IEnumerable<IEnumerable<State>> input)
        => input.Select(row => row.ToArray()).ToArray();
}

enum State {
    Floor = '.',
    Occupied = '#',
    Empty = 'L',
}
