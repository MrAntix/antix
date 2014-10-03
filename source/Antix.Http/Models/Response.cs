namespace Antix.Http.Models
{
    public class Response
    {
        public static Response OK()
        {
            return new Response();
        }

        public static Response<T> OK<T>(T data)
        {
            return new Response<T>
            {
                Data = data
            };
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}