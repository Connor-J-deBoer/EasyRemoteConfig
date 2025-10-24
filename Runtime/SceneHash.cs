//=================================================================\\
//======Copyright (C) 2024 Connor deBoer, All Rights Reserved======\\
//=================================================================\\

using System.Security.Cryptography;
using System.Text;
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
                return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}