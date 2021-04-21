# SCGraphTheory.AdjacencyList

[![NuGet version (SCGraphTheory.AdjacencyList)](https://img.shields.io/nuget/v/SCGraphTheory.AdjacencyList.svg?style=flat-square)](https://www.nuget.org/packages/SCGraphTheory.AdjacencyList/)

Adjacency list graph implementation that implements the interfaces defined in the [SCGraphTheory.Abstractions](https://github.com/sdcondon/SCGraphTheory.Abstractions) package, and can thus work with other packages that also use this abstraction - such as [SCGraphTheory.Search](https://github.com/sdcondon/SCGraphTheory.Search).

## Usage Examples

**Directed graphs** are the simplest to use. Here's an example with some data properties:

```csharp
using SCGraphTheory.AdjacencyList

namespace MyDirectedGraph
{
    public class Node : NodeBase<Node, Edge>
    {
        public Node(string myNodeProp) => MyNodeProp = myNodeProp;

        public string MyNodeProp { get; }
    }

    public class Edge : EdgeBase<Node, Edge>
    {
        public Edge(Node from, Node to, string myEdgeProp)
            : base(from, to) => MyEdgeProp = myEdgeProp;

        public string MyEdgeProp { get; }
    }

    public static class Program
    {
        ...

        private static Graph<Node, Edge> MakeGraph()
        {
            var graph = new Graph<Node, Edge>();
            Node node1, node2;
            graph.Add(node1 = new Node("node 1"));
            graph.Add(node2 = new Node("node 2"));
            graph.Add(new Edge(node1, node2, "edge 1-2"));
            ...
        }
    }
}
```

**Undirected graphs** take a little more effort, though there's a handy `UndirectedEdgeBase` class to do a bit of the work for you. `UndirectedEdgeBase` still conforms to the `IEdge<TNode, TEdge>` interface, so each undirected edge actually consists of a pair of edge objects*. Here's an example with a direction-ignorant settable edge data property:

*\* Note that if we really wanted a single object on the heap for an undirected edge, we could probably do something with by making the actual IEdges value types that refer to the single "edge". The extra complexity and resulting caveats (e.g. needing to be careful with mutability) mean that it's not something I've bothered exploring thus far..*

```csharp
using SCGraphTheory.AdjacencyList

namespace MyUndirectedGraph
{
    public class Node : NodeBase<Node, Edge>
    {
    }

    public class Edge : UndirectedEdgeBase<Node, Edge>
    {
        private string myEdgeProp;

        public Edge(Node from, Node to, string myEdgeProp)
            : base(from, to, (f, t, r) => new Edge(f, t, r, myEdgeProp))
        {
            this.myEdgeProp = myEdgeProp;
        }

        private Edge(Node from, Node to, Edge reverse, string myEdgeProp)
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

    public static class Program
    {
        ...

        public static Graph<Node, Edge> MakeGraph()
        {
            var graph = new Graph<Node, Edge>();
            Node node1, node2;
            graph.Add(node1 = new Node());
            graph.Add(node2 = new Node());
            graph.Add(new Edge(node1, node2, "A"));
            ...
        }
    }
}
```

Finally, here's an example with "undirected" edges with a direction-specific settable data property (reverse edge negates the value of the property). Obviously its significant that `int` is a value type - solution would be a little more complex with a mutable reference type..

```csharp
using SCGraphTheory.AdjacencyList

namespace MyUndirectedGraph2
{
    public class Node : NodeBase<Node, Edge>
    {
    }

    public class Edge : UndirectedEdgeBase<Node, Edge>
    {
        private int myEdgeProp;

        public Edge(Node from, Node to, int myEdgeProp)
            : base(from, to, (f, t, r) => new Edge(f, t, r, -myEdgeProp))
        {
            this.myEdgeProp = myEdgeProp;
        }

        private Edge(Node from, Node to, Edge reverse, int myEdgeProp)
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

    public static class Program
    {
        ...

        public static Graph<Node, Edge> MakeGraph()
        {
            var graph = new Graph<Node, Edge>();
            Node node1, node2;
            graph.Add(node1 = new Node());
            graph.Add(node2 = new Node());
            graph.Add(new Edge(node1, node2, 1));
            ...
        }
    }
}
```