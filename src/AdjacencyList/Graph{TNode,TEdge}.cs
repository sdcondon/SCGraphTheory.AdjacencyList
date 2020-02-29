using System;
using System.Collections.Generic;
using System.Linq;

namespace SCGraphTheory.AdjacencyList.WithUnsealedNodesAndEdges
{
    /// <summary>
    /// Mutable implementation of <see cref="IGraph{TNode, TEdge}"/> that uses adjacency list representation.
    /// <para />
    /// This implementation allows data to be included in nodes and edges via inheritance, but has a couple of potential drawbacks.
    /// Firstly, the addition of a <see cref="TNode"/> to more than one <see cref="Graph{TNode, TEdge}"/> isn't protected against, with potentially confusing results.
    /// Secondly, undirected edges are a complex to implement.
    /// </summary>
    /// <typeparam name="TNode">The type of each node.</typeparam>
    /// <typeparam name="TEdge">The type of each edge.</typeparam>
    public sealed class Graph<TNode, TEdge> : IGraph<TNode, TEdge>
        where TNode : NodeBase<TNode, TEdge>
        where TEdge : EdgeBase<TNode, TEdge>
    {
        private readonly HashSet<TNode> nodes = new HashSet<TNode>();

        /// <inheritdoc />
        public IEnumerable<TNode> Nodes => this.nodes;

        /// <inheritdoc />
        public IEnumerable<TEdge> Edges => this.nodes.SelectMany(n => n.Edges);

        /// <summary>
        /// Adds a new node to the graph.
        /// </summary>
        /// <param name="node">The new node.</param>
        public void Add(TNode node)
        {
            // BUG: There's nothing to stop people adding a node to two different graphs.
            // A problem because we can't distinguish the edges in one graph from another, which means we could move between graphs when navigating.
            // Should somehow disallow this for integrity (but ideally want to avoid overhead of extra graph field in each and every node),
            // or merge the graphs completely when it happens (explore existing edges here, add em all to this graph - but that wouldn't suffice)..
            // Implementation with sealed nodes doesn't have this issue because the constructors are internal..
            this.nodes.Add(node);
        }

        /// <summary>
        /// Removes a node from the graph (along with all edges incident with it).
        /// </summary>
        /// <param name="node">The node to remove.</param>
        /// <returns>True if the node was found and removed, otherwise false.</returns>
        public bool Remove(TNode node)
        {
            if (!this.nodes.Remove(node))
            {
                return false;
            }

            node.RemoveAllIncidentEdges();
            return true;
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="edge">The new edge.</param>
        public void Add(TEdge edge)
        {
            if (!this.nodes.Contains(edge.From))
            {
                throw new ArgumentException("Cannot add edge from node that isn't in this graph", nameof(edge));
            }

            if (!this.nodes.Contains(edge.To))
            {
                throw new ArgumentException("Cannot add edge to node that isn't in this graph", nameof(edge));
            }

            edge.Add();
        }

        /// <summary>
        /// Removes an edge from the graph.
        /// </summary>
        /// <param name="edge">The edge to remove.</param>
        /// <returns>True if the edge was found and removed, otherwise false.</returns>
        public bool Remove(TEdge edge)
        {
            if (!this.nodes.Contains(edge.From))
            {
                // NB: Only need to check one end of the edge, because both are
                // checked when adding the edge and can't be changed.
                return false;
            }

            return edge.Remove();
        }

        /// <summary>
        /// Clears the graph, removing all edges and nodes.
        /// </summary>
        public void Clear()
        {
            this.nodes.Clear();
        }
    }
}
