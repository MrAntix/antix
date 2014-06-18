using System;
using Antix.Services;

namespace Antix.Security
{
    public interface IHashService : IService, IDisposable
    {
        string Hash(string value, string salt);
        string Hash64(string value, string salt);
    }
}