using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Antix.IO;

namespace Antix.Web.Caching
{
    public class FileSystemOutputCacheStorage
        : IOutputCacheStorage
    {
        string _cacheFolder;

        public void Initialize(NameValueCollection config)
        {
            if (!string.IsNullOrEmpty(config["cacheFolder"]))
            {
                _cacheFolder = config["cacheFolder"];
                if (!_cacheFolder.EndsWith(@"\")) _cacheFolder += @"\";

                config.Remove("cacheFolder");
            }
            else
            {
                _cacheFolder = string.Format(
                    @"{0}\OutputCache\",
                    AppDomain.CurrentDomain.GetData("DataDirectory"));
            }

            CheckCacheFolder();
        }

        public bool Exists(string key)
        {
            var filePath = GetFilePath(key);
            return File.Exists(filePath);
        }

        public bool IsExpired(string key)
        {
            var filePath = GetFilePath(key);
            return File.GetCreationTimeUtc(filePath) < DateTime.UtcNow;
        }

        public object Read(string key)
        {
            var filePath = GetFilePath(key);
            if (!File.Exists(filePath))
                return null;

            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return formatter.Deserialize(stream);
            }
        }

        public void Write(string key, object data, DateTime expiresOn)
        {
            CheckCacheFolder();

            var filePath = GetFilePath(key);
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, data);
            }

            File.SetCreationTimeUtc(filePath, expiresOn);
        }

        public void Delete(string key)
        {
            var filePath = GetFilePath(key);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        void CheckCacheFolder()
        {
            if (!Directory.Exists(_cacheFolder))
                Directory.CreateDirectory(_cacheFolder);
        }

        string GetFilePath(string key)
        {
            return Path.Combine(_cacheFolder, key.ToSafeFileName());
        }
    }
}