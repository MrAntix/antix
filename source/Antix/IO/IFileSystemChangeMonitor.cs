using System;

namespace Antix.IO
{
    public interface IFileSystemChangeMonitor :
        IDisposable
    {
        IFileSystemChangeMonitor OnChange(
            Action<FileSystemChangedEvent> action,
            FileSystemChangeMonitorOptions options);

        void Change(FileSystemChangedEvent e);
    }
}