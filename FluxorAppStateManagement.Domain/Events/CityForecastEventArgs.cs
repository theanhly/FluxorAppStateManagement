namespace FluxorAppStateManagement.Domain.Events
{
    public class CityForecastEventArgs : ReduceEventArgs
    {
        public string City { get; set; }

        public override void InvokeStateCreator(IEventReducer creator)
        {
            creator.Create(this);
        }
    }
}
