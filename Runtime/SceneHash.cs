//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Connor.EasyRemoteConfig.Runtime
{
    public static class SceneHash
    {
        public static string GetSceneId(Scene scene)
        {
            string scenePath = scene.path; // ex: "Assets/Scenes/MyScene.unity"

            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(scenePath));
                string hash = System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static string GetScene(string hash)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(i);
                string sceneHash = GetSceneId(scene);
                if (sceneHash == hash)
                    return scene.name;
            }

            return "";
        }
    }
}