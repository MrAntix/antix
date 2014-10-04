using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public static class ResponseExtensions
    {
        public static string ToSummary(this IEnumerable<ResponseError> errors)
        {
            return string.Join("\n", errors.Select(e => e.ToString()));
        }
    }
}