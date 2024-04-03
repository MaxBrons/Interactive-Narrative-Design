using System.Collections.Generic;
using System.Linq;

public struct DialogueLine
{
    public string Header;
    public string Content;

    public DialogueLine(string header, string content)
    {
        Header = header;
        Content = content;
    }
}

public class DialogueTree
{
    public List<INodeData> Nodes { get; }
    public bool EndReached => GetConnections(_currentNode).Count == 0;

    private Dictionary<ulong, int> _nodesLookup = new();
    private INodeData _currentNode;

    public DialogueTree(INodeData startData)
    {
        Nodes = new() { startData };
        _currentNode = startData;
    }

    public void Stack(INodeData data)
    {
        Nodes.Add(data);
        _nodesLookup.Add(data.ID, Nodes.Count - 1);
    }

    public DialogueLine Pop()
    {
        if (_currentNode is not TextNodeExportData data) {
            _currentNode = GetChoice(GetConnections(_currentNode));
            return new("...", "Interactive narrative by Erik, Owen & Max");
        }

        _currentNode = GetChoice(GetConnections(_currentNode));
        return new(data.Title, data.Content);
    }

    public INodeData GetChoice(List<INodeData> data)
    {
        //if (data.Count == 1)
        return data.FirstOrDefault();

        // TODO: implement choice through UI
        return null;
    }

    private List<INodeData> GetConnections(INodeData current)
    {
        if (current.Connections.Count < 0)
            return null;

        List<INodeData> result = new();

        foreach (var connection in current.Connections) {
            int index = _nodesLookup[ulong.Parse(connection.ToNode.Trim())];
            var connectedNode = Nodes[index];

            result.Add(connectedNode);
        }

        return result;
    }
}
