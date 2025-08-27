namespace FluxorAppStateManagement.Domain.Events
{
    public abstract class ReduceEventArgs : EventArgs
    {
        public abstract IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator);
    }
}
