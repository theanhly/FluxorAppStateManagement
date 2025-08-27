using FluxorAppStateManagement.Domain;

namespace FluxorAppStateManagement.State
{
    public class NewProjectedApplicationStateEventArgs : EventArgs
    {
        public IProjectedApplicationState NewState { get; set; }
    }
}
