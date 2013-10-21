namespace Antix
{
    public class StringSearch
    {
        public StringSearch(string subject)
        {
            Subject = subject;
        }

        public int Start { get; set; }
        public int Index { get; set; }
        public string Subject { get; set; }

        public bool IsFound { get; private set; }

        public void Execute(string data, int start = 0)
        {
            var offset = Index;
            Start = start;

            int index;
            while (!(IsFound = Index == Subject.Length)
                   && (index = Start + Index - offset) < data.Length)
            {
                if (data[index] == Subject[Index])
                {
                    Index++;
                }
                else
                {
                    Index = 0;
                    offset = 0;
                    Start++;
                }
            }

            if (offset > 0) Start = -offset;
        }
    }
}