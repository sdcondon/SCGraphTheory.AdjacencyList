namespace SCGraphTheory.AdjacencyList.WithUnsealedNodesAndEdges
{
    /// <summary>
    /// Edge type for the <see cref="Graph{TNode, TEdge}"/> class.
    /// </summary>
    /// <typeparam name="TNode">The type of each node.</typeparam>
    /// <typeparam name="TEdge">The type of each edge.</typeparam>
    public abstract class EdgeBase<TNode, TEdge> : IEdge<TNode, TEdge>
        where TNode : NodeBase<TNode, TEdge>
        where TEdge : EdgeBase<TNode, TEdge>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeBase{TNode, TEdge}"/> class.
        /// </summary>
        /// <param name="from">The node that the edge connects from.</param>
        /// <param name="to">The node that the edge connects to.</param>
        public EdgeBase(TNode from, TNode to)
        {
            From = from;
            To = to;
        }

        /// <inheritdoc />
        public TNode From { get; }

        /// <inheritdoc />
        public TNode To { get; }

        /// <summary>
        /// Adds this edge to its nodes.
        /// </summary>
        internal virtual void Add()
        {
            this.From.AddOutboundEdge((TEdge)this);
        }

        /// <summary>
        /// Removes this edge from its nodes.
        /// </summary>
        /// <returns>True if the edge was removed, false if it wasn't present to be removed.</returns>
        internal virtual bool Remove()
        {
            return this.From.RemoveOutboundEdge((TEdge)this);
        }
    }
}
