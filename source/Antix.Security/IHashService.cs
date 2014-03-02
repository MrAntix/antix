using System;

namespace Antix.Security
{
    public interface IHashService : IDisposable
    {
        string Hash(string value);
        string Hash64(string value);
    }
}