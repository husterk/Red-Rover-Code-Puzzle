using CodePuzzleCLI;

Console.WriteLine("Please provide the input string:");
var inputString = Console.ReadLine();

IEnumerable<Node> nodes;
try
{
    nodes = InputParser.ParseInputString(inputString);
    if (!nodes.Any())
    {
        Console.WriteLine("Error: Parsed input string resulted in an empty list.");
        return 2;
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error: Failed to parse input string: {e.Message}");
    return 1;
}

Console.WriteLine();

Console.WriteLine("Output provided in in-order format:");
Console.WriteLine("-----------------------------------");
Console.WriteLine(OutputFormatter.CreateInOrderOutputFormat(nodes));

Console.WriteLine();

Console.WriteLine("Output provided in alphabetical format:");
Console.WriteLine("---------------------------------------");
Console.WriteLine(OutputFormatter.CreateAlphabeticalOutputFormat(nodes));

return 0;