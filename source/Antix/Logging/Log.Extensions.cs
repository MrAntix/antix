using System;

namespace Antix.Logging
{
    public static partial class Log
    {
        public static Guid Debug(
          this Delegate log, Action<Message> getMessage,
          params string[] tags)
        {
            return Write(log, Level.Debug, getMessage, tags);
        }

        public static Guid Information(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Information, getMessage, tags);
        }

        public static Guid Warning(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Warning, getMessage, tags);
        }

        public static Guid Warning(
            this Delegate log, Action<MessageException> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Warning, getMessage, tags);
        }

        public static Guid Error(
            this Delegate log, Action<MessageException> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Error, getMessage, tags);
        }

        public static Guid Error(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Error, getMessage, tags);
        }

        public static Guid Fatal(
            this Delegate log, Action<MessageException> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Fatal, getMessage, tags);
        }

        public static Guid Fatal(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Fatal, getMessage, tags);
        }
    }
}