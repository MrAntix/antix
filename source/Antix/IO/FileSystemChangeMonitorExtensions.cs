namespace Antix.IO
{
    public static class FileSystemChangeMonitorExtensions
    {
        public static void RaiseAddedOrUpdated(
            this IFileSystemChangeMonitor monitor, 
            string path)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    Type = FileSystemChangedEventType.Changed
                };

            monitor.Change(e);
        }

        public static void RaiseDeleted(
            this IFileSystemChangeMonitor monitor,
            string path)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    Type = FileSystemChangedEventType.Deleted
                };

            monitor.Change(e);
        }

        public static void RaiseRenamed(
            this IFileSystemChangeMonitor monitor,
            string path, string newPath)
        {
            var e =
                new FileSystemChangedEvent
                {
                    Path = path,
                    NewPath = newPath,
                    Type = FileSystemChangedEventType.Renamed
                };

            monitor.Change(e);
        }
    }
}