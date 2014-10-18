using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Antix.IO
{
    public class FileChangeWatcher :
        IFileChangeWatcher
    {
        readonly IDictionary<string, Timer> _events
            = new Dictionary<string, Timer>();

        Action<FileChangedEvent> _action;
        FileSystemWatcher _watcher;

        int _delay;

        public IFileChangeWatcher OnChange(
            Action<FileChangedEvent> action,
            FileChangeWatcherOptions options)
        {
            _action = action;

            if (action == null) return this;

            _delay = options.Delay;

            if (_watcher != null) _watcher.Dispose();

            if (options.Directory == null) return this;

            _watcher = new FileSystemWatcher(options.Directory, options.Pattern)
            {
                IncludeSubdirectories = options.IncludeSubdirectories,
            };

            _watcher.Changed += (s, e) => RaiseChanged(e.FullPath);
            _watcher.Deleted += (s, e) => RaiseDeleted(e.FullPath);
            _watcher.Renamed += (s, e) => RaiseRenamed(e.OldFullPath, e.FullPath);

            _watcher.EnableRaisingEvents = true;

            return this;
        }

        void Enqueue(string path, FileChangedEvent e)
        {
            if (_events.ContainsKey(path))
            {
                Dequeue(path);
            }

            var timer =
                new Timer(
                    o =>
                    {
                        while (_events.Keys.First() != path)
                            Thread.Sleep(0);

                        Debug.WriteLine("fire " + path);
                        _action(e);

                        Dequeue(path);
                    }, null, _delay, 0);

            _events.Add(
                path,
                timer
                );

            Debug.WriteLine("queue " + path);
        }

        void Dequeue(string path)
        {
            Debug.WriteLine("dequeue " + path);

            _events[path].Dispose();
            _events.Remove(path);
        }

        public void RaiseChanged(string path)
        {
            var e =
                new FileChangedEvent
                {
                    Path = path,
                    Type = FileChangedEventType.Changed
                };

            Enqueue(path, e);
        }

        public void RaiseDeleted(string path)
        {
            var e =
                new FileChangedEvent
                {
                    Path = path,
                    Type = FileChangedEventType.Deleted
                };

            Enqueue(path, e);
        }

        public void RaiseRenamed(string oldPath, string path)
        {
            var e =
                new FileChangedEvent
                {
                    Path = path,
                    OldPath = oldPath,
                    Type = FileChangedEventType.Renamed
                };

            Enqueue(oldPath, e);
        }

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_watcher != null)
                {
                    _watcher.Dispose();
                    _watcher = null;
                }
            }

            _disposed = true;
        }

        #endregion
    }
}