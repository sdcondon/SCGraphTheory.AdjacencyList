using FluentAssertions;
using FlUnit;

namespace SCGraphTheory.AdjacencyList
{
    public static class DirectedGraphTests
    {
        public static Test Construction => TestThat
            .When(() =>
            {
                var graph = new Graph<Node, Edge>();

                Node node1, node2, node3;
                graph.Add(node1 = new Node());
                graph.Add(node2 = new Node());
                graph.Add(node3 = new Node());

                Edge edge1, edge2, edge3;
                graph.Add(edge1 = new Edge(node1, node2));
                graph.Add(edge2 = new Edge(node2, node3));
                graph.Add(edge3 = new Edge(node3, node1));

                return new { graph, node1, node2, node3, edge1, edge2, edge3 };
            })
            .ThenReturns()
            .And(o => o.graph.Nodes.Should().BeEquivalentTo(new[] { o.node1, o.node2, o.node3 }))
            .And(o => o.graph.Edges.Should().BeEquivalentTo(new[] { o.edge1, o.edge2, o.edge3 }))
            .And(o => o.node1.Edges.Should().BeEquivalentTo(new[] { o.edge1 }))
            .And(o => o.node2.Edges.Should().BeEquivalentTo(new[] { o.edge2 }))
            .And(o => o.node3.Edges.Should().BeEquivalentTo(new[] { o.edge3 }));

        public static Test EdgeRemoval => TestThat
            .Given(() =>
            {
                var graph = new Graph<Node, Edge>();

                Node node1, node2, node3;
                graph.Add(node1 = new Node());
                graph.Add(node2 = new Node());
                graph.Add(node3 = new Node());

                Edge edge1, edge2, edge3;
                graph.Add(edge1 = new Edge(node1, node2));
                graph.Add(edge2 = new Edge(node2, node3));
                graph.Add(edge3 = new Edge(node3, node1));

                return new { graph, node1, node2, node3, edge1, edge2, edge3 };
            })
            .When(given => given.graph.Remove(given.edge3))
            .ThenReturns()
            .And((_, returnValue) => returnValue.Should().BeTrue())
            .And((given, _) => given.graph.Nodes.Should().BeEquivalentTo(new[] { given.node1, given.node2, given.node3 }))
            .And((given, _) => given.graph.Edges.Should().BeEquivalentTo(new[] { given.edge1, given.edge2 }))
            .And((given, _) => given.node1.Edges.Should().BeEquivalentTo(new[] { given.edge1 }))
            .And((given, _) => given.node2.Edges.Should().BeEquivalentTo(new[] { given.edge2 }))
            .And((given, _) => given.node3.Edges.Should().BeEmpty());

        public static Test NodeRemoval => TestThat
            .Given(() =>
            {
                var graph = new Graph<Node, Edge>();

                Node node1, node2, node3;
                graph.Add(node1 = new Node());
                graph.Add(node2 = new Node());
                graph.Add(node3 = new Node());

                Edge edge1, edge2, edge3;
                graph.Add(edge1 = new Edge(node1, node2));
                graph.Add(edge2 = new Edge(node2, node3));
                graph.Add(edge3 = new Edge(node3, node1));

                return new { graph, node1, node2, node3, edge1, edge2, edge3 };
            })
            .When(given => given.graph.Remove(given.node3))
            .ThenReturns()
            .And((_, returnValue) => returnValue.Should().BeTrue())
            .And((given, _) => given.graph.Nodes.Should().BeEquivalentTo(new[] { given.node1, given.node2 }))
            .And((given, _) => given.graph.Edges.Should().BeEquivalentTo(new[] { given.edge1 }))
            .And((given, _) => given.node1.Edges.Should().BeEquivalentTo(new[] { given.edge1 }))
            .And((given, _) => given.node2.Edges.Should().BeEmpty());

        private class Node : NodeBase<Node, Edge>
        {
        }

        private class Edge : EdgeBase<Node, Edge>
        {
            public Edge(Node from, Node to)
                : base(from, to)
            {
            }
        }
    }
}
