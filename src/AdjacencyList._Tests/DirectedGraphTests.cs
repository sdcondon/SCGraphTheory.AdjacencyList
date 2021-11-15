using FlUnit;
using Shouldly;

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
            .ThenReturns(o => o.graph.Nodes.ShouldBe(new[] { o.node1, o.node2, o.node3 }))
            .And(o => o.graph.Edges.ShouldBe(new[] { o.edge1, o.edge2, o.edge3 }))
            .And(o => o.node1.Edges.ShouldBe(new[] { o.edge1 }))
            .And(o => o.node2.Edges.ShouldBe(new[] { o.edge2 }))
            .And(o => o.node3.Edges.ShouldBe(new[] { o.edge3 }));

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
            .ThenReturns((_, returnValue) => returnValue.ShouldBeTrue())
            .And((given, _) => given.graph.Nodes.ShouldBe(new[] { given.node1, given.node2, given.node3 }))
            .And((given, _) => given.graph.Edges.ShouldBe(new[] { given.edge1, given.edge2 }))
            .And((given, _) => given.node1.Edges.ShouldBe(new[] { given.edge1 }))
            .And((given, _) => given.node2.Edges.ShouldBe(new[] { given.edge2 }))
            .And((given, _) => given.node3.Edges.ShouldBeEmpty());

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
            .ThenReturns((_, returnValue) => returnValue.ShouldBeTrue())
            .And((given, _) => given.graph.Nodes.ShouldBe(new[] { given.node1, given.node2 }))
            .And((given, _) => given.graph.Edges.ShouldBe(new[] { given.edge1 }))
            .And((given, _) => given.node1.Edges.ShouldBe(new[] { given.edge1 }))
            .And((given, _) => given.node2.Edges.ShouldBeEmpty());

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
