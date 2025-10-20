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
        public static Dictionary<(string, string), string[]> Fields = new Dictionary<(string, string), string[]>();
        private static string _remoteFieldName = "RemoteList";

        /// <summary>
        /// This guy finds all the fields in the current scene with the RemoteField attribute
        /// </summary>
        /// <returns>a tuple where item one is a list of fields and item two is a list of their parent objects</returns>
        private static (List<FieldInfo>, List<MonoBehaviour>) GetFields()
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            List<MonoBehaviour> objects = new List<MonoBehaviour>();
            foreach (var obj in Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                var type = obj.GetType();
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

        public static void GetAllFields()
        {
            Fields.Clear();
            MonoBehaviour[] objects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var obj in objects)
            {
                // Get every field marked with RemoteFieldAttribute
                var fieldWithAttribute = obj.GetType().GetFields(
                        BindingFlags.Instance | 
                        BindingFlags.NonPublic | 
                        BindingFlags.Public)
                    .Where(field => System.Attribute.IsDefined(field, typeof(RemoteFieldAttribute)))
                    .ToList();
                
                string[] fields = new string[fieldWithAttribute.Count];
                for (int i = 0; i < fields.Length; ++i)
                {
                    fields[i] = $"\"{fieldWithAttribute[i].Name}\": {fieldWithAttribute[i].GetValue(obj)}";
                }
                Fields.Add((obj.name, obj.GetType().Name), fields);
            }
        }

        public static void CreateAsset()
        {
            (List<FieldInfo>, List<MonoBehaviour>) data = GetFields();

            //string remoteListJson = JsonUtility.ToJson(Fields);
            string path = $"Assets/Resources/RemoteList_{SceneManager.GetActiveScene().name}/";

            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            //System.IO.File.WriteAllText($"{path}/RemoteList.json", "{}");
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}
