using System;
using System.Runtime.Serialization;

namespace Antix.Data.Keywords
{
    [Serializable]
    public class EntityTypeNotIndexedException : Exception
    {
        public EntityTypeNotIndexedException(Type type)
            : base(type.FullName)
        {
        }

        public EntityTypeNotIndexedException(Type type, Exception inner)
            : base(type.FullName, inner)
        {
        }

        protected EntityTypeNotIndexedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}