//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class GetRemoteFields
    {
        private static Dictionary<(string, string), string[]> GetAllFields()
        {
            Dictionary<(string, string), string[]> fields = new();
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
                
                string[] fieldsString = new string[fieldWithAttribute.Count];
                if (fieldsString.Length == 0)
                    continue;
                
                for (int i = 0; i < fieldsString.Length; ++i)
                {
                    fieldsString[i] = $"\"{fieldWithAttribute[i].Name}\": {fieldWithAttribute[i].GetValue(obj)}";
                }
                fields.Add((obj.name, obj.GetType().Name), fieldsString);
            }

            return fields;
        }

        private static string BuildJSON(Dictionary<(string, string), string[]> fields)
        {
            string json = "{";
            foreach (var field in fields)
            {
                string allFields = "";
                foreach (var fieldValue in field.Value)
                {
                    allFields += $"{fieldValue},";
                }
                allFields = allFields.Substring(0, allFields.Length - 1);
                json += $"\n\t\"{field.Key.Item1}\":\n\t{{\n\t\t\"{field.Key.Item2}\":\n\t\t{{\n\t\t\t{allFields}\n\t\t}}\n\t}},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "\n}";
            return json;
        }

        public static void CreateAsset()
        {
            string data = BuildJSON(GetAllFields());
            string path = $"Assets/Resources/DoNotTouch/";
            string sceneGuid = AssetDatabase.AssetPathToGUID(SceneManager.GetActiveScene().path);
            
            if (!System.IO.Directory.Exists(path)) 
                System.IO.Directory.CreateDirectory(path);
            
            System.IO.File.WriteAllText($"{path}ERC-{sceneGuid}.json", data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
