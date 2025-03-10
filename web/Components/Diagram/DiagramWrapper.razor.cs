using System.Text.Json;
using auth.Models;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;

namespace web.Components.Diagram;

public partial class DiagramWrapper : ComponentBase
{
    private BlazorDiagram Diagram { get; set; } = null!;

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
        //
        // Actions = await DbService.GetActions();
        //
        // var firstNode = Diagram.Nodes.Add(new NodeModel(position: new Point(50, 50))
        // {
        //     Title = "Node 1"
        // });
        // var secondNode = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 100))
        // {
        //     Title = "Node 2"
        // });
        // var secondNode2 = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 200))
        // {
        //     Title = "Node 22"
        // });
        // var secondNode3 = Diagram.Nodes.Add(new NodeWrapper(Actions, position: new Point(200, 150))
        // {
        //     Title = "Node 23"
        // });
        // var thirdNode = Diagram.Nodes.Add(new NodeModel(position: new Point(350, 50))
        // {
        //     Title = "Node 3"
        // });
        // var leftPort = secondNode.AddPort(PortAlignment.Left);
        // var rightPort = secondNode.AddPort(PortAlignment.Right);
        //
        // secondNode2.AddPort(PortAlignment.Left);
        // secondNode2.AddPort(PortAlignment.Right);
        //
        // secondNode3.AddPort(PortAlignment.Left);
        // secondNode3.AddPort(PortAlignment.Right);
        //
        // thirdNode.AddPort(PortAlignment.Left);
        //
        // // The connection point will be the intersection of
        // // a line going from the target to the center of the source
        // var sourceAnchor = new ShapeIntersectionAnchor(firstNode);
        // // The connection point will be the port's position
        // var targetAnchor = new SinglePortAnchor(leftPort);
        // var link = Diagram.Links.Add(new LinkModel(sourceAnchor, targetAnchor));
    }

    public string Export()
    {
        var diagramData = new DiagramData();

        foreach (var node in Diagram.Nodes)
        {
            var nodeData = new NodeData
            {
                Id = node.Id,
                Title = node.Title,
                X = node.Position.X,
                Y = node.Position.Y,
                Ports = node.Ports.Select(port => new PortData
                {
                    Id = port.Id,
                    Alignment = port.Alignment.ToString()
                }).ToList()
            };
            diagramData.Nodes.Add(nodeData);
        }

        foreach (var link in Diagram.Links)
        {
            if (link is { Source: SinglePortAnchor sourceAnchor, Target: SinglePortAnchor targetAnchor })
            {
                diagramData.Links.Add(new LinkData
                {
                    SourcePortId = sourceAnchor.Port.Id,
                    TargetPortId = targetAnchor.Port.Id
                });
            }
        }

        return JsonSerializer.Serialize(diagramData);
    }
    
    public void Load(string data)
    {
        var diagramData = JsonSerializer.Deserialize<DiagramData>(data);
        
        Diagram.Nodes.Clear();
        Diagram.Links.Clear();

        foreach (var nodeData in diagramData.Nodes)
        {
            var newNode = new NodeModel(new Point(nodeData.X, nodeData.Y))
            {
                Title = nodeData.Title
            };

            foreach (var portData in nodeData.Ports)
            {
                newNode.AddPort(Enum.Parse<PortAlignment>(portData.Alignment));
            }

            Diagram.Nodes.Add(newNode);
        }

        foreach (var linkData in diagramData.Links)
        {
            var sourcePort = Diagram.Nodes.SelectMany(n => n.Ports).FirstOrDefault(p => p.Id == linkData.SourcePortId);
            var targetPort = Diagram.Nodes.SelectMany(n => n.Ports).FirstOrDefault(p => p.Id == linkData.TargetPortId);

            if (sourcePort != null && targetPort != null)
            {
                Diagram.Links.Add(new LinkModel(new SinglePortAnchor(sourcePort), new SinglePortAnchor(targetPort)));
            }
        }
    }
}