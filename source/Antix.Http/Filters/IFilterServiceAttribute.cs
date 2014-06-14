using System;

namespace Antix.Http.Filters
{
    public interface IFilterServiceAttribute
    {
        Type ServiceType { get; }
    }
}