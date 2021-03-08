namespace GraphLibrary.Interfaces
{
    public interface IEdgeCollection
    {
        Edge Pop();
        Edge Peek();
        void Push(Edge e);
        bool IsEmpty();
    }
}
