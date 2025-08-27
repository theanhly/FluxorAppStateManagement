namespace FluxorAppStateManagement.Domain.Events
{
    public class NewCounterEventArgs : ReduceEventArgs
    {
        public Guid Id { get; init; }

        public int Count { get; init; }
        public override IReadOnlyList<IProjectedApplicationState> InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
