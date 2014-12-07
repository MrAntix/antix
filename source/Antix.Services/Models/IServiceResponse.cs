using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        IEnumerable<string> Errors { get; }
    }
}