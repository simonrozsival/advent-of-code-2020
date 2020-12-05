using System;
using System.IO;
using System.Linq;

Console.WriteLine("TEST");

var tests = new[] { "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" };
foreach (var test in tests) {
    Console.Write("{0}: ", test);
    var seat = Parse(test);
    Console.WriteLine("row {0}, column {1}, seat ID {2}", seat.Row, seat.Column, seat.Id);
}

using var inputReader = new StreamReader("./in.txt");
var input = inputReader.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);

Console.Write("Puzzle 1: ");
var highestSeatId = input.Select(Parse).Max(seat => seat.Id);
Console.WriteLine(highestSeatId);

Console.Write("Puzzle 2: ");
var sortedSeats = input.Select(Parse).OrderBy(seat => seat.Id).ToArray();
for (int i = 1; i < sortedSeats.Length - 1; i++) {
    if (sortedSeats[i].Id - sortedSeats[i - 1].Id == 2) {
        Console.WriteLine(sortedSeats[i].Id - 1);
        break;
    }
}

static Seat Parse(string line) => new(Row(line), Column(line));
static int Row(string line) => FromBinaryString(input: line.Substring(0, 7), one: 'B', zero: 'F');
static int Column(string line) => FromBinaryString(input: line.Substring(7, 3), one: 'R', zero: 'L');

static int FromBinaryString(string input, char one, char zero) {
    var bin = input.Replace(one, '1').Replace(zero, '0');
    return Convert.ToInt32(bin, fromBase: 2);
}

record Seat(int Row, int Column) {
    public int Id => Row * 8 + Column;
}
