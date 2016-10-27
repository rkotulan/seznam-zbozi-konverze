// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CamelCaseContractResolver.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Infrastructure
{
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CamelCaseContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var res = base.CreateProperty(member, memberSerialization);

            var attrs = member.GetCustomAttributes(typeof(JsonPropertyAttribute), true).ToArray();
            if (attrs.Any())
            {
                var attr = attrs[0] as JsonPropertyAttribute;
                if (res.PropertyName != null && attr?.PropertyName != null)
                {
                    res.PropertyName = attr.PropertyName;
                }
            }

            return res;
        }
    }
}