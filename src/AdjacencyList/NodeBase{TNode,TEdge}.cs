using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCGraphTheory.AdjacencyList
{
    /// <summary>
    /// Base node type for the <see cref="Graph{TNode, TEdge}"/> class.
    /// </summary>
    /// <typeparam name="TNode">The type of each node.</typeparam>
    /// <typeparam name="TEdge">The type of each edge.</typeparam>
    public abstract class NodeBase<TNode, TEdge> : INode<TNode, TEdge>
        where TNode : NodeBase<TNode, TEdge>
        where TEdge : EdgeBase<TNode, TEdge>
    {
        private readonly Collection<TEdge> outboundEdges = new Collection<TEdge>();
        private readonly Collection<TEdge> inboundEdges = new Collection<TEdge>();

        /// <inheritdoc />
        public IReadOnlyCollection<TEdge> Edges => this.outboundEdges;

        /// <summary>
        /// Adds a new outbound edge to the node.
        /// </summary>
        /// <param name="edge">The new edge.</param>
        /// <remarks>Internal because callers should use the appropriate method of the graph, which carries out integrity checks.</remarks>
        internal void AddOutboundEdge(TEdge edge)
        {
            this.outboundEdges.Add(edge);
            edge.To.inboundEdges.Add(edge);
        }

        /// <summary>
        /// Removes an outbound edge from the node.
        /// </summary>
        /// <param name="edge">The edge to remove.</param>
        /// <remarks>Internal because callers should use the appropriate method of the graph, which carries out integrity checks.</remarks>
        /// <returns>True if an edge was removed, otherwise false.</returns>
        internal bool RemoveOutboundEdge(TEdge edge)
        {
            return this.outboundEdges.Remove(edge)
                && edge.To.inboundEdges.Remove(edge);
        }

        /// <summary>
        /// Removes all incident (inbound and outbound) edges of the node from the graph.
        /// </summary>
        internal void RemoveAllIncidentEdges()
        {
            while (this.outboundEdges.Count > 0)
            {
                RemoveOutboundEdge(this.outboundEdges[0]);
            }

            while (this.inboundEdges.Count > 0)
            {
                var edge = this.inboundEdges[0];
                edge.From.RemoveOutboundEdge(edge);
            }
        }
    }
}
