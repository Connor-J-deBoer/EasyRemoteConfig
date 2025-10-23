//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class UpdateLocalFields
    {
        public static void Update()
        {
            Debug.Log($"Get ERC-{AssetDatabase.AssetPathToGUID(SceneManager.GetActiveScene().path)} from firestore");
        }
    }
}