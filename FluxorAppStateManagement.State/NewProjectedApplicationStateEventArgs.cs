using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class NewProjectedApplicationStateEventArgs : EventArgs
    {
        public IProjectedApplicationState NewState { get; set; }

        public EventArgs EventArgs { get; set; }
    }
}
