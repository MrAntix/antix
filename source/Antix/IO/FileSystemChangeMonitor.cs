using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Antix.IO
{
    public class FileSystemChangeMonitor :
        IFileSystemChangeMonitor
    {
        readonly IDictionary<string, Timer> _events
            = new Dictionary<string, Timer>();

        Action<FileSystemChangedEvent> _action;
        FileSystemWatcher _fileWatcher;
        FileSystemWatcher _directoryWatcher;

        int _delay;

        public IFileSystemChangeMonitor OnChange(
            Action<FileSystemChangedEvent> action,
            FileSystemChangeMonitorOptions options)
        {
            _action = action;

            if (action == null) return this;

            _delay = options.Delay;

            if (_fileWatcher != null)
            {
                _fileWatcher.Dispose();
                _directoryWatcher.Dispose();
            }

            if (options.Directory == null) return this;

            _fileWatcher = new FileSystemWatcher(options.Directory, options.Pattern)
            {
                IncludeSubdirectories = options.IncludeSubdirectories,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            _fileWatcher.Changed += (s, e) => RaiseChanged(e.FullPath);
            _fileWatcher.Deleted += (s, e) => RaiseDeleted(e.FullPath);
            _fileWatcher.Renamed += (s, e) => RaiseRenamed(e.OldFullPath, e.FullPath);

            _fileWatcher.EnableRaisingEvents = true;

            _directoryWatcher = new FileSystemWatcher(options.Directory)
            {
                IncludeSubdirectories = options.IncludeSubdirectories,
                NotifyFilter = NotifyFilters.DirectoryName
            };

            _directoryWatcher.Deleted += (s, e) => RaiseDeleted(e.FullPath);
            _directoryWatcher.Renamed += (s, e) => RaiseRenamed(e.OldFullPath, e.FullPath);

            _directoryWatcher.EnableRaisingEvents = true;

            return this;
        }

        void Enqueue(string path, FileSystemChangedEvent e)
        {
            if (_events.ContainsKey(path))
            {
                Dequeue(path);
            }

            _events.Add(
                path,
                new Timer(
                    o =>
                    {
                        while (!path
                            .Equals(_events.Keys.FirstOrDefault()))
                            Thread.Sleep(1);

                        Dequeue(path);

                        _action(e);
                    }, null, _delay, 0)
                );
        }

        void Dequeue(string path)
        {
            _events[path].Dispose();
            _events.Remove(path);
        }

        public void RaiseChanged(string path)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    Type = FileSystemChangedEventType.Changed
                };

            Enqueue(path, e);
        }

        public void RaiseDeleted(string path)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    Type = FileSystemChangedEventType.Deleted
                };

            Enqueue(path, e);
        }

        public void RaiseRenamed(string oldPath, string path)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    OldPath = oldPath,
                    Type = FileSystemChangedEventType.Renamed
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
                if (_fileWatcher != null)
                {
                    _fileWatcher.Dispose();
                    _fileWatcher = null;
                    _directoryWatcher.Dispose();
                    _directoryWatcher = null;
                }
            }

            _disposed = true;
        }

        #endregion
    }
}