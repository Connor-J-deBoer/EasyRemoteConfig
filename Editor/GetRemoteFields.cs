//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Connor.EasyRemoteConfig.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.EasyRemoteConfig.Editor
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
                    fieldsString[i] = $"\"{fieldWithAttribute[i].Name}\":\n\t\t\t{{\n\t\t\t\t";
                    fieldsString[i] += $"\"value\": \"{fieldWithAttribute[i].GetValue(obj)}\",\n\t\t\t\t";
                    fieldsString[i] += $"\"type\": \"{fieldWithAttribute[i].FieldType}\"\n\t\t\t}}";
                }
                fields.Add((obj.name, obj.GetType().Name), fieldsString);
            }

            return fields;
        }

        private static string BuildAssetContent(Dictionary<(string, string), string[]> fields)
        {
            string remoteList = "{";
            foreach (var field in fields)
            {
                string allFields = "";
                foreach (var fieldValue in field.Value)
                {
                    allFields += $"{fieldValue},\n\t\t\t";
                }
                allFields = allFields.Substring(0, allFields.Length - 5);
                remoteList += $"\n\t\"{field.Key.Item1}\":\n\t{{\n\t\t\"{field.Key.Item2}\":\n\t\t{{\n\t\t\t{allFields}\n\t\t}}\n\t}},";
            }
            remoteList = remoteList.Substring(0, remoteList.Length - 1);
            remoteList += "\n}";
            return remoteList;
        }
        
        [MenuItem("Easy Remote Config/Create Remote Asset")]
        private static void CreateAsset()
        {
            string data = BuildAssetContent(GetAllFields());
            string path = "Assets/ERC/";
            string sceneGuid = SceneHash.GetSceneId(SceneManager.GetActiveScene());
            
            if (!System.IO.Directory.Exists(path)) 
                System.IO.Directory.CreateDirectory(path);
            
            System.IO.File.WriteAllText($"{path}{sceneGuid}.json", data);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
