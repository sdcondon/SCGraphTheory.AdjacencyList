using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace SCGraphTheory.AdjacencyList
{
    [TestClass]
    public class UndirectedGraphTests
    {
        [TestMethod]
        public void Construction()
        {
            // Arrange
            var graph = new Graph<Node, Edge>();

            // Act
            Node node1, node2, node3;
            graph.Add(node1 = new Node());
            graph.Add(node2 = new Node());
            graph.Add(node3 = new Node());

            Edge edge1, edge2, edge3;
            graph.Add(edge1 = new Edge(node1, node2));
            graph.Add(edge2 = new Edge(node2, node3));
            graph.Add(edge3 = new Edge(node3, node1));

            // Assert
            graph.Nodes.ShouldBe(new[] { node1, node2, node3 }, ignoreOrder: true);
            graph.Edges.ShouldBe(new[] { edge1, edge1.Reverse, edge2, edge2.Reverse, edge3, edge3.Reverse }, ignoreOrder: true);
            node1.Edges.ShouldBe(new[] { edge1, edge3.Reverse }, ignoreOrder: true);
            node2.Edges.ShouldBe(new[] { edge2, edge1.Reverse }, ignoreOrder: true);
            node3.Edges.ShouldBe(new[] { edge3, edge2.Reverse }, ignoreOrder: true);
            edge1.Reverse.Reverse.ShouldBeSameAs(edge1);
            edge2.Reverse.Reverse.ShouldBeSameAs(edge2);
            edge3.Reverse.Reverse.ShouldBeSameAs(edge3);
        }

        [TestMethod]
        public void EdgeRemovalRemovesReverseEdge()
        {
            // Arrange
            var graph = new Graph<Node, Edge>();

            Node node1, node2, node3;
            graph.Add(node1 = new Node());
            graph.Add(node2 = new Node());
            graph.Add(node3 = new Node());

            Edge edge1, edge2, edge3;
            graph.Add(edge1 = new Edge(node1, node2));
            graph.Add(edge2 = new Edge(node2, node3));
            graph.Add(edge3 = new Edge(node3, node1));

            // Act
            graph.Remove(edge3);

            // Assert
            graph.Nodes.ShouldBe(new[] { node1, node2, node3 }, ignoreOrder: true);
            graph.Edges.ShouldBe(new[] { edge1, edge1.Reverse, edge2, edge2.Reverse }, ignoreOrder: true);
            node1.Edges.ShouldBe(new[] { edge1 });
            node2.Edges.ShouldBe(new[] { edge1.Reverse, edge2 }, ignoreOrder: true);
            node3.Edges.ShouldBe(new[] { edge2.Reverse });
        }

        [TestMethod]
        public void NodeRemovalRemovesIncidentEdge()
        {
            // Arrange
            var graph = new Graph<Node, Edge>();

            Node node1, node2, node3;
            graph.Add(node1 = new Node());
            graph.Add(node2 = new Node());
            graph.Add(node3 = new Node());

            Edge edge1, edge2, edge3;
            graph.Add(edge1 = new Edge(node1, node2));
            graph.Add(edge2 = new Edge(node2, node3));
            graph.Add(edge3 = new Edge(node3, node1));

            // Act
            graph.Remove(node3);

            // Assert
            graph.Nodes.ShouldBe(new[] { node1, node2 }, ignoreOrder: true);
            graph.Edges.ShouldBe(new[] { edge1, edge1.Reverse }, ignoreOrder: true);
            node1.Edges.ShouldBe(new[] { edge1 });
            node2.Edges.ShouldBe(new[] { edge1.Reverse });
        }

        private class Node : NodeBase<Node, Edge>
        {
        }

        private class Edge : UndirectedEdgeBase<Node, Edge>
        {
            public Edge(Node from, Node to)
                : base(from, to, (f, t, r) => new Edge(f, t, r))
            {
            }

            private Edge(Node from, Node to, Edge reverse)
                : base(from, to, reverse)
            {
            }
        }
    }
}
