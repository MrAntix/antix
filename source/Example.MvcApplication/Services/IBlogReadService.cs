using System.Collections.Generic;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Services
{
    public interface IBlogReadService
    {
        IEnumerable<BlogEntry> Search(string text, int index, int pageSize);
    }
}