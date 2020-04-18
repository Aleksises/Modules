using MvcMusicStore.Monitoring;
using System.Diagnostics;
using System.Web.Mvc;

namespace MvcMusicStore.Filters
{
    public class ProfileRequestAttribute : ActionFilterAttribute
    {
        private Stopwatch _timer;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _timer.Stop();
            PerformanceCountersMonitor.IncrementRequestAverageTimeCounter(_timer.ElapsedMilliseconds);
        }
    }
}