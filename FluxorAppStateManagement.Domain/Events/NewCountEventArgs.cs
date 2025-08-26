namespace FluxorAppStateManagement.Domain.Events
{
    public class NewCountEventArgs : ReduceEventArgs
    {
        public Guid Id { get; init; }

        public int Count { get; init; }
        public override void InvokeReducer(IProjectedApplicationState applicationState)
        {
            applicationState.Reduce(this);
        }
    }
}
