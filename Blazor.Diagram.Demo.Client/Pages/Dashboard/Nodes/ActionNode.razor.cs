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
    private IEnumerable<TelephonyActionModel> _actions;
    
    public ActionNodeModel(IEnumerable<TelephonyActionModel> actions)
    {
        _actions = actions;

        AddPort(PortAlignment.Top);
        AddPort(PortAlignment.Right);
        AddPort(PortAlignment.Bottom);
        AddPort(PortAlignment.Left);
    }
    
    [NodeParameter("Title")]
    public new string Title { get; set; }

    [NodeParameter("Action")]
    public string? Action { get; set; }

    [NodeParameter("Keyword")]
    public string? Keyword { get; set; }

    [NodeParameter("Data")]
    public string? Data { get; set; }
}