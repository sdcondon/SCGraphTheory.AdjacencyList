using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace SCGraphTheory.AdjacencyList
{
    [TestClass]
    public class UndirectedGraphTests_WithDataProps
    {
        [TestMethod]
        public void Construction1()
        {
            // Arrange
            var graph = new Graph<Node1, Edge1>();

            // Act
            Node1 node1, node2;
            graph.Add(node1 = new Node1());
            graph.Add(node2 = new Node1());
            Edge1 edge;
            graph.Add(edge = new Edge1(node1, node2, "A"));

            // Assert
            edge.MyEdgeProp.ShouldBe("A");
            edge.Reverse.MyEdgeProp.ShouldBe("A");
        }

        [TestMethod]
        public void Construction2()
        {
            // Arrange
            var graph = new Graph<Node2, Edge2>();

            // Act
            Node2 node1, node2;
            graph.Add(node1 = new Node2());
            graph.Add(node2 = new Node2());
            Edge2 edge;
            graph.Add(edge = new Edge2(node1, node2, 1));

            // Assert
            edge.MyEdgeProp.ShouldBe(1);
            edge.Reverse.MyEdgeProp.ShouldBe(-1);
        }

        private class Node1 : NodeBase<Node1, Edge1>
        {
        }

        private class Edge1 : UndirectedEdgeBase<Node1, Edge1>
        {
            private string myEdgeProp;

            public Edge1(Node1 from, Node1 to, string myEdgeProp)
                : base(from, to, (f, t, r) => new Edge1(f, t, r, myEdgeProp))
            {
                this.myEdgeProp = myEdgeProp;
            }

            private Edge1(Node1 from, Node1 to, Edge1 reverse, string myEdgeProp)
                : base(from, to, reverse)
            {
                this.myEdgeProp = myEdgeProp;
            }

            public string MyEdgeProp
            {
                get => myEdgeProp;
                set
                {
                    myEdgeProp = value;
                    Reverse.myEdgeProp = value;
                }
            }
        }

        private class Node2 : NodeBase<Node2, Edge2>
        {
        }

        private class Edge2 : UndirectedEdgeBase<Node2, Edge2>
        {
            private int myEdgeProp;

            public Edge2(Node2 from, Node2 to, int myEdgeProp)
                : base(from, to, (f, t, r) => new Edge2(f, t, r, -myEdgeProp))
            {
                this.myEdgeProp = myEdgeProp;
            }

            private Edge2(Node2 from, Node2 to, Edge2 reverse, int myEdgeProp)
                : base(from, to, reverse)
            {
                this.myEdgeProp = myEdgeProp;
            }

            public int MyEdgeProp
            {
                get => myEdgeProp;
                set
                {
                    myEdgeProp = value;
                    Reverse.myEdgeProp = -value;
                }
            }
        }
    }
}
