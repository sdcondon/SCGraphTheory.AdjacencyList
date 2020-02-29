using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace SCGraphTheory.AdjacencyList.WithUnsealedNodesAndEdges
{
    [TestClass]
    public class DirectedGraphTests
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
            graph.Edges.ShouldBe(new[] { edge1, edge2, edge3 }, ignoreOrder: true);
            node1.Edges.ShouldBe(new[] { edge1 });
            node2.Edges.ShouldBe(new[] { edge2 });
            node3.Edges.ShouldBe(new[] { edge3 });
        }

        [TestMethod]
        public void NodeRemovalRemovesIncidentEdges()
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
            graph.Edges.ShouldBe(new[] { edge1 });
            node1.Edges.ShouldBe(new[] { edge1 });
            node2.Edges.ShouldBeEmpty();
        }

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
