using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Antix.Services.Serialization
{
    public class DefaultSerializerContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);

            return members
                .Where(m => !m.GetCustomAttributes(typeof (SerializerIgnoreAttribute)).Any())
                .ToList();
        }
    }
}