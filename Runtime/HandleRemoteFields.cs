using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RemoteConfigHelper
{
    public class HandleRemoteFields
    {
        private HandleRemoteFields() { }
        private static HandleRemoteFields _instance = null;
        public static HandleRemoteFields Service
        {
            get
            {
                if (_instance == null) _instance = new HandleRemoteFields();
                return _instance;
            }
        }

        /// <summary>
        /// This guy Updates every remote field with their respective remote
        /// IMPORTANT This guy uses a lot of reflection and dynamic types, meaning he's quite expensive, use him carefully
        /// </summary>
        public async void UpdateFields()
        {
            HandleRemoteData remote = new HandleRemoteData();
            var result = GetFields();
            FieldInfo[] fields = result.Item1.ToArray();
            MonoBehaviour[] objects = result.Item2.ToArray();

            for (int i = 0; i < fields.Length; ++i)
            {
                // get the remote value
                dynamic remoteValue = await remote.GetValue(fields[i]);
                if (remoteValue == null)
                {
                    // TODO: create the field in the remote
                    Debug.LogWarning($"No remote found for {fields[i].Name}");
                    continue;
                }
                try
                {
                    // Cast remoteValue to the type of the field
                    object convertedValue = Convert.ChangeType(remoteValue, fields[i].FieldType);

                    // override the local with the remote
                    fields[i].SetValue(objects[i], convertedValue);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Failed to override local field \"{fields[i].Name}\" because {ex}");
                }
            }
        }

        /// <summary>
        /// This guy finds all the fields in the current scene with the RemoteField attribute
        /// </summary>
        /// <returns>a tuple where item one is a list of fields and item two is a list of their parent objects</returns>
        private (List<FieldInfo>, List<MonoBehaviour>) GetFields()
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            List<MonoBehaviour> objects = new List<MonoBehaviour>();
            foreach (var obj in MonoBehaviour.FindObjectsOfType<MonoBehaviour>())
            {
                Type type = obj.GetType();
                var fieldWithAttribute = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(field => Attribute.IsDefined(field, typeof(RemoteField)))
                    .ToList();
                foreach (var field in fieldWithAttribute)
                {
                    fields.Add(field);
                    objects.Add(obj);
                }
            }
            return (fields, objects);
        }
    }
}