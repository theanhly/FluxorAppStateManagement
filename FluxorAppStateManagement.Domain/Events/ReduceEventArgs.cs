namespace FluxorAppStateManagement.Domain.Events
{
    public abstract class ReduceEventArgs : EventArgs
    {
        public abstract void InvokeStateCreator(IEventReducer creator);
    }
}
