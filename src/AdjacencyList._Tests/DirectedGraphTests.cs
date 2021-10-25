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
            .Then(o => o.graph.Nodes.ShouldBe(new[] { o.node1, o.node2, o.node3 }))
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
            .When(g => g.graph.Remove(g.edge3))
            .Then((_, returnValue) => returnValue.ShouldBeTrue())
            .And((g, _) => g.graph.Nodes.ShouldBe(new[] { g.node1, g.node2, g.node3 }))
            .And((g, _) => g.graph.Edges.ShouldBe(new[] { g.edge1, g.edge2 }))
            .And((g, _) => g.node1.Edges.ShouldBe(new[] { g.edge1 }))
            .And((g, _) => g.node2.Edges.ShouldBe(new[] { g.edge2 }))
            .And((g, _) => g.node3.Edges.ShouldBeEmpty());

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
            .When(g => g.graph.Remove(g.node3))
            .Then((_, returnValue) => returnValue.ShouldBeTrue())
            .And((g, _) => g.graph.Nodes.ShouldBe(new[] { g.node1, g.node2 }))
            .And((g, _) => g.graph.Edges.ShouldBe(new[] { g.edge1 }))
            .And((g, _) => g.node1.Edges.ShouldBe(new[] { g.edge1 }))
            .And((g, _) => g.node2.Edges.ShouldBeEmpty());

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
