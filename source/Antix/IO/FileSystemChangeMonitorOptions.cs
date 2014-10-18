namespace Antix.IO
{
    public class FileSystemChangeMonitorOptions
    {
        public FileSystemChangeMonitorOptions()
        {
            Directory = null;
            Pattern = "*.*";
            IncludeSubdirectories = true;
            Delay = 100;
        }

        public string Directory { get; set; }
        public string Pattern { get; set; }
        public bool IncludeSubdirectories { get; set; }
        public int Delay { get; set; }
    }
}