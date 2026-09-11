namespace CodePuzzleCLI;

public class Node
{
    public string Name { get; }
    public int Depth { get; }
    public Node? Parent { get; }

    public Node(string name, int depth, Node? parent)
    {
        Name = name;
        Depth = depth;
        Parent = parent;
    }
}