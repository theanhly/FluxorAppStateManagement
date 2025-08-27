namespace FluxorAppStateManagement.Domain.Events
{
    public class NewCountEventArgs : ReduceEventArgs
    {
        public Guid Id { get; init; }

        public int Count { get; init; }

        public override IProjectedApplicationState InvokeStateCreator(IProjectedStateCreator creator)
        {
            return creator.Create(this);
        }
    }
}
