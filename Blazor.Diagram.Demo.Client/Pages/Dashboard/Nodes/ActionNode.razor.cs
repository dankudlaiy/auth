using Blazor.Diagram.Demo.Client.Models;
using Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Diagram.Demo.Client.Pages.Dashboard.Nodes;

public partial class ActionNode
{
    [Parameter] public ActionNodeModel Node { get; set; } = null!;
}

[NodeParameter("Action Node")]
public class ActionNodeModel : NodeModelBase
{
    public ActionNodeModel()
    {
        AddPort(PortAlignment.Top);
        AddPort(PortAlignment.Right);
        AddPort(PortAlignment.Bottom);
        AddPort(PortAlignment.Left);
    }
    
    [NodeParameter("Title")]
    public new string Title { get; set; }

    [NodeParameter("Action")]
    public int? Action { get; set; }

    [NodeParameter("Keyword")]
    public string? Keyword { get; set; }

    [NodeParameter("Data")]
    public string? Data { get; set; }
}