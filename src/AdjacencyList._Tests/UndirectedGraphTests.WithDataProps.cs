using FlUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace SCGraphTheory.AdjacencyList
{
    [TestClass]
    public class UndirectedGraphTests_WithDataProps
    {
        public static Test Construction_StringData => TestThat
            .When(() =>
            {
                var graph = new Graph<Node1, Edge1>();

                Node1 node1, node2;
                graph.Add(node1 = new Node1());
                graph.Add(node2 = new Node1());

                Edge1 edge;
                graph.Add(edge = new Edge1(node1, node2, "A"));

                return new { graph, node1, node2, edge };
            })
            .Then(o => o.edge.MyEdgeProp.ShouldBe("A"))
            .And(o => o.edge.Reverse.MyEdgeProp.ShouldBe("A"));

        public static Test Construction_IntData => TestThat
            .When(() =>
            {
                var graph = new Graph<Node2, Edge2>();

                Node2 node1, node2;
                graph.Add(node1 = new Node2());
                graph.Add(node2 = new Node2());

                Edge2 edge;
                graph.Add(edge = new Edge2(node1, node2, 1));

                return new { graph, node1, node2, edge };
            })
            .Then(o => o.edge.MyEdgeProp.ShouldBe(1))
            .And(o => o.edge.Reverse.MyEdgeProp.ShouldBe(-1));

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
