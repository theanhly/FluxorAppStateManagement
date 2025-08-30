using FluxorAppStateManagement.State.State;

namespace FluxorAppStateManagement.State
{
    public class NewProjectedApplicationStateEventArgs : EventArgs
    {
        public IProjectedApplicationState NewState { get; set; }
    }
}
