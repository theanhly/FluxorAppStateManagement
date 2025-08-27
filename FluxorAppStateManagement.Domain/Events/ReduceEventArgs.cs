namespace FluxorAppStateManagement.Domain.Events
{
    public abstract class ReduceEventArgs : EventArgs
    {
        public abstract IProjectedApplicationState InvokeStateCreator(IProjectedStateCreator creator);
    }
}
