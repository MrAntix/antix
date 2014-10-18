using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Antix.IO;
using Xunit;

namespace Antix.Tests.IO
{
    public class FileChangedWatcherTest
    {
        [Fact]
        public void raises_single_events()
        {
            const string filePath = "A.FILE";
            const string newFilePath = "NEW.FILE";

            var events
                = new List<FileChangedEvent>();

            using (var watcher = (FileChangeWatcher)
                new FileChangeWatcher()
                    .OnChange(
                        ce =>
                        {
                            Debug.WriteLine(ce);
                            events.Add(ce);
                        },
                        new FileChangeWatcherOptions
                        {
                            Directory = null, // won't actually watch
                            Delay = 100
                        }))
            {
                watcher.RaiseDeleted(filePath);
                watcher.RaiseChanged(filePath);
                watcher.RaiseChanged(filePath);
                watcher.RaiseRenamed(filePath, newFilePath);

                Thread.Sleep(200);
            }

            Assert.Equal(1, events.Count);

            var e = events.Single();
            Assert.Equal(filePath, e.OldPath);
            Assert.Equal(newFilePath, e.Path);
        }

        [Fact]
        public void raises_multipe_events()
        {
            const string filePath = "A.FILE";
            const string newFilePath = "NEW.FILE";

            var events
                = new List<FileChangedEvent>();

            using (var watcher = (FileChangeWatcher)
                new FileChangeWatcher()
                    .OnChange(
                        ce =>
                        {
                            Debug.WriteLine(ce);
                            events.Add(ce);
                        },
                        new FileChangeWatcherOptions
                        {
                            Directory = null, // won't actually watch
                            Delay = 100
                        }))
            {
                watcher.RaiseRenamed(filePath, newFilePath);
                watcher.RaiseChanged(newFilePath);

                Thread.Sleep(2000);
            }

            Assert.Equal(2, events.Count);
            Assert.Equal(FileChangedEventType.Renamed, events.ElementAt(0).Type);
            Assert.Equal(FileChangedEventType.Changed, events.ElementAt(1).Type);
        }
    }
}