namespace FluxorAppStateManagement.EventBus
{
    public class EventBus
    {
        private Dictionary<Type, HashSet<Delegate>> eventArgsBus = new();

        public void Publish<T>(T args)
        {
            if (eventArgsBus.TryGetValue(typeof(T), out HashSet<Delegate> list))
            {
                foreach (var handler in list)
                {
                    ((Action<T>)handler)(args);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : EventArgs
        {
            if (eventArgsBus.ContainsKey(typeof(T)))
            {
                eventArgsBus[typeof(T)].Add(action);
            }
            else
            {
                eventArgsBus[typeof(T)] = [action];
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : EventArgs
        {
            if (eventArgsBus.TryGetValue(typeof(T), out HashSet<Delegate> list))
            {
                list.Remove(action);
            }
        }
    }
}
