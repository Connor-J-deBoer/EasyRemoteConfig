//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace MQG.EasyRemoteConfig.Runtime
{
    public class ERCResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var bindingFlags =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic;

            var fields = type.GetFields(bindingFlags)
                .Select(f => base.CreateProperty(f, memberSerialization))
                .ToList();

            foreach (var property in fields)
            {
                property.Writable = true;
                property.Readable = true;
            }

            return fields;
        }
    }
}