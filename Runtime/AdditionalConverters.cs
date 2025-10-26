//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Connor.EasyRemoteConfig.Runtime
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            Debug.Log("nerd");
            var obj = JObject.Load(reader);
            
            if (obj == null)
                throw new JsonSerializationException("Cannot convert null value");
            
            return new Vector2(
                obj["x"].Value<float>(),
                obj["y"].Value<float>()
            );
        }
    }
}