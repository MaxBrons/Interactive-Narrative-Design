
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

public class StartNodeExportData
{
    public ulong ID { get; set; }
    public string Title { get; set; }
    public GraphConnectionsData Connections { get; set; }
}

public class TextNodeExportData
{
    public ulong ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public GraphConnectionsData Connections { get; set; }
}