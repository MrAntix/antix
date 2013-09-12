using System;

namespace Antix.WebApi.Upload.Abstraction
{
    public interface IUploadWriter : IDisposable
    {
        void WriteBlock(byte[] buffer, int offset, int count);
        string GetUrl();
    }
}