using System;
using System.Threading;
using Antix.Logging;

namespace Antix.Timers
{
    public class Schedule
    {
        readonly int _startTime;
        readonly int _endTime;
        readonly Log.Delegate _log;

        Schedule(
            int startTime, int endTime,
            Log.Delegate log)
        {
            _startTime = startTime;
            _endTime = endTime;
            _log = log;
        }

        public Schedule(Log.Delegate log) :
            this(0, 0, log)
        {
        }

        public Schedule() :
            this(0, 0, null)
        {
        }

        public static Schedule Create(Log.Delegate log = null)
        {
            return new Schedule(log);
        }

        public Schedule At(int milliseconds, Action action, string label)
        {
            var _ = new Timer(
                o => Run(action, label), null,
                _startTime + milliseconds, Timeout.Infinite);

            _log.Debug(m => m("action '{0}' at {1}", label, _startTime + milliseconds));

            return new Schedule(
                _startTime,
                Math.Max(_startTime + milliseconds, _endTime),
                _log);
        }

        void Run(Action action, string label)
        {
            _log.Debug(m => m("running action '{0}'", label));

            try
            {
                action();

                _log.Debug(m => m("completed action '{0}'", label));
            }
            catch (Exception ex)
            {
                _log.Debug(m => m("error running action '{0}'\n{1}", label, ex));
            }
        }

        public Schedule Then(Action action, string label)
        {
            return new Schedule(
                _endTime, _endTime,
                _log)
                .At(0, action, label);
        }

        public Schedule ThenAt(int milliseconds, Action action, string label)
        {
            return Wait().At(milliseconds, action, label);
        }

        public Schedule Wait()
        {
            return new Schedule(
                _endTime, _endTime,
                _log);
        }

        public Schedule Wait(int milliseconds)
        {
            return new Schedule(
                _endTime + milliseconds, _endTime + milliseconds,
                _log);
        }
    }
}