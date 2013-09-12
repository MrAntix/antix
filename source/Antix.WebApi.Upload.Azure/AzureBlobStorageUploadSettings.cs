namespace Antix.WebApi.Upload.Azure
{
    public class AzureBlobStorageUploadSettings
    {
        readonly string _connectionString;
        readonly string _containerName;

        public AzureBlobStorageUploadSettings(
            string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string ContainerName
        {
            get { return _containerName; }
        }
    }
}