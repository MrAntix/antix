namespace Antix.WebApi.Upload.Abstraction
{
    public interface IUploadWriterProvider
    {
        IUploadWriter GetStream(string contentType);
    }
}