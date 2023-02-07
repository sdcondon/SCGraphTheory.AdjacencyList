using System;

namespace SCGraphTheory.AdjacencyList
{
    /// <summary>
    /// Edge type for the <see cref="Graph{TNode, TEdge}"/> class.
    /// </summary>
    /// <typeparam name="TNode">The type of each node.</typeparam>
    /// <typeparam name="TEdge">The type of each edge.</typeparam>
    public abstract class UndirectedEdgeBase<TNode, TEdge> : EdgeBase<TNode, TEdge>
        where TNode : NodeBase<TNode, TEdge>
        where TEdge : UndirectedEdgeBase<TNode, TEdge>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UndirectedEdgeBase{TNode, TEdge}"/> class, including its reverse edge.
        /// </summary>
        /// <param name="from">The node that the edge connects from.</param>
        /// <param name="to">The node that the edge connects to.</param>
        /// <param name="makeReverse">
        /// Delegate to create the reverse of this edge (which should ultimately call the other ctor of this class).
        /// The arguments of the function are the "from" node, the "to" node, and the reverse edge.
        /// </param>
        protected UndirectedEdgeBase(TNode from, TNode to, Func<TNode, TNode, TEdge, TEdge> makeReverse)
            : base(from, to)
        {
            Reverse = makeReverse(to, from, (TEdge)this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndirectedEdgeBase{TNode, TEdge}"/> class that is the reverse of an edge that has already been constructed.
        /// </summary>
        /// <param name="from">The node that the edge connects from.</param>
        /// <param name="to">The node that the edge connects to.</param>
        /// <param name="reverse">The already-constructed reverse edge of this edge.</param>
        protected UndirectedEdgeBase(TNode from, TNode to, TEdge reverse)
            : base(from, to)
        {
            Reverse = reverse;
        }

        /// <summary>
        /// Gets the reverse edge of this edge.
        /// </summary>
        public TEdge Reverse { get; }

        /// <inheritdoc/>
        internal override void Add()
        {
            this.From.AddOutboundEdge((TEdge)this);
            this.To.AddOutboundEdge(Reverse);
        }

        /// <inheritdoc/>
        internal override bool Remove()
        {
            return this.From.RemoveOutboundEdge((TEdge)this)
                && this.To.RemoveOutboundEdge(Reverse);
        }
    }
}
