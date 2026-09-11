namespace CodePuzzleCLI;

public static class InputParser
{
    public static IEnumerable<Node> ParseInputString(string? inputString)
    {
        ArgumentNullException.ThrowIfNull(inputString);

        if (!inputString.StartsWith('(')
            || !inputString.EndsWith(')'))
        {
            // Input is not wrapped in the required parentheses "(...)".
            throw new ArgumentException("Input string must start with '(' and end with ')'");
        }
        
        var openParenthesesCount = inputString.Count(c => c == '(');
        var closeParenthesesCount = inputString.Count(c => c == ')');
        if (openParenthesesCount != closeParenthesesCount)
        {
            // Missing wrapping parentheses at the root level.
            throw new ArgumentException("Input string must contain matching pairs of '(' and ')'");
        }
        
        var trimmedInputString = inputString.Trim('(', ')').Trim();
        if (trimmedInputString.Length == 0)
        {
            throw new ArgumentException("Input string must not be empty");
        }
        
        var currentDepth = 0;
        var nodes = new List<Node>();
        var parentNodeHierarchy = new List<Node>();
        
        if (!trimmedInputString.Contains(','))
        {
            //The input contains no commas, so it has only one item.
            nodes.Add(new Node(trimmedInputString, currentDepth, parentNodeHierarchy.LastOrDefault()));
            return nodes;
        }
        
        var rawItems = trimmedInputString.Split(',');
        foreach (var rawItem in rawItems)
        {
            var trimmedItem = rawItem.Trim();
            if (trimmedItem.Length == 0)
            {
                // Empty item, skip it.
                continue;
            }
            
            if (trimmedItem.Contains('('))
            {
                // Item at current depth + item at next deeper depth.
                var rawSubItems = trimmedItem.Split('(');
                nodes.Add(new Node(rawSubItems[0].Trim(), currentDepth, parentNodeHierarchy.LastOrDefault()));
                currentDepth++;
                parentNodeHierarchy.Add(nodes.Last());
                nodes.Add(new Node(rawSubItems[1].Trim(), currentDepth, parentNodeHierarchy.LastOrDefault()));
                continue;
            }

            if (trimmedItem.Contains(')'))
            {
                // Item at current depth + rise up n depth levels.
                var closingParenthesesCount = trimmedItem.Count(c => c == ')');
                nodes.Add(new Node(trimmedItem.Substring(0, trimmedItem.Length - closingParenthesesCount).Trim(), currentDepth,parentNodeHierarchy.LastOrDefault()));
                currentDepth -= closingParenthesesCount;
                parentNodeHierarchy.RemoveRange(currentDepth, parentNodeHierarchy.Count - currentDepth);
                continue;
            }
            
            // Standard item at the current level.
            nodes.Add(new Node(trimmedItem, currentDepth, parentNodeHierarchy.LastOrDefault()));
        }
        
        return nodes;
    }
}