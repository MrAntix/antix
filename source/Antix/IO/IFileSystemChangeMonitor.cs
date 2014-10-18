using System;

namespace Antix.IO
{
    public interface IFileSystemChangeMonitor :
        IDisposable
    {
        IFileSystemChangeMonitor OnChanged(
            Action<FileSystemChangedEvent> action,
            FileSystemChangeMonitorOptions options);

        void Changed(FileSystemChangedEvent e);
    }
}