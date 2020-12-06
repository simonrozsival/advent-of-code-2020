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

static Seat Parse(string line) {
    var bin = line.Replace('B', '1').Replace('F', '0').Replace('R', '1').Replace('L', '0');
    return new(Convert.ToInt32(bin, fromBase: 2));
}

record Seat(int Id) {
    public int Row => Id >> 3;
    public int Column => Id & 7;
}
