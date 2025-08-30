namespace FluxorAppStateManagement.Domain.Events
{
    public class NewForecastEventArgs : ReduceEventArgs
    {
        public string City { get; init; }

        public override void InvokeStateCreator(IEventReducer creator)
        {
            creator.Create(this);
        }
    }
}
