namespace Antix.IO
{
    public class FileSystemChangedEvent
    {
        public string Path { get; set; }
        public FileSystemChangedEventType Type { get; set; }
        public string NewPath { get; set; }

        public override string ToString()
        {
            switch (Type)
            {
                default:
                    return string.Format("{0} {1}", Path, Type);
                case FileSystemChangedEventType.Renamed:
                    return string.Format("{0} {1} {2}", Path, Type, NewPath);
            }
        }
    }
}