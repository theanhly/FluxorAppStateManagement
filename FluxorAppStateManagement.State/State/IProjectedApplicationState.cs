namespace FluxorAppStateManagement.State.State
{
    public interface IProjectedApplicationState
    {
        IReadOnlyList<Type> GetStates();
    }
}
