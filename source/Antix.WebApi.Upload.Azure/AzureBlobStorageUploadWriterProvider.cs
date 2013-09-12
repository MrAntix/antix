using System;
using Antix.WebApi.Upload.Abstraction;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Antix.WebApi.Upload.Azure
{
    public class AzureBlobStorageUploadWriterProvider :
        IUploadWriterProvider
    {
        CloudBlobContainer _container;
        readonly string _connectionString;
        readonly string _containerName;

        public AzureBlobStorageUploadWriterProvider(
            string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        public IUploadWriter GetStream(string contentType)
        {
            if (_container == null)
                _container = GetWebApiContainer();

            var blobName = Guid.NewGuid().ToString("N");
            var blobReference = _container.GetBlockBlobReference(blobName);

            return new AzureBlobStorageUploadWriter(blobReference, contentType);
        }

        CloudBlobContainer GetWebApiContainer()
        {
            // Retrieve storage account from connection-string
            var storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the blob client 
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(_containerName);

            // Create the container if it doesn't already exist
            if (!container.CreateIfNotExists())
            {
                container.SetPermissions(new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
            }

            return container;
        }
    }
}