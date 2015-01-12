using System;

namespace Antix.Data.Models
{
    public interface IHasPublicId
    {
        Guid Id { get; }
    }
}