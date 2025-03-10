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

namespace auth.Components.Diagram;

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