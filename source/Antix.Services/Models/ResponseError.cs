namespace Antix.Services.Models
{
    public class ResponseError
    {
        public string Code { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Path, Code);
        }
    }
}