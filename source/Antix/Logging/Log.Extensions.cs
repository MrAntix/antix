using System;

namespace Antix.Logging
{
    public static partial class Log
    {
        public static Action<Message> Entry(
            Action<Message> getMessage)
        {
            return getMessage;
        }

        public static Action<Message> Append(
            this Action<Message> getMessage, Action<Message> getAppendMessage)
        {
            var resolve =
                (Func<Action<Message>, string>)
                    (m =>
                    {
                        string message = null;
                        m((f, a) => { message = string.Format(f, a); });

                        return message;
                    });

            return Entry(m =>
                m("{0}\n{1}",
                    resolve(getMessage),
                    resolve(getAppendMessage)));
        }

        public static Guid Write(
            this Delegate log, Level level, Action<Message> getMessage,
            Exception ex,
            params string[] tags)
        {
            if (log == null
                || level < (Level)LogSettings.Default.Level) return default(Guid);

            var identifier = Guid.NewGuid();
            getMessage(log(level, identifier, ex, tags));

            return identifier;
        }

        public static Guid Write(
            this Delegate log, Level level, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, level, getMessage, null, tags);
        }

        public static Guid Debug(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Debug, getMessage, null, tags);
        }

        public static Guid Information(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Information, getMessage, null, tags);
        }

        public static Guid Warning(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Warning, getMessage, null, tags);
        }

        public static Guid Warning(
            this Delegate log, Action<Message> getMessage,
            Exception ex,
            params string[] tags)
        {
            return Write(log, Level.Warning, getMessage, ex, tags);
        }

        public static Guid Error(
            this Delegate log, Action<Message> getMessage,
            Exception ex,
            params string[] tags)
        {
            return Write(log, Level.Error, getMessage, ex, tags);
        }

        public static Guid Error(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Error, getMessage, null, tags);
        }

        public static Guid Fatal(
            this Delegate log, Action<Message> getMessage,
            Exception ex,
            params string[] tags)
        {
            return Write(log, Level.Fatal, getMessage, ex, tags);
        }

        public static Guid Fatal(
            this Delegate log, Action<Message> getMessage,
            params string[] tags)
        {
            return Write(log, Level.Fatal, getMessage, null, tags);
        }
    }
}