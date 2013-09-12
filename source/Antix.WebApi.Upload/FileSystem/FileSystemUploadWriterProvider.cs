using System;
using Antix.WebApi.Upload.Abstraction;

namespace Antix.WebApi.Upload.FileSystem
{
    public class FileSystemUploadWriterProvider :
        IUploadWriterProvider
    {
        readonly string _pathTemplate;
        readonly string _urlTemplate;
        readonly Func<string, string> _mimeMap;

        public FileSystemUploadWriterProvider(
            string pathTemplate, string urlTemplate, Func<string, string> mimeMap)
        {
            _pathTemplate = pathTemplate;
            _urlTemplate = urlTemplate;
            _mimeMap = mimeMap;
        }

        public IUploadWriter GetStream(string contentType)
        {
            var name = string.Concat(
                Guid.NewGuid().ToString(), ".",
                _mimeMap(contentType)
                );


            var path = string.Format(_pathTemplate, name);
            var uri = string.Format(_urlTemplate, name);

            return new FileSystemUploadWriter(path, uri);
        }
    }
}