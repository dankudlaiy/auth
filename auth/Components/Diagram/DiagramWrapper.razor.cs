using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;

namespace auth.Components.Diagram;

public partial class DiagramWrapper : ComponentBase
{
    private BlazorDiagram? Diagram { get; set; } = null!;

    protected override void OnInitialized()
    {
        var options = new BlazorDiagramOptions
        {
            AllowMultiSelection = true,
            Zoom =
            {
                Enabled = false,
            },
            Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator()
            },
        };

        Diagram = new BlazorDiagram(options);
        
        var firstNode = Diagram.Nodes.Add(new NodeModel(position: new Point(50, 50))
        {
            Title = "Node 1"
        });
        var secondNode = Diagram.Nodes.Add(new NodeModel(position: new Point(200, 100))
        {
            Title = "Node 2"
        });
        var leftPort = secondNode.AddPort(PortAlignment.Left);
        var rightPort = secondNode.AddPort(PortAlignment.Right);
        
        var sourceAnchor = new ShapeIntersectionAnchor(firstNode);

        var targetAnchor = new SinglePortAnchor(leftPort);
        var link = Diagram.Links.Add(new LinkModel(sourceAnchor, targetAnchor));
    }
}