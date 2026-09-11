namespace CodePuzzleCLI.UnitTests;

public class OutputFormatterTests
{
    [Fact]
    public void CreateInOrderOutputFormat_CodePuzzleExampleInputString_ShouldFormatSuccessfully()
    {
        var parentNodeType = new Node("type", 0, null);
        var parentNodeCustomFields = new Node("customFields", 1, parentNodeType);
        var nodes = new List<Node>
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
        const string expectedResult =
            """
            - id
            - name
            - email
            - type
              - id
              - name
              - customFields
                - c1
                - c2
                - c3
            - externalId
            """;

        var actualResult = OutputFormatter.CreateInOrderOutputFormat(nodes);
        
        Assert.Equal(expectedResult, actualResult);
    }
    
    [Fact]
    public void CreateAlphabeticalOutputFormat_CodePuzzleExampleInputString_ShouldFormatSuccessfully()
    {
        var parentNodeType = new Node("type", 0, null);
        var parentNodeCustomFields = new Node("customFields", 1, parentNodeType);
        var nodes = new List<Node>
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
        const string expectedResult =
            """
            - email
            - externalId
            - id
            - name
            - type
              - customFields
                - c1
                - c2
                - c3
              - id
              - name
            """;

        var actualResult = OutputFormatter.CreateAlphabeticalOutputFormat(nodes);
        
        Assert.Equal(expectedResult, actualResult);
    }
}