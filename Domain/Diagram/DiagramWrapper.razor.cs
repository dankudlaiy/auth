using auth.Services;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;

namespace auth.Domain.Diagram;

public partial class DiagramWrapper : ComponentBase
{
    private BlazorDiagram Diagram { get; set; } = null!;
    private List<TelephonyAction> Actions { get; set; }

    protected override async Task OnInitializedAsync()
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
            }
        };

        Diagram = new BlazorDiagram(options);
        
        Diagram.RegisterComponent<NodeWrapper, NodeWidget>();

        Actions = await DbService.GetActions();
        
        var firstNode = Diagram.Nodes.Add(new NodeModel(position: new Point(50, 50))
        {
            Title = "Node 1"
        });
        var secondNode = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 100))
        {
            Title = "Node 2"
        });
        var secondNode2 = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 200))
        {
            Title = "Node 22"
        });
        var secondNode3 = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 150))
        {
            Title = "Node 23"
        });
        var thirdNode = Diagram.Nodes.Add(new NodeModel(position: new Point(350, 50))
        {
            Title = "Node 3"
        });
        var leftPort = secondNode.AddPort(PortAlignment.Left);
        var rightPort = secondNode.AddPort(PortAlignment.Right);
        
        secondNode2.AddPort(PortAlignment.Left);
        secondNode2.AddPort(PortAlignment.Right);
        
        secondNode3.AddPort(PortAlignment.Left);
        secondNode3.AddPort(PortAlignment.Right);

        thirdNode.AddPort(PortAlignment.Left);
        
        // The connection point will be the intersection of
        // a line going from the target to the center of the source
        var sourceAnchor = new ShapeIntersectionAnchor(firstNode);
        // The connection point will be the port's position
        var targetAnchor = new SinglePortAnchor(leftPort);
        var link = Diagram.Links.Add(new LinkModel(sourceAnchor, targetAnchor));
    }
}