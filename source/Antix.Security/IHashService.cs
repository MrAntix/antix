using System;
using Antix.Services;

namespace Antix.Security
{
    public interface IHashService : IService, IDisposable
    {
        string Hash(string value);
        string Hash64(string value);
    }
}