using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
// ReSharper disable ConvertToPrimaryConstructor

namespace auth.Domain.Diagram;

public class NodeWrapper: NodeModel
{
    public NodeWrapper(List<TelephonyAction> actions, Point? position = null) : base(position)
    {
        Actions = actions;
    }

    public string Trigger { get; set; } = string.Empty;
    public Guid SelectedAction { get; set; }
    public List<TelephonyAction> Actions { get; set; }
}