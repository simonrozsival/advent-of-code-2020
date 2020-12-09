using System;
using System.IO;
using System.Linq;

using var inputFile = new StreamReader("in.txt");
var input = inputFile.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

var puzzle1 = FindInvalidValue(preambleLength: 25, data: input);
Console.WriteLine("Puzzle 1: " + puzzle1);

var puzzle2 = FindEncryptionWeakness(puzzle1, data: input);
Console.WriteLine("Puzzle 2: " + puzzle2);

long FindInvalidValue(int preambleLength, long[] data) {
    for (int i = preambleLength; i < data.Length; i++) {
        if (!IsValid(i, preambleLength, data)) {
            return data[i];
        }
    }

    return 0;
}

bool IsValid(int i, int preambleLength, long[] data) {
    var n = data[i];
    for (int x = 1; x <= preambleLength; x++) {
        for (int y = 1; y <= preambleLength; y++) {
            if (x == y) continue;
            if (data[i - x] + data[i - y] == n) return true;
        }
    }

    return false;
}

long FindEncryptionWeakness(long sum, long[] data) {
    for (int i = 0; i < data.Length; i++) {
        long acc = data[i];
        for (int j = 1; j < data.Length; j++) {
            acc += data[i + j];
            if (acc == sum) return MinMaxBetween(i, i + j + 1, data);
            else if (acc > sum) break;
        }
    }

    return 0;
}

long MinMaxBetween(int start, int end, long[] data) {
    var min = data[start..end].Min();
    var max = data[start..end].Max();
    return min + max;
}
