using System;

namespace Antix.IO
{
    public interface IFileChangeWatcher :
        IDisposable
    {
        IFileChangeWatcher OnChange(
            Action<FileChangedEvent> action,
            FileChangeWatcherOptions options);
    }
}