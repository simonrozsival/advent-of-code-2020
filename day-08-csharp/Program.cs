using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using var inputFile = new StreamReader("./in.txt");
var program = inputFile.ReadToEnd()
    .Split("\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(Parse)
    .ToArray();

Console.WriteLine($"Puzzle 1: {ExecuteInfiniteLoop(program)}");
Console.WriteLine($"Puzzle 2: {ExecuteWithCorrection(program)}");

Instruction Parse(string line) {
    var args = line.Trim().Split(" ");
    var opcode = args[0];
    var argument = int.Parse(args[1]);
    return opcode switch {
        "acc" => new Acc(argument),
        "jmp" => new Jmp(argument),
        "nop" => new Nop(argument),
        _ => throw new ArgumentOutOfRangeException($"Unsupported opcode {opcode}")
    };
}

int ExecuteInfiniteLoop(Instruction[] program) {
    var executedInstructions = new HashSet<int>();
    var state = new ComputationState(IP: 0, Accumulator: 0);

    while (!executedInstructions.Contains(state.IP)) {
        executedInstructions.Add(state.IP);
        state = program[state.IP].Execute(state);
    }

    return state.Accumulator;
}

int ExecuteWithCorrection(Instruction[] program)
    => AllPossibleFixes(program).Select(ExecuteToCompletion).Where(result => result != null).First().Value;

int? ExecuteToCompletion(Instruction[] program) {
    var executedInstructions = new HashSet<int>();
    var state = new ComputationState(IP: 0, Accumulator: 0);

    while (state.IP < program.Length) {
        executedInstructions.Add(state.IP);
        state = program[state.IP].Execute(state);

        if (executedInstructions.Contains(state.IP)) {
            return null; // we detected an infinite loop!
        }
    }

    return state.Accumulator;
}

IEnumerable<Instruction[]> AllPossibleFixes(Instruction[] program) {
    for (int i = 0; i < program.Length; i++) {
        var instruction = program[i];
        if (instruction is Nop) yield return program.Replace(i, new Jmp(instruction.Argument));
        else if (instruction is Jmp) yield return program.Replace(i, new Nop(instruction.Argument));
    }
}

record ComputationState(int IP, int Accumulator);

abstract record Instruction(int Argument) {
    public abstract ComputationState Execute(ComputationState currentState);
}

sealed record Acc(int Argument) : Instruction(Argument) {
    public override ComputationState Execute(ComputationState currentState)
        => new ComputationState(IP: currentState.IP + 1, Accumulator: currentState.Accumulator + Argument);
}

sealed record Jmp(int Argument) : Instruction(Argument) {
    public override ComputationState Execute(ComputationState currentState)
        => currentState with { IP = currentState.IP + Argument };
}

sealed record Nop(int Argument) : Instruction(Argument) {
    public override ComputationState Execute(ComputationState currentState)
        => currentState with { IP = currentState.IP + 1 };
}

static class Extensions {
    public static Instruction[] Replace(this Instruction[] program, int i, Instruction instruction) {
        var list = new List<Instruction>(program);
        list[i] = instruction;
        return list.ToArray();
    }
}
