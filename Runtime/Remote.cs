//=================================================================\\
//======Copyright (C) 2025 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MQG.EasyRemoteConfig.Runtime
{
    public static class Remote
    {
        private static Context _context => Resources.Load<Context>("Context"); 
        
        /// <summary>
        /// Overwrites every value in the current scene with the RemoteField attribute with the remote values
        /// </summary>
        public static async void ApplyRemoteValues()
        {
            var allData = await GetRemoteFields();
            string sceneValues = allData[SceneManager.GetActiveScene().name];
            
            OverwriteObjects(sceneValues);
        }

        private static async Task<Dictionary<string, string>> GetRemoteFields()
        {
            FirebaseFirestore db = await FirebaseConnect.DB();
            DocumentReference docRef = db.Collection("Remotes").Document(_context.GetRemoteUID()).Collection(_context.CurrentEnvironment).Document(SceneHash.GetSceneId(SceneManager.GetActiveScene()));
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
                    
                    SetFields(script.Value.ToString(), component);
                }
            }
        }

        private static void SetFields(JToken value, Component component)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ContractResolver = new ERCResolver()
            };
            JsonConvert.PopulateObject(value.ToString(), component, settings);
            Debug.LogWarning("Pulled Remote Values");
        }
    }
}