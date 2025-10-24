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
        [MenuItem("Easy Remote Config/Push Current Assets")]
        private static async void PushCurrentAssets()
        {
            var assets = Resources.LoadAll<TextAsset>("DoNotTouch");
            assets = assets.Where(ass => ass.name.Contains("ERC")).ToArray();
            
            string projectName = Application.productName;
            FirebaseFirestore db = await FirebaseConnect.DB();
            DocumentReference docRef = db.Collection(projectName).Document(Environment.CurrentEnvironment);
            Dictionary<string, string> updates = new();
            foreach (var asset in assets)
            {
                updates.Add(asset.name, asset.text);
            }

            await docRef.SetAsync(updates);
        }
    }
}