//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.RemoteConfigHelper.Runtime
{
#if UNITY_EDITOR
    public static class GetRemoteFields
    {
        private static string _remoteFieldName = "RemoteList";

        /// <summary>
        /// This guy finds all the fields in the current scene with the RemoteField attribute
        /// </summary>
        /// <returns>a tuple where item one is a list of fields and item two is a list of their parent objects</returns>
        private static (List<FieldInfo>, List<MonoBehaviour>) GetFields()
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            List<MonoBehaviour> objects = new List<MonoBehaviour>();
            foreach (var obj in MonoBehaviour.FindObjectsOfType<MonoBehaviour>())
            {
                System.Type type = obj.GetType();
                var fieldWithAttribute = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(field => System.Attribute.IsDefined(field, typeof(RemoteFieldAttribute)))
                    .ToList();
                foreach (var field in fieldWithAttribute)
                {
                    fields.Add(field);
                    objects.Add(obj);
                }
            }
            return (fields, objects);
        }

        public static RemoteFieldList CreateAsset()
        {
            RemoteFieldList remoteList = new RemoteFieldList();

            (List<FieldInfo>, List<MonoBehaviour>) data = GetFields();
            remoteList.RemoteFields = data.Item1.Select(field => field.Name).ToList();
            remoteList.FieldDeclaringTypeNames = data.Item1.Select(field => field.DeclaringType.Name).ToList();
            remoteList.RemoteObjects = data.Item2.Select(obj => obj.name).ToList();

            string remoteListJson = JsonUtility.ToJson(remoteList);
            string path = $"Assets/Resources/RemoteList_{SceneManager.GetActiveScene().name}/";

            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            System.IO.File.WriteAllText($"{path}/RemoteList.json", remoteListJson);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return remoteList;
        }
    }
#endif
}
