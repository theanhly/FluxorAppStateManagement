namespace FluxorAppStateManagement.State.Events.Notify
{
    public class NewStateActionEvent<TProjectedState> : EventArgs
    {
        public Func<TProjectedState, TProjectedState> ApplicationStateTransition { get; set; }
    }
}
