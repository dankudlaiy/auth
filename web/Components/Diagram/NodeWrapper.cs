using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

// ReSharper disable ConvertToPrimaryConstructor

namespace web.Components.Diagram;

public class NodeWrapper: NodeModel
{
    public NodeWrapper(List<Data.TelephonyAction> actions, Point? position = null) : base(position)
    {
        Actions = actions;
    }

    public string Trigger { get; set; } = string.Empty;
    public Guid SelectedAction { get; set; }
    public List<Data.TelephonyAction> Actions { get; set; }
}