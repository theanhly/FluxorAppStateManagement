using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxorAppStateManagement.Domain.Events
{
    public class NewCounterEventArgs : ReduceEventArgs
    {
        public Guid Id { get; init; }

        public int Count { get; init; }
        public override void InvokeReducer(IProjectedApplicationState applicationState)
        {
            applicationState.Reduce(this);
        }
    }
}
