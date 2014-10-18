using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Antix.IO;
using Xunit;

namespace Antix.Tests.IO
{
    public class FileSystemChangeMonitorTest
    {
        [Fact]
        public void raises_single_events()
        {
            const string filePath = "A.FILE";
            const string newFilePath = "NEW.FILE";

            var events
                = new List<FileSystemChangedEvent>();

            using (var watcher = new FileSystemChangeMonitor()
                    .OnChanged(
                        ce =>
                        {
                            Debug.WriteLine(ce);
                            events.Add(ce);
                        },
                        new FileSystemChangeMonitorOptions
                        {
                            Directory = null, // won't actually watch
                            Delay = 100
                        }))
            {
                watcher.RaiseDeleted(filePath);
                watcher.RaiseAddedOrUpdated(filePath);
                watcher.RaiseAddedOrUpdated(filePath);
                watcher.RaiseRenamed(filePath, newFilePath);

                Thread.Sleep(200);
            }

            Assert.Equal(1, events.Count);

            var e = events.Single();
            Assert.Equal(filePath, e.Path);
            Assert.Equal(newFilePath, e.NewPath);
        }

        [Fact]
        public void raises_multipe_events()
        {
            const string filePath = "A.FILE";
            const string newFilePath = "NEW.FILE";

            var events
                = new List<FileSystemChangedEvent>();

            using (var watcher = (FileSystemChangeMonitor)
                new FileSystemChangeMonitor()
                    .OnChanged(
                        ce =>
                        {
                            Debug.WriteLine(ce);
                            events.Add(ce);
                        },
                        new FileSystemChangeMonitorOptions
                        {
                            Directory = null, // won't actually watch
                            Delay = 100
                        }))
            {
                watcher.RaiseRenamed(filePath, newFilePath);
                watcher.RaiseAddedOrUpdated(newFilePath);

                Thread.Sleep(2000);
            }

            Assert.Equal(2, events.Count);
            Assert.Equal(FileSystemChangedEventType.Renamed, events.ElementAt(0).Type);
            Assert.Equal(FileSystemChangedEventType.AddedOrUpdated, events.ElementAt(1).Type);
        }
    }
}