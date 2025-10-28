//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Linq;
using Firebase.Firestore;
using UnityEditor;
using UnityEngine;

namespace MQG.EasyRemoteConfig.Runtime
{
    public class PushAssets
    {
        private static Context _context => Resources.Load<Context>("Context"); 
        [MenuItem("Easy Remote Config/Push Current Assets")]
        private static async void PushCurrentAssets()
        {
            string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { "Assets/ERC" });
            if (guids.Length == 0)
            {
                Debug.LogError("No Default Values Found");
                return;
            }
            
            TextAsset[] assets = guids.Select(guid => AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(guid))).ToArray();
            string projectName = _context.GetRemoteUID();
            FirebaseFirestore db = await FirebaseConnect.DB();
            Dictionary<string, string> updates = new();
            foreach (var asset in assets)
            {
                DocumentReference docRef = db.Collection("Remotes").Document(projectName).Collection(_context.CurrentEnvironment).Document(asset.name);
                string sceneName = SceneHash.GetScene(asset.name);
                updates.Add($"{sceneName}", asset.text);
                
                await docRef.SetAsync(updates);
            }
        }
    }
}