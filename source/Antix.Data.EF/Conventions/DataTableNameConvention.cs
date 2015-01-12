using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Antix.Data.EF.Conventions
{
    public class DataTableNameConvention :
        Convention
    {
        public DataTableNameConvention(
            IPluralizationService pluralizationService,
            IDictionary<string, string> explicitNames)
        {
            Types().Configure(
                c => c.ToTable(
                    GetName(c.ClrType, pluralizationService, explicitNames))
                );
        }

        static string GetName(
            Type type,
            IPluralizationService pluralizationService,
            IDictionary<string, string> explicitNames)
        {
            if (explicitNames.ContainsKey(type.Name))
                return explicitNames[type.Name];

            return pluralizationService
                .Pluralize(type.Name.TrimEnd("Data"));
        }
    }
}