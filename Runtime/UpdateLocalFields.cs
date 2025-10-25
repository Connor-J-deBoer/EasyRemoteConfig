//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Firebase.Firestore;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class UpdateLocalFields
    {
        private static Context _context => Resources.Load<Context>("Context"); 
        public static async void Update()
        {
            var allData = await GetRemoteFields();
            string sceneValues = allData[SceneManager.GetActiveScene().name];
            
            OverwriteObjects(sceneValues);
        }

        private static async Task<Dictionary<string, string>> GetRemoteFields()
        {
            FirebaseFirestore db = await FirebaseConnect.DB();
            DocumentReference docRef = db.Collection("Remotes").Document(_context.GetRemoteUID()).Collection(_context.CurrentEnvironment).Document($"ERC-{SceneHash.GetSceneId(SceneManager.GetActiveScene())}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (!snapshot.Exists)
            {
                Debug.LogWarning($"Could not find remote values");
                return null;
            }

            return snapshot.ConvertTo<Dictionary<string, string>>();
        }
        
        private static void OverwriteObjects(string json)
        {
            var jsonData = JObject.Parse(json);
            foreach (var gameobject in jsonData)
            {
                GameObject toOverwrite = GameObject.Find(gameobject.Key);
                if (!toOverwrite)
                    continue;
                var fieldValue = (JObject)gameobject.Value;
                foreach (var script in fieldValue)
                {
                    var component = toOverwrite.GetComponent(script.Key);
                    if (!component)
                        continue;
                    
                    var fields = (JObject)script.Value;
                    foreach (var field in fields)
                    {
                        JToken value = field.Value;
                        SetFields(component, field.Key, value);
                    }
                }
            }
        }

        private static void SetFields(Component component, string fieldName, JToken value)
        {
            var type = component.GetType();
            var field = type.GetField(fieldName, BindingFlags.Public |  BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                return;
            
            var convertedValue = value.ToObject(field.FieldType);
            field.SetValue(component, convertedValue);
        }
    }
}