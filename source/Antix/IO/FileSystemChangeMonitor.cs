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

        public IFileSystemChangeMonitor OnChanged(
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

            _fileWatcher.Changed += (s, e) => this.RaiseAddedOrUpdated(e.FullPath);
            _fileWatcher.Deleted += (s, e) => this.RaiseDeleted(e.FullPath);
            _fileWatcher.Renamed += (s, e) => this.RaiseRenamed(e.OldFullPath, e.FullPath);

            _fileWatcher.EnableRaisingEvents = true;

            _directoryWatcher = new FileSystemWatcher(options.Directory)
            {
                IncludeSubdirectories = options.IncludeSubdirectories,
                NotifyFilter = NotifyFilters.DirectoryName
            };

            _directoryWatcher.Deleted += (s, e) => this.RaiseDeleted(e.FullPath);
            _directoryWatcher.Renamed += (s, e) => this.RaiseRenamed(e.OldFullPath, e.FullPath);

            _directoryWatcher.EnableRaisingEvents = true;

            return this;
        }

        public void Changed(FileSystemChangedEvent e)
        {
            if (_events.ContainsKey(e.Path))
            {
                ClearTimer(e.Path);
            }

            _events.Add(
                e.Path,
                new Timer(
                    o =>
                    {
                        while (!e.Path
                            .Equals(_events.Keys.ToArray().FirstOrDefault()))
                            Thread.Sleep(1);

                        ClearTimer(e.Path);

                        _action(e);
                    }, null, _delay, 0)
                );
        }

        void ClearTimer(string path)
        {
            _events[path].Dispose();
            _events.Remove(path);
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