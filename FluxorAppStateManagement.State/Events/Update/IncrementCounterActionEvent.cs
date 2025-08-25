namespace FluxorAppStateManagement.State.Events.Update
{
    public class IncrementCounterActionEvent : ActionEvent
    {
        public Guid CounterId { get; init; }
    }
}
