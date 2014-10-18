using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Antix.Logging;
using Antix.Timers;
using Xunit;

namespace Antix.Tests.Timers
{
    public class ScheduleTest
    {
        [Fact]
        public void run_action_in()
        {
            var log = Log.ToConsole;

            var start = DateTime.UtcNow;
            var stop = DateTime.MinValue;

            var reset = new AutoResetEvent(false);

            log.Debug(m => m("create shedule"));

            Schedule.Create(log)
                    .At(500, () => stop = DateTime.UtcNow, "set stop time")
                    .At(1000, () => { throw new Exception("Boo!"); }, "throw exception")
                    .ThenAt(500, () => reset.Set(), "stop waiting");

            log.Debug(m => m("waiting"));
            reset.WaitOne();

            log.Debug(m => m("start {0}", start.Millisecond));
            log.Debug(m => m("stop {0}", stop.Millisecond));

            Assert.True(stop > start);
        }
    }
}
