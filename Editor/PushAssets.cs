//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Linq;
using Firebase.Firestore;
using UnityEditor;
using UnityEngine;

namespace Connor.EasyRemoteConfig.Runtime
{
    public class PushAssets
    {
        private static Context _context => Resources.Load<Context>("Context"); 
        [MenuItem("Easy Remote Config/Push Current Assets")]
        private static async void PushCurrentAssets()
        {
            var assets = Resources.LoadAll<TextAsset>("DoNotTouch");
            assets = assets.Where(ass => ass.name.Contains("ERC")).ToArray();

            string projectName = _context.GetRemoteUID();
            FirebaseFirestore db = await FirebaseConnect.DB();
            Dictionary<string, string> updates = new();
            foreach (var asset in assets)
            {
                DocumentReference docRef = db.Collection("Remotes").Document(projectName).Collection(_context.CurrentEnvironment).Document(asset.name);
                string sceneName = SceneHash.GetScene(asset.name.Split('-')[1]);
                updates.Add($"{sceneName}", asset.text);
                
                await docRef.SetAsync(updates);
            }
        }
    }
}