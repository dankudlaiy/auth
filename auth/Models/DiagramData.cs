namespace auth.Models;

public class DiagramData
{
    public List<NodeData> Nodes { get; set; } = new();
    public List<LinkData> Links { get; set; } = new();
}

public class NodeData
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public List<PortData> Ports { get; set; } = new();
}

public class PortData
{
    public string Id { get; set; }
    public string Alignment { get; set; }
}

public class LinkData
{
    public string SourcePortId { get; set; }
    public string TargetPortId { get; set; }
}