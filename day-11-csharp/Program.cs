using System;
using System.IO;
using System.Linq;

using var inputFile = new StreamReader("./in.txt");
var grid =
    inputFile.ReadToEnd()
        .Split("\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(line => line.ToCharArray())
        .ToArray();

void printGrid(char[][] grid) {
    foreach (var row in grid) {
        foreach (var col in row) {
            Console.Write(col);
        }
        Console.WriteLine();
    }
}

printGrid(grid);

const char floor = '.';
const char occupied = '#';
const char empty = 'L';

static class Extensions {
    public static int CalculateNeighbors(this char[][] grid, int row, int column, char type) {
        int count = 0;
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (i == 0 && j == 0) continue;
                if (row + i < 0 || row + i >= grid.Length) continue;
                if (column + j < 0 || column + j >= grid[0].Length) continue;
                if (grid[row + i][column + j] == type) count++;
            }
        }

        return count;
    }

    public static char[][] CalculateNextStep(this char[][] grid)
        => grid.Select((row, i) => row.Select((c, j) => c.NextStep(i, j, grid)).ToArray()).ToArray();

    public static char NextStep(this char c, int row, int column, char[][] grid) {
        if (c == empty && grid.CalculateNeighbors(row, column, occupied) == 0) {
            return occupied;
        }
        return c;
    }

}
