namespace CodePuzzleCLI;

public static class OutputFormatter
{
    public static string CreateInOrderOutputFormat(IEnumerable<Node> nodes) =>
        string.Join(
            Environment.NewLine,
            nodes
                .Select(node => FormatLineItem(node.Depth, node.Name)));

    public static string CreateAlphabeticalOutputFormat(IEnumerable<Node> nodes) =>
        CreateInOrderOutputFormat(SortNodesAlphabetically(nodes));
    
    private static string FormatLineItem(int depth, string item) =>
        $"{string.Empty.PadLeft(depth * 2)}- {item}";
    
    private static IEnumerable<Node> SortNodesAlphabetically(IEnumerable<Node> nodes)
    {
        if (!nodes.Any())
        {
            return Enumerable.Empty<Node>();   
        }
        
        var nodesLookup = nodes.ToLookup(node => node.Parent);
        var sortedRootNodes = nodesLookup[null].OrderBy(n => n.Name);
        var sortedNodes = new List<Node>(nodes.Count());
        foreach (var sortedRootNode in sortedRootNodes)
        {
            RecursivelySortNodes(sortedRootNode, nodesLookup, sortedNodes);
        }

        return sortedNodes;
    }

    private static void RecursivelySortNodes(Node currentNode, ILookup<Node?, Node> nodesLookup, List<Node> sortedNodes)
    {
        sortedNodes.Add(currentNode);
        
        var sortedChildNodes = nodesLookup[currentNode].OrderBy(c => c.Name);
        foreach (var sortedChildNode in sortedChildNodes)
        {
            RecursivelySortNodes(sortedChildNode, nodesLookup, sortedNodes);
        }
    }
}