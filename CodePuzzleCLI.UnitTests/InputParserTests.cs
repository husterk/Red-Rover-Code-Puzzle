namespace CodePuzzleCLI.UnitTests;

public class InputParserTests
{
    [Fact]
    public void ParseInputString_CodePuzzleExampleInputString_ShouldParseSuccessfully()
    {
        const string inputString = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        var parentNodeType = new Node("type", 0, null);
        var parentNodeCustomFields = new Node("customFields", 1, parentNodeType);
        var expectedResult = new List<Node>
        {
            new Node("id", 0, null),
            new Node("name", 0, null),
            new Node("email", 0, null),
            parentNodeType,
            new Node("id", 1, parentNodeType),
            new Node("name", 1, parentNodeType),
            parentNodeCustomFields,
            new Node("c1", 2, parentNodeCustomFields),
            new Node("c2", 2, parentNodeCustomFields),
            new Node("c3", 2, parentNodeCustomFields),
            new Node("externalId", 0, null),
        };

        var actualResult = InputParser.ParseInputString(inputString);
        
        Assert.Equivalent(expectedResult, actualResult);
    }
}