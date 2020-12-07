using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using RuleSet = System.Collections.Generic.Dictionary<Bag, Rule>;

using var inputFile = new StreamReader("./in.txt");
var input = inputFile.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
var rules = input.Select(Parse);
var ruleSet = rules.ToDictionary(rule => rule.Bag);

var myBag = new Bag("shiny gold");

var result1 = rules.Count(rule => rule.CanContain(myBag, ruleSet));
var result2 = myBag.MustContain(ruleSet);

Console.WriteLine($"Puzzle 1: {result1}");
Console.WriteLine($"Puzzle 1: {result2}");

Rule Parse(string line) {
    var containingBagRegex = new Regex(@"(?<color>[a-zA-Z ]+) bags contain ");
    var containedBagsRegex = new Regex(@"(?<count>\d+) (?<color>[a-zA-Z ]+) bags?");
    var bagColor = containingBagRegex.Match(line).Groups[1].Value;
    var allowedContent = new List<AllowedContent>();
    foreach (Match match in containedBagsRegex.Matches(line)) {
        var bag = new AllowedContent(
            Count: int.Parse(match.Groups["count"].Value),
            Bag: new(match.Groups["color"].Value)
        );
        allowedContent.Add(bag);
    }

    return new(new Bag(bagColor), allowedContent.ToArray());
}

// types
record Bag(string Color) {
    public int MustContain(RuleSet rules)
        => rules[this].AllowedContent.Sum(content => content.Count * (1 + content.Bag.MustContain(rules)));
}
record AllowedContent(int Count, Bag Bag);
record Rule(Bag Bag, AllowedContent[] AllowedContent) {
    public bool CanContain(Bag bag, RuleSet ruleSet) => CanContainDirectly(bag) || CanContainNested(bag, ruleSet);
    private bool CanContainDirectly(Bag bag)
        => AllowedContent.Any(allowed => allowed.Bag == bag);
    private bool CanContainNested(Bag bag, RuleSet ruleSet)
        => AllowedContent.Select(allowed => ruleSet[allowed.Bag]).Any(rule => rule.CanContain(bag, ruleSet));
}
