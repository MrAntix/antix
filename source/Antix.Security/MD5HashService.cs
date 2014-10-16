using System;
using System.Security.Cryptography;
using System.Text;

namespace Antix.Security
{
    public class MD5HashService : IHashService
    {
        readonly MD5CryptoServiceProvider _service = new MD5CryptoServiceProvider();

        #region IHashService Members

        public string Hash(
            string value,
            string salt)
        {
            return Hash(value, salt, false);
        }

        public string Hash64(
            string value,
            string salt)
        {
            return Hash(value, salt, true);
        }

        #endregion

        string Hash(string value, string salt, bool base64Encode)
        {
            var bytes = _service.ComputeHash(
                Encoding.Default.GetBytes(
                    string.Concat(value, salt)));

            return base64Encode
                ? Convert.ToBase64String(bytes)
                : Encoding.Default.GetString(bytes);
        }

        #region IDisposable

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose managed resources.
                _service.Dispose();
            }

            // unmanaged resources here.

            _disposed = true;
        }

        #endregion
    }
}