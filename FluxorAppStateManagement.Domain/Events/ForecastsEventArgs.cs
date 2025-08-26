using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxorAppStateManagement.Domain.Events
{
    public class ForecastsEventArgs : ReduceEventArgs
    {
        public IList<Weather> Forecasts { get; set; }

        public override void InvokeReducer(IProjectedApplicationState applicationState)
        {
            applicationState.Reduce(this);
        }
    }
}
