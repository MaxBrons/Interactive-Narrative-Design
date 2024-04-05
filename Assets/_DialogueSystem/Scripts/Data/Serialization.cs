
using System.Collections.Generic;

public class GraphConnectionsData : List<GraphConnectionsData.GraphConnectionPair>
{
    public class GraphConnectionPair
    {
        public string FromNode { get; set; }
        public int FromPort { get; set; }
        public string ToNode { get; set; }
        public int ToPort { get; set; }
    }
}

public interface INodeData
{
    public ulong ID { get; set; }
    public string Title { get; set; }
    public GraphConnectionsData Connections { get; set; }
}

public class StartNodeExportData : INodeData
{
    public ulong ID { get; set; }
    public string Title { get; set; }
    public GraphConnectionsData Connections { get; set; }
}

public class TextNodeExportData : INodeData
{
    public ulong ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public GraphConnectionsData Connections { get; set; }

    public string ToDialogueLine()
    {
        return "[" + (Title != string.Empty ? Title : "...") + "] " + Content;
    }
}