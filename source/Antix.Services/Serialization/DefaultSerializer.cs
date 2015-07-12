using System;
using Newtonsoft.Json;

namespace Antix.Services.Serialization
{
    public class DefaultSerializer :
        ISerializer
    {
        readonly JsonSerializerSettings _serializerSettings;

        public DefaultSerializer(
            JsonSerializerSettings serializerSettings = null)
        {
            _serializerSettings = serializerSettings ?? new JsonSerializerSettings
            {
                ContractResolver = new DefaultSerializerContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public string Serialize(object data)
        {
            try
            {
                return JsonConvert.SerializeObject(data, _serializerSettings);
            }
            catch (Exception)
            {
                return string.Format("Failed to serialize {0}", data);
            }
        }
    }
}